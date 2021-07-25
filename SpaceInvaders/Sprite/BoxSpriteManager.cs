using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BoxSpriteMan_MLink : Manager
    {
        public BoxSprite_Base poActive;
        public BoxSprite_Base poReserve;
    }
    class BoxSpriteManager : BoxSpriteMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        //Static Data.
        //---------------------------------------------------------------------------------------------------------
        private static BoxSpriteManager _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private readonly BoxSprite pDataCompare;

        //---------------------------------------------------------------------------------------------------------
        //Private Constructor.
        //---------------------------------------------------------------------------------------------------------
        private BoxSpriteManager(int initialReserveSize = 2, int newGrowthSize = 2)
        : base()   //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new BoxSprite();    //Initialize compare box sprite with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        //Singleton Static Method.
        //---------------------------------------------------------------------------------------------------------
        public static void NewBoxSprite(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if box sprite manager is not present
            if (_pInstance == null)
            {
                //If not, create a new box sprite manager.
                _pInstance = new BoxSpriteManager(initialReserveSize, newGrowthSize);
            }
        }

        //Add to active List.
        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color newColor = null)
        {
            //Get box sprite manager instance.
            BoxSpriteManager pMan = BoxSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add box sprite to active list.
            BoxSprite pTmp = (BoxSprite)pMan.BaseAdd();

            //Verify box sprite is added.
            Debug.Assert(pTmp != null);

            //Set new box sprite data.
            pTmp.SetBox(name, x, y, width, height, newColor);

            //Print box sprite added.
            Debug.WriteLine("\n\n***Added Box Sprite to Active List***");

            //Return added box sprite.
            return (pTmp);
        }

        //Search from active list.
        public static BoxSprite Search(BoxSprite.Name newName)
        {
            //Get box sprite manager instance.
            BoxSpriteManager pMan = BoxSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing box sprite with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing box sprite.
            BoxSprite pTmp = (BoxSprite)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(BoxSprite pNewData)
        {
            //Get box sprite manager instance.
            BoxSpriteManager pMan = BoxSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get box sprite manager instance.
            BoxSpriteManager pMan = BoxSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print box sprite manager.
            pMan.BasePrint("BOX SPRITE");
        }

        public static void Destroy()
        {
            //Get box sprite manager instance.
            BoxSpriteManager pMan = BoxSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all sprite data.
            Print();

            //Delete the sprite manager.
            _pInstance = null;
        }

        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new box sprite.
            DLink pTmp = new BoxSprite();

            //Verify new sprite is created.
            Debug.Assert(pTmp != null);

            //Return new sprite.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to box sprite to compare.
            BoxSprite pTmp1 = (BoxSprite)pCompareWith;
            BoxSprite pTmp2 = (BoxSprite)pCompareTo;

            //Compare with names.
            if (pTmp1.ReturnName() == pTmp2.ReturnName())
            {
                return true;        //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            //Convert link to box sprite.
            BoxSprite pTmp = (BoxSprite)pResetLink;

            //Reset box sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to box sprite.
            BoxSprite pTmp = (BoxSprite)pPrintLink;

            //Print box sprite details.
            pTmp.GetBoxSprite();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to box sprite.
            BoxSprite pTmp = (BoxSprite)pPrintLink;

            //Print box sprite name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static BoxSpriteManager GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }
    }
}
