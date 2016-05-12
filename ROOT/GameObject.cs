using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    public class GameObject
    {
        //Fields common to all game objects
        private Rectangle hitBox; //Size of the space the object occupies
        private bool isSolid; //Is the object solid? Players can't move through solid objects
        private Texture2D tex; //Object's texture

        #region Properties
        //Properties for hitBox
        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        //Properties for the x value of the hitbox
        public int X
        {
            get { return hitBox.X; }
            set { hitBox.X = value; }
        }

        //Properties for the y value of the hitbox
        public int Y
        {
            get { return hitBox.Y; }
            set { hitBox.Y = value; }
        }

        //Properties for the width of the hitbox
        public int Width
        {
            get { return hitBox.Width; }
        }

        //Properties for the height of the hitbox
        public int Height
        {
            get { return hitBox.Height; }
        }

        //Properties for the center of the hitbox
        public Point Center
        {
            get { return hitBox.Center; }
        }

        //Properties for the left edge of the hitbox
        public int Left
        {
            get { return hitBox.Left; }
        }

        //Properties for the right edge of the hitbox
        public int Right
        {
            get { return hitBox.Right; }
        }

        //Properties for the bottom edge of the hitbox
        public int Bottom
        {
            get { return hitBox.Bottom; }
        }

        //Properties for the top edge of the hitbox
        public int Top
        {
            get { return hitBox.Top; }
        }
        
        //Properties for tex
        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }

        #endregion


        //Constructor for GameObject
        //Requires x/y coordinates for a starting point, dimensions for the hitbox rectangle,
        //and a solid bool value
        public GameObject(int x, int y, int width, int height, bool solid, Texture2D texture)
        {
            tex = texture;
            hitBox = new Rectangle(x, y, width, height);
            isSolid = solid;
        }

        //Draw method for the game object
        public virtual void Draw(SpriteBatch s)
        {
            s.Draw(tex, hitBox, Color.White);
        }
    }

}
