using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallTop : WallCategory
    {
        public WallTop(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Top)
        {
            this.pColObj.pColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        ~WallTop()
        {
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallTop(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallTop
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs WallTop
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        // Data: ---------------


    }
}
