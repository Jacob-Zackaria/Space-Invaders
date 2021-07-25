﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBombObserver : CollisionObserver
    {
        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pBomb;
        public RemoveBombObserver()
        {
            this.pBomb = null;
        }
        public RemoveBombObserver(RemoveBombObserver b)
        {
            this.pBomb = b.pBomb;
        }
        public override void Notify()
        {
            if(this.pSubject.pObjA.name == GameObject.Name.Missile)
            {
                this.pBomb = (Bomb)this.pSubject.pObjB;
            }
            else
            {
                this.pBomb = (Bomb)this.pSubject.pObjA;
            }
            
            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pBomb.Delete();

            //Explosion sprite.
            Explosion exp = new Explosion(GameObject.Name.Explosion, GameSprite.Name.AlienShotExplosion, this.pBomb.x, this.pBomb.y);

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchManager.Search(SpriteBatch.Name.Explosion);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Search(SpriteBatch.Name.Boxes);

            exp.ActivateGameSprite(pSB_Aliens);
            exp.ActivateCollisionSprite(pSB_Boxes);

            // Attach the missile to the missile root
            GameObject pExplosion = GameObjectManager.Search(GameObject.Name.ExplosionRoot);
            Debug.Assert(pExplosion != null);

            // Add to GameObject Tree - {update and collisions}
            pExplosion.Add(exp);

            //Remove explosion event.
            RemoveExplosionEvent rEve = new RemoveExplosionEvent(exp);
            TimerManager.Add(TimeEvent.Name.ExplosionRemove, rEve, 0.3f);
        }
    }
}
