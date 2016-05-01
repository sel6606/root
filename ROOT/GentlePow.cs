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
    class GentlePow : Powerup
    {
        //The player using the power up
        private Player user;

        private SpriteBatch sp;


        //List of all players.
        private List<Player> PlayList;

        //the int to decide what power to use
        private int x;

        //the powerups cooldown time
        private double cooldownTime = 15;

        //How long the power up is active
        private double activeTime = 5;

        //Random for the power up
        Random Rand = new Random();

        //Instances of each powerup
        CavePow cave ;

        CowPow cow;

        KnightPow Kpow;

        //constructor for powerup, takes in the player who uses the power up.
        public GentlePow(Player player, List<Player> plaList, SpriteBatch s,GraphicsDevice g)
        {
            user = player;
            PlayList = plaList;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;
            cave = new CavePow(user, PlayList,s);
            cow = new CowPow(user, PlayList,s,g);
            Kpow = new KnightPow(user, PlayList,s);
            sp = s;
        }


        //activates the power up.
        public override void Effect()
        {
            
             x = Rand.Next(1, 4);
            if(x == 1)
            {
                cave.Effect();
            }
            else if (x == 2)
            {
                cow.Effect();
            }
            else if (x == 3)
            {
                Kpow.Effect();
            }

        }


        //ends the effect.
        public override void EndEffect()
        {
            if (x == 1)
            {
                cave.EndEffect();
            }
            else if (x == 2)
            {
                cow.EndEffect();
            }
            else if (x == 3)
            {
                Kpow.EndEffect();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (x == 2)
            {
                cow.Draw(sb);
            }
        }
    }
}
