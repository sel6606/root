using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ROOT
{
    public class Player : GameObject
    {
        //Enum for playerState
        public enum PlayerState //keeps track of what player is doing
        {
            FaceRight,
            FaceLeft,
            Idle,
            Walk,
            Move,
            Jump,
            //MoveRight,
            //MoveLeft,
            //JumpRight,
            //JumpLeft,
            PowerUp
        }

        #region Fields
        //The player class
        private PlayerType thisType;

        //Fields for player controls
        public int moveRight;
        public int moveLeft;
        public int jump;
        public int use;

        private bool selectRight;
        private bool selectLeft;
        private bool selectDown;
        private bool selectUp;


        //Fields for collision logic
        private bool hasOrb;
        private bool ground;
        private bool topWall;
        private bool leftWall;
        private bool rightWall;

        private bool jumped = false;
        bool stunned; //checks if player is stunned
        private double stunTime = 10.00; //keeps track of how long a player is stunned

        //Fields for position and movement logic
        private bool xBox = false;
        private PlayerIndex playerNumber;
        private int jumpUp = -1;
        private int gravDelay = 0;
        private Vector2 previousPosition;
        private Vector2 currentPosition;
        private int previousGravSpeed = -3;
        private int gravSpeed = -3;
        private Rectangle between;
        private int speed = 2;
        private int baseSpeed = 2;

        //Input variables
        private PlayerState currentState;
        private PlayerState currentDirectionState;
        private KeyboardState previousKbState;



        // Texture and drawing
        public Texture2D spriteSheet;  // The single image with all of the animation frames

        // Animation
        public int frame;              // The current animation frame
        public double timeCounter;     // The amount of time that has passed
        public double fps;             // The speed of the animation
        public double timePerFrame;    // The amount of time (in fractional seconds) per frame

        // Constants for "source" rectangle (inside the image)
        public int WALK_FRAME_COUNT = 11;         // The number of frames in the animation
        public int RECT_Y_OFFSET = 0;    // How far down in the image are the frames?
        public int RECT_HEIGHT = 72;       // The height of a single frame
        public int RECT_WIDTH = 44;        // The width of a single frame

        private Game1 game;

        #endregion
        #region Properties
        public PlayerState CurentState
        {
            get { return currentState; }
        }

        public PlayerType ThisType
        {
            get { return thisType; }
        }

        public bool SelectRight
        {
            get { return selectRight; }
        }
        public bool SelectLeft
        {
            get { return selectLeft; }
        }
        public bool SelectDown
        {
            get { return selectDown; }
        }
        public bool SelectUp
        {
            get { return selectUp; }
        }

        public int BaseSpeed
        {
            get { return baseSpeed; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public PlayerIndex PlayerNumber
        {
            get { return playerNumber; }
            set { playerNumber = value; }
        }

        public bool XBox
        {
            get { return xBox; }
            set { xBox = value; }
        }
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

        public PlayerState CurrentDirectionState
        {
            get { return currentDirectionState; }
        }

        #endregion

        //constructor, calls game object's but forces isSolid to be false
        public Player(Game1 game, int x, int y, int width, int height, double time, Texture2D texture, PlayerIndex playerNum, int type)
            : base(x, y, width, height, false, texture)
        {
            this.game = game;
            spriteSheet = this.Tex;
            currentState = PlayerState.Idle;
            currentDirectionState = PlayerState.FaceRight;
            hasOrb = false; //player doesn't start with orb
            playerNumber = playerNum;
            xBox = GamePad.GetState(playerNumber).IsConnected;
            selectLeft = false;
            selectRight = false;
            selectUp = false;
            selectDown = false;
            setFPS();
            previousKbState = Keyboard.GetState();
            thisType = (PlayerType)type;
        }

        public void UpdateSelect()
        {
            KeyboardState input = Keyboard.GetState();
            bool up = false;
            bool right = false;
            bool left = false;
            bool down = false;

            if (!xBox)
            {
                up = input.IsKeyDown((Keys)jump) && previousKbState.IsKeyUp((Keys)jump);
                right = input.IsKeyDown((Keys)moveRight) && previousKbState.IsKeyUp((Keys)moveRight);
                left = input.IsKeyDown((Keys)moveLeft) && previousKbState.IsKeyUp((Keys)moveLeft);
                down = input.IsKeyDown((Keys)use) && previousKbState.IsKeyUp((Keys)use);
            }
            else if (xBox)
            {
                GamePadState gamePad = GamePad.GetState(playerNumber);
                if (gamePad.ThumbSticks.Left.Y < 0)
                {
                    down = true;
                }
                if (gamePad.ThumbSticks.Left.Y > 0)
                {
                    up = true;
                }
                if (gamePad.ThumbSticks.Left.X < 0)
                {
                    left = true;
                }
                if (gamePad.ThumbSticks.Left.X > 0)
                {
                    right = true;
                }
            }

            previousKbState = input;
            selectLeft = left;
            selectRight = right;
            selectUp = up;
            selectDown = down;

        }

        public void Update(List<Tile> tiles)
        {
            KeyboardState input = Keyboard.GetState();
            bool up = false;
            bool right = false;
            bool left = false;

            jumped = false;
            if (!xBox)
            {
                up = input.IsKeyDown((Keys)jump);
                right = input.IsKeyDown((Keys)moveRight);
                left = input.IsKeyDown((Keys)moveLeft);
            }
            else if (xBox)
            {
                GamePadState gamePad = GamePad.GetState(playerNumber);
                up = gamePad.IsButtonDown(Buttons.A);
                if (gamePad.ThumbSticks.Left.X < 0)
                {
                    left = true;
                }
                if (gamePad.ThumbSticks.Left.X > 0)
                {
                    right = true;
                }
            }
            for (int i = 0; i < speed; i++)
            {
                Move(up, right, left);
                CheckCollision(tiles);
            }
        }
        //Updates the position of the player depending on the user input
        public void Move(bool up, bool right, bool left)
        {
            gravSpeed = previousGravSpeed;
            //Previous position is the position at the start of the movement
            //Current position is the position at the end of movement
            previousPosition = new Vector2(this.X, this.Y);
            currentPosition = new Vector2(this.X, this.Y);

            if (!stunned)
            { //If the player isn't stunned, do the movement logic
                if (up)
                { //If the jump key is pressed
                    Jump();
                }
                if (right)
                { //If the "right" key is pressed

                    if (!rightWall)
                    { //If the player is not colliding with a wall on the right
                      //update the x position
                        currentPosition.X += 1;
                        this.X += 1;
                    }
                }
                if (left)
                { //If the "left" key is pressed

                    if (!leftWall)
                    { //If the player is not colliding with a wall on the left
                      //update the x position
                        currentPosition.X -= 1;
                        this.X -= 1;
                    }
                }
                if (!jumped)
                {
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
                    jumped = true;
                }


                //Determines the direction the player is facing
                if (right)
                {
                    currentDirectionState = PlayerState.FaceRight;
                }
                if(left)
                {
                    currentDirectionState = PlayerState.FaceLeft;
                }
                

                //Determines if the the player is idle, jumping, or moving
                switch (currentState)
                {
                    case PlayerState.Idle:
                        if (right || left)
                        {
                            currentState = PlayerState.Move;
                        }
                        else if (up)
                        {
                            currentState = PlayerState.Jump;
                        }
                        break;
                    case PlayerState.Jump:
                        if (right || left)
                        {
                            currentState = PlayerState.Move;
                        }
                        break;
                    case PlayerState.Move:
                        if (!left && !right)
                        {
                            currentState = PlayerState.Idle;
                        }
                        if (up)
                        {
                            currentState = PlayerState.Jump;
                        }
                        break;
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
                    jumpUp = 12;
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
                    (bBound >= g[i].Y && uBound <= g[i].Y + g[i].Height) && this.Right > g[i].Right)
                {
                    //Player will stop moving left because of the wall

                    leftWall = true;
                }

                //Checks for walls to the right of the player
                //and that tile and player are in the same relative y-coordinate
                if ((this.Right == g[i].Left || this.HitBox.Intersects(g[i].HitBox)) &&
                    (bBound >= g[i].Y && uBound <= g[i].Y + g[i].Height) && this.Left < g[i].Left)
                {
                    //Player will stop moving right because of the wall
                    rightWall = true;
                }
                if (between.Intersects(g[i].HitBox) && !ground && rBound >= g[i].X && lBound <= g[i].X + g[i].Width)
                { //Note to self: Change the <= to < might fix. Needs more testing
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
            if (p1.HitBox.Intersects(p2.HitBox) && !p1.Stunned && !p2.Stunned)
            {
                if (p1.Orb)
                {
                    p2.Orb = true;
                    p1.Orb = false;
                    p1.Stunned = true;
                    //p1.Stun(gameTime);
                }
                else if (p2.Orb)
                {
                    p1.Orb = true;
                    p2.Orb = false;
                    p2.Stunned = true;
                    //p2.Stun(gameTime);
                }
            }
            else
            {
                //p1.Stun(gameTime);
                //p2.Stun(gameTime);
            }
        }

        public void Stun(double gameTime)
        //player will be unable to move while stunned
        {
            if (stunned)
            {
                stunTime -= gameTime;
                if (stunTime <= 0)
                {
                    stunned = false;
                    stunTime = 10.00;
                }
            }
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
                this.Y = maxY - this.Height / 2;
            }
            if (this.HitBox.Center.Y > maxY)
            {
                this.Y = 0;
            }
        }

        private void DrawStanding(SpriteEffects flipSprite, SpriteBatch s)
        {
            if(this.Orb) //changes player color if they have the orb
            {
                s.Draw(spriteSheet, new Vector2(this.X, this.Y - this.Height), new Rectangle(0, RECT_HEIGHT * 3, RECT_WIDTH, RECT_HEIGHT), Color.Gold, 0, Vector2.Zero, 0.9f, flipSprite, 0);
            }
            else
            {
                s.Draw(spriteSheet, new Vector2(this.X, this.Y - this.Height), new Rectangle(0, RECT_HEIGHT * 3, RECT_WIDTH, RECT_HEIGHT), Color.White, 0, Vector2.Zero, 0.9f, flipSprite, 0);
            }
        }

        private void DrawJumping(SpriteEffects flipSprite, SpriteBatch s)
        {
            if(this.Orb)
            {
                if (jumpUp < 0)
                {
                    if (ground)
                    {
                        DrawStanding(flipSprite, s);
                    }
                    else
                    {
                        s.Draw(
                               spriteSheet,                    // - The texture to draw
                               new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                               new Rectangle(                  // - The "source" rectangle
                                   RECT_WIDTH,   //   - This rectangle specifies
                                   RECT_HEIGHT * 2,        //	   where "inside" the texture
                                   RECT_WIDTH,           //     to get pixels (We don't want to
                                   RECT_HEIGHT),         //     draw the whole thing)
                               Color.Gold,                    // - The color
                               0,                              // - Rotation (none currently)
                               Vector2.Zero,                   // - Origin inside the image (top left)
                               0.9f,                           // - Scale (100% - no change)
                               flipSprite,                     // - Can be used to flip the image
                               0);
                    }
                }
                else
                {
                    s.Draw(
                           spriteSheet,                    // - The texture to draw
                           new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                           new Rectangle(                  // - The "source" rectangle
                               0,   //   - This rectangle specifies
                               RECT_HEIGHT * 2,        //	   where "inside" the texture
                               RECT_WIDTH,           //     to get pixels (We don't want to
                               RECT_HEIGHT),         //     draw the whole thing)
                           Color.Gold,                    // - The color
                           0,                              // - Rotation (none currently)
                           Vector2.Zero,                   // - Origin inside the image (top left)
                           0.9f,                           // - Scale (100% - no change)
                           flipSprite,                     // - Can be used to flip the image
                           0);
                }
            }
            else
            {
                if (jumpUp < 0)
                {
                    if (ground)
                    {
                        DrawStanding(flipSprite, s);
                    }
                    else
                    {
                        s.Draw(
                               spriteSheet,                    // - The texture to draw
                               new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                               new Rectangle(                  // - The "source" rectangle
                                   RECT_WIDTH,   //   - This rectangle specifies
                                   RECT_HEIGHT * 2,        //	   where "inside" the texture
                                   RECT_WIDTH,           //     to get pixels (We don't want to
                                   RECT_HEIGHT),         //     draw the whole thing)
                               Color.White,                    // - The color
                               0,                              // - Rotation (none currently)
                               Vector2.Zero,                   // - Origin inside the image (top left)
                               0.9f,                           // - Scale (100% - no change)
                               flipSprite,                     // - Can be used to flip the image
                               0);
                    }
                }
                else
                {
                    s.Draw(
                           spriteSheet,                    // - The texture to draw
                           new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                           new Rectangle(                  // - The "source" rectangle
                               0,   //   - This rectangle specifies
                               RECT_HEIGHT * 2,        //	   where "inside" the texture
                               RECT_WIDTH,           //     to get pixels (We don't want to
                               RECT_HEIGHT),         //     draw the whole thing)
                           Color.White,                    // - The color
                           0,                              // - Rotation (none currently)
                           Vector2.Zero,                   // - Origin inside the image (top left)
                           0.9f,                           // - Scale (100% - no change)
                           flipSprite,                     // - Can be used to flip the image
                           0);
                }
            }
        }

        private void DrawWalking(SpriteEffects flipSprite, SpriteBatch s)
        {
            if(this.Orb)
            {
                if (frame > 5)
                {
                    s.Draw(
                           spriteSheet,                    // - The texture to draw
                           new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                           new Rectangle(                  // - The "source" rectangle
                               (frame - 6) * RECT_WIDTH,   //   - This rectangle specifies
                               RECT_Y_OFFSET + 72,        //	   where "inside" the texture
                               RECT_WIDTH,           //     to get pixels (We don't want to
                               RECT_HEIGHT),         //     draw the whole thing)
                          Color.Gold,                    // - The color
                          0,                              // - Rotation (none currently)
                          Vector2.Zero,                   // - Origin inside the image (top left)
                          0.9f,                           // - Scale (100% - no change)
                          flipSprite,                     // - Can be used to flip the image
                          0);
                    // - Layer depth (unused)
                }
                else
                {
                    s.Draw(
                        spriteSheet,                    // - The texture to draw
                        new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                        new Rectangle(                  // - The "source" rectangle
                            frame * RECT_WIDTH,   //   - This rectangle specifies
                            RECT_Y_OFFSET,        //	   where "inside" the texture
                            RECT_WIDTH,           //     to get pixels (We don't want to
                            RECT_HEIGHT),         //     draw the whole thing)
                        Color.Gold,                    // - The color
                        0,                              // - Rotation (none currently)
                        Vector2.Zero,                   // - Origin inside the image (top left)
                        0.9f,                           // - Scale (100% - no change)
                        flipSprite,                     // - Can be used to flip the image
                        0);
                    // - Layer depth (unused)
                }
            }
            else
            {
                if (frame > 5)
                {
                    s.Draw(
                           spriteSheet,                    // - The texture to draw
                           new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                           new Rectangle(                  // - The "source" rectangle
                               (frame - 6) * RECT_WIDTH,   //   - This rectangle specifies
                               RECT_Y_OFFSET + 72,        //	   where "inside" the texture
                               RECT_WIDTH,           //     to get pixels (We don't want to
                               RECT_HEIGHT),         //     draw the whole thing)
                          Color.White,                    // - The color
                          0,                              // - Rotation (none currently)
                          Vector2.Zero,                   // - Origin inside the image (top left)
                          0.9f,                           // - Scale (100% - no change)
                          flipSprite,                     // - Can be used to flip the image
                          0);
                    // - Layer depth (unused)
                }
                else
                {
                    s.Draw(
                        spriteSheet,                    // - The texture to draw
                        new Vector2(this.X, this.Y - this.Height),                       // - The location to draw on the screen
                        new Rectangle(                  // - The "source" rectangle
                            frame * RECT_WIDTH,   //   - This rectangle specifies
                            RECT_Y_OFFSET,        //	   where "inside" the texture
                            RECT_WIDTH,           //     to get pixels (We don't want to
                            RECT_HEIGHT),         //     draw the whole thing)
                        Color.White,                    // - The color
                        0,                              // - Rotation (none currently)
                        Vector2.Zero,                   // - Origin inside the image (top left)
                        0.9f,                           // - Scale (100% - no change)
                        flipSprite,                     // - Can be used to flip the image
                        0);
                    // - Layer depth (unused)
                }
            }
        }

        public void setFPS()
        {
            fps = 10.0;
            timePerFrame = 1.0 / fps;
        }

        public override void Draw(SpriteBatch s)
        {
            //base.Draw(s);
            //s.Draw(this.Tex, between, Color.Black);
            SpriteEffects flip;

            //Sets the direction the player is facing
            if (currentDirectionState == PlayerState.FaceLeft)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else
            {
                flip = SpriteEffects.None;
            }

            //Draws the player either idle, moving, or jumping
            switch (currentState)
            {
                case PlayerState.Idle:
                    DrawStanding(flip, s);
                    break;
                case PlayerState.Jump:
                    DrawJumping(flip, s);
                    break;
                case PlayerState.Move:
                    if (ground)
                    {
                        DrawWalking(flip, s);
                    }
                    else
                    {
                        DrawJumping(flip, s);
                    }
                    break;
                case PlayerState.PowerUp:
                    s.Draw(this.Tex, between, Color.Black);
                    break;
            }
        }
    }
}
