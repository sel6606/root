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
        //SpriteBatch needed for drawing the menu stuff in the draw method
        //Will be the SpriteBatch defined in Game1
        SpriteBatch sb;

        //Texture of the menu stuff
        Texture2D menuTex;

        //Sets the SpriteBatch
        public SpriteBatch SB
        {
            set { sb = value; }
        }

        //Sets the menu stuff texture
        public Texture2D MenuTex
        {
            set { menuTex = value; }
        }

        public MenuMan() { }

        public void Draw(MenuState currentState)
        {
            switch (currentState)
            {
                case MenuState.Instructions:
                    break;
                case MenuState.Main: //Priority
                    //sb.Draw(menuTex, new Rectangle(10,10,10,10))
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
            //Switch case for the menu state
            switch (currentState)
            {
                case MenuState.Start:
                    break;
                case MenuState.Instructions:
                    break;
                case MenuState.Main:
                    break;
                case MenuState.Options: //Unused for now
                    break;
                case MenuState.Controls: //Unused for now
                    break;
                case MenuState.Quit:
                    break;
            }
            return currentState;
        }

    }
}
