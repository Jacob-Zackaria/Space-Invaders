using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundObserver : CollisionObserver
    {
        public SoundObserver()
        {
          
        }
        public override void Notify()
        {
            SoundManager.PlaySound(SoundSource.Name.ExplosionPlayer, false, false, false);
        }

    }
}
