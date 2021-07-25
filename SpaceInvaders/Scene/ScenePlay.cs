using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ScenePlay : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchManPlayer1;
        //public SpriteBatchManager poSpriteBatchManPlayer2;

        //public bool pausePlayUpdate = true;
        public int noOfLives = 3;

        AlienFactory AF = null;
        GameObject pGrid = null;

        //false means 1 player.
        //private bool numberOfPlayer = false;

        public ScenePlay()
        {
            this.Initialize();
            //this.numberOfPlayer = false;
        }
        public override void Handle()
        {

        }

        /*public void TwoPlayers()
        {
            this.numberOfPlayer = true;
        }

        public void OnePlayer()
        {
            this.numberOfPlayer = false;
        }*/

        public void RespawnGrid(float startPosition, float animTime)
        {
            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            GameObject pGameObj = null;
            

            for (int i = 0; i < 11; i++)
            {
                GameObject pCol = AF.CreateAliens(GameObject.Name.AlienColumn_0 + i, AlienCategory.Type.Column);

                pGameObj = AF.CreateAliens(GameObject.Name.Octopus, AlienCategory.Type.Octopus, 100.0f + (i * 55.0f), startPosition - 200.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.CreateAliens(GameObject.Name.Octopus, AlienCategory.Type.Octopus, 100.0f + (i * 55.0f), startPosition - 150.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.CreateAliens(GameObject.Name.Crab, AlienCategory.Type.Crab, 100.0f + (i * 55.0f), startPosition - 100.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.CreateAliens(GameObject.Name.Crab, AlienCategory.Type.Crab, 100.0f + (i * 55.0f), startPosition - 50.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.CreateAliens(GameObject.Name.Squid, AlienCategory.Type.Squid, 100.0f + (i * 55.0f), startPosition);
                pCol.Add(pGameObj);

                pGrid.Add(pCol);
            }

            GameObjectManager.Attach(pGrid);

            //---------------------------------------------------------------------------------------------------------
            // Timer
            //---------------------------------------------------------------------------------------------------------

            // Create an animation sprite
            AnimationSprite pAnimSprite = new AnimationSprite(GameSprite.Name.SquidA);

            // attach several images to cycle
            pAnimSprite.Attach(Image.Name.SquidB);
            pAnimSprite.Attach(Image.Name.SquidA);

            // add AnimationSprite to timer
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimSprite, animTime);

            pAnimSprite = new AnimationSprite(GameSprite.Name.CrabA);

            // attach several images to cycle
            pAnimSprite.Attach(Image.Name.CrabB);
            pAnimSprite.Attach(Image.Name.CrabA);

            // add AnimationSprite to timer
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimSprite, animTime);

            pAnimSprite = new AnimationSprite(GameSprite.Name.OctopusA);

            // attach several images to cycle
            pAnimSprite.Attach(Image.Name.OctopusB);
            pAnimSprite.Attach(Image.Name.OctopusA);

            // add AnimationSprite to timer
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, pAnimSprite, animTime);



            BombSpawnEvent pBombSpawn = new BombSpawnEvent(pGrid);
            // add BombSpawnEvent to timer
            TimerManager.Add(TimeEvent.Name.BombSpawn, pBombSpawn, 1.5f);
        }
        public override void Initialize()
        {
            //---------------------------------------------------------------------------------------------------------
            // Load the Texture.
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.SpaceInvaders, "SpaceInvaders.tga");
            TextureManager.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");

            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            ImageManager.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageManager.Add(Image.Name.OctopusB, Texture.Name.SpaceInvaders, 18, 3, 12, 8);
            ImageManager.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageManager.Add(Image.Name.CrabB, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageManager.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageManager.Add(Image.Name.SquidB, Texture.Name.SpaceInvaders, 72, 3, 8, 8);
            ImageManager.Add(Image.Name.AlienExplosion, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageManager.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);
            ImageManager.Add(Image.Name.SaucerExplosion, Texture.Name.SpaceInvaders, 118, 3, 21, 8);
            
            ImageManager.Add(Image.Name.Player, Texture.Name.SpaceInvaders, 3, 14, 13, 8);
            ImageManager.Add(Image.Name.PlayerExplosionA, Texture.Name.SpaceInvaders, 19, 14, 16, 8);
            ImageManager.Add(Image.Name.PlayerExplosionB, Texture.Name.SpaceInvaders, 38, 14, 16, 8);
            ImageManager.Add(Image.Name.AlienPullYA, Texture.Name.SpaceInvaders, 57, 14, 15, 8);
            ImageManager.Add(Image.Name.AlienPullYB, Texture.Name.SpaceInvaders, 75, 14, 15, 8);
            ImageManager.Add(Image.Name.AlienPullUpisdeDownYA, Texture.Name.SpaceInvaders, 93, 14, 14, 8);
            ImageManager.Add(Image.Name.AlienPullUpsideDownYB, Texture.Name.SpaceInvaders, 110, 14, 14, 8);
            
            ImageManager.Add(Image.Name.PlayerShot, Texture.Name.SpaceInvaders, 3, 29, 1, 4);
            ImageManager.Add(Image.Name.PlayerShotExplosion, Texture.Name.SpaceInvaders, 7, 25, 8, 8);
            ImageManager.Add(Image.Name.SquigglyShotA, Texture.Name.SpaceInvaders, 18, 26, 3, 7);
            ImageManager.Add(Image.Name.SquigglyShotB, Texture.Name.SpaceInvaders, 24, 26, 3, 7);
            ImageManager.Add(Image.Name.SquigglyShotC, Texture.Name.SpaceInvaders, 30, 26, 3, 7);
            ImageManager.Add(Image.Name.SquigglyShotD, Texture.Name.SpaceInvaders, 36, 26, 3, 7);
            ImageManager.Add(Image.Name.PlungerShotA, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageManager.Add(Image.Name.PlungerShotB, Texture.Name.SpaceInvaders, 48, 27, 3, 6);
            ImageManager.Add(Image.Name.PlungerShotC, Texture.Name.SpaceInvaders, 54, 27, 3, 6);
            ImageManager.Add(Image.Name.PlungerShotD, Texture.Name.SpaceInvaders, 60, 27, 3, 6);
            ImageManager.Add(Image.Name.RollingShotA, Texture.Name.SpaceInvaders, 65, 26, 3, 7);
            ImageManager.Add(Image.Name.RollingShotB, Texture.Name.SpaceInvaders, 70, 26, 3, 7);
            ImageManager.Add(Image.Name.RollingShotC, Texture.Name.SpaceInvaders, 75, 26, 3, 7);
            ImageManager.Add(Image.Name.RollingShotD, Texture.Name.SpaceInvaders, 80, 26, 3, 7);
            ImageManager.Add(Image.Name.AlienShotExplosion, Texture.Name.SpaceInvaders, 86, 25, 6, 8);
            
            ImageManager.Add(Image.Name.A, Texture.Name.SpaceInvaders, 3, 36, 5, 7);
            ImageManager.Add(Image.Name.B, Texture.Name.SpaceInvaders, 11, 36, 5, 7);
            ImageManager.Add(Image.Name.C, Texture.Name.SpaceInvaders, 19, 36, 5, 7);
            ImageManager.Add(Image.Name.D, Texture.Name.SpaceInvaders, 27, 36, 5, 7);
            ImageManager.Add(Image.Name.E, Texture.Name.SpaceInvaders, 35, 36, 5, 7);
            ImageManager.Add(Image.Name.F, Texture.Name.SpaceInvaders, 43, 36, 5, 7);
            ImageManager.Add(Image.Name.G, Texture.Name.SpaceInvaders, 51, 36, 5, 7);
            ImageManager.Add(Image.Name.H, Texture.Name.SpaceInvaders, 59, 36, 5, 7);
            ImageManager.Add(Image.Name.I, Texture.Name.SpaceInvaders, 67, 36, 5, 7);
            ImageManager.Add(Image.Name.J, Texture.Name.SpaceInvaders, 75, 36, 5, 7);
            ImageManager.Add(Image.Name.K, Texture.Name.SpaceInvaders, 83, 36, 5, 7);
            ImageManager.Add(Image.Name.L, Texture.Name.SpaceInvaders, 91, 36, 5, 7);
            ImageManager.Add(Image.Name.M, Texture.Name.SpaceInvaders, 99, 36, 5, 7);
            
            ImageManager.Add(Image.Name.N, Texture.Name.SpaceInvaders, 3, 46, 5, 7);
            ImageManager.Add(Image.Name.O, Texture.Name.SpaceInvaders, 11, 46, 5, 7);
            ImageManager.Add(Image.Name.P, Texture.Name.SpaceInvaders, 19, 46, 5, 7);
            ImageManager.Add(Image.Name.Q, Texture.Name.SpaceInvaders, 27, 46, 5, 7);
            ImageManager.Add(Image.Name.R, Texture.Name.SpaceInvaders, 35, 46, 5, 7);
            ImageManager.Add(Image.Name.S, Texture.Name.SpaceInvaders, 43, 46, 5, 7);
            ImageManager.Add(Image.Name.T, Texture.Name.SpaceInvaders, 51, 46, 5, 7);
            ImageManager.Add(Image.Name.U, Texture.Name.SpaceInvaders, 59, 46, 5, 7);
            ImageManager.Add(Image.Name.V, Texture.Name.SpaceInvaders, 67, 46, 5, 7);
            ImageManager.Add(Image.Name.W, Texture.Name.SpaceInvaders, 75, 46, 5, 7);
            ImageManager.Add(Image.Name.X, Texture.Name.SpaceInvaders, 83, 46, 5, 7);
            ImageManager.Add(Image.Name.Y, Texture.Name.SpaceInvaders, 91, 46, 5, 7);
            ImageManager.Add(Image.Name.Z, Texture.Name.SpaceInvaders, 99, 46, 5, 7);
            
            ImageManager.Add(Image.Name.Zero, Texture.Name.SpaceInvaders, 3, 56, 5, 7);
            ImageManager.Add(Image.Name.One, Texture.Name.SpaceInvaders, 11, 56, 5, 7);
            ImageManager.Add(Image.Name.Two, Texture.Name.SpaceInvaders, 19, 56, 5, 7);
            ImageManager.Add(Image.Name.Three, Texture.Name.SpaceInvaders, 27, 56, 5, 7);
            ImageManager.Add(Image.Name.Four, Texture.Name.SpaceInvaders, 35, 56, 5, 7);
            ImageManager.Add(Image.Name.Five, Texture.Name.SpaceInvaders, 43, 56, 5, 7);
            ImageManager.Add(Image.Name.Six, Texture.Name.SpaceInvaders, 51, 56, 5, 7);
            ImageManager.Add(Image.Name.Seven, Texture.Name.SpaceInvaders, 59, 56, 5, 7);
            ImageManager.Add(Image.Name.Eight, Texture.Name.SpaceInvaders, 67, 56, 5, 7);
            ImageManager.Add(Image.Name.Nine, Texture.Name.SpaceInvaders, 75, 56, 5, 7);
            ImageManager.Add(Image.Name.LessThan, Texture.Name.SpaceInvaders, 83, 56, 5, 7);
            ImageManager.Add(Image.Name.GreaterThan, Texture.Name.SpaceInvaders, 91, 56, 5, 7);
            ImageManager.Add(Image.Name.Space, Texture.Name.SpaceInvaders, 99, 56, 5, 7);
            ImageManager.Add(Image.Name.Equals, Texture.Name.SpaceInvaders, 107, 56, 5, 7);
            ImageManager.Add(Image.Name.Asterisk, Texture.Name.SpaceInvaders, 115, 56, 5, 7);
            ImageManager.Add(Image.Name.Question, Texture.Name.SpaceInvaders, 123, 56, 5, 7);
            ImageManager.Add(Image.Name.Hyphen, Texture.Name.SpaceInvaders, 131, 56, 5, 7);

            ImageManager.Add(Image.Name.Brick, Texture.Name.SpaceInvaders, 122, 35, 5, 2);
            ImageManager.Add(Image.Name.BrickLeft_Top0, Texture.Name.SpaceInvaders, 114, 31, 5, 2);
            ImageManager.Add(Image.Name.BrickLeft_Top1, Texture.Name.SpaceInvaders, 114, 33, 5, 2);
            ImageManager.Add(Image.Name.BrickLeft_Bottom, Texture.Name.SpaceInvaders, 119, 43, 5, 2);
            ImageManager.Add(Image.Name.BrickRight_Top0, Texture.Name.SpaceInvaders, 131, 31, 5, 2);
            ImageManager.Add(Image.Name.BrickRight_Top1, Texture.Name.SpaceInvaders, 131, 33, 5, 2);
            ImageManager.Add(Image.Name.BrickRight_Bottom, Texture.Name.SpaceInvaders, 125, 43, 5, 2);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------

            GameSpriteManager.Add(GameSprite.Name.OctopusA, Image.Name.OctopusA, 50, 500, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.OctopusB, Image.Name.OctopusB, 300, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.CrabA, Image.Name.CrabA, 400, 200, 45, 33);
            GameSpriteManager.Add(GameSprite.Name.CrabB, Image.Name.CrabB, 600, 200, 45, 33);
            GameSpriteManager.Add(GameSprite.Name.SquidA, Image.Name.SquidA, 50, 50, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.SquidB, Image.Name.SquidB, 72, 3, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.AlienExplosion, Image.Name.AlienExplosion, 83, 3, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Saucer, Image.Name.Saucer, 99, 3, 50, 35, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.SaucerExplosion, Image.Name.SaucerExplosion, 118, 3, 33, 33, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));

            GameSpriteManager.Add(GameSprite.Name.Player, Image.Name.Player, 3, 14, 40, 35, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.PlayerExplosionA, Image.Name.PlayerExplosionA, 19, 14, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.PlayerExplosionB, Image.Name.PlayerExplosionB, 38, 14, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.AlienPullYA, Image.Name.AlienPullYA, 57, 14, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.AlienPullYB, Image.Name.AlienPullYB, 75, 14, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.AlienPullUpisdeDownYA, Image.Name.AlienPullUpisdeDownYA, 93, 14, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.AlienPullUpsideDownYB, Image.Name.AlienPullUpsideDownYB, 110, 14, 33, 33);

            GameSpriteManager.Add(GameSprite.Name.PlayerShot, Image.Name.PlayerShot, 3, 29, 3, 15);
            GameSpriteManager.Add(GameSprite.Name.PlayerShotExplosion, Image.Name.PlayerShotExplosion, 7, 25, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.SquigglyShotA, Image.Name.SquigglyShotA, 18, 26, 10, 15);
            GameSpriteManager.Add(GameSprite.Name.SquigglyShotB, Image.Name.SquigglyShotB, 24, 26, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.SquigglyShotC, Image.Name.SquigglyShotC, 30, 26, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.SquigglyShotD, Image.Name.SquigglyShotD, 36, 26, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.PlungerShotA, Image.Name.PlungerShotA, 42, 27, 10, 15);
            GameSpriteManager.Add(GameSprite.Name.PlungerShotB, Image.Name.PlungerShotB, 48, 27, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.PlungerShotC, Image.Name.PlungerShotC, 54, 27, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.PlungerShotD, Image.Name.PlungerShotD, 60, 27, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.RollingShotA, Image.Name.RollingShotA, 65, 26, 10, 15);
            GameSpriteManager.Add(GameSprite.Name.RollingShotB, Image.Name.RollingShotB, 70, 26, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.RollingShotC, Image.Name.RollingShotC, 75, 26, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.RollingShotD, Image.Name.RollingShotD, 80, 26, 14, 14);
            GameSpriteManager.Add(GameSprite.Name.AlienShotExplosion, Image.Name.AlienShotExplosion, 86, 25, 33, 33);
                              
            GameSpriteManager.Add(GameSprite.Name.A, Image.Name.A, 3, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.B, Image.Name.B, 11, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.C, Image.Name.C, 19, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.D, Image.Name.D, 27, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.E, Image.Name.E, 35, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.F, Image.Name.F, 43, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.G, Image.Name.G, 51, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.H, Image.Name.H, 59, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.I, Image.Name.I, 67, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.J, Image.Name.J, 75, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.K, Image.Name.K, 83, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.L, Image.Name.L, 91, 36, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.M, Image.Name.M, 99, 36, 33, 33);
                                  
            GameSpriteManager.Add(GameSprite.Name.N, Image.Name.N, 3, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.O, Image.Name.O, 11, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.P, Image.Name.P, 19, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Q, Image.Name.Q, 27, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.R, Image.Name.R, 35, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.S, Image.Name.S, 43, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.T, Image.Name.T, 51, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.U, Image.Name.U, 59, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.V, Image.Name.V, 67, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.W, Image.Name.W, 75, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.X, Image.Name.X, 83, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Y, Image.Name.Y, 91, 46, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Z, Image.Name.Z, 99, 46, 33, 33);
                                 
            GameSpriteManager.Add(GameSprite.Name.Zero, Image.Name.Zero, 3, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.One, Image.Name.One, 11, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Two, Image.Name.Two, 19, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Three, Image.Name.Three, 27, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Four, Image.Name.Four, 35, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Five, Image.Name.Five, 43, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Six, Image.Name.Six, 51, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Seven, Image.Name.Seven, 59, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Eight, Image.Name.Eight, 67, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Nine, Image.Name.Nine, 75, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.LessThan, Image.Name.LessThan, 83, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.GreaterThan, Image.Name.GreaterThan, 91, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Space, Image.Name.Space, 99, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Equals, Image.Name.Equals, 107, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Asterisk, Image.Name.Asterisk, 115, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Question, Image.Name.Question, 123, 56, 33, 33);
            GameSpriteManager.Add(GameSprite.Name.Hyphen, Image.Name.Hyphen, 131, 56, 33, 33);
                        
            //BRICKS.
            GameSpriteManager.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
            GameSpriteManager.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 15, 10, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            //---------------------------------------------------------------------------------------------------------
            // Create Sounds.
            //---------------------------------------------------------------------------------------------------------
            SoundManager.NewSound(3, 1);
            SoundManager.Add(SoundSource.Name.Theme, "theme.wav");
            SoundManager.Add(SoundSource.Name.Missile, "shoot.wav");
            SoundManager.Add(SoundSource.Name.InvaderKilled, "invaderkilled.wav");
            SoundManager.Add(SoundSource.Name.ExplosionPlayer, "explosion.wav");
            SoundManager.Add(SoundSource.Name.Saucer, "ufo_lowpitch.wav");
            SoundManager.Add(SoundSource.Name.SaucerKilled, "ufo_highpitch.wav");
            SoundManager.Add(SoundSource.Name.AlienMarch1, "fastinvader1.wav");
            SoundManager.Add(SoundSource.Name.AlienMarch2, "fastinvader2.wav");
            SoundManager.Add(SoundSource.Name.AlienMarch3, "fastinvader3.wav");
            SoundManager.Add(SoundSource.Name.AlienMarch4, "fastinvader4.wav");

            //---------------------------------------------------------------------------------------------------------
            // Create BoxSprite
            //---------------------------------------------------------------------------------------------------------

            BoxSpriteManager.Add(BoxSprite.Name.Box1, 550.0f, 500.0f, 50.0f, 150.0f, new Azul.Color(1.0f, 1.0f, 1.0f, 1.0f));
            BoxSpriteManager.Add(BoxSprite.Name.Box2, 550.0f, 100.0f, 50.0f, 100.0f);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            /*if(this.numberOfPlayer)
            {
                this.poSpriteBatchManPlayer2 = new SpriteBatchManager(3, 1);
                SpriteBatchManager.SetActive(this.poSpriteBatchManPlayer2);
            }
            else
            {*/
                this.poSpriteBatchManPlayer1 = new SpriteBatchManager(3, 1);
                SpriteBatchManager.SetActive(this.poSpriteBatchManPlayer1);
           // }
            

            SpriteBatch pSB_Box = SpriteBatchManager.Add(SpriteBatch.Name.Boxes);
            pSB_Box.SetDrawEnable(false);
            SpriteBatchManager.Add(SpriteBatch.Name.Aliens);
            SpriteBatchManager.Add(SpriteBatch.Name.Shields);
            SpriteBatchManager.Add(SpriteBatch.Name.Bombs);
            SpriteBatchManager.Add(SpriteBatch.Name.Texts);
            SpriteBatchManager.Add(SpriteBatch.Name.Saucer);
            SpriteBatchManager.Add(SpriteBatch.Name.Player);
            SpriteBatchManager.Add(SpriteBatch.Name.Explosion);

            //---------------------------------------------------------------------------------------------------------
            // Add Fonts.
            //---------------------------------------------------------------------------------------------------------

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas36pt, 20, 980);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 350, 980);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas36pt, 710, 980);

            FontManager.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 25, 930);
            FontManager.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 370, 930);

            FontManager.Add(Font.Name.Life, SpriteBatch.Name.Texts, "3", Glyph.Name.Consolas36pt, 20, 20);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "CREDIT 00", Glyph.Name.Consolas36pt, 700, 20);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputManager.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

            Simulation.SetState(Simulation.State.Realtime);

            //---------------------------------------------------------------------------------------------------------
            // Bomb
            //---------------------------------------------------------------------------------------------------------

            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectManager.Attach(pBombRoot);

            //---------------------------------------------------------------------------------------------------------
            // Timer.
            //---------------------------------------------------------------------------------------------------------

            // Create an animation sprite
            PlayerExplosionEvent pEvent = new PlayerExplosionEvent(GameSprite.Name.PlayerExplosionA);

            // attach several images to cycle
            pEvent.Attach(Image.Name.PlayerExplosionB);
            pEvent.Attach(Image.Name.PlayerExplosionA);

            // add AnimationSprite to timer
            TimerManager.Add(TimeEvent.Name.AlienExplosion, pEvent, 0.1f);
            //---------------------------------------------------------------------------------------------------------
            // Walls
            //---------------------------------------------------------------------------------------------------------

            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateGameSprite(pSB_Box);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject, 448, 900, 850, 5);
            pWallTop.ActivateCollisionSprite(pSB_Box);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.NullObject, 448, 50, 850, 5);
            pWallBottom.ActivateCollisionSprite(pSB_Box);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, 870, 470, 5, 850);
            pWallRight.ActivateCollisionSprite(pSB_Box);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, 30, 470, 5, 850);
            pWallLeft.ActivateCollisionSprite(pSB_Box);

            // Add to the composite the children
            pWallGroup.Add(pWallTop);
            pWallGroup.Add(pWallBottom);
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);

            GameObjectManager.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Missile
            //---------------------------------------------------------------------------------------------------------

            // Missile Root
            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pMissileGroup.ActivateCollisionSprite(pSB_Box);

            GameObjectManager.Attach(pMissileGroup);

            //---------------------------------------------------------------------------------------------------------
            // Explosion Sprite.
            //---------------------------------------------------------------------------------------------------------

            // Explosion Root
            ExplosionRoot pExplosion = new ExplosionRoot(GameObject.Name.ExplosionRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pExplosion.ActivateCollisionSprite(pSB_Box);

            GameObjectManager.Attach(pExplosion);

            //---------------------------------------------------------------------------------------------------------
            // Ship
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pShipRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectManager.Attach(pShipRoot);

            ShipManager.NewShip();

            //---------------------------------------------------------------------------------------------------------
            // GRID SPAWN.
            //---------------------------------------------------------------------------------------------------------
            //ALIEN GRID SPAWN.
            AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);
            pGrid = AF.CreateAliens(GameObject.Name.AlienGrid, AlienCategory.Type.Grid, 0.0f, 0.0f, this);
            //Spawn Aliens and animation.
            this.RespawnGrid(850.0f, 0.7f);

            //---------------------------------------------------------------------------------------------------------
            // Saucer
            //---------------------------------------------------------------------------------------------------------
            SaucerRoot pSaucerRoot = new SaucerRoot(GameObject.Name.SaucerRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pSaucerRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectManager.Attach(pSaucerRoot);

            //Saucer Spawn.
            SaucerSpawnEvent pSaucerSpawn = new SaucerSpawnEvent();
            // add BombSpawnEvent to timer
            TimerManager.Add(TimeEvent.Name.SaucerSpawn, pSaucerSpawn, 15.0f);

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------

            // Create the factory ... prototype
            Composite pShieldRoot = (Composite)new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GameObjectManager.Attach(pShieldRoot);

            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

            for(int k = 0; k < 4; k++)
            {
                int j = 0;

                GameObject pColumn;

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                float start_x = 100.0f + (200.0f * k);
                float start_y = 200.0f;
                float off_x = 0;
                float brickWidth = 15.0f;
                float brickHeight = 10.0f;

                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, start_x, start_y + 9 * brickHeight);

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                SF.SetParent(pShieldRoot);
                pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                SF.SetParent(pColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

            }

            //---------------------------------------------------------------------------------------------------------
            // ColPair 
            //---------------------------------------------------------------------------------------------------------
            CollisionPair pColPair;

            // Missile vs Wall
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Wall, pMissileGroup, pWallGroup);
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());

            // Missile vs Shield
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Misslie_Shield, pMissileGroup, pShieldRoot);
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new ShipReadyObserver());

            // Missile vs Alien
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Alien, pMissileGroup, pGrid);
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveAlienObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new AlienExplosionObserver());

            //Missile vs Saucer
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Saucer, pMissileGroup, pSaucerRoot);
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveSaucerObserver());
            pColPair.Attach(new ShipReadyObserver());

            // Bomb vs Bottom Wall
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Wall, pBombRoot, pWallGroup);
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new SoundObserver());

            // Alien Grid vs Wall                                                                                
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, pGrid, pWallGroup);
            pColPair.Attach(new GridObserver());

            // Alien Grid vs Player                                                                                
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Player, pGrid, pShipRoot);
            pColPair.Attach(new EndObserver());

            // Bomb vs Player
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Player, pBombRoot, pShipRoot);
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new RemoveShipObserver());
            pColPair.Attach(new SoundObserver());
            //*
            //Diable shoot observer.
            //Disable move.

            // Bomb vs Shield
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new SoundObserver());

            // Alien vs Shield
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_Shield, pGrid, pShieldRoot);
            pColPair.Attach(new RemoveBrickObserver());

            // Missile vs Bomb
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Bomb, pMissileGroup, pBombRoot);
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new RemoveBombObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SoundObserver());

            // Player vs Wall
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Player_Wall, pShipRoot, pWallGroup);
            pColPair.Attach(new ShipMoveObserver());

            // Saucer vs Wall
            pColPair = CollisionPairManager.Add(CollisionPair.Name.Saucer_Wall, pSaucerRoot, pWallGroup);
            pColPair.Attach(new RemoveSaucerObserver());

        }

        public override void Update(float systemTime)
        {
            /*if (!pausePlayUpdate)
            {*/
                // Single Step, Free running...
                Simulation.Update(systemTime);

                //Play theme.
                if (!SoundManager.IsPlaying(SoundSource.Name.Theme))
                {
                    SoundManager.PlaySound(SoundSource.Name.Theme, true, false, false);
                    SoundManager.SetVolume(SoundSource.Name.Theme, 0.3f);
                }
                // Input
                InputManager.Update();

                // Run based on simulation stepping
                if (Simulation.GetTimeStep() > 0.0f)
                {
                    // Fire off the timer events
                    TimerManager.Update(Simulation.GetTotalTime());

                    // walk through all objects and push to flyweight
                    GameObjectManager.Update();

                    // Do the collision checks
                    CollisionPairManager.Process();

                    // Delete any objects here...
                    DelayedObjectMan.Process();
                }

            //}
            ///IF SHIP DIES AND PLAYER IS 1 AND LIFES ARE EMPTY TRANSITION to next scene.
        }
        public override void Draw()
        {
            // draw all objects
            SpriteBatchManager.Draw();
        }
        public override void Entering()
        {
            // update SpriteBatchMan()
            SpriteBatchManager.SetActive(this.poSpriteBatchManPlayer1);

            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;
            TimerManager.PauseUpdate(delta);
        }
        public override void Leaving()
        {

            // Need a better way to do this
            this.TimeAtPause = GlobalTimer.GetTime();
        }
        /*public override void Transition()
        {
            this.pausePlayUpdate = false;
            // update SpriteBatchMan()
            *//*if(this.numberOfPlayer)
            {
                SpriteBatchManager.SetActive(this.poSpriteBatchManPlayer2);
            }
            else
            {*//*
                SpriteBatchManager.SetActive(this.poSpriteBatchManPlayer1);
            //}
            
        }*/
        
    }
}
