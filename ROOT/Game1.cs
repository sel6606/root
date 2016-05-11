using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Credits,
        Instructions,
        Options,
        Selection,
        Title
    }

    //Enum for the different characters
    //Enum for player character
    public enum PlayerType //Decides what sprite to draw and what powerup to do.
    {
        GentleMan,
        Knight,
        Cowboy,
        Caveman

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
        #region Game Fields
        private GameState currentState;
        private MenuState currentMenuState;
        private MenuMan menuManager;
        private UIMan uiManager;
        private PowMan powerManager;
        private double timer1;
        private double timer2;
        private double timer3;
        private double timer4;
        private Player p1;
        private Player p2;
        private Player p3;
        private Player p4;
        private Stage gameStage;
        private Orb orb;
        private int playerNum; //keeps track of how many players are in the game
        private List<Player> playerList;
        private Player winner;
        #endregion

        #region Textures
        private Texture2D brickTexture;
        private Texture2D startTexture;
        private Texture2D instructionsTexture;
        private Texture2D creditButton;
        private Texture2D quitTexture;
        private Texture2D backTexture;
        private Texture2D restartTexture;
        private Texture2D optionsTexture;
        private Texture2D menuTexture;
        private Texture2D cancelTexture;
        private Texture2D orbTexture;
        private Texture2D instructionScreen;
        private Texture2D cowboyInfo;
        private Texture2D knightInfo;
        private Texture2D cavemanInfo;
        private Texture2D gentlemanInfo;
        private Texture2D select;
        private Texture2D gSheet;
        private Texture2D kSheet;
        private Texture2D cmSheet;
        private Texture2D cbSheet;
        private Texture2D cowPowTex;
        private Texture2D cavePowTex;
        private Texture2D gentlePowTex;
        private Texture2D knightPowTex;
        private Texture2D credits;
        private Texture2D background;
        private Texture2D title;
        private Texture2D p1Win;
        private Texture2D p2Win;
        private Texture2D p3Win;
        private Texture2D p4Win;
        #endregion

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

        private const double TIMER_LENGTH= 120;



        #region Properties
        public Texture2D Title
        {
            get { return title; }
        }

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

        public PowMan PowerManager
        {
            get { return powerManager; }
        }

        public Texture2D CowPowTex
        {
            get { return cowPowTex; }
            set { cowPowTex = value; }
        }

        public Texture2D CavePowTex
        {
            get { return cavePowTex; }
            set { cavePowTex = value; }
        }

        public Texture2D GentlePowTex
        {
            get { return gentlePowTex; }
            set { gentlePowTex = value; }
        }

        public Texture2D KnightPowTex
        {
            get { return knightPowTex; }
            set { knightPowTex = value; }
        }

        public int PlayerNum
        {
            get { return playerNum; }
            set { playerNum = value; }
        }
        #endregion
        //Variables for testing purposes
        private KeyboardState kbState;
        private KeyboardState previousKbState;



        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<SoundEffect> soundEffects;
        List<Texture2D> spritesheets;

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
            playerNum = 4;
            currentState = GameState.Menu;
            currentMenuState = MenuState.Title;
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
            optionsTexture = Content.Load<Texture2D>("MenuOptions");
            menuTexture = Content.Load<Texture2D>("MenuMenu");
            instructionScreen = Content.Load<Texture2D>("Terrible Instructions");
            credits = Content.Load<Texture2D>("Credits");
            creditButton = Content.Load<Texture2D>("MenuCredits");
            select = Content.Load<Texture2D>("select");

            background = Content.Load<Texture2D>("background");
            title = Content.Load<Texture2D>("title");

            List<Texture2D> playerButtons = new List<Texture2D>();
            playerButtons.Add(Content.Load<Texture2D>("twoPlayers"));
            playerButtons.Add(Content.Load<Texture2D>("threePlayers"));
            playerButtons.Add(Content.Load<Texture2D>("fourPlayers"));

            gentlemanInfo = Content.Load<Texture2D>("gentleman_info");
            cavemanInfo = Content.Load<Texture2D>("caveman_info");
            cowboyInfo = Content.Load<Texture2D>("cowboy_info");
            knightInfo = Content.Load<Texture2D>("knight_info");

            gSheet = Content.Load<Texture2D>("gSheet");
            cmSheet = Content.Load<Texture2D>("cmSheet");
            cbSheet = Content.Load<Texture2D>("cbSheet");
            kSheet = Content.Load<Texture2D>("kSheet");

            spritesheets = new List<Texture2D> { gSheet, kSheet, cbSheet, cmSheet };


            List<Texture2D> portraits = new List<Texture2D>();
            portraits.Add(Content.Load<Texture2D>("cmPortrait"));
            portraits.Add(Content.Load<Texture2D>("cbPortrait"));
            portraits.Add(Content.Load<Texture2D>("kPortrait"));
            portraits.Add(Content.Load<Texture2D>("gPortrait"));
            portraits.Add(Content.Load<Texture2D>("placeholder"));
            portraits.Add(Content.Load<Texture2D>("placeholder2"));

            //PowerUp Textures
            knightPowTex = Content.Load<Texture2D>("sprint");
            cavePowTex = Content.Load<Texture2D>("cavePow");
            cowPowTex = Content.Load<Texture2D>("cowPow");
            gentlePowTex = Content.Load<Texture2D>("gentlePow");

            //Win messages
            p1Win = Content.Load<Texture2D>("win1");
            p2Win = Content.Load<Texture2D>("win2");
            p3Win = Content.Load<Texture2D>("win3");
            p4Win = Content.Load<Texture2D>("win4");

            brickTexture = Content.Load<Texture2D>("brick-wall");
            gameStage = new Stage(spriteBatch, brickTexture);
            gameStage.ReadStage("Milestone4.txt");
            menuManager = new MenuMan(this, startTexture, instructionsTexture, quitTexture,
                backTexture, optionsTexture, instructionScreen, creditButton, credits, cavemanInfo, cowboyInfo,
                knightInfo, gentlemanInfo,background, portraits, playerButtons, select, soundEffects[0],playerNum);
            menuManager.MenuFont = Content.Load<SpriteFont>("menuText");
            uiFont = Content.Load<SpriteFont>("menuText");
            cancelTexture = Content.Load<Texture2D>("cancel");
            uiManager = new UIMan(this, uiFont, cancelTexture);
            orbTexture = Content.Load<Texture2D>("orb");


            buttonWidth = 300;
            buttonHeight = 100;
            halfScreen = (GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            bottomDistance = (GraphicsDevice.Viewport.Height - (buttonHeight + 50));
            //Sets the location of the restart button
            restart = new Button(restartTexture, new Rectangle(halfScreen - ((buttonWidth / 2) + 20), bottomDistance, buttonWidth, buttonHeight));
            //Sets the location of the menu button
            menu = new Button(menuTexture, new Rectangle(halfScreen + ((buttonWidth / 2) + 20), bottomDistance, buttonWidth, buttonHeight));
            Reset();
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
                    if (currentMenuState == MenuState.Selection)
                    {
                        menuManager.SelectionState(playerNum, p1, p2, p3, p4);
                    }

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

                    #region Orb Collection
                    //checking for orb collision
                    if (p3 == null && p4 == null) // only two players
                    {
                        if (p1.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p1.Orb = true;
                        }
                        else if (p2.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p2.Orb = true;
                        }
                    }
                    else if (p3 != null && p4 == null) //three players
                    {
                        if (p1.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p1.Orb = true;
                        }
                        else if (p2.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p2.Orb = true;
                        }
                        else if (p3.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p3.Orb = true;
                        }
                    }
                    else if (p3 != null && p4 != null) //four players
                    {
                        if (p1.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p1.Orb = true;
                        }
                        else if (p2.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p2.Orb = true;
                        }
                        else if (p3.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p3.Orb = true;
                        }
                        else if (p4.HitBox.Intersects(orb.HitBox) && orb.Active)
                        {
                            p4.Orb = true;
                        }
                    }
                    #endregion

                    #region Orb Active
                    //collecting the orb
                    //orb is not drawn if a player has it
                    if (p3 == null && p4 == null) //only two players
                    {
                        if (p1.Orb || p2.Orb)
                        {
                            orb.Active = false;
                        }
                    }
                    else if(p3 != null && p4 == null) //three players
                    {
                        if (p1.Orb || p2.Orb || p3.Orb)
                        {
                            orb.Active = false;
                        }
                    }
                    else if(p3 != null && p4 != null) //four players
                    {
                        if (p1.Orb || p2.Orb || p3.Orb || p4.Orb)
                        {
                            orb.Active = false;
                        }
                    }
                    #endregion

                    #region Timer logic
                    //incrementing timers
                    if (p3 == null && p4 == null) //only two players
                    {
                        if (p1.Orb)
                        {
                            timer1 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else if (p2.Orb)
                        {
                            timer2 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                    else if(p3 != null && p4 == null) //three players
                    {
                        if (p1.Orb)
                        {
                            timer1 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else if (p2.Orb)
                        {
                            timer2 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else if (p3.Orb)
                        {
                            timer3 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                    else if(p3 != null && p4 != null) //four players
                    {
                        if (p1.Orb)
                        {
                            timer1 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else if (p2.Orb)
                        {
                            timer2 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else if (p3.Orb)
                        {
                            timer3 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        else if (p4.Orb)
                        {
                            timer4 -= gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                    #endregion

                    #region Timer Check
                    if (playerNum == 2 && (timer1 <= 0 || timer2 <= 0 || SingleKeyPress(Keys.O))) //two players
                    {
                        if(timer1 <= 0)
                        {
                            winner = p1;
                        }

                        else
                        {
                            winner = p2;
                        }
                        currentState = GameState.GameOver;
                    }
                    else if (playerNum == 3 && (timer1 <= 0 || timer2 <= 0 || timer3 <= 0 || SingleKeyPress(Keys.O))) //three players
                    {
                        if (timer1 <= 0)
                        {
                            winner = p1;
                        }

                        else if (timer2 <= 0)
                        {
                            winner = p2;
                        }

                        else
                        {
                            winner = p3;
                        }
                        currentState = GameState.GameOver;
                    }
                    else if (playerNum == 4 && (timer1 <= 0 || timer2 <= 0 || timer3 <= 0 || timer4 <= 0 || SingleKeyPress(Keys.O))) //four players
                    {
                        if (timer1 <= 0)
                        {
                            winner = p1;
                        }

                        else if (timer2 <= 0)
                        {
                            winner = p2;
                        }

                        else if (timer3 <= 0)
                        {
                            winner = p3;
                        }

                        else
                        {
                            winner = p4;
                        }
                        currentState = GameState.GameOver;
                    }
                    #endregion

                    foreach(Player p in playerList)
                    {
                        p.Stun(gameTime.ElapsedGameTime.TotalSeconds);
                    }

                    if (uiManager.CheckExit(mState, previousMState))
                    {
                        playerNum = 4;
                        Reset();
                        soundEffects[0].CreateInstance().Play();
                        currentMenuState = MenuState.Main;
                        currentState = GameState.Menu;
                    }
                    //Otherwise, run logic and call the UI and Player draw methods
                    break;
                case GameState.GameOver: //If the game is on the game over screen
                    if (restart.MouseHovering(mState.X, mState.Y) && SingleMouseClick()) //If the player chooses play again, change state to game and reset values
                    { //If the player clicks restart, restart the game
                        soundEffects[0].CreateInstance().Play();
                        currentState = GameState.Game;
                        Reset();
                    }
                    else if (menu.MouseHovering(mState.X, mState.Y) && SingleMouseClick()) //If the player chooses back to menu, change state to menu and menustate to main
                    { //If the player clicks menu, return to the main menu
                        soundEffects[0].CreateInstance().Play();
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
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();
            
            switch (currentState)
            {
                case GameState.Menu:    //Draws a menu dependent on the current menu state
                    menuManager.Draw(currentMenuState,spriteBatch);
                    break;
                case GameState.Game:    //Draws the game screen, drawing the stage, both players, and the orb
                    gameStage.Draw();
                    #region Drawing Players
                    if(p3 == null && p4 == null) //two players
                    {
                        p1.Draw(spriteBatch);
                        p2.Draw(spriteBatch);
                    }
                    else if(p3 != null && p4 == null) //three players
                    {
                        p1.Draw(spriteBatch);
                        p2.Draw(spriteBatch);
                        p3.Draw(spriteBatch);
                    }
                    else if(p3 != null && p4 != null) //four players
                    {
                        p1.Draw(spriteBatch);
                        p2.Draw(spriteBatch);
                        p3.Draw(spriteBatch);
                        p4.Draw(spriteBatch);
                    }
                    #endregion
                    uiManager.Draw(spriteBatch, playerNum);
                    #region Drawing Orb
                    if (p3 == null & p4 == null) //two players
                    {
                        if (!p1.Orb && !p2.Orb)
                        {
                            orb.Draw(spriteBatch);
                        }
                    }
                    else if(p3 != null && p4 == null) //three players
                    {
                        if (!p1.Orb && !p2.Orb && !p3.Orb)
                        {
                            orb.Draw(spriteBatch);
                        }
                    }
                    else if(p3 != null && p4 != null) //four players
                    {
                        if (!p1.Orb && !p2.Orb && !p3.Orb && !p4.Orb)
                        {
                            orb.Draw(spriteBatch);
                        }
                    }
                    #endregion
                    powerManager.Draw(spriteBatch);
                    break;
                case GameState.GameOver:    //Draws the game over screen, drawing a restart button and a menu button
                    spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    if(winner == p1)
                    {
                        spriteBatch.Draw(p1Win, new Rectangle(halfScreen, 25, buttonWidth, buttonHeight), Color.White);
                    }
                    if (winner == p2)
                    {
                        spriteBatch.Draw(p2Win, new Rectangle(halfScreen, 25, buttonWidth, buttonHeight), Color.White);
                    }
                    if (winner == p3)
                    {
                        spriteBatch.Draw(p3Win, new Rectangle(halfScreen, 25, buttonWidth, buttonHeight), Color.White);
                    }
                    if (winner == p4)
                    {
                        spriteBatch.Draw(p4Win, new Rectangle(halfScreen, 25, buttonWidth, buttonHeight), Color.White);
                    }

                    if (winner.ThisType == PlayerType.Caveman)
                    {
                        spriteBatch.Draw(cavemanInfo, new Vector2((GraphicsDevice.Viewport.Width / 2) - 100, (GraphicsDevice.Viewport.Height / 2) - 110), Color.White);
                    }
                    if (winner.ThisType == PlayerType.Cowboy)
                    {
                        spriteBatch.Draw(cowboyInfo, new Vector2((GraphicsDevice.Viewport.Width / 2) - 100, (GraphicsDevice.Viewport.Height / 2) - 110), Color.White);
                    }
                    else if (winner.ThisType == PlayerType.GentleMan)
                    {
                        spriteBatch.Draw(gentlemanInfo, new Vector2((GraphicsDevice.Viewport.Width / 2) - 100, (GraphicsDevice.Viewport.Height / 2) - 110), Color.White);
                    }
                    else if (winner.ThisType == PlayerType.Knight)
                    {
                        spriteBatch.Draw(knightInfo, new Vector2((GraphicsDevice.Viewport.Width / 2) - 100, (GraphicsDevice.Viewport.Height / 2) - 110), Color.White);
                    }
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

            p3 = null;
            p4 = null;

            playerList = new List<Player>();
            gameStage = new Stage(spriteBatch, brickTexture);
            gameStage.ReadStage("Milestone4.txt");

            orb = new Orb(gameStage.OrbstartX, gameStage.OrbstartY, 25, 25, orbTexture);

            p1 = new Player(this, gameStage.P1startX, gameStage.P1startY - 50, 40, 30, timer1, spritesheets[(int)menuManager.Types[0]], PlayerIndex.One, (int)menuManager.Types[0]);
            p1.SetControls(Keys.D, Keys.A, Keys.W, Keys.S);
            timer1 = TIMER_LENGTH;
            playerList.Add(p1); //adds player 1 to the list of players

            p2 = new Player(this, gameStage.P2startX, gameStage.P2startY - 50, 40, 30, timer2, spritesheets[(int)menuManager.Types[1]], PlayerIndex.Two, (int)menuManager.Types[1]);
            p2.SetControls(Keys.Right, Keys.Left, Keys.Up, Keys.Down);
            timer2 = TIMER_LENGTH;
            playerList.Add(p2); //adds player 2 to the list of players
            if (playerNum == 3) //three players
            {
                p3 = new Player(this, gameStage.P3startX, gameStage.P3startY - 50, 40, 30, timer3, spritesheets[(int)menuManager.Types[2]], PlayerIndex.Three, (int)menuManager.Types[2]);
                p3.SetControls(Keys.NumPad6, Keys.NumPad4, Keys.NumPad8, Keys.NumPad5);
                timer3 = TIMER_LENGTH;
                playerList.Add(p3); //adds player 3 to the list
            }
            else if(playerNum == 4) //four players
            {
                p3 = new Player(this, gameStage.P3startX, gameStage.P3startY - 50, 40, 30, timer3, spritesheets[(int)menuManager.Types[2]], PlayerIndex.Three, (int)menuManager.Types[2]);
                p3.SetControls(Keys.NumPad6, Keys.NumPad4, Keys.NumPad8, Keys.NumPad5);
                timer3 = TIMER_LENGTH;
                playerList.Add(p3); //adds player 3 to the list

                p4 = new Player(this, gameStage.P4startX, gameStage.P4startY - 50, 40, 30, timer4, spritesheets[(int)menuManager.Types[3]], PlayerIndex.Four, (int)menuManager.Types[3]);
                p4.SetControls(Keys.L, Keys.J, Keys.I, Keys.K);
                timer4 = TIMER_LENGTH;
                playerList.Add(p4); //adds player 4 to the list
            }
            powerManager = new PowMan(playerList, spriteBatch, GraphicsDevice, this);
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
                    p1.frame = 0;

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
                    p2.frame = 0;                  // Back to 1 (since 0 is the "standing" frame)

                p2.timeCounter -= p2.timePerFrame;    // Remove the time we "used"
            }

            //player 3
            if(p3 != null) //only checks player 3's logic if they're in the game
            {
                p3.Update(gameStage.StageBounds);
                p3.ScreenWrap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                p3.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
                if (p3.timeCounter >= p3.timePerFrame)
                {
                    p3.frame += 1;                     // Adjust the frame

                    if (p3.frame > p3.WALK_FRAME_COUNT)
                    {  // Check the bounds
                        p3.frame = 0;

                    }// Back to 1 (since 0 is the "standing" frame)

                    p3.timeCounter -= p3.timePerFrame;    // Remove the time we "used"
                }
            }

            //player 4 
            if(p4 != null) //only checks player 4's logic if they're in the game
            {
                p4.Update(gameStage.StageBounds);
                p4.ScreenWrap(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                p4.timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
                if (p4.timeCounter >= p4.timePerFrame)
                {
                    p4.frame += 1;                     // Adjust the frame

                    if (p4.frame > p4.WALK_FRAME_COUNT)
                    {  // Check the bounds
                        p4.frame = 0;

                    }// Back to 1 (since 0 is the "standing" frame)

                    p4.timeCounter -= p4.timePerFrame;    // Remove the time we "used"
                }
            }
        }



        public void PlayerCollisions(GameTime gameTime)
        //handles all of the player collision logic in one spot
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            if (p3 == null && p4 == null) //only 2 players
            {
                p1.CheckPlayerCollision(p1, p2, time); //p1 and p2
            }
            else if (p3 != null && p4 == null) //3 players
            {
                p1.CheckPlayerCollision(p1, p2, time); //p1 and p2
                p1.CheckPlayerCollision(p1, p3, time); //p1 and p3
                p2.CheckPlayerCollision(p2, p3, time); //p2 and p3
            }
            else
            {
                p1.CheckPlayerCollision(p1, p2, time); //p1 and p2
                p1.CheckPlayerCollision(p1, p3, time); //p1 and p3
                p2.CheckPlayerCollision(p2, p3, time); //p2 and p3
                p1.CheckPlayerCollision(p1, p4, time); //p1 and p4
                p2.CheckPlayerCollision(p2, p4, time); //p2 and p4
                p3.CheckPlayerCollision(p3, p4, time); //p3 and p4
            }
        }

    }
}
