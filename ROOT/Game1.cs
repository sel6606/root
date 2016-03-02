using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Switch case for game state
            switch (currentState)
            {
                case GameState.Menu:
                    //If this returns Start, change state to Game and reset values
                    //If this returns quit, end the program
                    //Else call the menu's draw method
                    currentMenuState=menuManager.NextState(currentMenuState);
                    break;
                case GameState.Game:
                    //If either player wins, change state to game over
                    //Otherwise, run logic and call the UI and Player draw methods
                    break;
                case GameState.GameOver:
                    //If the player chooses play again, change state to game and reset values
                    //If the player chooses back to menu, change state to menu and menustate to main
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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
