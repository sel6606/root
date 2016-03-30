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
        int moveRight;
        int moveLeft;
        int jump;
        int use;

        //Fields for collision logic
        private bool hasOrb;
        bool ground;
        bool topWall;
        bool leftWall;
        bool rightWall;
        static bool stunned; //checks if player is stunned

        //Fields for position and movement logic
        private int jumpUp = -1;
        private int gravDelay = 0;
        private Vector2 previousPosition;
        private Vector2 currentPosition;
        private int previousGravSpeed = -2;
        private int gravSpeed = -2;
        private Rectangle between;


        //Properties for hasOrb
        public bool Orb
        {
            get { return hasOrb; }
            set { hasOrb = value; }
        }

        //constructor, calls game object's but forces isSolid to be false
        public Player(int x, int y, int width, int height, double time, Texture2D texture)
            : base(x, y, width, height, false, texture)
        {
            hasOrb = false; //player doesn't start with orb
        }

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
                        currentPosition.X += 1;
                        this.X += 1;
                    }
                }
                if (input.IsKeyDown((Keys)moveLeft))
                { //If the "left" key is pressed
                    if (!leftWall)
                    { //If the player is not colliding with a wall on the left
                      //update the x position
                        currentPosition.X -= 1;
                        this.X -= 1;
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
            between = new Rectangle((int)previousPosition.X, (int)previousPosition.Y, (int)(currentPosition.X + this.Width - previousPosition.X), (int)(currentPosition.Y + this.Height - previousPosition.Y));

            for (int i = 0; i < g.Count; i++)
            { //For each tile in the list
              //Checks for platforms below the player and 
              //checks that tile and player are in the same relative x-coordinate
                if (this.Bottom == g[i].Top && (this.Center.X + (this.Width / 2) - 1 >= g[i].X && this.Center.X - (this.Width / 2) + 1 <= g[i].X + g[i].Width))
                { //Move method will work as though the player were on the ground
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

        public void UsePowerUp()
        //will eventually work as powerup functionality
        {

        }
    }
}
