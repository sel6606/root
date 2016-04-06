using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class DefaultPower : Powerup
    {
        private Player user;
        private double cooldownTime=15;
        private double activeTime=5;

        public DefaultPower(Player player)
        {
            user = player;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime ;
            this.activeDuration = activeTime;
        }

        public override void Effect()
        {
            user.speed = 2;
        }

        public override void EndEffect()
        {
            user.speed = 1;
        }
    }
}
