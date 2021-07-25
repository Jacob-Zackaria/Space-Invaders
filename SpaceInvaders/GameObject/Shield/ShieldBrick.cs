using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName, ShieldCategory.Type.Brick)
        {
            this.x = posX;
            this.y = posY;

            this.SetCollisionColor(0.0f, 0.0f, 0.0f);
        }
        ~ShieldBrick()
        {

        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldBrick(this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitGrid(AlienGrid a)
        {
            // AlienGrid vs ShieldBrick
            GameObject pGameObj = (GameObject)Iterator.GetChild(a);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitColumn(AlienColumn a)
        {
            // AlienColumn vs ShieldBrick
            GameObject pGameObj = (GameObject)Iterator.GetChild(a);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitCrab(Crab a)
        {
            // Crab vs ShieldBrick
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitOctopus(Octopus a)
        {
            // AlienGrid vs ShieldBrick
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitSquid(Squid a)
        {
            // AlienGrid vs ShieldBrick
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }
        public override void Update()
        {
            base.Update();
        }

        // ---------------------------------
        // Data: 
        // ---------------------------------


    }
}
