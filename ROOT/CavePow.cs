using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ROOT
{
    class CavePow : Powerup
    {
        //The player using the power up
        private Player user;

        private SpriteBatch sp;

        //List of all players.
        private List<Player> PlayList;


        //the powerups cooldown time
        private double cooldownTime = 15;

        //How long the power up is active
        private double activeTime = 5;


        //constructor for powerup, takes in the player who uses the power up.
        public CavePow(Player player, List<Player> plaList, SpriteBatch s, Texture2D texture)
        {
            user = player;
            PlayList = plaList;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;
            sp = s;
            Tex = texture;
        }


        //activates the power up.
        public override void Effect()
        {
            foreach(Player play in PlayList)
            {
                if(!(user == play))
                {
                    int y = 0;
                    y = play.moveRight;
                    play.moveRight = play.moveLeft;
                    play.moveLeft = y;
                }
            }
            isActive = true;
        }


        //ends the effect.
        public override void EndEffect()
        {
            foreach (Player play in PlayList)
            {
                if (!(user == play))
                {
                    int y = 0;
                    y = play.moveRight;
                    play.moveRight = play.moveLeft;
                    play.moveLeft = y;
                }
            }
        }

    }
}
