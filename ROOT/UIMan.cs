﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROOT
{
    class UIMan
    {

        //Fields
        private Game1 game;
        private SpriteFont UIFont;
        private Button stop;
        private MouseState mState;
        private MouseState previousMState;

        //Constructor for UIMan
        public UIMan(Game1 game, SpriteFont UIFont, Texture2D stopTexture)
        {
            this.game = game;
            this.UIFont = UIFont;
            stop = new Button(stopTexture, new Rectangle(0, 0, 30, 30));
        }

        //Draw method for the UIMan
        public void Draw(SpriteBatch s, int playerNum)
        {
            if (playerNum == 2)
            {
                #region Two Players

                //Draws the timers
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer1), new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 50, 10), Color.Black);
                s.DrawString(UIFont, "      |      ", new Vector2((game.GraphicsDevice.Viewport.Width / 2), 10), Color.Black);
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer2), new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 50, 10), Color.Black);

                //If player 1's power is ready, draw the icon normally
                if (game.PowerManager.PowerP1.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP1.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 50, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP1.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 50, 35, 30, 30), Color.Gray);
                }

                //If player 2's power is ready, draw the icon normally
                if (game.PowerManager.PowerP2.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP2.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 50, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP2.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 50, 35, 30, 30), Color.Gray);
                }
                #endregion
            }
            else if (playerNum == 3)
            {
                #region Three Players

                //Draws the timers
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer1), new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 100, 10), Color.Black);
                s.DrawString(UIFont, "      |      ", new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 50, 10), Color.Black);
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer2), new Vector2((game.GraphicsDevice.Viewport.Width / 2), 10), Color.Black);
                s.DrawString(UIFont, "      |      ", new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 50, 10), Color.Black);
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer3), new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 100, 10), Color.Black);

                //If player 1's power is ready, draw the icon normally
                if (game.PowerManager.PowerP1.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP1.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 100, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP1.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 100, 35, 30, 30), Color.Gray);
                }

                //If player 2's power is ready, draw the icon normally
                if (game.PowerManager.PowerP2.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP2.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2), 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP2.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2), 35, 30, 30), Color.Gray);
                }

                //If player 3's power is ready, draw the icon normally
                if (game.PowerManager.PowerP3.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP3.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 100, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP3.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 100, 35, 30, 30), Color.Gray);
                }
                #endregion
            }
            else if (playerNum == 4)
            {
                #region Four Players

                //Draw the timers
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer1), new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 150, 10), Color.Black);
                s.DrawString(UIFont, "      |      ", new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 100, 10), Color.Black);
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer2), new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 50, 10), Color.Black);
                s.DrawString(UIFont, "      |      ", new Vector2((game.GraphicsDevice.Viewport.Width / 2), 10), Color.Black);
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer3), new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 50, 10), Color.Black);
                s.DrawString(UIFont, "      |      ", new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 100, 10), Color.Black);
                s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer4), new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 150, 10), Color.Black);

                //If player 1's power is ready, draw the icon normally
                if (game.PowerManager.PowerP1.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP1.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 150, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP1.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 150, 35, 30, 30), Color.Gray);
                }

                //If player 2's power is ready, draw the icon normally
                if (game.PowerManager.PowerP2.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP2.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 50, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP2.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) - 50, 35, 30, 30), Color.Gray);
                }

                //If player 3's power is ready, draw the icon normally
                if (game.PowerManager.PowerP3.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP3.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 50, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP3.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 50, 35, 30, 30), Color.Gray);
                }

                //If player 4's power is ready, draw the icon normally
                if (game.PowerManager.PowerP4.IsReady)
                {
                    s.Draw(game.PowerManager.PowerP4.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 150, 35, 30, 30), Color.White);
                }
                else //Otherwise draw the icon grayed out
                {
                    s.Draw(game.PowerManager.PowerP4.Tex, new Rectangle((game.GraphicsDevice.Viewport.Width / 2) + 150, 35, 30, 30), Color.Gray);
                }
                #endregion
            }


            stop.Draw(s);
        }

        //Returns true if the "return to menu" (x) button has been clicked
        public bool CheckExit(MouseState mouseState, MouseState previousMouseState)
        {
            mState = mouseState;
            previousMState = previousMouseState;
            if (stop.MouseHovering(mState.X, mState.Y) && SingleMouseClick())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Checks to see if the left mouse button was clicked exactly once
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
