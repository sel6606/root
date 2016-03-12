using Microsoft.Xna.Framework;
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
        Options
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
        private double timer1;
        private double timer2;
        private bool hasOrbP1;
        private bool hasOrbP2;
        private Player p1;
        private Player p2;
        private Stage gameStage;
        private Orb orb;
        private Texture2D brickTexture;
        private Texture2D menuTexture;
        private int buttonWidth;
        private int buttonHeight;
        private int halfScreen;
        private int bottomDistance;

        //Variables for testing purposes
        private KeyboardState kbState;
        private KeyboardState previousKbState;
        private int playerSize = 100;
        private bool NEEDSCONDITION = false;
        private MouseState mState;
        private MouseState previousMState;
        private Button restart;
        private Button menu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            menuTexture = Content.Load<Texture2D>("m2Menu");
            
            brickTexture = Content.Load<Texture2D>("brick-wall");
            gameStage = new Stage(spriteBatch, brickTexture);
            gameStage.ReadStage("stagetest.txt");
            menuManager = new MenuMan(this, menuTexture);
            menuManager.MenuFont = Content.Load<SpriteFont>("menuText");

            buttonWidth = 300;
            buttonHeight = 100;
            halfScreen = (GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            bottomDistance = (GraphicsDevice.Viewport.Height - (buttonHeight + 50));
            restart = new Button(menuTexture, new Rectangle(halfScreen - ((buttonWidth/2) + 20), bottomDistance, buttonWidth, buttonHeight));
            menu = new Button(menuTexture, new Rectangle(halfScreen + ((buttonWidth/2) + 20), bottomDistance, buttonWidth, buttonHeight));
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
            //Gets the current state of the keyboard
            kbState = Keyboard.GetState();

            //Switch case for game state
            switch (currentState)
            {
                case GameState.Menu:
                    mState = Mouse.GetState();
                    currentMenuState = menuManager.NextState(currentMenuState,mState,previousMState);
                    if (currentMenuState == MenuState.Start)
                    {
                        currentState = GameState.Game;
                        Reset();
                    }else if(currentMenuState== MenuState.Quit)
                    {
                        Exit();
                    }
                    break;
                case GameState.Game:

                    //If either player wins, change state to game over
                    p1.intersect = false;
                    for (int i = 0; i < gameStage.StageBounds.Count; i++)
                    {
                        p1.CheckCollision(gameStage.StageBounds[i]);
                    }
                    p1.Move();
                    
                    //p2.Move();
                    if (timer1==0 || timer2 == 0 || SingleKeyPress(Keys.O))
                    {
                        currentState = GameState.GameOver;
                    }
                    //Otherwise, run logic and call the UI and Player draw methods
                    break;
                case GameState.GameOver:
                    if (restart.MouseHovering(mState.X, mState.Y) && SingleMouseClick()) //If the player chooses play again, change state to game and reset values
                    {
                        currentState = GameState.Game;
                        //*Code to reset values*
                    }else if (menu.MouseHovering(mState.X, mState.Y) && SingleMouseClick()) //If the player chooses back to menu, change state to menu and menustate to main
                    {
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
                case GameState.Menu:
                    menuManager.Draw(currentMenuState, spriteBatch);
                    break;
                case GameState.Game:
                    gameStage.Draw();
                    p1.Draw(spriteBatch);
                    p2.Draw(spriteBatch);
                    //orb.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    menuManager.Draw(currentMenuState, spriteBatch);
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
            timer1 = 180;
            timer2 = 180;
            hasOrbP1 = false;
            hasOrbP2 = false;
            p1 = new Player(0, 0, playerSize, playerSize, timer1, menuTexture);
            p1.SetControls(Keys.D, Keys.A, Keys.W, Keys.S);
            p2 = new Player(0, 0, playerSize, playerSize, timer2, menuTexture);
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



    }
}
