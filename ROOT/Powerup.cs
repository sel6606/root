using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    abstract class Powerup
    {
        protected double activeTimer;
        protected double cooldownTimer;
        protected bool isActive;
        protected bool isReady;
        protected double activeDuration;
        protected double coolDuration;

        public void Cooldown(double elapsedTime)
        {
                cooldownTimer -= elapsedTime;
                if (cooldownTimer <= 0)
                {
                    isReady = true;
                }
            
        }

        public void Activate()
        {
            if (isReady)
            {
                isActive = true;
                activeTimer = activeDuration;
                isReady = false;
                cooldownTimer = coolDuration;
            }
        }

        public void Update(double elapsedTime)
        {
            if (!isReady && !isActive)
            {
                Cooldown(elapsedTime);
            }
            else if (isActive)
            {
                Effect();
                activeTimer -= elapsedTime;
                if (activeTimer <= 0)
                {
                    isActive = false;
                    EndEffect();
                }
            }

        }

        public abstract void Effect();

        public abstract void EndEffect();
      

    }
}
