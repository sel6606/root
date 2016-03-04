using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class MenuMan
    {
        public MenuMan() { }

        public void Draw(MenuState currentState)
        {
            switch (currentState)
            {
                case MenuState.Instructions:
                    break;
                case MenuState.Main: //Priority
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
