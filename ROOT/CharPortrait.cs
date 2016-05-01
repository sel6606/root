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
        private CharPortrait vertical;
        private int boxNum;
        private CharPortrait left;
        private CharPortrait right;
        private List<Color> colors = new List<Color> { Color.Red, Color.Black, Color.Green, Color.Yellow };
        private List<Rectangle> selectors;
        private List<bool> isSelected;
        private Rectangle position;
        private bool top;
        Texture2D texture;
        Texture2D texture2;

        public List<bool> IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        public int BoxNum
        {
            get { return boxNum; }
        }

        public PlayerType Type
        {
            get { return type; }
            set { type = value; }
        }

        public CharPortrait Vertical
        {
            get { return vertical; }
            set { vertical = value; }
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

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        public CharPortrait(Texture2D texture, Texture2D texture2, Rectangle position, bool top, int number)
        {
            boxNum = number;
            this.position = position;
            this.texture = texture;
            this.texture2 = texture2;
            this.top = top;
            selectors = new List<Rectangle>();
            int width = position.Width / 4;
            int bottom = position.Y + position.Height;
            if (top)
            {
                selectors.Add(new Rectangle(position.X, position.Y - 16, width, 10));
                selectors.Add(new Rectangle(position.X + width, position.Y - 16, width, 10));
                selectors.Add(new Rectangle(position.X + (width * 2), position.Y - 16, width, 10));
                selectors.Add(new Rectangle(position.X + (width * 3), position.Y - 16, width, 10));
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

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
            for (int i = 0; i < selectors.Count; i++)
            {
                if (isSelected[i])
                {
                    if (top)
                    {
                        sb.Draw(texture2, new Vector2(selectors[i].X,selectors[i].Y), colors[i]);
                    }
                    else
                    {
                        sb.Draw(texture2, new Vector2(selectors[i].X, selectors[i].Y), null, colors[i], 0, Vector2.Zero, 1.0f, SpriteEffects.FlipVertically, 0);
                    }
                }
            }
        }
    }
}
