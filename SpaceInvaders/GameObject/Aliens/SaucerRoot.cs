using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SaucerRoot : Composite
    {
        public SaucerRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        public override void Accept(CollisionVisitor other)
        { 
            // Call the appropriate collision reaction            
            other.VisitSaucerRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            // Go to first child
            GameObject sauc = (GameObject)Iterator.GetChild(this);
            if(sauc != null)
            {
                base.BaseUpdateBoundingBox(this);
            }
            else
            {
                this.x = -20.0f;
                this.y = -20.0f;
            }

            base.Update();
        }

    }
}
