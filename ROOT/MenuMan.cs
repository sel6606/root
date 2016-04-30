using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ROOT
{
    class MenuMan
    {
        private KeyboardState kbState;
        private KeyboardState previousKbState;
        private Button instructions;
        private Button start;
        private Button quit;
        private Button back;

        //Use these variables for the character selection
        private Button play;
        private Button options;

        private MouseState mState;
        private MouseState previousMState;
        private SoundEffect clickSound;
        private CharPortrait portrait1;
        private CharPortrait portrait2;
        private CharPortrait portrait3;
        private CharPortrait portrait4;
        private CharPortrait portrait5;
        private CharPortrait portrait6;
        private List<CharPortrait> portraits;

        //Width of each button
        private int buttonWidth;
        //Height of each button    
        private int buttonHeight;
        //Finds half of the screen's width to help center the buttons   
        private int halfScreen;

        private int fullScreen;

        private int selectButtonWidth;

        private int selectButtonHeight;

        private int selectHalfScreen;

        private int portraitWidth;

        private int portraitHeight;

        private int portraitHalfScreen;


        //Textures of the buttons
        Texture2D startButton;
        Texture2D instructionsButton;
        Texture2D quitButton;
        Texture2D backButton;
        Texture2D badInstructions;

        //Selection screen textures
        Texture2D caveInfo;
        Texture2D gInfo;
        Texture2D kInfo;
        Texture2D cowInfo;
        List<Texture2D> info;

        SpriteFont menuFont;

        //Sets the menu stuff texture
        /*public Texture2D MenuTex
        {
            set { menuTex = value; }
        }*/

        public SpriteFont MenuFont
        {
            set { menuFont = value; }
        }

        //Constructor for MenuMan
        public MenuMan(Game1 game, Texture2D startTexture, 
            Texture2D instructionsTexture, Texture2D quitTexture, 
            Texture2D backTexture, Texture2D instructionScreen, Texture2D cavemanInfo,
            Texture2D cowboyInfo,Texture2D knightInfo,Texture2D gentlemanInfo, SoundEffect click)
        {
            buttonWidth = 300;
            buttonHeight = 100;
            selectButtonWidth = 200;
            selectButtonHeight = 66;
            portraitWidth = 66;
            portraitHeight = 66;
            cowInfo = cowboyInfo;
            kInfo = knightInfo;
            gInfo = gentlemanInfo;
            caveInfo = cavemanInfo;
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

            //First button on main menu screen
            start = new Button(startButton, new Rectangle(halfScreen, 50, buttonWidth, buttonHeight));
            //Second button on main menu screen            
            instructions = new Button(instructionsButton, new Rectangle(halfScreen, 200, buttonWidth, buttonHeight));
            //Third button on main menu screen      
            quit = new Button(quitButton, new Rectangle(halfScreen, 350, buttonWidth, buttonHeight));
            //Only button on instructions menu              
            back = new Button(backTexture, new Rectangle(halfScreen, (game.GraphicsDevice.Viewport.Height - (buttonHeight + 50)), buttonWidth, buttonHeight));
            //Play button on character select screen
            play = new Button(backTexture, new Rectangle(selectHalfScreen, (game.GraphicsDevice.Viewport.Height - (selectButtonHeight + 10)), selectButtonWidth, selectButtonHeight));
            //Options button on character select screen
            options = new Button(backTexture, new Rectangle(selectHalfScreen, 10, selectButtonWidth, selectButtonHeight));
            //First Character portrait
            portrait1 = new CharPortrait(startButton, new Rectangle(portraitHalfScreen - (10 + portraitWidth), 240 - (portraitHeight + 10), portraitWidth, portraitHeight), true,1);
            portrait2 = new CharPortrait(startButton, new Rectangle(portraitHalfScreen, 240 - (portraitHeight + 10), portraitWidth, portraitHeight), true,2);
            portrait3 = new CharPortrait(startButton, new Rectangle(portraitHalfScreen + ((portraitWidth) + 10), 240 - (portraitHeight + 10), portraitWidth, portraitHeight), true,3);
            portrait4 = new CharPortrait(startButton, new Rectangle(portraitHalfScreen - (10 + portraitWidth), 240 + 10, portraitWidth, portraitHeight), false,4);
            portrait5 = new CharPortrait(startButton, new Rectangle(portraitHalfScreen, 240 + 10, portraitWidth, portraitHeight), false,5);
            portrait6 = new CharPortrait(startButton, new Rectangle(portraitHalfScreen + ((portraitWidth) + 10), 240 + 10, portraitWidth, portraitHeight), false,6);
            SetNeighbors();
            portrait1.IsSelected = new List<bool> { true, true, true, true };
            portraits = new List<CharPortrait>();
            info = new List<Texture2D> { cavemanInfo, cavemanInfo, cavemanInfo, cavemanInfo };
            portraits.Add(portrait1);
            portraits.Add(portrait2);
            portraits.Add(portrait3);
            portraits.Add(portrait4);
            portraits.Add(portrait5);
            portraits.Add(portrait6);
        }

        public void SetNeighbors()
        {
            //Portrait 1
            portrait1.Vertical = portrait4;
            portrait1.Left = portrait3;
            portrait1.Right = portrait2;
            portrait1.Type = PlayerType.Caveman;

            //Portrait 2
            portrait2.Vertical = portrait5;
            portrait2.Left = portrait1;
            portrait2.Right = portrait3;
            portrait2.Type = PlayerType.Cowboy;

            //Portrait 3
            portrait3.Vertical = portrait6;
            portrait3.Left = portrait2;
            portrait3.Right = portrait1;
            portrait3.Type = PlayerType.Knight;

            //Portrait 4
            portrait4.Vertical = portrait1;
            portrait4.Left = portrait6;
            portrait4.Right = portrait5;
            portrait4.Type = PlayerType.GentleMan;

            //Portrait 5
            portrait5.Vertical = portrait2;
            portrait5.Left = portrait4;
            portrait5.Right = portrait6;
            portrait5.Type = PlayerType.Caveman;

            //Portrait 6
            portrait6.Vertical = portrait3;
            portrait6.Left = portrait5;
            portrait6.Right = portrait4;
            portrait6.Type = PlayerType.Caveman;
        }

        public void Draw(MenuState currentState, SpriteBatch sb)
        {
            switch (currentState)
            {
                case MenuState.Instructions: //Displays instructions screen
                    sb.Draw(badInstructions, new Rectangle(0, 0, 800, 480), Color.White);
                    back.Draw(sb);
                    break;
                case MenuState.Main:  //Displays main menu screen
                    start.Draw(sb);
                    instructions.Draw(sb);
                    quit.Draw(sb);
                    break;
                case MenuState.Selection:
                    play.Draw(sb);
                    options.Draw(sb);
                    sb.Draw(info[0], new Rectangle(10, 10, 200, 220), Color.White);
                    sb.Draw(info[1], new Rectangle(10, 250, 200, 220), Color.White);
                    sb.Draw(info[2], new Rectangle(fullScreen - 210, 10, 200, 220), Color.White);
                    sb.Draw(info[3], new Rectangle(fullScreen - 210, 250, 200, 220), Color.White);
                    portrait1.Draw(sb);
                    portrait2.Draw(sb);
                    portrait3.Draw(sb);
                    portrait4.Draw(sb);
                    portrait5.Draw(sb);
                    portrait6.Draw(sb);
                    break;
                case MenuState.Options:
                    back.Draw(sb);
                    break;
                case MenuState.Controls: //Unused for now
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

                        //Change this once selection screen is implemented
                        currentState = MenuState.Selection;
                    }
                    else if (quit.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Quit;
                    }
                    break;
                case MenuState.Selection:
                    if(play.MouseHovering(mState.X,mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Start;
                    }else if (options.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Options;
                    }
                    break;
                case MenuState.Options: //Unused for now
                    if (back.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        clickSound.CreateInstance().Play();
                        currentState = MenuState.Selection;
                    }
                    break;
                case MenuState.Controls: //Unused for now
                    break;
            }
            previousKbState = kbState;
            return currentState;
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
            CharPortrait current=null;
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
                current.IsSelected[playerNumber - 1 ] = true;

            }else if (player.SelectRight)
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
        }

    }
}
