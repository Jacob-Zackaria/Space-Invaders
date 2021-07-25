using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ProxySpriteMan_MLink : Manager
    {
        public ProxySprite_Base poActive;
        public ProxySprite_Base poReserve;
    }
    public class ProxySpriteManager : ProxySpriteMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        //Static Data.
        //---------------------------------------------------------------------------------------------------------
        private static ProxySpriteManager _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private ProxySprite pDataCompare;

        //----------------------------------------------------------------------
        //Private Constructor.
        //----------------------------------------------------------------------
        private ProxySpriteManager(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new ProxySprite();  //Initialize compare proxy sprite with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        //Singleton Static Method.
        //---------------------------------------------------------------------------------------------------------
        //Create new proxy.
        public static void NewProxy(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if proxy sprite manager is not present
            if (_pInstance == null)
            {
                //If not, create a new proxy sprite manager.
                _pInstance = new ProxySpriteManager(initialReserveSize, newGrowthSize);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //Static Methods.
        //---------------------------------------------------------------------------------------------------------
        //Add proxy sprite to active list in manager.
        public static ProxySprite Add(GameSprite.Name newName)
        {
            //Get proxy sprite manager instance.
            ProxySpriteManager pMan = ProxySpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add proxy sprite to active list.
            ProxySprite pTmp = (ProxySprite)pMan.BaseAdd();

            //Verify sprite is added.
            Debug.Assert(pTmp != null);

            //Set new proxy sprite data.
            pTmp.SetProxy(newName);

            //Print proxy sprite added.
            Debug.WriteLine("\n\n***Added Proxy Sprite:\"" + newName + "\" to Active List***");

            //Return added proxy sprite.
            return (pTmp);
        }

        public static ProxySprite Search(ProxySprite.Name newName)
        {
            //Get proxy sprite manager instance.
            ProxySpriteManager pMan = ProxySpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing proxy sprite with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing sprite.
            ProxySprite pTmp = (ProxySprite)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(GameSprite pNewData)
        {
            //Get proxy sprite manager instance.
            ProxySpriteManager pMan = ProxySpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify image is present to delete.
            Debug.Assert(pNewData != null);

            //Delete image from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get proxy sprite manager instance.
            ProxySpriteManager pMan = ProxySpriteManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print proxy sprite manager.
            pMan.BasePrint("PROXY SPRITE");
        }

        public static void Destroy()
        {
            //Get proxy sprite manager instance.
            ProxySpriteManager pMan = ProxySpriteManager.GetPrivateInstance();
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
            //Create new proxy sprite.
            DLink pTmp = new ProxySprite();

            //Verify new proxy sprite is created.
            Debug.Assert(pTmp != null);

            //Return new proxy sprite.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite to compare.
            ProxySprite pTmp1 = (ProxySprite)pCompareWith;
            ProxySprite pTmp2 = (ProxySprite)pCompareTo;

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
            //Convert link to proxy sprite.
            ProxySprite pTmp = (ProxySprite)pResetLink;

            //Reset proxy sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to proxy sprite.
            ProxySprite pTmp = (ProxySprite)pPrintLink;

            //Print proxy sprite details.
            pTmp.GetProxy();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to proxy sprite.
            ProxySprite pTmp = (ProxySprite)pPrintLink;

            //Print proxy sprite name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static ProxySpriteManager GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }
    }
}
