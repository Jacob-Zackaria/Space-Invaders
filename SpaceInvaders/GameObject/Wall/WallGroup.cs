using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallGroup : Composite
    {
        public WallGroup(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        ~WallGroup()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallGroup(this);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitGrid(AlienGrid a)
        {
            // BirdGroup vs WallGroup
            // Debug.WriteLine("collide: {0} with {1}", a, this);

            // BirdGroup vs WallGroup
            //              go down a level in Wall Group.
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(a, pGameObj);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }
        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(b);
            CollisionPair.Collide(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(b, pGameObj);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            // ShipRoot vs WallRight.
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(s, pGameObj);
        }

        public override void VisitSaucerRoot(SaucerRoot s)
        {
            // ShipRoot vs WallRight.
            GameObject pGameObj = (GameObject)Iterator.GetChild(s);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitSaucer(Saucer s)
        {
            // ShipRoot vs WallRight.
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(s, pGameObj);
        }


        // Data: ---------------


    }
}
