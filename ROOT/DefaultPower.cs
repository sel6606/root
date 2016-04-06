using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class DefaultPower : Powerup
    {
        //The player using the power up
        private Player user;

        //the powerups cooldown time
        private double cooldownTime=15;

        //How long the power up is active
        private double activeTime=5;


        //constructor for powerup, takes in the player who uses the power up.
        public DefaultPower(Player player)
        {
            user = player;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime ;
            this.activeDuration = activeTime;
        }


        //activates the power up.
        public override void Effect()
        {
            user.speed = user.speed * 2;
        }


        //ends the effect.
        public override void EndEffect()
        {
            user.speed = user.speed / 2;
        }
    }
}
