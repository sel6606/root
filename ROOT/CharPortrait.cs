using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class CharPortrait
    {
        private PlayerType type;
        private CharPortrait above;
        private CharPortrait below;
        private CharPortrait left;
        private CharPortrait right;
        private List<Rectangle> selectors;
        private List<bool> isSelected;

        public PlayerType Type
        {
            get { return type; }
            set { type = value; }
        }

        public CharPortrait Above
        {
            get { return above; }
            set { above = value; }
        }

        public CharPortrait Below
        {
            get { return below; }
            set { below = value; }
        }

        public CharPortrait Left
        {
            get { return left; }
            set { left = value; }
        }

        public CharPortrait Right
        {
            get { return right; }
            set { right = value; }
        }

        public CharPortrait(Texture2D texture, Rectangle position, bool top)
        {

            int width = position.Width / 4;
            int bottom = position.Y + position.Height + 10;
            if (top)
            {
                selectors.Add(new Rectangle(position.X, position.Y, width, 10));
                selectors.Add(new Rectangle(position.X + width, position.Y, width, 10));
                selectors.Add(new Rectangle(position.X + (width*2), position.Y, width, 10));
                selectors.Add(new Rectangle(position.X + (width*3), position.Y, width, 10));
            }
            else
            {
                selectors.Add(new Rectangle(position.X, bottom, width, 10));
                selectors.Add(new Rectangle(position.X + width, bottom, width, 10));
                selectors.Add(new Rectangle(position.X + (width * 2), bottom, width, 10));
                selectors.Add(new Rectangle(position.X + (width * 3), bottom, width, 10));
            }

            isSelected = new List<bool>() { false, false, false, false }; 
            }

    }
}
