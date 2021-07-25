using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName(":::SPACE INVADERS:::");
            this.SetWidthHeight(896, 1024);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Setup Managers
            //---------------------------------------------------------------------------------------------------------

            Simulation.NewSimulation();
            TimerManager.NewTimer(3, 1);
            TextureManager.NewTexture(1, 1);
            ImageManager.NewImage(5, 2);
            GameSpriteManager.NewSprite(4, 2);
            BoxSpriteManager.NewBoxSprite(3, 1);

            SpriteBatchManager.NewSpriteBatch();
            GlyphManager.NewGlyph(3, 1);
            FontManager.NewFont(1, 1);
            ProxySpriteManager.NewProxy(10, 1);
            GameObjectManager.NewGameObject(3, 1);
            CollisionPairManager.NewCollisionPair(1, 1);
            


            SceneContext.NewSceneContext();
        }
        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {
            GlobalTimer.Update(this.GetTime());

            if ((SceneContext.GetScene() == SceneContext.Scene.Demo) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE)))
            {
                SceneContext.SetState(SceneContext.Scene.Select);
            }
            else if ((SceneContext.GetScene() == SceneContext.Scene.Select) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1)))
            {
                SceneContext.SetState(SceneContext.Scene.Play);
               // SceneContext.SetPlayerOne();
            }
            /*else if ((SceneContext.GetScene() == SceneContext.Scene.Select) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2)))
            {
                SceneContext.SetState(SceneContext.Scene.Play);
                //SceneContext.SetPlayerTwo();
            }*/
            
            else if ((SceneContext.GetScene() == SceneContext.Scene.Over) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE)))
            {
                SceneContext.SetState(SceneContext.Scene.Select);
            }

            if((SceneContext.GetScene() == SceneContext.Scene.Play) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_T)))
            {
                SpriteBatch pSB = SpriteBatchManager.Search(SpriteBatch.Name.Boxes);
                Debug.Assert(pSB != null);
                pSB.SetDrawEnable(false);
            }

            if ((SceneContext.GetScene() == SceneContext.Scene.Play) && (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Y)))
            {
                SpriteBatch pSB = SpriteBatchManager.Search(SpriteBatch.Name.Boxes);
                Debug.Assert(pSB != null);
                pSB.SetDrawEnable(true);
            }
            // Update the current scene.
            SceneContext.GetState().Update(this.GetTime());
        }
        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            // Draw the scene
            SceneContext.GetState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

    }
}

