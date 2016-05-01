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
    class CowPow : Powerup
    {
        //The player using the power up
        private Player user;

        public bool IsActive { get { return isActive; } }

        private SpriteBatch sp;

        //Rectangle for the projectile
        private Rectangle rec;

        public Rectangle Rec { get { return rec; } }

        private Rectangle rec2;

        private int x;

        Texture2D tex;

        //List of all players.
        private List<Player> PlayList;

        //the powerups cooldown time
        private double cooldownTime = 15;

        //How long the power up is active
        private double activeTime = 5;


        //constructor for powerup, takes in the player who uses the power up.
        public CowPow(Player player, List<Player> plaList, SpriteBatch s, GraphicsDevice g)
        {
            user = player;
            PlayList = plaList;
            this.isActive = false;
            this.isReady = true;
            this.coolDuration = cooldownTime;
            this.activeDuration = activeTime;
            sp = s;
            tex = new Texture2D(g, 1, 1);
            tex.SetData<Color>(new Color[] { Color.Chartreuse });
        }


        public override void Update(double elapsedTime)
        {
            if (!isReady && !isActive)
            {
                Cooldown(elapsedTime);
            }
            else if (isActive)
            {
                
                if (x == 0 || x == 2 || x == 4)
                {
                     rec2 = new Rectangle(rec.X, rec.Y, rec.X + 10, rec.Y);
                    foreach(Player play in PlayList)
                    {
                        if (rec2.Contains(play.HitBox))
                        {
                            play.Stun(elapsedTime);
                        }
                    }
                    rec.X = rec.X + 10;
                    
                }
                else if (x == 1 || x == 3 || x == 5)
                {
                    Rectangle rec2 = new Rectangle(rec.X, rec.Y, rec.X - 10, rec.Y);
                    foreach (Player play in PlayList)
                    {
                        if (rec2.Intersects(play.HitBox))
                        {
                            if (!play.Stunned)
                            {
                                play.Stunned = true;
                            }
                            play.Stun(elapsedTime);
                        }
                    }
                    rec.X = rec.X - 10;
                }
                
                activeTimer -= elapsedTime;
                if (activeTimer <= 0)
                {
                    //ends the effect if time is out
                    isActive = false;
                    EndEffect();
                }
            }

        }

        //activates the power up.
        public override void Effect()
        {
            rec = new Rectangle(user.X, user.Y + (user.Height / 2), 20, 20);
            x = (int)user.CurentState;
        }


        //ends the effect.
        public override void EndEffect()
        {
            user.Speed = user.BaseSpeed;
        }

       
    }

}
