using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    abstract class Powerup
    {

        //timer to check activation time
        protected double activeTimer;

        //timer to check cooldown time.
        protected double cooldownTimer;

        //whether tha powerup is in use
        protected bool isActive;

        //whether the powerup can be used
        protected bool isReady;

        //how long the power up is active.
        protected double activeDuration;

        //how long the cooldown is
        protected double coolDuration;


        //does the cooldown of the power up.
        public void Cooldown(double elapsedTime)
        {
                cooldownTimer -= elapsedTime;
                if (cooldownTimer <= 0)
                {
                    isReady = true;
                }
            
        }


        //starts the power up if it is avaible
        public void Activate()
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


        //updates the timer.
        public void Update(double elapsedTime)
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
                    //ends the effect if time is out
                    isActive = false;
                    EndEffect();
                }
            }

        }


        //abstract methods for the power ups effects
        public abstract void Effect();

        public abstract void EndEffect();
      

    }
}
