using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneSelect : SceneState
    {
        //public bool pauseSelectUpdate = true;
        public SceneSelect()
        {
            this.Initialize();
        }
        public override void Handle()
        {
            Debug.WriteLine("Handle");
        }
        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActive(this.poSpriteBatchMan);

            SpriteBatchManager.Add(SpriteBatch.Name.Texts);

            TextureManager.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas36pt, 20, 980);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 350, 980);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas36pt, 710, 980);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 25, 930);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 370, 930);
            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 730, 930);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PRESS", Glyph.Name.Consolas36pt, 370, 530);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "1  PLAYER BUTTON", Glyph.Name.Consolas36pt, 270, 450);

            FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "CREDIT 08", Glyph.Name.Consolas36pt, 680, 100);
        }
        public override void Update(float systemTime)
        { 
            // walk through all objects and push to flyweight
            GameObjectManager.Update();
        }

        public override void Draw()
        {
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

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchManager poSpriteBatchMan;

    }
}
