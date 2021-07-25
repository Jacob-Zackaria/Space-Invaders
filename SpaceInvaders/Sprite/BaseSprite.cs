using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class BaseSprite : DLink
    {
        private SpriteData pBackSpriteNode;
        public BaseSprite()
        :   base()
        {
            this.pBackSpriteNode = null;
        }

        public SpriteData GetSpriteData()
        {
            Debug.Assert(this.pBackSpriteNode != null);
            return this.pBackSpriteNode;
        }
        public void SetSpriteData(SpriteData pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pBackSpriteNode = pSpriteBatchNode;
        }

        //Abstract functions that derived class should implement.
        abstract public void Update();
        abstract public void Render();
    }
}
