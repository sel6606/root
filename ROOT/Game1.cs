using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private int playerSize = 100;
        private bool NEEDSCONDITION = true;
        private Player p1;
        private Player p2;
        private Stage gameStage;
        private Orb orb;

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
            currentState = GameState.Menu;
            menuManager = new MenuMan();
            currentMenuState = MenuState.Main;

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

            menuManager.SB = spriteBatch;
            menuManager.MenuTex = Content.Load<Texture2D>("m2Menu");
            menuManager.MenuFont = Content.Load<SpriteFont>("ComicSansMS_14");
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
            
            //Switch case for game state
            switch (currentState)
            {
                case GameState.Menu:
                    if (currentMenuState == MenuState.Start)
                    {
                        currentState = GameState.Game;
                        Reset();
                    }else if(currentMenuState== MenuState.Quit)
                    {
                        Exit();
                    }
                    currentMenuState=menuManager.NextState(currentMenuState);
                    break;
                case GameState.Game:
                    //If either player wins, change state to game over
                    if (timer1==0 || timer2 == 0)
                    {
                        currentState = GameState.GameOver;
                    }
                    //Otherwise, run logic and call the UI and Player draw methods
                    break;
                case GameState.GameOver:
                    if (NEEDSCONDITION) //If the player chooses play again, change state to game and reset values
                    {
                        currentState = GameState.Game;
                        //*Code to reset values*
                    }else if (NEEDSCONDITION) //If the player chooses back to menu, change state to menu and menustate to main
                    {
                        currentState = GameState.Menu;
                        currentMenuState = MenuState.Main;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (currentState)
            {
                case GameState.Menu:
                    menuManager.Draw(currentMenuState);
                    break;
                case GameState.Game:
                    gameStage.Draw();
                    p1.Draw(spriteBatch);
                    p2.Draw(spriteBatch);
                    orb.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    menuManager.Draw(currentMenuState);
                    break;
            }

            base.Draw(gameTime);
        }

        //Resets variables to their initial values that they should have at the start
        public void Reset()
        {
            timer1 = 180;
            timer2 = 180;
            hasOrbP1 = false;
            hasOrbP2 = false;
            p1 = new Player(0, 0, playerSize, playerSize, timer1);
            p2 = new Player(0, 0, playerSize, playerSize, timer2);
        }

    }
}
