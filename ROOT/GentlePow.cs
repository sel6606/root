using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    class GentlePow : Powerup
    {
        //The player using the powerup
        private Player user;

        private SpriteBatch sp;


        //List of all players.
        private List<Player> PlayList;

        //The int to decide what power to use
        private int p = 2;

        //The powerup's cooldown time
        private double cooldownTime = 15;

        //How long the powerup is active
        private double activeTime = 5;

        //Random for the powerup
        Random Rand = new Random();

        //Instances of each powerup
        private CavePow cave;
        private CowPow cow;
        private KnightPow Kpow;

        //Game1
        private Game1 game;

        //Constructor for powerup, takes in the player who uses the powerup.
        public GentlePow(Player player, List<Player> plaList, SpriteBatch s, GraphicsDevice g, Game1 game, Texture2D texture)
        {
            this.game = game;
            user = player;
            PlayList = plaList;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;
            cave = new CavePow(user, PlayList, s, game.CavePowTex);
            cow = new CowPow(user, PlayList, s, g, game.CowPowTex);
            Kpow = new KnightPow(user, PlayList, s, game.KnightPowTex);
            sp = s;
            Tex = texture;
        }

        //Activates the powerup
        public override void Activate()
        {
            //If the powerup is usable, set p equal to a random integer between 1 and 4
            if (isReady)
            {
                p = Rand.Next(1, 4);
            }

            isReady = false;
            isActive = true;

            if (p == 1)
            {
                //Activate the caveman powerup
                cave.Activate();
            }
            else if (p == 2)
            {
                //Activate the cowboy powerup
                cow.Activate();
            }
            else if (p == 3)
            {
                //Activate the knight powerup
                Kpow.Activate();
            }
        }

        //Triggers the powerup effect
        public override void Effect()
        {
            //Depending on p, trigger the corresponding effect
            if (p == 1)
            {
                cave.Effect();
            }
            else if (p == 2)
            {
                cow.Effect();
            }
            else if (p == 3)
            {
                Kpow.Effect();
            }

        }


        //Ends the effect
        public override void EndEffect()
        {
            isActive = false;

            //Depending on p, call the corresponding EndEffect()
            if (p == 1)
            {
                cave.EndEffect();
            }
            else if (p == 2)
            {
                cow.EndEffect();
            }
            else if (p == 3)
            {
                Kpow.EndEffect();
            }
        }

        //Draws the powerup
        public override void Draw(SpriteBatch sb)
        {
            if (p == 2)
            {
                cow.Draw(sb);
            }
        }

        //Update method for GentlePow
        public override void Update(double elapsedTime)
        {
            //Depending on p, update the corresponding powerup
            if (p == 1)
            {
                cave.Update(elapsedTime);
                isReady = cave.IsReady;
            }
            else if (p == 2)
            {
                cow.Update(elapsedTime);
                isReady = cow.IsReady;
            }
            else if (p == 3)
            {
                Kpow.Update(elapsedTime);
                isReady = Kpow.IsReady;
            }
        }
    }
}
