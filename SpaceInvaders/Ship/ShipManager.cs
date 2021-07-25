using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipManager
    {
        // Data: ----------------------------------------------
        private static ShipManager instance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private ShipStateDead pStateDead;
        private ShipNoMoveLeft pStateNoMoveLeft;
        private ShipNoMoveRight pStateNoMoveRight;

        SpriteBatch pSB_Box;
        SpriteBatch pSB_Player;

        public enum State
        {
            Ready,
            MissileFlying,
            Dead,
            NoMoveLeft,
            NoMoveRight
        }

        private ShipManager()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying();
            this.pStateDead = new ShipStateDead();
            this.pStateNoMoveLeft = new ShipNoMoveLeft();
            this.pStateNoMoveRight = new ShipNoMoveRight();

            // set active
            this.pShip = null;
            this.pMissile = null;

            pSB_Box = SpriteBatchManager.Search(SpriteBatch.Name.Boxes);
            pSB_Player = SpriteBatchManager.Search(SpriteBatch.Name.Player);
        }

        public static void NewShip()
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipManager();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            ActivateShip();
            instance.pShip.SetState(ShipManager.State.Ready);

        }

        public static void Destroy()
        {
            instance = null;
        }
        private static ShipManager GetPrivateInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            ShipManager pShipMan = ShipManager.GetPrivateInstance();

            Debug.Assert(pShipMan != null);

            return pShipMan.pShip;
        }

        public static ShipState GetState(State state)
        {
            ShipManager pShipMan = ShipManager.GetPrivateInstance();
            Debug.Assert(pShipMan != null);

            ShipState pShipState = null;

            switch (state)
            {
                case ShipManager.State.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipManager.State.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipManager.State.Dead:
                    pShipState = pShipMan.pStateDead;
                    break;

                case ShipManager.State.NoMoveLeft:
                    pShipState = pShipMan.pStateNoMoveLeft;
                    break;

                case ShipManager.State.NoMoveRight:
                    pShipState = pShipMan.pStateNoMoveRight;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            ShipManager pShipMan = ShipManager.GetPrivateInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipManager pShipMan = ShipManager.GetPrivateInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Missile pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.PlayerShot, 400, 100);
            pShipMan.pMissile = pMissile;

            SoundManager.PlaySound(SoundSource.Name.Missile, false, false, false);

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchManager.Search(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Search(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectManager.Search(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }


        public static Ship ActivateShip()
        {
            ShipManager pShipMan = ShipManager.GetPrivateInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Ship pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Player, 200, 100);
            pShip.ActivateCollisionSprite(pShipMan.pSB_Box);
            pShip.ActivateGameSprite(pShipMan.pSB_Player);
            pShipMan.pShip = pShip;

            //Attach to root.
            GameObject pShipRoot = GameObjectManager.Search(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            return pShipMan.pShip;
        }
    }
}
