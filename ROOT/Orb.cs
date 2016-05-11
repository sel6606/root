using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    class Orb : GameObject
    {
        private bool active; //orb is active if it hasn't been picked up

        public bool Active
        //deactivates orb if it has been picked up
        {
            set { active = value; }
            get { return active; }
        }

        public Orb(int x, int y, int width, int height, Texture2D texture)
            : base(x,y,width,height,false, texture)
        {
            active = true; //orb is active by default
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s); //draws itself on the screen
        }

    }
}
