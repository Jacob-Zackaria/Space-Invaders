using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveExplosionEvent : Command
    {
        private Explosion explode;
        private bool isPlayer;
        public RemoveExplosionEvent(Explosion exe, bool val = false)
        {
            this.explode = exe;
            this.isPlayer = val;
        }

        public override void Execute(float deltaTime)
        {
            this.explode.Delete();

            if(this.isPlayer)
            {
                ShipManager.ActivateShip();
                ShipManager.GetShip().SetState(ShipManager.State.Ready);
            }
        }
    }
}
