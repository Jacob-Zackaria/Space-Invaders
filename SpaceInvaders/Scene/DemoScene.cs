using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DemoScene : SceneState
    {
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchMan;
       // public bool pauseDemoUpdate = true;
        GameSprite pOctopus, pCrab, pSquid, pSaucer;
        public DemoScene()
        {
            this.Initialize();
        }

        public override void Handle()
        {
            
        }

        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchMan);

            SpriteBatchManager.Add(SpriteBatch.Name.Texts);

            TextureManager.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            TextureManager.Add(Texture.Name.SpaceInvaders, "SpaceInvaders.tga");

            ImageManager.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageManager.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageManager.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageManager.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);

            pOctopus = GameSpriteManager.Add(GameSprite.Name.OctopusA, Image.Name.OctopusA, 310, 250, 49, 33, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            pCrab = GameSpriteManager.Add(GameSprite.Name.CrabA, Image.Name.CrabA, 310, 300, 45, 33);

            pSquid = GameSpriteManager.Add(GameSprite.Name.SquidA, Image.Name.SquidA, 310, 350, 33, 33);

            pSaucer = GameSpriteManager.Add(GameSprite.Name.Saucer, Image.Name.Saucer, 310, 400, 50, 35);
            


            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas36pt, 20, 980);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 350, 980);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas36pt, 710, 980);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 25, 930);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 370, 930);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 730, 930);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PLAY", Glyph.Name.Consolas36pt, 370, 730);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SPACE   INVADERS", Glyph.Name.Consolas36pt, 270, 630);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "*SCORE  ADVANCE  TABLE*", Glyph.Name.Consolas36pt, 200, 500);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=?  MYSTERY", Glyph.Name.Consolas36pt, 350, 400);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=30  POINTS", Glyph.Name.Consolas36pt, 350, 350);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=20  POINTS", Glyph.Name.Consolas36pt, 350, 300);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=10  POINTS", Glyph.Name.Consolas36pt, 350, 250);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "(Press Space to Continue)", Glyph.Name.Consolas36pt, 170, 150);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "CREDIT 00", Glyph.Name.Consolas36pt, 680, 100);
        }

        public override void Update(float systemTime)
        {
            
            this.pOctopus.Update();
            this.pSquid.Update();
            this.pCrab.Update();
            this.pSaucer.Update();


            // walk through all objects and push to flyweight
            GameObjectManager.Update(); 
        }

        public override void Draw()
        {
            this.pOctopus.Render();
            this.pSquid.Render();
            this.pCrab.Render();
            this.pSaucer.Render();
            // draw all objects
            SpriteBatchManager.Draw();
        }

        public override void Entering()
        {
            // update SpriteBatchMan()
            SpriteBatchManager.SetActive(this.poSpriteBatchMan);
        }
        public override void Leaving()
        {
            // update SpriteBatchMan()
            this.TimeAtPause = TimerManager.GetCurrTime();
        }
    }
}
