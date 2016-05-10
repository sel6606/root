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

        private Rectangle rec2;

        public Rectangle Rec { get { return rec; } }

        private int x;

        Texture2D tex;

        //List of all players.
        private List<Player> PlayList;

        //the powerups cooldown time
        private double cooldownTime = 15;

        //How long the power up is active
        private double activeTime = 5;


        //constructor for powerup, takes in the player who uses the power up.
        public CowPow(Player player, List<Player> plaList, SpriteBatch s, GraphicsDevice g, Texture2D texture)
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
            Tex = texture;
        }


        public override void Update(double elapsedTime)
        {
            if (!isReady && !isActive)
            {
                Cooldown(elapsedTime);
            }
            else if (isActive)
            {
                
                if (x == 0)
                {
                    rec2 = new Rectangle(rec.X, rec.Y, 4, 2);
                    foreach(Player play in PlayList)
                    {
                        if (play != user)
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
                    }
                    rec.X = rec.X + 4;
                    
                }
                else if (x == 1)
                {
                    rec2 = new Rectangle(rec.X, rec.Y, 4, 2);
                    foreach (Player play in PlayList)
                    {
                        if (play != user)
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
                    }
                    rec.X = rec.X - 4;
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
            isActive = true;
            rec = new Rectangle(user.X, user.Y + (user.Height / 2), 20, 20);
            x = (int)user.CurrentDirectionState;
        }


        //ends the effect.
        public override void EndEffect()
        {
            user.Speed = user.BaseSpeed;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, rec2, Color.White);
            base.Draw(sb);
        }


    }

}
