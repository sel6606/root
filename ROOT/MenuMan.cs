﻿
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ROOT
{
    class MenuMan
    {
        //Input states
        private KeyboardState kbState;
        private KeyboardState previousKbState;
        private MouseState mState;
        private MouseState previousMState;

        #region Buttons
        private Button instructions;
        private Button start;
        private Button quit;
        private Button back;
        private Button creditButton;
        private Button pNum2;
        private Button pNum3;
        private Button pNum4;
        private Button selectBack;
        private Button play;
        private Button options;

        //Width of each button
        private int buttonWidth;

        //Height of each button    
        private int buttonHeight;

        #endregion

        //Select screen fields
        private CharPortrait portrait1;
        private CharPortrait portrait2;
        private CharPortrait portrait3;
        private CharPortrait portrait4;
        private List<CharPortrait> portraits;

        private int selectButtonWidth;
        private int selectButtonHeight;
        private int selectHalfScreen;
        private int portraitWidth;
        private int portraitHeight;
        private int portraitHalfScreen;


        //Other fields
        private Game1 game;
        private List<PlayerType> types;
        private SoundEffect clickSound;

        //Finds half of the screen's width to help center the buttons   
        private int halfScreen;
        private int fullScreen;

        #region Textures

        //Textures of the buttons
        Texture2D startButton;
        Texture2D instructionsButton;
        Texture2D quitButton;
        Texture2D backButton;
        Texture2D badInstructions;
        Texture2D creditScreen;
        Texture2D creditTexture;
        Texture2D select;

        //Selection screen textures
        Texture2D caveInfo;
        Texture2D gInfo;
        Texture2D kInfo;
        Texture2D cowInfo;

        Texture2D menuBg;

        List<Texture2D> info;
        List<Texture2D> portraitTexture;


        SpriteFont menuFont;

        #endregion

        //Properties for types
        public List<PlayerType> Types
        {
            get { return types; }
        }

        //Properties for menuFont
        public SpriteFont MenuFont
        {
            set { menuFont = value; }
        }

        //Constructor for MenuMan
        public MenuMan(Game1 game, Texture2D startTexture,
            Texture2D instructionsTexture, Texture2D quitTexture,
            Texture2D backTexture, Texture2D optionsTexture, Texture2D instructionScreen, Texture2D creditsButton, Texture2D credits, Texture2D cavemanInfo,
            Texture2D cowboyInfo, Texture2D knightInfo, Texture2D gentlemanInfo, Texture2D background,
            List<Texture2D> portraitTexture, List<Texture2D> playerButtons, Texture2D select, SoundEffect click, int playerNum)
        {
            this.game = game;
            types = new List<PlayerType> { PlayerType.Caveman, PlayerType.Caveman, PlayerType.Caveman, PlayerType.Caveman };
            buttonWidth = 300;
            buttonHeight = 100;
            selectButtonWidth = 200;
            selectButtonHeight = 66;
            portraitWidth = 66;
            portraitHeight = 66;
            this.portraitTexture = portraitTexture;
            this.select = select;
            cowInfo = cowboyInfo;
            kInfo = knightInfo;
            gInfo = gentlemanInfo;
            caveInfo = cavemanInfo;
            creditTexture = creditsButton;
            creditScreen = credits;
            //Sets halfScreen equal to half of the screen minus half the width of each button
            //so the center of the button will be in the center of the screen
            halfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            fullScreen = (game.GraphicsDevice.Viewport.Width);
            selectHalfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (selectButtonWidth / 2);
            portraitHalfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (portraitWidth / 2);
            startButton = startTexture;
            instructionsButton = instructionsTexture;
            quitButton = quitTexture;
            backButton = backTexture;
            clickSound = click;
            badInstructions = instructionScreen;
            menuBg = background;


            //First button on main menu screen
            start = new Button(startButton, new Rectangle(halfScreen, 25, buttonWidth, buttonHeight));
            //Second button on main menu screen            
            instructions = new Button(instructionsButton, new Rectangle(halfScreen, 141, buttonWidth, buttonHeight));
            //Third button on main menu screen
            creditButton = new Button(creditTexture, new Rectangle(halfScreen, 257, buttonWidth, buttonHeight));
            //Fourth button on main menu screen      
            quit = new Button(quitButton, new Rectangle(halfScreen, 373, buttonWidth, buttonHeight));
            //Only button on instructions menu              
            back = new Button(backTexture, new Rectangle(halfScreen, (game.GraphicsDevice.Viewport.Height - (buttonHeight + 50)), buttonWidth, buttonHeight));
            //Play button on character select screen
            play = new Button(startTexture, new Rectangle(selectHalfScreen, (game.GraphicsDevice.Viewport.Height - (selectButtonHeight + 10)), selectButtonWidth, selectButtonHeight));
            //Options button on character select screen
            options = new Button(optionsTexture, new Rectangle(selectHalfScreen, 10, selectButtonWidth, selectButtonHeight));
            //Back button on character select screen
            selectBack = new Button(backTexture, new Rectangle(selectHalfScreen, (game.GraphicsDevice.Viewport.Height - ((2 * selectButtonHeight) + 10)), selectButtonWidth, selectButtonHeight));

            pNum2 = new Button(playerButtons[0], new Rectangle(halfScreen, 25, buttonWidth, buttonHeight));
            //Second button on main menu screen            
            pNum3 = new Button(playerButtons[1], new Rectangle(halfScreen, 125, buttonWidth, buttonHeight));
            //Third button on main menu screen
            pNum4 = new Button(playerButtons[2], new Rectangle(halfScreen, 225, buttonWidth, buttonHeight));

            //Reset character portraits
            ResetPortraits(playerNum);

            info = new List<Texture2D> { cavemanInfo, cavemanInfo, cavemanInfo, cavemanInfo };

        }


        //Resets the portraits on the select screen
        public void ResetPortraits(int playerNum)
        {
            portrait1 = new CharPortrait(portraitTexture[0], select, new Rectangle(portraitHalfScreen - (10 + (portraitWidth/2)), 240 - (portraitHeight + 10), portraitWidth, portraitHeight), true, 1, playerNum);
            portrait2 = new CharPortrait(portraitTexture[1], select, new Rectangle(portraitHalfScreen + (portraitWidth/2), 240 - (portraitHeight + 10), portraitWidth, portraitHeight), true, 2, playerNum);
            portrait3 = new CharPortrait(portraitTexture[2], select, new Rectangle(portraitHalfScreen - (10 + (portraitWidth/2)), 250, portraitWidth, portraitHeight), false, 3, playerNum);
            portrait4 = new CharPortrait(portraitTexture[3], select, new Rectangle(portraitHalfScreen + (portraitWidth/2), 250, portraitWidth, portraitHeight), false, 4, playerNum);

            SetNeighbors();

            portrait1.IsSelected = new List<bool> { true, true, true, true };
            portraits = new List<CharPortrait>();
            portraits.Add(portrait1);
            portraits.Add(portrait2);
            portraits.Add(portrait3);
            portraits.Add(portrait4);
        }


        //Sets up the neighbors for each portrait
        public void SetNeighbors()
        {
            //Portrait 1
            portrait1.Vertical = portrait3;
            portrait1.Left = portrait2;
            portrait1.Right = portrait2;
            portrait1.Type = PlayerType.Caveman;

            //Portrait 2
            portrait2.Vertical = portrait4;
            portrait2.Left = portrait1;
            portrait2.Right = portrait1;
            portrait2.Type = PlayerType.Cowboy;

            //Portrait 3
            portrait3.Vertical = portrait1;
            portrait3.Left = portrait4;
            portrait3.Right = portrait4;
            portrait3.Type = PlayerType.Knight;

            //Portrait 4
            portrait4.Vertical = portrait2;
            portrait4.Left = portrait3;
            portrait4.Right = portrait3;
            portrait4.Type = PlayerType.GentleMan;

        }

        //Draw method for MenuMan
        public void Draw(MenuState currentState, SpriteBatch sb)
        {
            switch (currentState)
            {
                case MenuState.Title: //Title screen when you start the game
                    sb.Draw(menuBg, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
                    sb.Draw(game.Title, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
                    break;
                case MenuState.Instructions: //Displays instructions screen
                    sb.Draw(badInstructions, new Rectangle(0, 0, 800, 480), Color.White);
                    back.Draw(sb);
                    break;
                case MenuState.Main:  //Displays main menu screen
                    sb.Draw(menuBg, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
                    start.Draw(sb);
                    instructions.Draw(sb);
                    creditButton.Draw(sb);
                    quit.Draw(sb);
                    break;
                case MenuState.Selection: //Displays selection screen
                    play.Draw(sb);
                    selectBack.Draw(sb);
                    options.Draw(sb);
                    sb.Draw(info[0], new Rectangle(10, 10, 200, 220), Color.White);
                    sb.Draw(info[1], new Rectangle(10, 250, 200, 220), Color.White);
                    sb.Draw(info[2], new Rectangle(fullScreen - 210, 10, 200, 220), Color.White);
                    sb.Draw(info[3], new Rectangle(fullScreen - 210, 250, 200, 220), Color.White);
                    portrait1.Draw(sb);
                    portrait2.Draw(sb);
                    portrait3.Draw(sb);
                    portrait4.Draw(sb);
                    break;
                case MenuState.Options: //Displays options screen
                    sb.Draw(menuBg, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
                    pNum2.Draw(sb);
                    pNum3.Draw(sb);
                    pNum4.Draw(sb);
                    back.Draw(sb);
                    break;
                case MenuState.Credits: //Displays credits screen
                    sb.GraphicsDevice.Clear(Color.Black);
                    sb.Draw(creditScreen, new Rectangle(0, 0, 800, 360), Color.White);
                    back.Draw(sb);
                    break;
            }
        }

        //Will return the next state of the menu
        public MenuState NextState(MenuState currentState, MouseState mouseState, MouseState previousMouseState)
        {
            //Gets the current state of the keyboard
            //Gets the current and previous state of the mouse
            kbState = Keyboard.GetState();
            mState = mouseState;
            previousMState = previousMouseState;

            //Switch case for the menu state
            switch (currentState)
            {
                case MenuState.Title: //If the menu is on the instructions screen
                    if (SingleMouseClick() || kbState.GetPressedKeys().Length > 0)
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Main;
                    }
                    break;
                case MenuState.Instructions: //If the menu is on the instructions screen
                    if (back.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Main;
                    }
                    break;
                case MenuState.Main: //If you are at the main menu
                    if (instructions.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Instructions;
                    }
                    else if (start.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        ResetPortraits(4);
                        //Change this once selection screen is implemented
                        currentState = MenuState.Selection;
                    }
                    else if (quit.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Quit;
                    }
                    else if (creditButton.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Credits;
                    }
                    break;
                case MenuState.Selection: //If you are on the selection screen
                    if (play.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Start;
                    }
                    else if (options.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Options;
                    }
                    else if (selectBack.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Main;
                    }
                    break;
                case MenuState.Options: //If you are on the options screen
                    CheckOptions();
                    if (back.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Selection;
                    }
                    break;
                case MenuState.Credits: //If you are on the credits screen
                    if (back.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Main;
                    }
                    break;
            }
            previousKbState = kbState;
            return currentState;
        }

        //Checks to see if any buttons on the options screen were pressed
        public void CheckOptions()
        {
            //If any of the buttons were pressed, set the player number appropriately and reset the portraits

            if (pNum2.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
            {
                clickSound.CreateInstance().Play();
                game.PlayerNum = 2;
                ResetPortraits(2);
            }
            else if (pNum3.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
            {
                clickSound.CreateInstance().Play();
                game.PlayerNum = 3;
                ResetPortraits(3);
            }
            else if (pNum4.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
            {
                clickSound.CreateInstance().Play();
                game.PlayerNum = 4;
                ResetPortraits(4);
            }
        }

        //Checks to see if there was a single click of the left mouse button
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

        //Method for the selection screen that takes the number of players, and the players themselves
        //Players 3 and 4 are optional parameters that don't have to be passed in
        public void SelectionState(int playerNum, Player p1, Player p2, Player p3 = null, Player p4 = null)
        {
            switch (playerNum)
            {
                case 2:
                    UpdateSelected(p1, 1);
                    UpdateSelected(p2, 2);
                    break;
                case 3:
                    UpdateSelected(p1, 1);
                    UpdateSelected(p2, 2);
                    UpdateSelected(p3, 3);
                    break;
                case 4:
                    UpdateSelected(p1, 1);
                    UpdateSelected(p2, 2);
                    UpdateSelected(p3, 3);
                    UpdateSelected(p4, 4);
                    break;
                default:
                    break;
            }
        }

        //Helper method for SelectionState
        private void UpdateSelected(Player player, int playerNumber)
        {
            CharPortrait current = null;
            player.UpdateSelect();
            for (int i = 0; i < portraits.Count; i++)
            {
                if (portraits[i].IsSelected[playerNumber - 1])
                {
                    current = portraits[i];
                }
            }
            if (player.SelectLeft)
            {
                current.IsSelected[playerNumber - 1] = false;
                current = current.Left;
                current.IsSelected[playerNumber - 1] = true;

            }
            else if (player.SelectRight)
            {
                current.IsSelected[playerNumber - 1] = false;
                current = current.Right;
                current.IsSelected[playerNumber - 1] = true;
            }
            else if (player.SelectUp || player.SelectDown)
            {
                current.IsSelected[playerNumber - 1] = false;
                current = current.Vertical;
                current.IsSelected[playerNumber - 1] = true;
            }

            switch (current.Type)
            {
                case PlayerType.Caveman:
                    info[playerNumber - 1] = caveInfo;
                    break;
                case PlayerType.Cowboy:
                    info[playerNumber - 1] = cowInfo;
                    break;
                case PlayerType.GentleMan:
                    info[playerNumber - 1] = gInfo;
                    break;
                case PlayerType.Knight:
                    info[playerNumber - 1] = kInfo;
                    break;
                default:
                    break;
            }
            types[playerNumber - 1] = current.Type;
        }

    }
}
