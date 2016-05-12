
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    class Button
    {
        //Texture of the button
        private Texture2D buttonTexture;

        //Rectangle defining the button's
        //position and size
        private Rectangle buttonArea;

        //Constructor, sets up the button's texture and its rectangle
        public Button(Texture2D buttonTexture, Rectangle buttonArea)
        {
            this.buttonTexture = buttonTexture;
            this.buttonArea = buttonArea;
        }

        //Detects if the mouse is hovering over the button.
        //The x and y parameters are the x and y coordinates
        //of the mouse cursor.
        public bool MouseHovering(int x, int y)
        {
            if (buttonArea.Contains(new Point(x, y)))
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        //Draws each button to the screen
        public void Draw(SpriteBatch s)
        {
            s.Draw(buttonTexture, buttonArea, Color.White);
        }




    }
}
