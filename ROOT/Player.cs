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
            Jumping,
            PowerUp
        }
        int moveRight, moveLeft, jump, use;

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height)
            : base(x,y,width,height,false)
        {        }

        public void Move()
        //it's move...what do you think it does
        {
            KeyboardState input = Keyboard.GetState();
            if(input.IsKeyDown((Keys)moveRight) && input.IsKeyDown((Keys)jump))
            {
                this.Y -= 5; // y - 5 goes up right?
                this.X += 5;
            }
            else if(input.IsKeyDown((Keys)moveLeft) && input.IsKeyDown((Keys)jump))
            {
                this.Y -= 5;
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

        public void Jump() { }

        public void CheckCollision(GameObject g)
        //checks if the player has collided with the given game object
        {
            if(this.HitBox.Intersects(g.HitBox))
            {
                if(g.IsSolid) //stops player movement if they hit a solid object
                {
                    
                }
                else if(g is Player)
                {
                    Stun();
                }
                else
                {

                }
            }
        }

        public void Stun() { }

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
