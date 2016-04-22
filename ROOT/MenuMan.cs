using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private MouseState mState;
        private MouseState previousMState;

        //Width of each button
        private int buttonWidth;
        //Height of each button    
        private int buttonHeight;
        //Finds half of the screen's width to help center the buttons   
        private int halfScreen;


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
        public MenuMan(Game1 game, Texture2D startTexture, Texture2D instructionsTexture, Texture2D quitTexture, Texture2D backTexture)
        {
            buttonWidth = 300;
            buttonHeight = 100;
            //Sets halfScreen equal to half of the screen minus half the width of each button
            //so the center of the button will be in the center of the screen
            halfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            startButton = startTexture;
            instructionsButton = instructionsTexture;
            quitButton = quitTexture;
            backButton = backTexture;

            //First button on main menu screen
            start = new Button(startButton, new Rectangle(halfScreen, 50, buttonWidth, buttonHeight));
            //Second button on main menu screen            
            instructions = new Button(instructionsButton, new Rectangle(halfScreen, 200, buttonWidth, buttonHeight));
            //Third button on main menu screen      
            quit = new Button(quitButton, new Rectangle(halfScreen, 350, buttonWidth, buttonHeight));
            //Only button on instructions menu              
            back = new Button(backTexture, new Rectangle(halfScreen, (game.GraphicsDevice.Viewport.Height - (buttonHeight + 50)), buttonWidth, buttonHeight));
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
                        currentState = MenuState.Main;
                    }
                    break;
                case MenuState.Main: //If you are at the main menu
                    if (instructions.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        currentState = MenuState.Instructions;
                    }
                    else if (start.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        currentState = MenuState.Start;
                    }
                    else if (quit.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        currentState = MenuState.Quit;
                    }
                    break;
                case MenuState.Options: //Unused for now
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

    }
}
