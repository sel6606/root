using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class PowMan
    {
        private Player player1;
        private Player player2;
        private Player player3;
        private Player player4;

        //Variables for testing purposes
        private DefaultPower powerP1;
        private DefaultPower powerP2;
        private DefaultPower powerP3;
        private DefaultPower powerP4;

        public PowMan(Player p1, Player p2, Player p3, Player p4)
        {
            player1 = p1;
            player2 = p2;
            player3 = p3;
            player4 = p4;
            powerP1 = new DefaultPower(player1);
            powerP2 = new DefaultPower(player2);
            powerP3 = new DefaultPower(player3);
            powerP4 = new DefaultPower(player4);
        }


        //updates all powerups.
        public void Update(double elapsedTime)
        {
            KeyboardState input = Keyboard.GetState();
            if (player1.XBox)
            {
                GamePadState gamePad1 = GamePad.GetState(player1.PlayerNumber);
                if (gamePad1.IsButtonDown(Buttons.X))
                {
                    powerP1.Activate();
                }
            }

            if (player2.XBox)
            {
                GamePadState gamePad2 = GamePad.GetState(player2.PlayerNumber);
                if (gamePad2.IsButtonDown(Buttons.X))
                {
                    powerP2.Activate();
                }
            }

            if (!player1.XBox)
            {
                if (input.IsKeyDown((Keys)player1.use))
                {
                    powerP1.Activate();
                }
            }
            if (!player2.XBox)
            {
                if (input.IsKeyDown((Keys)player2.use))
                {
                    powerP2.Activate();
                }
            }

            powerP1.Update(elapsedTime);
            powerP2.Update(elapsedTime);
        }
    }
}
