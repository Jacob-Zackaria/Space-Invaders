using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveAlienObserver : CollisionObserver
    {
        public GameObject pAlien;
        
        public RemoveAlienObserver()
        {
            this.pAlien = null;
        }

        public RemoveAlienObserver(RemoveAlienObserver m)
        {
            this.pAlien = m.pAlien;
        }
        public override void Notify()
        {

            this.pAlien = (AlienCategory)this.pSubject.pObjB;
            if(this.pAlien.name == GameObject.Name.Octopus)
            {
                GameObjectManager.UpdatePlayerScore(10);
            }
            else if(this.pAlien.name == GameObject.Name.Crab)
            {
                GameObjectManager.UpdatePlayerScore(20);
            }
            else if (this.pAlien.name == GameObject.Name.Squid)
            {
                GameObjectManager.UpdatePlayerScore(30);
            }
            GameObjectManager.UpdateScore();

            SoundManager.PlaySound(SoundSource.Name.InvaderKilled, false, false, false);


            Debug.Assert(this.pAlien != null);

            if (pAlien.bMarkForDeath == false)
            {
                pAlien.bMarkForDeath = true;
                //   Delay
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)Iterator.GetParent(pA);

            pA.Delete();

            //Explosion sprite.
            Explosion exp = new Explosion(GameObject.Name.Explosion, GameSprite.Name.AlienExplosion, this.pAlien.x, this.pAlien.y);

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

            if (privCheckParent(pB) == true)
            {
                GameObject pC = (GameObject)Iterator.GetParent(pB);
                pB.Delete();

                if (privCheckParent(pC) == true)
                {
                    pC.Delete();
                }

            }
        }
        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }
    }
}
