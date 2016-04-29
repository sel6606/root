﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ROOT
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    //Enum for the menu states
    public enum MenuState
    {
        Main,
        Start,
        Quit,
        Controls,
        Instructions,
        Options,
        Selection
    }

    public class Game1 : Game
    {

        //Enum for game states
        public enum GameState
        {
            Menu,
            Game,
            GameOver
        }

        //Fields
        private GameState currentState;
        private MenuState currentMenuState;
        private MenuMan menuManager;
        private UIMan uiManager;
        private PowMan powerManager;
        private double timer1;
        private double timer2;
        private double timer3;
        private double timer4;
        private bool hasOrbsP1;
        private bool hasOrbP2;
        private Player p1;
        private Player p2;
        private Player p3;
        private Player p4;
        private Stage gameStage;
        private Orb orb;
        private Texture2D brickTexture;
        private Texture2D startTexture;
        private Texture2D instructionsTexture;
        private Texture2D quitTexture;
        private Texture2D backTexture;
        private Texture2D restartTexture;
        private Texture2D menuTexture;
        private Texture2D cancelTexture;
        private Texture2D orbTexture;
        private Texture2D playerTexture;
        //Width of each button
        private int buttonWidth;
        //Height of each button
        private int buttonHeight;
        //Finds half of the screen's width to help center the buttons
        private int halfScreen;
        //Distance of buttons from the bottom of the screen (used for the restart and menu buttons on the game over screen
        private int bottomDistance;
        private MouseState mState;
        private MouseState previousMState;
        private Button restart;
        private Button menu;
        private SpriteFont uiFont;

        public double Timer1
        {
            get { return timer1; }
        }

        public double Timer2
        {
            get { return timer2; }
        }

        public double Timer3
        {
            get { return timer3; }
        }

        public double Timer4
        {
            get { return timer4; }
        }

        //Variables for testing purposes
        private KeyboardState kbState;
        private KeyboardState previousKbState;
        private int playerSize = 25;
        private int playerWidth = 44;
        private int playerHeight = 72;
        private bool NEEDSCONDITION = false;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<SoundEffect> soundEffects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            soundEffects = new List<SoundEffect>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Sets mouse to visible and sets the inital GameState and MenuState
            //Also sets inital values
            IsMouseVisible = true;
            currentState = GameState.Menu;
            currentMenuState = MenuState.Main;
            Reset();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Sound effects
            soundEffects.Add(Content.Load<SoundEffect>("click"));

            //Texture for buttons on the menus
            startTexture = Content.Load<Texture2D>("MenuStart");
            instructionsTexture = Content.Load<Texture2D>("MenuInstructions");
            quitTexture = Content.Load<Texture2D>("MenuQuit");
            backTexture = Content.Load<Texture2D>("MenuBack");
            restartTexture = Content.Load<Texture2D>("MenuRestart");
            menuTexture = Content.Load<Texture2D>("MenuMenu");

            playerTexture = Content.Load<Texture2D>("Mario");


            brickTexture = Content.Load<Texture2D>("brick-wall");
            gameStage = new Stage(spriteBatch, brickTexture);
            gameStage.ReadStage("stagetest2.txt", p1, p2, p3, p4, orb);
            menuManager = new MenuMan(this, startTexture, instructionsTexture, quitTexture, backTexture,soundEffects[0]);
            menuManager.MenuFont = Content.Load<SpriteFont>("menuText");
            uiFont = Content.Load<SpriteFont>("menuText");
            cancelTexture = Content.Load<Texture2D>("cancel");
            uiManager = new UIMan(this, uiFont, cancelTexture, Content.Load<Texture2D>("sprint"));
            orbTexture = Content.Load<Texture2D>("orb");


            buttonWidth = 300;
            buttonHeight = 100;
            halfScreen = (GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            bottomDistance = (GraphicsDevice.Viewport.Height - (buttonHeight + 50));
            //Sets the location of the restart button
            restart = new Button(restartTexture, new Rectangle(halfScreen - ((buttonWidth / 2) + 20), bottomDistance, buttonWidth, buttonHeight));
            //Sets the location of the menu button
            menu = new Button(menuTexture, new Rectangle(halfScreen + ((buttonWidth / 2) + 20), bottomDistance, buttonWidth, buttonHeight));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Gets the current state of the keyboard and Mouse
            kbState = Keyboard.GetState();
            mState = Mouse.GetState();

            //Switch case for game state
            switch (currentState)
            {
                case GameState.Menu: //If the game is on the menu
                    currentMenuState = menuManager.NextState(currentMenuState, mState, previousMState);
                    if (currentMenuState == MenuState.Start)
                    { //If the player pressed start, start the game 
                        currentState = GameState.Game;
                        Reset();
                    }
                    else if (currentMenuState == MenuState.Quit)
                    { //If the player pressed quit, exit the game
                        Exit();
                    }
                    break;
                case GameState.Game:
                    powerManager.Update(gameTime.ElapsedGameTime.TotalSeconds);
                    //If either player wins, change state to game over
                    PlayerStuff(gameTime); //most player logic is handled here

                    PlayerCollisions(gameTime); //all player collision logic

                    //checking for orb collision
                    if (p1.HitBox.Intersects(orb.HitBox) && orb.Active)
                    {
                        p1.Orb = true;
                    }
                    else if(p2.HitBox.Intersects(orb.HitBox) && orb.Active)
                    {
                        p2.Orb = true;
                    }
                    else if(p3.HitBox.Intersects(orb.HitBox) && orb.Active)
                    {
                        p3.Orb = true;
                    }
                    else if(p4.HitBox.Intersects(orb.HitBox) && orb.Active)
                    {
                        p4.Orb = true;
                    }

                    if(p1.Orb || p2.Orb || p3.Orb || p4.Orb)
                    {
                        orb.Active = false; //orb is not drawn if either player has it
                    }
                    if (p1.Orb)
                    {
                        timer1 -= gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (p2.Orb)
                    {
                        timer2 -= gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if(p3.Orb)
                    {
                        timer3 -= gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if(p4.Orb)
                    {
                        timer4 -= gameTime.ElapsedGameTime.TotalSeconds;
                    }


                    if (timer1 <= 0 || timer2 <= 0 || timer3 <=0 || timer4 <= 0 || SingleKeyPress(Keys.O))
                    {
                        currentState = GameState.GameOver;
                    }
                    if (uiManager.CheckExit(mState, previousMState))
                    {
                        soundEffects[0].CreateInstance().Play();
                        currentMenuState = MenuState.Main;
                        currentState = GameState.Menu;
                    }
                    //Otherwise, run logic and call the UI and Player draw methods
                    break;
                case GameState.GameOver: //If the game is on the game over screen
                    if (restart.MouseHovering(mState.X, mState.Y) && SingleMouseClick()) //If the player chooses play again, change state to game and reset values
                    { //If the player clicks restart, restart the game
                        currentState = GameState.Game;
                        Reset();
                    }
                    else if (menu.MouseHovering(mState.X, mState.Y) && SingleMouseClick()) //If the player chooses back to menu, change state to menu and menustate to main
                    { //If the player clicks menu, return to the main menu
                        currentState = GameState.Menu;
                        currentMenuState = MenuState.Main;
                    }
                    break;
            }
            previousKbState = kbState;
            previousMState = mState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.Menu:    //Draws a menu dependent on the current menu state
                    menuManager.Draw(currentMenuState, spriteBatch);
                    break;
                case GameState.Game:    //Draws the game screen, drawing the stage, both players, and the orb
                    gameStage.Draw();
                    p1.Draw(spriteBatch); //player should change color to show they have the orb
                    p2.Draw(spriteBatch);
                    p3.Draw(spriteBatch);
                    p4.Draw(spriteBatch);
                    uiManager.Draw(spriteBatch);
                    if(!p1.Orb && !p2.Orb && !p3.Orb && !p4.Orb)
                    {
                        orb.Draw(spriteBatch);
                    }
                    break;
                case GameState.GameOver:    //Draws the game over screen, drawing a restart button and a menu button
                    restart.Draw(spriteBatch);
                    menu.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Resets variables to their initial values that they should have at the start
        public void Reset()
        {
            gameStage = new Stage(spriteBatch, brickTexture);
            gameStage.ReadStage("milestone3.txt", p1, p2, p3, p4, orb);
            timer1 = 1200;
            timer2 = 1200;
            timer3 = 1200;
            timer4 = 1200;
            p1 = new Player(this, gameStage.P1startX, gameStage.P1startY-50, 40, 30, timer1, playerTexture,PlayerIndex.One);
            p1.SetControls(Keys.D, Keys.A, Keys.W, Keys.S);
            orb = new Orb(gameStage.OrbstartX, gameStage.OrbstartY, 25, 25, orbTexture);
            p2 = new Player(this, gameStage.P2startX, gameStage.P2startY-50, 40, 30, timer2, playerTexture,PlayerIndex.Two);
            p2.SetControls(Keys.Right, Keys.Left, Keys.Up, Keys.Down);
            powerManager = new PowMan(p1, p2, p3, p4);
            p3 = new Player(this, gameStage.P3startX, gameStage.P3startY - 50, 40, 30, timer3, playerTexture, PlayerIndex.Three);
            p3.SetControls(Keys.NumPad6, Keys.NumPad4, Keys.NumPad8, Keys.NumPad5);
            p4 = new Player(this, gameStage.P4startX, gameStage.P4startY - 50, 40, 30, timer4, playerTexture, PlayerIndex.Four);
            p4.SetControls(Keys.L, Keys.J, Keys.I, Keys.K);
        }

        //Checks to see if a key was pressed exactly once
        private bool SingleKeyPress(Keys key)
        {
            if (kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key))
            { //Returns true if the key being pressed is different from the key pressed in the previous state
                return true;
            }
            else
            {
                return false;
            }
        }

        //Checks to see if the left mouse button was clicked exactly once
        private bool SingleMouseClick()
        {
            if (mState.LeftButton == ButtonState.Pressed &&
                previousMState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void PlayerStuff(GameTime gameTime)
        //all player logic is processed here
        {
            //player 1
            p1.Update(gameStage.StageBounds);
            p1.ScreenWrap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            p1.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (p1.timeCounter >= p1.timePerFrame)
            {
                p1.frame += 1;                     // Adjust the frame

                if (p1.frame > p1.WALK_FRAME_COUNT)
                {  // Check the bounds
                    p1.frame = 1;

                }// Back to 1 (since 0 is the "standing" frame)

                p1.timeCounter -= p1.timePerFrame;    // Remove the time we "used"
            }

            //player 2
            p2.Update(gameStage.StageBounds);
            p2.ScreenWrap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            p2.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (p2.timeCounter >= p2.timePerFrame)
            {
                p2.frame += 1;                     // Adjust the frame

                if (p2.frame > p2.WALK_FRAME_COUNT)   // Check the bounds
                    p2.frame = 1;                  // Back to 1 (since 0 is the "standing" frame)

                p2.timeCounter -= p2.timePerFrame;    // Remove the time we "used"
            }

            //player 3
            p3.Update(gameStage.StageBounds);
            p3.ScreenWrap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            p3.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (p3.timeCounter >= p3.timePerFrame)
            {
                p3.frame += 1;                     // Adjust the frame

                if (p3.frame > p3.WALK_FRAME_COUNT)
                {  // Check the bounds
                    p3.frame = 1;

                }// Back to 1 (since 0 is the "standing" frame)

                p3.timeCounter -= p3.timePerFrame;    // Remove the time we "used"
            }

            //player 4 
            p4.Update(gameStage.StageBounds);
            p4.ScreenWrap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            p4.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (p4.timeCounter >= p4.timePerFrame)
            {
                p4.frame += 1;                     // Adjust the frame

                if (p4.frame > p4.WALK_FRAME_COUNT)
                {  // Check the bounds
                    p4.frame = 1;

                }// Back to 1 (since 0 is the "standing" frame)

                p4.timeCounter -= p4.timePerFrame;    // Remove the time we "used"
            }
        }

       

        public void PlayerCollisions(GameTime gameTime)
        //handles all of the player collision logic in one spot
        {
            //checks if each player is colliding with player 1
            p1.CheckPlayerCollision(p1, p2, gameTime.ElapsedGameTime.TotalSeconds);
            p1.CheckPlayerCollision(p1, p3, gameTime.ElapsedGameTime.TotalSeconds);
            p1.CheckPlayerCollision(p1, p4, gameTime.ElapsedGameTime.TotalSeconds);

            //checks if players 3 or 4 are colliding with player 2
            p2.CheckPlayerCollision(p2, p3, gameTime.ElapsedGameTime.TotalSeconds);
            p2.CheckPlayerCollision(p2, p4, gameTime.ElapsedGameTime.TotalSeconds);

            //checks if player 3 is colliding with player 4
            p3.CheckPlayerCollision(p3, p4, gameTime.ElapsedGameTime.TotalSeconds);
        }

    }
}
