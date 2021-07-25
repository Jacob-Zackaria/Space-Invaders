using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteData_Link : DLink
    {

    }
    public class SpriteData : SpriteData_Link
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private BaseSprite pBaseSprite;
        private SpriteDataManager pBackSpriteDataMan;

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteData()
        : base()
        {
            this.pBaseSprite = null;
            this.pBackSpriteDataMan = null;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods.
        //---------------------------------------------------------------------------------------------------------
        //Used for Game Sprite.
        public void SetSpriteData(BaseSprite pNode, SpriteDataManager _pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pBaseSprite = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pBaseSprite != null);
            this.pBaseSprite.SetSpriteData(this);

            Debug.Assert(_pSpriteNodeMan != null);
            this.pBackSpriteDataMan = _pSpriteNodeMan;
        }

        //Get Base sprite.
        public BaseSprite GetSpriteBase()
        {
            return this.pBaseSprite;
        }

        public SpriteDataManager GetSBDataManager()
        {
            Debug.Assert(this.pBackSpriteDataMan != null);
            return this.pBackSpriteDataMan;
        }

        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteDataMan != null);
            return this.pBackSpriteDataMan.GetSpriteBatch();
        }

        //Clean.
        public void Clean()
        {
            this.pBaseSprite = null;
        }
    }
}
