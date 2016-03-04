using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class Tile : GameObject
    {
        public Tile(int x, int y, int width, int height)
            : base(x,y,width,height,true)
        {

        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);
        }
    }
}
