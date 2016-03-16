using Microsoft.Xna.Framework;
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
        private int jumpUp = -1;
        private int gravDelay = 0;
        private Vector2 previousPosition;
        private Vector2 currentPosition;
        private int previousGravSpeed=-2;
        private int gravSpeed=-2;
        private Rectangle between;
        

        //properties
        public bool Orb { get { return hasOrb; } set { hasOrb = value; } }

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height, double time, Texture2D texture)
            : base(x, y, width, height, false, texture)
        {
            hasOrb = false; //player doesn't start with orb
        }

        public void Move()
        //it's move...what do you think it does
        {
            previousPosition = new Vector2(this.X, this.Y);
            currentPosition = new Vector2(this.X, this.Y);

            if (!stunned)
            {
                KeyboardState input = Keyboard.GetState();
                if (input.IsKeyDown((Keys)jump))
                {
                    Jump();
                }
                if (input.IsKeyDown((Keys)moveRight))
                {
                    if (!rightWall)
                    {
                        currentPosition.X += 1;
                        this.X += 1;
                    }
                }
                if (input.IsKeyDown((Keys)moveLeft))
                {
                    if (!leftWall)
                    {
                        currentPosition.X -= 1;
                        this.X -= 1;
                    }
                }
                if (!ground)
                {
                    //jumps
                    currentPosition.Y = currentPosition.Y - jumpUp;
                    this.Y = this.Y - jumpUp;
                    //makes the arc work properly
                    if (gravDelay > 0)
                    {
                        gravDelay = gravDelay - 1;
                    }
                    else
                    {
                        //edit this to change negative acceleration
                        if (jumpUp > gravSpeed)
                        {
                            jumpUp = jumpUp - 1;
                        }
                    }

                }
                else
                {
                    //makes them jump while on the ground
                    if (jumpUp > 0)
                    {
                        currentPosition.Y = currentPosition.Y - jumpUp;
                        this.Y = this.Y - jumpUp;
                    }
                }

                
                
            }
        }

        public void Jump()
        //player ascends as though they have actual physics (don't move at constant speed)
        {
            if (ground)
            {
                if (!topWall)
                {
                    jumpUp = 8;
                    gravDelay = 3;
                }

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
            between = new Rectangle((int)previousPosition.X, (int)previousPosition.Y, (int)(currentPosition.X + this.Width - previousPosition.X), (int)(currentPosition.Y + this.Height - previousPosition.Y));

            for (int i = 0; i < g.Count; i++)
            {
                
                if (this.Bottom == g[i].Top && //checks for platforms below the player
                    (this.Center.X + (this.Width / 2) - 1 >= g[i].X && this.Center.X - (this.Width / 2) + 1 <= g[i].X + g[i].Width)) //(checks that tile and player are in the same relative x-coordinate)
                {
                    //move method will work as though the player were on the ground
                    ground = true;
                    gravSpeed = previousGravSpeed;
                }
                if (this.Top == g[i].HitBox.Bottom && //checks for platforms above the player
                    (this.Center.X + (this.Width / 2) - 1 >= g[i].X && this.Center.X - (this.Width / 2) + 1 <= g[i].X + g[i].Width)) //(checks that tile and player are in the same relative x-coordinate)
                {
                    //jump method will stop moving the player up
                    topWall = true;
                }
                if ((this.HitBox.Intersects(g[i].HitBox) || this.Left == g[i].Right) && //checks for walls to the left of the player
                    (this.Center.Y + (this.Height / 2) >= g[i].Y && this.Center.Y - (this.Height / 2) <= g[i].Y + g[i].Height)) //(checks that tile and player are in the same relative y-coordinate)
                {
                    //player will stop moving left because of the wall
                    leftWall = true;
                }
                if ((this.Right == g[i].Left || this.HitBox.Intersects(g[i].HitBox)) && //checks for walls to the right of the player
                    (this.Center.Y + (this.Height / 2) >= g[i].Y && this.Center.Y - (this.Height / 2) <= g[i].Y + g[i].Height)) //(checks that tile and player are in the same relative y-coordinate)
                {
                    //player will stop moving right because of the wall
                    rightWall = true;
                }
                if (between.Intersects(g[i].HitBox) && !ground)
                {
                    gravSpeed = -1;
                    jumpUp = -1;
                    currentPosition = previousPosition;
                }
            }

            this.X = (int)currentPosition.X;
            this.Y = (int)currentPosition.Y;
        }

        public bool CheckPlayerCollision(Player p)
        //this method specifically handles logic for player on player collision\
        //returns false unless the player who called this method has taken the orb
        {
            if (this.HitBox.Intersects(p.HitBox))
            {
                if (!stunned)
                {
                    if (this.Orb) //if this player has the orb
                    {
                        Stun();
                        return true;
                    }
                    else if (p.Orb) //if other player has orb
                    {
                        return false;
                    }
                }
            }
            return false;
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
            s.Draw(this.Tex, between, Color.Black);
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
            if (this.HitBox.Center.X < 0)
            {
                this.X = maxX - this.Width/2;
            }
            if (this.HitBox.Center.X > maxX)
            {
                this.X = 0;
            }
            if (this.HitBox.Center.Y < 0)
            {
                this.Y = maxY - this.Height/2; //top of the screen seems to act as some sort of solid wall for some reason
            }
            if (this.HitBox.Center.Y > maxY)
            {
                this.Y = 0;
            }
        }

        public void UsePowerUp()
        //will eventually work as powerup functionality
        {

        }
    }
}
