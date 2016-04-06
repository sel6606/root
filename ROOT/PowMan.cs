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

        //Variables for testing purposes
        private DefaultPower powerP1;
        private DefaultPower powerP2;

        public PowMan(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
            powerP1 = new DefaultPower(player1);
            powerP2 = new DefaultPower(player2);
        }

        public void Update(double elapsedTime)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown((Keys)player1.use))
            {
                powerP1.Activate();
            }
            if (input.IsKeyDown((Keys)player2.use))
            {
                powerP2.Activate();
            }

            powerP1.Update(elapsedTime);
            powerP2.Update(elapsedTime);
        }
    }
}
