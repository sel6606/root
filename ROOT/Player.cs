using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        double timer; //keeps track of how long the player needs to hold the orb
        bool hasOrb; //checks if player has the orb
        bool stunned; //checks if player is stunned

        //properties
        public double Timer { get { return timer; } set { timer = value; } }
        public bool Orb { get { return hasOrb; } set { hasOrb = value; } }
        public bool Stunned { get { return stunned; } }

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height, double time)
            : base(x,y,width,height,false)
        {
            timer = time; //sets the starting time
            hasOrb = false; //player doesn't start with orb
        }

        public void Move() //movement should be complete by next meeting
        //it's move...what do you think it does
        {
            KeyboardState input = Keyboard.GetState();
            if(input.IsKeyDown((Keys)moveRight) && input.IsKeyDown((Keys)jump))
            {
                Jump(); 
                this.X += 5;
            }
            else if(input.IsKeyDown((Keys)moveLeft) && input.IsKeyDown((Keys)jump))
            {
                Jump();
                this.X -= 5;
            }
            else if(input.IsKeyDown((Keys)moveRight))
            {
                this.X += 5;
                this.Y += 5; //simulates the effects of gravity
            }
            else if(input.IsKeyDown((Keys)moveLeft))
            {
                this.X -= 5;
                this.Y += 5; //still simluates gravity
            }
        }

        public void Jump()
        //player ascends as though they have actual physics (don't move at constant speed)
        {

        }

        public void CheckCollision(GameObject g)
        //checks if the player has collided with the given game object
        {
            if(this.HitBox.Intersects(g.HitBox))
            {
                if(g.IsSolid) //stops player movement if they hit a solid object
                {
                    
                }
                else if(g is Orb)
                {
                    hasOrb = true;
                }
            }
        }

        public void CheckPlayerCollision(Player p)
        //this method specifically handles logic for player on player collision
        {
            if(hasOrb) //if this player has the orb
            {
                Stun();
                hasOrb = false;
                p.Orb = true;
            }
            else if(p.Orb) //if other player has orb
            {
                p.Orb = false;
                hasOrb = true;
            }
        }

        public void Stun()
        //player will be unable to move while stunned, player will also blink
        {
            
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
