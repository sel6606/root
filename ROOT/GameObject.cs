using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class GameObject
    {
        //fields common to all game objects
        private Rectangle hitBox; //size of the space object occupies
        private bool isSolid; //is the object solid, players cant move through solid objects
        private Point loc; //where in the game world is this object located
        private Texture2D tex; //object's texture

        //properties
        public bool IsSolid { get { return isSolid; } }
        public Rectangle HitBox { get { return hitBox; } }
        public int X { get { return loc.X; } set { loc.X = value; } }
        public int Y { get { return loc.Y; } set { loc.Y = value; } }
        public int Width { get { return hitBox.Width; } }
        public int Height { get { return hitBox.Height; } }
        public Texture2D Tex { get { return tex; } set { tex = value; } }
        
        //constructor
        public GameObject(int x, int y, int width, int height, bool solid)
        //requires x/y coordinates for a starting point, dimensions for the hitbox rectangle,
        //and a solid bool value
        {
            loc.X = x;
            loc.Y = y;
            hitBox = new Rectangle(loc.X, loc.Y, width, height);
            isSolid = solid;
        }

        public virtual void Draw(SpriteBatch s)
        //allows game objects to draw themselves
        {
            s.Draw(tex, hitBox, Color.White);
        }
    }

}
