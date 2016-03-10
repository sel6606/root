using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class Orb : GameObject
    {
        private bool active;

        public bool Active { set { active = value; } }

        public Orb(int x, int y, int width, int height, Texture2D texture)
            : base(x,y,width,height,false, texture)
        {

        }

        public override void Draw(SpriteBatch s)
        {
            while(active)
            {
                base.Draw(s);
            }
        }

    }
}
