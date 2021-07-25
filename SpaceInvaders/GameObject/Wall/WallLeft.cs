using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallLeft : WallCategory
    {
        public WallLeft(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Left)
        {
            this.pColObj.pColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        ~WallLeft()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallLeft(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitGrid(AlienGrid a)
        {
            // BirdGroup vs WallRight
            Debug.WriteLine("\ncollide: {0} with {1}", this, a);
            Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            // ShipRoot vs WallLeft
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(s, this);
            pColPair.NotifyListeners();

        }

        public override void VisitSaucer(Saucer a)
        {
            // BirdGroup vs WallRight
            Debug.WriteLine("\ncollide: {0} with {1}", this, a);
            Debug.WriteLine("               --->DONE<----");

            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(this, a);
            pColPair.NotifyListeners();
        }
        // Data: ---------------


    }
}
