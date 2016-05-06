using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    public class GameObject
    {
        //fields common to all game objects
        private Rectangle hitBox; //size of the space object occupies
        private bool isSolid; //is the object solid, players cant move through solid objects
        private Texture2D tex; //object's texture


        //Properties for isSolid
        public bool IsSolid
        {
            get { return isSolid; }
        }

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


        //constructor
        public GameObject(int x, int y, int width, int height, bool solid, Texture2D texture)
        //requires x/y coordinates for a starting point, dimensions for the hitbox rectangle,
        //and a solid bool value
        {
            tex = texture;

            hitBox = new Rectangle(x, y, width, height);
            isSolid = solid;
        }

        public virtual void Draw(SpriteBatch s)
        //allows game objects to draw themselves
        {
            s.Draw(tex, hitBox, Color.White);
        }
    }

}
