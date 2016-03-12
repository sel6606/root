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
        private bool NEEDSCONDITION=false;
        private KeyboardState kbState;
        private KeyboardState previousKbState;
        private Button instructions;
        private Button start;
        private Button quit;
        private Button back;
        private MouseState mState;
        private MouseState previousMState;
        private int buttonWidth;
        private int buttonHeight;
        private int halfScreen;


        //Texture of the menu stuff
        Texture2D menuTex;

        SpriteFont menuFont;

        //Sets the menu stuff texture
        public Texture2D MenuTex
        {
            set { menuTex = value; }
        }

        public SpriteFont MenuFont
        {
            set { menuFont = value; }
        }

        public MenuMan(Game1 game, Texture2D menuTexture)
        {
            buttonWidth = 300;
            buttonHeight = 100;
            halfScreen = (game.GraphicsDevice.Viewport.Width / 2) - (buttonWidth / 2);
            menuTex = menuTexture;
            start = new Button(menuTex, new Rectangle(halfScreen, 50, buttonWidth, buttonHeight));
            instructions = new Button(menuTex, new Rectangle(halfScreen, 200, buttonWidth, buttonHeight));
            quit = new Button(menuTex, new Rectangle(halfScreen, 350, buttonWidth, buttonHeight));
            back = new Button(menuTex, new Rectangle(halfScreen, (game.GraphicsDevice.Viewport.Height - (buttonHeight + 50)), buttonWidth, buttonHeight));
        }

        public void Draw(MenuState currentState, SpriteBatch sb)
        {
            switch (currentState)
            {
                case MenuState.Instructions:

                    back.Draw(sb);
                    break;
                case MenuState.Main: //Priority

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
            kbState = Keyboard.GetState();
            mState = mouseState;
            previousMState = previousMouseState;

            //Switch case for the menu state
            switch (currentState)
            {
                case MenuState.Instructions:
                    if (NEEDSCONDITION || SingleKeyPress(Keys.B))
                    {
                        currentState = MenuState.Main;
                    }
                    break;
                case MenuState.Main:
                    if (instructions.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        currentState = MenuState.Instructions;
                    } else if (start.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
                    {
                        currentState = MenuState.Start;
                    } else if (quit.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
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
