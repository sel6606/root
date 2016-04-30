using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class CavePow : Powerup
    {
        //The player using the power up
        private Player user;

        //List of all players.
        private List<Player> PlayList;


        //the powerups cooldown time
        private double cooldownTime = 15;

        //How long the power up is active
        private double activeTime = 5;


        //constructor for powerup, takes in the player who uses the power up.
        public CavePow(Player player, List<Player> plaList)
        {
            user = player;
            PlayList = plaList;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;
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
