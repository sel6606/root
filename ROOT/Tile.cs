using Microsoft.Xna.Framework.Graphics;


namespace ROOT
{
    public class Tile : GameObject
    {
        public Tile(int x, int y, int width, int height, Texture2D texture)
            : base(x, y, width,height,true, texture)
        {

        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);
        }
    }
}
