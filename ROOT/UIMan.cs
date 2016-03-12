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

        public UIMan(Game1 game, SpriteFont UIFont)
        {
            this.game = game;
            this.UIFont = UIFont;
        }

        public void Draw(SpriteBatch s)
        {
            s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer1), new Vector2((game.GraphicsDevice.Viewport.Width / 2) - 20, 50), Color.White);
            s.DrawString(UIFont, String.Format("{0:0.00}", game.Timer2), new Vector2((game.GraphicsDevice.Viewport.Width / 2) + 20, 50), Color.White);
        }
    }
}
