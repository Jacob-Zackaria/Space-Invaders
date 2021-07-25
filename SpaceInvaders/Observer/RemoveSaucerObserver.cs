using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveSaucerObserver : CollisionObserver
    {
        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pSacuer;
        private Random rand;
        public RemoveSaucerObserver()
        {
            this.rand = new Random();
            this.pSacuer = null;
        }
        public RemoveSaucerObserver(RemoveSaucerObserver b)
        {
            this.rand = b.rand;
            this.pSacuer = b.pSacuer;
        }
        public override void Notify()
        {
             this.pSacuer = (Saucer)this.pSubject.pObjB;

            if(this.pSubject.pObjA.name == GameObject.Name.Missile)
            { 
                GameObjectManager.UpdatePlayerScore(rand.Next(100, 250));
                GameObjectManager.UpdateScore();
            }
            Debug.Assert(this.pSacuer != null);

            SoundManager.StopSound(SoundSource.Name.Saucer);
            SoundManager.PlaySound(SoundSource.Name.SaucerKilled, false, false, false);

            if (pSacuer.bMarkForDeath == false)
            {
                pSacuer.bMarkForDeath = true;
                //   Delay
                RemoveSaucerObserver pObserver = new RemoveSaucerObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            GameObject pB = (GameObject)Iterator.GetParent(this.pSacuer);
            this.pSacuer.Delete();
            pB.Update();

            //Explosion sprite.
            Explosion exp = new Explosion(GameObject.Name.Explosion, GameSprite.Name.SaucerExplosion, this.pSacuer.x, this.pSacuer.y);

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
            TimerManager.Add(TimeEvent.Name.ExplosionRemove, rEve, 0.5f);
        }
    }
}
