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
        //Enum for playerState
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

        //Fields for player controls
        public int moveRight;
        public int moveLeft;
        public int jump;
        public int use;

        //Fields for collision logic
        private bool hasOrb;
        private bool ground;
        private bool topWall;
        private bool leftWall;
        private bool rightWall;
        bool stunned; //checks if player is stunned
        private double stunTime = 3.00; //keeps track of how long a player is stunned

        //Fields for position and movement logic
        private int jumpUp = -1;
        private int gravDelay = 0;
        private Vector2 previousPosition;
        private Vector2 currentPosition;
        private int previousGravSpeed = -2;
        private int gravSpeed = -2;
        private Rectangle between;
        public int speed = 1;


        //Properties for hasOrb
        public bool Orb
        {
            get { return hasOrb; }
            set { hasOrb = value; }
        }

        public bool Stunned
        {
            get { return stunned; }
            set { stunned = value; }
        }

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height, double time, Texture2D texture)
            : base(x, y, width, height, false, texture)
        {
            hasOrb = false; //player doesn't start with orb
        }

        //Updates the position of the player depending on the user input
        public void Move()
        {
            //Previous position is the position at the start of the movement
            //Current position is the position at the end of movement
            previousPosition = new Vector2(this.X, this.Y);
            currentPosition = new Vector2(this.X, this.Y);

            if (!stunned)
            { //If the player isn't stunned, do the movement logic
                KeyboardState input = Keyboard.GetState();
                if (input.IsKeyDown((Keys)jump))
                { //If the jump key is pressed
                    Jump();
                }
                if (input.IsKeyDown((Keys)moveRight))
                { //If the "right" key is pressed
                    if (!rightWall)
                    { //If the player is not colliding with a wall on the right
                      //update the x position
                        currentPosition.X += speed;
                        this.X += speed;
                    }
                }
                if (input.IsKeyDown((Keys)moveLeft))
                { //If the "left" key is pressed
                    if (!leftWall)
                    { //If the player is not colliding with a wall on the left
                      //update the x position
                        currentPosition.X -= speed;
                        this.X -= speed;
                    }
                }
                
                if (!ground)
                { //If the player is in the air
                  //Run the gravity logic
                    
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

        //Sets parameters for jumping
        public void Jump()
        {
            if (ground)
            { //If the player is on the ground
                if (!topWall)
                { //If the player is not colliding with a wall above them
                    jumpUp = 8;
                    gravDelay = 3;
                }

            }
        }

        //Checks if the player is colliding with anything in the given list of tiles
        public void CheckCollision(List<Tile> g)
        {
            //Initially sets all collisions to false
            ground = false;
            topWall = false;
            leftWall = false;
            rightWall = false;

            //Rectangle that represents the change in position of the player
            if (jumpUp < 0)
            {
                between = new Rectangle((int)previousPosition.X, (int)previousPosition.Y, (int)(currentPosition.X + this.Width - previousPosition.X), (int)(currentPosition.Y + this.Height - previousPosition.Y));
            }
            else
            {
                between = new Rectangle((int)currentPosition.X, (int)currentPosition.Y, (int)(previousPosition.X + this.Width - currentPosition.X), (int)(previousPosition.Y + this.Height - currentPosition.Y));
            }

            //X coordinates for the bounds of the player
            int rBound = this.Center.X + (this.Width / 2) - 1;
            int lBound = this.Center.X - (this.Width / 2) + 1;

            //Y coordinates for the bounds of the player
            int bBound = this.Center.Y + (this.Height / 2) - 1;
            int uBound = this.Center.Y - (this.Height / 2) + 1;

            //For each tile in the list
            for (int i = 0; i < g.Count; i++)
            {
                //Checks for platforms below the player and 
                //checks that tile and player are in the same relative x-coordinate
                if (this.Bottom == g[i].Top && (rBound >= g[i].X && lBound <= g[i].X + g[i].Width))
                {
                    //Move method will work as though the player were on the ground
                    ground = true;
                    gravSpeed = previousGravSpeed;
                }

                if (this.Top == g[i].HitBox.Bottom && //checks for platforms above the player
                 (rBound >= g[i].X && lBound <= g[i].X + g[i].Width)) //(checks that tile and player are in the same relative x-coordinate)
                {
                    //Jump method will stop moving the player up
                    topWall = true;
                }

                //Checks for walls to the left of the player 
                //and that tile and player are in the same relative y-coordinate
                if ((this.HitBox.Intersects(g[i].HitBox) || this.Left == g[i].Right) &&
                    (bBound >= g[i].Y && uBound <= g[i].Y + g[i].Height && this.Right>g[i].Left))
                {
                    //Player will stop moving left because of the wall
                    leftWall = true;
                }

                //Checks for walls to the right of the player
                //and that tile and player are in the same relative y-coordinate
                if ((this.Right == g[i].Left || this.HitBox.Intersects(g[i].HitBox)) &&
                    (bBound >= g[i].Y && uBound <= g[i].Y + g[i].Height ) && this.Left < g[i].Right)
                {
                    //Player will stop moving right because of the wall
                    rightWall = true;
                }
                if (between.Intersects(g[i].HitBox) && !ground)
                {
                    gravSpeed = -1;
                    jumpUp = -1;
                    currentPosition = previousPosition;
                }
            }

            //Updates the x and y of the player with the current position
            this.X = (int)currentPosition.X;
            this.Y = (int)currentPosition.Y;
        }

        //this method specifically handles logic for player on player collision
        //returns false unless the player who called this method has taken the orb
        public void CheckPlayerCollision(Player p1, Player p2, double gameTime)
        {
            if(p1.HitBox.Intersects(p2.HitBox) && !p1.Stunned && !p2.Stunned)
            {
                if(p1.Orb)
                {
                    p2.Orb = true;
                    p1.Orb = false;
                    p1.Stunned = true;
                    p1.Stun(gameTime);
                }
                else if(p2.Orb)
                {
                    p1.Orb = true;
                    p2.Orb = false;
                    p2.Stunned = true;
                    p2.Stun(gameTime);
                }
            }
            else
            {
                p1.Stun(gameTime);
                p2.Stun(gameTime);
            }
        }

        public void Stun(double gameTime)
        //player will be unable to move while stunned, player will also blink
        {
            if (stunned)
            {
                stunTime -= gameTime;
                if(stunTime <= 0)
                {
                    stunned = false;
                    stunTime = 3.00;
                }
            }
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
                this.X = maxX - this.Width / 2;
            }
            if (this.HitBox.Center.X > maxX)
            {
                this.X = 0;
            }
            if (this.HitBox.Center.Y < 0)
            {
                this.Y = maxY - this.Height / 2; //top of the screen seems to act as some sort of solid wall for some reason
            }
            if (this.HitBox.Center.Y > maxY)
            {
                this.Y = 0;
            }
        }

    }
}
