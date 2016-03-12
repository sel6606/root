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

        public MenuMan() { }

        public void Draw(MenuState currentState, SpriteBatch sb)
        {
            switch (currentState)
            {
                case MenuState.Instructions:
                    break;
                case MenuState.Main: //Priority
                    Start = new Button(menuTex, new Rectangle(0, 0, 100, 100));

                    Start.Draw(sb);

                    /*sb.Draw(menuTex, new Rectangle(100, 100, 100, 30), Color.White);
                    sb.DrawString(menuFont, "Start", new Vector2(148, 115), Color.White);
                    sb.Draw(menuTex, new Rectangle(100, 300, 100, 30), Color.White);
                    sb.DrawString(menuFont, "Instructions", new Vector2(148, 315), Color.White);
                    sb.Draw(menuTex, new Rectangle(100, 400, 100, 30), Color.White);
                    sb.DrawString(menuFont, "Quit", new Vector2(148, 415), Color.White);*/
                    break;
                case MenuState.Options: //Unused for now
                    break;
                case MenuState.Controls: //Unused for now
                    break;
            }
        }

        //Will return the next state of the menu
        public MenuState NextState(MenuState currentState)
        {
            //Gets the current state of the keyboard
            kbState = Keyboard.GetState();

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
                    if (NEEDSCONDITION || SingleKeyPress(Keys.I))
                    {
                        currentState = MenuState.Instructions;
                    } else if (NEEDSCONDITION || SingleKeyPress(Keys.S))
                    {
                        currentState = MenuState.Start;
                    } else if (NEEDSCONDITION || SingleKeyPress(Keys.Q))
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

    }
}
