using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienExplosionObserver : CollisionObserver
    {
        private GameSprite pSprite;
        private GameObject pAlien;
        public AlienExplosionObserver()
        {
            this.pSprite = null;
            this.pAlien = null;
        }

        public override void Notify()
        {
            this.pAlien = (AlienCategory)this.pSubject.pObjB;

            this.pSprite = GameSpriteManager.Search(GameSprite.Name.PlayerExplosionA);
            this.pSprite.x = this.pAlien.x;
            this.pSprite.y = this.pAlien.y;

            this.pSprite.Update();
            this.pSprite.Render();
        }

        public override void Execute()
        {

        }
    } 
}
