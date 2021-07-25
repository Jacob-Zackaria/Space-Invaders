using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SaucerSpawnEvent : Command
    {
        SpriteBatch pSB_Box;
        SpriteBatch pSB_Saucer;
        Random pRand;
        public SaucerSpawnEvent()
        {
            pSB_Box = SpriteBatchManager.Search(SpriteBatch.Name.Boxes);
            pSB_Saucer = SpriteBatchManager.Search(SpriteBatch.Name.Saucer);
            pRand = new Random();
        }

        override public void Execute(float deltaTime)
        {
            Saucer pSauc = new Saucer(GameObject.Name.Saucer, GameSprite.Name.Saucer, 800.0f, 860.0f);
            pSauc.ActivateCollisionSprite(pSB_Box);
            pSauc.ActivateGameSprite(pSB_Saucer);

            GameObject pSaucerRoot = GameObjectManager.Search(GameObject.Name.SaucerRoot);
            Debug.Assert(pSaucerRoot != null);
            Debug.Assert(pSauc != null);
            pSaucerRoot.Add(pSauc);

            SoundManager.PlaySound(SoundSource.Name.Saucer, true, false, false);

            SaucerBombSpawnEvent pEvent = new SaucerBombSpawnEvent(pSauc);
            TimerManager.Add(TimeEvent.Name.SaucerBomb, pEvent, pRand.Next(1, 4));

            // Add itself back to timer
            deltaTime = pRand.Next(15, 25);
            TimerManager.Add(TimeEvent.Name.SaucerSpawn, this, deltaTime);
        }
    }
}
