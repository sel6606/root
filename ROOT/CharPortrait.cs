using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ROOT
{
    class CharPortrait
    {
        #region Fields
        private PlayerType type;
        private Rectangle position;
        private bool top;
        private int boxNum;

        //Variables to hold the CharPortraits in each direction
        private CharPortrait vertical;
        private CharPortrait left;
        private CharPortrait right;

        //List of colors that correspond to each player
        private List<Color> colors = new List<Color> { Color.Red, Color.Black, Color.Green, Color.Yellow };

        //List of selectors for each player
        private List<Rectangle> selectors;

        //List of booleans that tells you if the corresponding selector has selected this box
        private List<bool> isSelected;

        //Textures for the portrait and the selectors
        Texture2D texture;
        Texture2D texture2;
        #endregion

        #region Properties

        //Properties for isSelected
        public List<bool> IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        //Properties for type
        public PlayerType Type
        {
            get { return type; }
            set { type = value; }
        }

        //Properties for vertical
        public CharPortrait Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }

        //Properties for left
        public CharPortrait Left
        {
            get { return left; }
            set { left = value; }
        }

        //Properties for right
        public CharPortrait Right
        {
            get { return right; }
            set { right = value; }
        }

        #endregion


        //Constructor for CharPortrait
        public CharPortrait(Texture2D texture, Texture2D texture2, Rectangle position, bool top, int number, int playerNum)
        {
            boxNum = number;
            this.position = position;
            this.texture = texture;
            this.texture2 = texture2;
            this.top = top;
            selectors = new List<Rectangle>();

            //Width of each selector
            int width = position.Width / 4;
            //Y position for selectors on the bottom of the portrait
            int bottom = position.Y + position.Height;

            #region Selector Setup
            //If CharPortrait is on the top row
            if (top)
            {
                //Add selectors for player 1 and player 2
                selectors.Add(new Rectangle(position.X, position.Y - 16, width, 10));
                selectors.Add(new Rectangle(position.X + width, position.Y - 16, width, 10));
                if (playerNum > 2)
                { //If there is a third player, add the selector for it
                    selectors.Add(new Rectangle(position.X + (width * 2), position.Y - 16, width, 10));
                    if (playerNum > 3)
                    { //If there is a fourth player, add the selector for it
                        selectors.Add(new Rectangle(position.X + (width * 3), position.Y - 16, width, 10));
                    }

                }
            }
            else
            { //If CharPortrait is on the bottom row

                //Add selectors for player 1 and player 2
                selectors.Add(new Rectangle(position.X, bottom, width, 10));
                selectors.Add(new Rectangle(position.X + width, bottom, width, 10));

                if (playerNum > 2)
                { //If there is a third player, add the selector for it
                    selectors.Add(new Rectangle(position.X + (width * 2), bottom, width, 10));
                    if (playerNum > 3)
                    { //If there is a fourth player, add the selector for it
                        selectors.Add(new Rectangle(position.X + (width * 3), bottom, width, 10));
                    }
                }
            }

            //Initial values for isSelected
            isSelected = new List<bool>() { false, false, false, false };
            #endregion
        }

        //Draw method for the CharPortrait
        public void Draw(SpriteBatch sb)
        {
            //Draws the portrait
            sb.Draw(texture, position, Color.White);

            //Loops through the list of selectors
            for (int i = 0; i < selectors.Count; i++)
            {
                //If the portrait is selected by the current selector, draw it
                if (isSelected[i])
                {
                    if (top)
                    {
                        sb.Draw(texture2, new Vector2(selectors[i].X, selectors[i].Y), colors[i]);
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
