using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneOver : SceneState
    {
        //public bool pauseOverUpdate = true;
        public SceneOver()
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
            GlyphManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            Font pFont = FontManager.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "GAME OVER", Glyph.Name.Consolas36pt, 370, 530);
            pFont.SetColor(1.0f, 0.0f, 0.0f);
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
