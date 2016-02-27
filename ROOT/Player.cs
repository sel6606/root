using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ROOT
{
    class Player : GameObject
    {
        //Fields for controls

        public Player()
            : base()
        {

        }

        public void Move() { }

        public void Jump() { }

        public void CheckCollision() { }

        public void Stun() { }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
