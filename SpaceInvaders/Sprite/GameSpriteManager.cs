using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameSpriteMan_MLink : Manager
    {
        public GameSprite_Base poActive;
        public GameSprite_Base poReserve;
    }
    public class GameSpriteManager : GameSpriteMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        //Static Data.
        //---------------------------------------------------------------------------------------------------------
        private static GameSpriteManager _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private readonly GameSprite pDataCompare;

        //---------------------------------------------------------------------------------------------------------
        //Private Constructor.
        //---------------------------------------------------------------------------------------------------------
        private GameSpriteManager(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new GameSprite();       //Initialize compare sprite with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        //Singleton Static Method.
        //---------------------------------------------------------------------------------------------------------
        public static void NewSprite(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if sprite manager is not present
            if (_pInstance == null)
            {
                //If not, create a new sprite manager.
                _pInstance = new GameSpriteManager(initialReserveSize, newGrowthSize);
            }

            //Null object texture.
            GameSprite pGSprite = GameSpriteManager.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
            Debug.Assert(pGSprite != null);
        }

        //---------------------------------------------------------------------------------------------------------
        //Static Methods.
        //---------------------------------------------------------------------------------------------------------
        //Add sprite to active list in manager.
        public static GameSprite Add(GameSprite.Name newName, Image.Name pImageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            //Get sprite manager instance.
            GameSpriteManager pMan = GameSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add sprite to active list.
            GameSprite pTmp = (GameSprite)pMan.BaseAdd();

            //Verify sprite is added.
            Debug.Assert(pTmp != null);

            //Search image with given image name.
            Image pImage = ImageManager.Search(pImageName);
            Debug.Assert(pImage != null);

            //Set new sprite data.
            pTmp.SetSprite(newName, pImage, x, y, width, height, pColor);

            //Print sprite added.
            Debug.WriteLine("\n\n***Added Sprite:\"" + newName + "\" to Active List***");

            //Return added sprite.
            return (pTmp);
        }

        //Search from active list.
        public static GameSprite Search(GameSprite.Name newName = GameSprite.Name.Uninitialized)
        {
            //Get sprite manager instance.
            GameSpriteManager pMan = GameSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing sprite with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing sprite.
            GameSprite pTmp = (GameSprite)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(GameSprite pNewData)
        {
            //Get sprite manager instance.
            GameSpriteManager pMan = GameSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get sprite manager instance.
            GameSpriteManager pMan = GameSpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print sprite manager.
            pMan.BasePrint("SPRITE");
        }

        public static void Destroy()
        {
            //Get sprite manager instance.
            GameSpriteManager pMan = GameSpriteManager.GetPrivateInstance();
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
            //Create new sprite.
            DLink pTmp = new GameSprite();

            //Verify new sprite is created.
            Debug.Assert(pTmp != null);

            //Return new sprite.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite to compare.
            GameSprite pTmp1 = (GameSprite)pCompareWith;
            GameSprite pTmp2 = (GameSprite)pCompareTo;

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
            //Convert link to sprite.
            GameSprite pTmp = (GameSprite)pResetLink;

            //Reset sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to sprite.
            GameSprite pTmp = (GameSprite)pPrintLink;

            //Print sprite details.
            pTmp.GetSprite();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to sprite.
            GameSprite pTmp = (GameSprite)pPrintLink;

            //Print sprite name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static GameSpriteManager GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }
    }
}
