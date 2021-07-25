using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReadyObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipManager.GetShip();
            pShip.Handle();
        }

        // data


    }
}
