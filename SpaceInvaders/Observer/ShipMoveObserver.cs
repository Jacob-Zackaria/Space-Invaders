using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveObserver : CollisionObserver
    {
        public ShipMoveObserver()
        {

        }
        public override void Notify()
        {
            WallCategory pWall = (WallCategory)this.pSubject.pObjB;

            if (pWall.GetCategoryType() == WallCategory.Type.Right)
            {
                Ship pShip = ShipManager.GetShip();
                pShip.SetState(ShipManager.State.NoMoveRight);
            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Left)
            {
                Ship pShip = ShipManager.GetShip();
                pShip.SetState(ShipManager.State.NoMoveLeft);
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}

