﻿
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    class KnightPow : Powerup
    {
        //The player using the power up
        private Player user;

        private SpriteBatch sp;

        //List of all players.
        private List<Player> PlayList;

        //the powerups cooldown time
        private double cooldownTime=15;

        //How long the power up is active
        private double activeTime=5;

       

        //constructor for powerup, takes in the player who uses the power up.
        public KnightPow(Player player, List<Player> plaList,SpriteBatch s, Texture2D texture)
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
            isActive = true;
            user.Speed = user.BaseSpeed*2;
        }


        //ends the effect.
        public override void EndEffect()
        {
            user.Speed = user.BaseSpeed;
        }
    }
}
