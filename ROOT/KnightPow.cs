using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    class KnightPow : Powerup
    {
        //The player using the powerup
        private Player user;

        private SpriteBatch sp;

        //List of all players.
        private List<Player> PlayList;

        //The powerup's cooldown time
        private double cooldownTime = 15;

        //How long the powerup is active
        private double activeTime = 5;



        //Constructor for powerup
        public KnightPow(Player player, List<Player> plaList, SpriteBatch s, Texture2D texture)
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


        //Triggers the powerup's effect
        public override void Effect()
        {
            isActive = true;
            user.Speed = (int)Math.Ceiling(user.BaseSpeed * 1.5);
        }


        //Ends the effect.
        public override void EndEffect()
        {
            user.Speed = user.BaseSpeed;
        }
    }
}
