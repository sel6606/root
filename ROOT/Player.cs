using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ROOT
{
    class Player : GameObject
    {
        //Fields
        enum PlayerState //keeps track of what player is doing
        {
            FaceRight,
            FaceLeft,
            MoveRight,
            MoveLeft,
            JumpRight,
            JumpLeft,
            PowerUp
        }
        int moveRight, moveLeft, jump, use; //will correspond to player controls
        bool hasOrb; //checks if player has the orb
        bool ground, topWall, leftWall, rightWall; //are there solid walls nearby
        static bool stunned; //checks if player is stunned
        public bool intersect = false;

        //properties
        public bool Orb { get { return hasOrb; } set { hasOrb = value; } }

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height, double time, Texture2D texture)
            : base(x,y,width,height,false, texture)
        {
            hasOrb = false; //player doesn't start with orb
        }

        public void Move() //movement should be complete by next meeting
        //it's move...what do you think it does
        {
            //ground = true; //TEMPORARY STATEMENT

            //ALL TEMPORARY STATEMENTS
            rightWall = false;
            leftWall = false;
            topWall = false;
           
            if(!stunned)
            {
                KeyboardState input = Keyboard.GetState();
                if (input.IsKeyDown((Keys)moveRight) && input.IsKeyDown((Keys)jump))
                {
                    Jump();
                    if(!rightWall)
                    {
                        this.X += 5;
                    }
                }
                else if (input.IsKeyDown((Keys)moveLeft) && input.IsKeyDown((Keys)jump))
                {
                    Jump();
                    if(!leftWall)
                    {
                        this.X -= 5;
                    }
                }
                else if(input.IsKeyDown((Keys)jump))
                {
                    Jump();
                }
                else if (input.IsKeyDown((Keys)moveRight))
                {
                    if (!rightWall)
                    {
                        this.X += 5;
                    }
                    if (!ground) //gravity should only be in effect when player is not on the ground
                    {
                        this.Y += 5;
                    }
                }
                else if (input.IsKeyDown((Keys)moveLeft))
                {
                    if (!leftWall)
                    {
                        this.X -= 5;
                    }
                    if (!ground)
                    {
                        this.Y += 5;
                    }
                }
                else
                {
                    if (!ground)
                    {
                        if(!topWall) //stop acceleration if they hit the bottom of a wall
                        {
                            this.Y += 10;
                        }
                    }
                }
            }           
        }

        public void Jump()
        //player ascends as though they have actual physics (don't move at constant speed)
        {
            if (ground) //player shouldn't be able to jump unless they are on the ground
            {
                Y -= 5; //temperary measure until acceleration can be implemented
            }
        }

        public void CheckCollision(GameObject g)
        //checks if the player has collided with the given game object
        {
            
            if(this.HitBox.Intersects(g.HitBox))
            {
                if(g.IsSolid) //stops player movement if they hit a solid object
                {
                    //should check if the player is on top of a solid object
                    if (this.HitBox.Bottom.CompareTo(g.HitBox.Top) == 0)
                    {
                        ground = true;
                        intersect = true;
                    }
                    else
                    {
                        ground = false;
                    }
                    //is there a wall to the right
                    if (this.HitBox.Right.CompareTo(g.HitBox.Left) == 0)
                    {
                        rightWall = true;
                    }
                    else
                    {
                        rightWall = false;
                    }
                    //is there a wall to the left
                    if (this.HitBox.Left.CompareTo(g.HitBox.Right) == 0)
                    {
                        leftWall = true;
                    }
                    else
                    {
                        leftWall = false;
                    }

                    if(this.HitBox.Top.CompareTo(g.HitBox.Bottom) == 0)
                    {
                        topWall = true;
                    }
                    else
                    {
                        topWall = false;
                    }
                }
                else if(g is Orb)
                {
                    hasOrb = true;
                    //set orb's active property to false in game 1
                }
            }
            if (!intersect)
            {
                ground = false;
            }

        }

        public void CheckPlayerCollision(Player p)
        //this method specifically handles logic for player on player collision
        {
            if (this.HitBox.Intersects(p.HitBox))
            {
                if(!stunned)
                {
                    if (hasOrb) //if this player has the orb
                    {
                        Stun();
                        hasOrb = false;
                        p.Orb = true;
                    }
                    else if (p.Orb) //if other player has orb
                    {
                        p.Orb = false;
                        hasOrb = true;
                    }
                }
            }
        }

        public static void Stun()
        //player will be unable to move while stunned, player will also blink
        {
            stunned = true;
            //use a thread to keep player stunned
            stunned = false;
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);
        }

        public void SetControls(Keys r, Keys l, Keys j, Keys u)
        //pre: Keys values to correspond to: moving right, left, jumping, and using powerups
        //post: sets the player's control mapping
        {
            moveRight = (int)r;
            moveLeft = (int)l;
            jump = (int)j;
            use = (int)u;
        }
    }
}
