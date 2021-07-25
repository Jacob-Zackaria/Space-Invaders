using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class EndObserver : CollisionObserver
    {
        private GameObject pShipDestroyed;
        public EndObserver()
        {
            this.pShipDestroyed = null;
        }

        public EndObserver(EndObserver s)
        {
            this.pShipDestroyed = s.pShipDestroyed;
        }

        public override void Notify()
        {
            this.pShipDestroyed = (Ship)this.pSubject.pObjB;
            Debug.Assert(this.pShipDestroyed != null);

            if (pShipDestroyed.bMarkForDeath == false)
            {
                pShipDestroyed.bMarkForDeath = true;
                //   Delay
                EndObserver pObserver = new EndObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }

            
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pShipDestroyed.Delete();
            Ship pShip = ShipManager.GetShip();
            pShip.SetState(ShipManager.State.Dead);

            if (GameObjectManager.DecreaseLife() == false)
            {

                //Explosion sprite.
                Explosion exp = new Explosion(GameObject.Name.Explosion, GameSprite.Name.PlayerExplosionA, this.pShipDestroyed.x, this.pShipDestroyed.y);

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
                RemoveExplosionEvent rEve = new RemoveExplosionEvent(exp, true);
                TimerManager.Add(TimeEvent.Name.ExplosionRemove, rEve, 1.0f);
            }
            else
            {
                ShipManager.Destroy();
                SoundManager.Destroy();
            }
        }
    }
}
