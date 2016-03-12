using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROOT
{
    class UIMan
    {
        Game1 game;
        SpriteFont UIFont;
        Button stop;

        public UIMan(Game1 game, SpriteFont UIFont, Texture2D stopTexture)
        {
            this.game = game;
            this.UIFont = UIFont;
            stop = new Button(stopTexture, new Rectangle(0, 0, 30, 30));
        }

        public void Draw(SpriteBatch s)
        {
            s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer1) + "      |      " + String.Format("{0:0.00}", game.Timer2), new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 50, 30), Color.White);
            stop.Draw(s);
        }
    }
}
