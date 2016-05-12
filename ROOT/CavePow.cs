
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    class CavePow : Powerup
    {
        //The player using the power up
        private Player user;

        private SpriteBatch sp;

        //List of all players.
        private List<Player> PlayList;


        //The powerup's cooldown time
        private double cooldownTime = 15;

        //How long the powerup is active
        private double activeTime = 5;


        //Constructor for the caveman powerup
        //Takes the player who used it, the list of all players, a spritebatch, and the powerup texture
        public CavePow(Player player, List<Player> plaList, SpriteBatch s, Texture2D texture)
        {
            user = player;
            PlayList = plaList;
            sp = s;
            Tex = texture;

            //Sets initial values for the base class variables
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;

        }


        //Triggers the powerup effect
        public override void Effect()
        {
            foreach (Player play in PlayList)
            {
                if (!(user == play))
                { //Inverts the left and right controls for each player that is not the user
                    int y = 0;
                    y = play.moveRight;
                    play.moveRight = play.moveLeft;
                    play.moveLeft = y;
                }
            }
            isActive = true;
        }


        //Ends the powerup effect
        public override void EndEffect()
        {
            foreach (Player play in PlayList)
            {
                if (!(user == play))
                { //Returns the player's controls to normal
                    int y = 0;
                    y = play.moveRight;
                    play.moveRight = play.moveLeft;
                    play.moveLeft = y;
                }
            }
        }

    }
}
