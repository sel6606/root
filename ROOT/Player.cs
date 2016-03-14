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

        //properties
        public bool Orb { get { return hasOrb; } set { hasOrb = value; } }

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height, double time, Texture2D texture)
            : base(x,y,width,height,false, texture)
        {
            hasOrb = false; //player doesn't start with orb
        }

        public void Move() 
        //it's move...what do you think it does
        {  
            if(!stunned)
            {
                KeyboardState input = Keyboard.GetState();
                if(input.IsKeyDown((Keys)jump))
                {
                    Jump();
                }
                if(input.IsKeyDown((Keys)moveRight))
                {
                    if(!rightWall)
                    {
                        this.X += 1;
                    }
                }
                if(input.IsKeyDown((Keys)moveLeft))
                {
                    if(!leftWall)
                    {
                        this.X -= 1;
                    }
                }
                if(!ground)
                {
                    this.Y += 1;
                }
            }           
        }

        public void Jump()
        //player ascends as though they have actual physics (don't move at constant speed)
        {
            if (ground) 
            {
                if(!topWall)
                Y -= 75; //temperary measure until acceleration can be implemented
            }
        }

        public void CheckCollision(List<Tile> g)
        //checks if the player has collided with a tile in the given list
        {
            //resets all the flags that check for solid walls
            ground = false;
            topWall = false;
            leftWall = false;
            rightWall = false;
            for (int i = 0; i < g.Count; i++)
            {
                if (this.HitBox.Bottom == g[i].HitBox.Top && //checks for solid platforms beneath player
                    (this.HitBox.Center.X+(this.HitBox.Width/2)-1 >= g[i].X && this.HitBox.Center.X - (this.HitBox.Width / 2) + 1 <= g[i].X + g[i].HitBox.Width)) //(checks that tile and player are in the same relative x-coordinate)
                {
                    ground = true;
                }
                if (this.HitBox.Top == g[i].HitBox.Bottom && //checks for platforms above the player
                    (this.HitBox.Center.X + (this.HitBox.Width / 2) - 1 >= g[i].X && this.HitBox.Center.X - (this.HitBox.Width / 2) + 1 <= g[i].X + g[i].HitBox.Width)) //(checks that tile and player are in the same relative x-coordinate)
                {
                    topWall = true;
                }
                if ((this.HitBox.Intersects(g[i].HitBox) ||this.HitBox.Left == g[i].HitBox.Right) && //checks for walls to the left of the player
                    (this.HitBox.Center.Y + (this.HitBox.Height / 2) >= g[i].Y && this.HitBox.Center.Y - (this.HitBox.Height / 2) <= g[i].HitBox.Y + g[i].HitBox.Height)) //(checks that tile and player are in the same relative y-coordinate)
                {
                    leftWall = true;
                }
                if ((this.HitBox.Right == g[i].HitBox.Left  || this.HitBox.Intersects(g[i].HitBox))&& //checks for walls to the right of the player
                    (this.HitBox.Center.Y + (this.HitBox.Height / 2) >= g[i].Y && this.HitBox.Center.Y - (this.HitBox.Height / 2) <= g[i].HitBox.Y + g[i].HitBox.Height)) //(checks that tile and player are in the same relative y-coordinate)
                {
                    rightWall = true;
                }
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

        public void ScreenWrap(int maxX, int maxY)
        //pre: the max X and Y coordinates of the screen
        //post: wraps the player around the screen if they go out of bounds
        {
            if(this.HitBox.Center.X < 0)
            {
                this.X = maxX;
            }
            if(this.HitBox.Center.X > maxX)
            {
                this.X = 0;
            } 
            if(this.HitBox.Center.Y < 0)
            {
                this.Y = maxY;
            }
            if(this.HitBox.Center.Y > maxY)
            {
                this.Y = 0;
            }
       }

        public void UsePowerUp() { }
    }
}
