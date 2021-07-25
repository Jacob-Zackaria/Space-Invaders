﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Octopus : AlienCategory
    {
        public Octopus(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an RedBird
            // Call the appropriate collision reaction            
            other.VisitOctopus(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Bird vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Bird vs Missile
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Crab.
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            // Debug.WriteLine("update: {0}", this);
            base.Update();
        }
    }
}
