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
        private List<Player> playIndex = new List<Player>(4);
        private int x = 0;
        private int y;
        //Variables for testing purposes
        private Powerup powerP1;
        private Powerup powerP2;
        private Powerup powerP3;
        private Powerup powerP4;
        
        public PowMan(Player p1, Player p2, Player p3, Player p4)
        {
            player1 = p1;
            player2 = p2;
            player3 = p3;
            player4 = p4;
            playIndex.Add(player1);
            playIndex.Add(player2);
            playIndex.Add(player3);
            playIndex.Add(player4);
            #region
            foreach (Player play in playIndex)
            {
                if(0 == (int)play.ThisType)
                {
                    y = 0;
                }
                else if (1 == (int)play.ThisType)
                {
                    y = 1;
                }
                else if ((int)play.ThisType == 2)
                {
                    y = 2;
                }
                else if ((int)play.ThisType == 3)
                {
                    y = 3;
                }
                if(x == 0)
                {
                    if(y == 0)
                    {
                        powerP1 = new GentlePow(play, playIndex);
                    }
                    if(y == 1)
                    {
                        powerP1 = new KnightPow(play, playIndex);
                    }
                    if (y == 2)
                    {
                        powerP1 = new CowPow(play, playIndex);
                    }
                    if (y == 3)
                    {
                        powerP1 = new CavePow(play, playIndex);
                    }
                }
                else if (x == 1)
                {
                    if (y == 0)
                    {
                        powerP2 = new GentlePow(play, playIndex);
                    }
                    if (y == 1)
                    {
                        powerP2 = new KnightPow(play, playIndex);
                    }
                    if (y == 2)
                    {
                        powerP2 = new CowPow(play, playIndex);
                    }
                    if (y == 3)
                    {
                        powerP2 = new CavePow(play, playIndex);
                    }
                }
                else if (x == 2)
                {
                    if (y == 0)
                    {
                        powerP3 = new GentlePow(play, playIndex);
                    }
                    if (y == 1)
                    {
                        powerP3 = new KnightPow(play, playIndex);
                    }
                    if (y == 2)
                    {
                        powerP3 = new CowPow(play, playIndex);
                    }
                    if (y == 3)
                    {
                        powerP3 = new CavePow(play, playIndex);
                    }
                }
                else if (x == 3)
                {
                    if (y == 0)
                    {
                        powerP4 = new GentlePow(play, playIndex);
                    }
                    if (y == 1)
                    {
                        powerP4 = new KnightPow(play, playIndex);
                    }
                    if (y == 2)
                    {
                        powerP4 = new CowPow(play, playIndex);
                    }
                    if (y == 3)
                    {
                        powerP4 = new CavePow(play, playIndex);
                    }
                }
                x++;
                #endregion
            }

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
            if (player3.XBox)
            {
                GamePadState gamePad3 = GamePad.GetState(player3.PlayerNumber);
                if (gamePad3.IsButtonDown(Buttons.X))
                {
                    powerP3.Activate();
                }
            }

            if (player4.XBox)
            {
                GamePadState gamePad4 = GamePad.GetState(player4.PlayerNumber);
                if (gamePad4.IsButtonDown(Buttons.X))
                {
                    powerP4.Activate();
                }
            }
            if (!player3.XBox)
            {
                if (input.IsKeyDown((Keys)player3.use))
                {
                    powerP3.Activate();
                }
            }
            if (!player4.XBox)
            {
                if (input.IsKeyDown((Keys)player4.use))
                {
                    powerP4.Activate();
                }
            }


            powerP1.Update(elapsedTime);
            powerP2.Update(elapsedTime);
            powerP3.Update(elapsedTime);
            powerP4.Update(elapsedTime);
        }
    }
}
