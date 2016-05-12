using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ROOT
{
    public class Tile : GameObject
    {
        //Constructor for Tile
        public Tile(int x, int y, int width, int height, Texture2D texture)
            : base(x, y, width, height, true, texture)
        {

        }

        //Draws the tile
        public override void Draw(SpriteBatch s)
        {
            s.Draw(Tex, HitBox, Color.Black);
        }
    }
}
