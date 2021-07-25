﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Saucer : SaucerCategory
    {
        private float delta;
        public Saucer(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        :   base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 1.5f;
        }
        public override void Accept(CollisionVisitor other)
        {
            other.VisitSaucer(this);
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
            base.Update();

            this.x -= delta;
        }
    }
}
