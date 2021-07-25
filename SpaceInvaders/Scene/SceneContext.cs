﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneContext
    {

        // ----------------------------------------------------
        // Data: 
        // ----------------------------------------------------     
        private static SceneContext _pInstance = null;

        SceneState pSceneState;
        DemoScene poSceneDemo;
        SceneSelect poSceneSelect;
        SceneOver poSceneOver;
        ScenePlay poScenePlay;
        Scene sceneName;

        public enum Scene
        {
            Demo,
            Select,
            Play,
            Over
        }
        private SceneContext()
        {
            // reserve the states
            this.poSceneDemo = new DemoScene();
            this.poSceneSelect = new SceneSelect();
            this.poSceneOver = new SceneOver();

            // initialize to the select state
            this.pSceneState = this.poSceneDemo;
            this.sceneName = SceneContext.Scene.Demo;
            this.pSceneState.Entering();
        }

        public static void NewSceneContext()
        {
            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if Scene context is not present
            if (_pInstance == null)
            {
                //If not, create a new Scene context.
                _pInstance = new SceneContext();
            }
        }

        public static SceneState GetState()
        {
            //Get texture manager instance.
            SceneContext pMan = SceneContext.GetPrivateInstance();
            Debug.Assert(pMan != null);

            return (pMan.pSceneState);
        }

        public static SceneContext.Scene GetScene()
        {
            //Get texture manager instance.
            SceneContext pMan = SceneContext.GetPrivateInstance();
            Debug.Assert(pMan != null);

            return (pMan.sceneName);
        }

        /*public static void SetPlayerTwo()
        {
            //Get texture manager instance.
            SceneContext pMan = SceneContext.GetPrivateInstance();
            Debug.Assert(pMan != null);

            pMan.poScenePlay.TwoPlayers();
        }*/

        /*public static void SetPlayerOne()
        {
            //Get texture manager instance.
            SceneContext pMan = SceneContext.GetPrivateInstance();
            Debug.Assert(pMan != null);

            pMan.poScenePlay.OnePlayer();
        }*/

        public static void SetState(Scene eScene)
        {
            //Get texture manager instance.
            SceneContext pMan = SceneContext.GetPrivateInstance();
            Debug.Assert(pMan != null);

            switch (eScene)
            {
                case Scene.Select:
                    // pMan.poSceneDemo.pauseDemoUpdate = true;
                    pMan.pSceneState.Leaving();
                    pMan.poScenePlay = new ScenePlay();
                    pMan.pSceneState = pMan.poSceneSelect;
                    pMan.sceneName = SceneContext.Scene.Select;
                    pMan.pSceneState.Entering();
                    break;

                case Scene.Play:
                    //pMan.poSceneSelect.pauseSelectUpdate = true;
                    pMan.pSceneState.Leaving();
                    pMan.pSceneState = pMan.poScenePlay;
                    pMan.sceneName = SceneContext.Scene.Play;
                    pMan.pSceneState.Entering();
                    break;

                case Scene.Over:
                    //pMan.poScenePlay.pausePlayUpdate = true;
                    pMan.pSceneState.Leaving();
                    pMan.pSceneState = pMan.poSceneOver;
                    pMan.sceneName = SceneContext.Scene.Over;
                    pMan.pSceneState.Entering();
                    break;

                case Scene.Demo:
                    //pMan.poSceneOver.pauseOverUpdate = true;
                    pMan.pSceneState.Leaving();
                    pMan.pSceneState = pMan.poSceneDemo;
                    pMan.sceneName = SceneContext.Scene.Demo;
                    pMan.pSceneState.Entering();
                    break;

            }
        }

        private static SceneContext GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }


    }
}
