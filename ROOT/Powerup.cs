
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ROOT
{
    public abstract class Powerup
    {
        //Protected Fields

        //Timer to check activation time
        protected double activeTimer;
        //Timer to check cooldown time.
        protected double cooldownTimer;
        //Whether the powerup is in use
        protected bool isActive;
        //Whether the powerup can be used
        protected bool isReady;
        //How long the power up is active.
        protected double activeDuration;
        //How long the cooldown is
        protected double coolDuration;

        //Private fields
        private Rectangle rec;
        private int x;
        private Texture2D tex;

        //Properties for isActive
        public bool IsActive
        {
            get { return isActive; }
        }

        //Properties for tex
        public Texture2D Tex
        {
            get { return tex; }

            set { tex = value; }
        }

        //Properties for isReady
        public bool IsReady
        {
            get { return isReady; }
        }


        //Runs the cooldown logic for the powerup
        public void Cooldown(double elapsedTime)
        {
            cooldownTimer -= elapsedTime;
            if (cooldownTimer <= 0)
            {
                isReady = true;
            }

        }


        //Starts the power up if it is avaible
        public virtual void Activate()
        {
            if (isReady)
            {
                isActive = true;
                activeTimer = activeDuration;
                isReady = false;
                cooldownTimer = coolDuration;
                Effect();
            }
        }


        //Updates the timer.
        public virtual void Update(double elapsedTime)
        {
            if (!isReady && !isActive)
            {
                Cooldown(elapsedTime);
            }
            else if (isActive)
            {

                activeTimer -= elapsedTime;
                if (activeTimer <= 0)
                {
                    //Ends the effect if time is out
                    isActive = false;
                    EndEffect();
                }
            }

        }

        //Empty Draw method for some reason
        public virtual void Draw(SpriteBatch sb)
        {
        }


        //Abstract methods for the powerup's effects
        public abstract void Effect();
        public abstract void EndEffect();


    }
}
