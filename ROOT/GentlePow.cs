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
        private int p = 2;

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

        //Game1
        Game1 game;

        //constructor for powerup, takes in the player who uses the power up.
        public GentlePow(Player player, List<Player> plaList, SpriteBatch s,GraphicsDevice g, Game1 game, Texture2D texture)
        {
            this.game = game;
            user = player;
            PlayList = plaList;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;
            cave = new CavePow(user, PlayList,s, game.CavePowTex);
            cow = new CowPow(user, PlayList,s,g, game.CowPowTex);
            Kpow = new KnightPow(user, PlayList,s, game.KnightPowTex);
            sp = s;
            Tex = texture;
        }
        public override void Activate()
        {
            isActive = true;
            if (isReady)
            {
               // p = Rand.Next(1, 4);
            }
           
            if (p == 1)
            {
                cave.Activate();
            }
            else if (p == 2)
            {
                cow.Activate();
            }
            else if (p == 3)
            {
                Kpow.Activate();
            }
        }

        //activates the power up.
        public override void Effect()
        {
            

            if(p == 1)
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


        //ends the effect.
        public override void EndEffect()
        {
            isActive = false;
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

        public override void Draw(SpriteBatch sb)
        {
            if (p == 2)
            {
                cow.Draw(sb);
            }
        }
        public override void Update(double elapsedTime)
        {
            if (p == 1)
            {
                cave.Update(elapsedTime);
            }
            else if (p == 2)
            {
                cow.Update(elapsedTime);
            }
            else if (p == 3)
            {
                Kpow.Update(elapsedTime);
            }
        }
    }
}
