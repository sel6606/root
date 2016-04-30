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
        private bool NEEDSCONDITION = false;
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


        //Textures of the buttons
        Texture2D startButton;
        Texture2D instructionsButton;
        Texture2D quitButton;
        Texture2D backButton;

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
        public MenuMan(Game1 game, Texture2D startTexture, Texture2D instructionsTexture, Texture2D quitTexture, Texture2D backTexture, SoundEffect click)
        {
            buttonWidth = 300;
            buttonHeight = 100;
            selectButtonWidth = 200;
            selectButtonHeight = 66;
            //Sets halfScreen equal to half of the screen minus half the width of each button
            //so the center of the button will be in the center of the screen
            halfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            fullScreen = (game.GraphicsDevice.Viewport.Width);
            selectHalfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (selectButtonWidth / 2);
            startButton = startTexture;
            instructionsButton = instructionsTexture;
            quitButton = quitTexture;
            backButton = backTexture;
            clickSound = click;

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
        }

        public void Draw(MenuState currentState, SpriteBatch sb)
        {
            switch (currentState)
            {
                case MenuState.Instructions: //Displays instructions screen
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
                    sb.Draw(backButton, new Rectangle(10, 10, 200, 220), Color.White);
                    sb.Draw(backButton, new Rectangle(10, 250, 200, 220), Color.White);
                    sb.Draw(backButton, new Rectangle(fullScreen - 210, 10, 200, 220), Color.White);
                    sb.Draw(backButton, new Rectangle(fullScreen - 210, 250, 200, 220), Color.White);
                    break;
                case MenuState.Options: //Unused for now
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
                    UpdateSelected(p1);
                    UpdateSelected(p2);
                    break;
                case 3:
                    UpdateSelected(p1);
                    UpdateSelected(p2);
                    UpdateSelected(p3);
                    break;
                case 4:
                    UpdateSelected(p1);
                    UpdateSelected(p2);
                    UpdateSelected(p3);
                    UpdateSelected(p4);
                    break;
                default:
                    break;
            }
        }

        //Helper method for SelectionState
        private void UpdateSelected(Player player)
        {
            player.UpdateSelect();
            if (player.SelectLeft)
            {
                //Do something
            }else if (player.SelectRight)
            {
                //Do something
            }else if (player.SelectUp)
            {
                //Do something
            }else if (player.SelectDown)
            {
                //Do something
            }

        }

    }
}
