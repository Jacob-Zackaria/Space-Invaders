using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteNodeMan_Link : Manager
    {
        public SpriteData_Link poActive = null;
        public SpriteData_Link poReserve = null;
    }
    public class SpriteDataManager : SpriteNodeMan_Link
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private readonly SpriteData pDataCompare;
        private SpriteBatch.Name name;
        private SpriteBatch pBackSpriteBatch;

        //---------------------------------------------------------------------------------------------------------
        //Constructor.
        //---------------------------------------------------------------------------------------------------------
        public SpriteDataManager(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point SBMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);
            this.pBackSpriteBatch = null;

            this.pDataCompare = new SpriteData();       //Initialize compare sprite data with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        //Methods.
        //---------------------------------------------------------------------------------------------------------
        public void Set(SpriteBatch.Name name, int initialReserveSize, int newGrowthSize)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Assign name.
            this.name = name;

            //Add to reserve list.
            this.BaseSetReserve(initialReserveSize, newGrowthSize);
        }

        //Attach Sprite.
        public SpriteData Attach(BaseSprite pNode)
        {
            //Add sprite data to active list.
            SpriteData pTmp = (SpriteData)this.BaseAdd();

            //Verify sprite data is added.
            Debug.Assert(pTmp != null);

            //Set new sprite data.
            pTmp.SetSpriteData(pNode, this);

            //Return added sprite data.
            return (pTmp);
        }

        public void Draw()
        {
            //Get active list.
            SpriteData pTmp = (SpriteData)this.BaseGetActive();

            //Traverse the list.
            while(pTmp != null)
            {
                //Render Base Sprite.
                pTmp.GetSpriteBase().Render();

                //Get next active base sprite.
                pTmp = (SpriteData)pTmp.pNext;
            }
        }

        public void Delete(SpriteData pNewData)
        {
            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            this.BaseDelete(pNewData);
        }

        //Print the manager details.
        public void Print()
        {
            //Print sprite data manager.
            this.BasePrint("SPRITE DATA");
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }
        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }

        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new sprite data.
            DLink pTmp = new SpriteData();

            //Verify new sprite data is created.
            Debug.Assert(pTmp != null);

            //Return new sprite data.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite data to compare.
            SpriteData pTmp1 = (SpriteData)pCompareWith;
            SpriteData pTmp2 = (SpriteData)pCompareTo;

            //Stubbed this function.
            //Compare with names.
            if (pTmp1 == pTmp2)
            {
                return false;        //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            //Convert link to sprite data.
            SpriteData pTmp = (SpriteData)pResetLink;

            //Reset sprite data.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to sprite data.
            SpriteData pTmp = (SpriteData)pPrintLink;
            //pTmp.ReturnName();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to sprite data.
            SpriteData pTmp = (SpriteData)pPrintLink;
            //return (pTmp.ReturnName().ToString());
            return null;
        }
    }
}
