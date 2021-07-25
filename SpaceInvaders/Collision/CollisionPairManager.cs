using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColPairMan_MLink : Manager
    {
        public ColPair_Link poActive;
        public ColPair_Link poReserve;
    }
    public class CollisionPairManager : ColPairMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        //Static Data.
        //---------------------------------------------------------------------------------------------------------
        private static CollisionPairManager _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private CollisionPair pDataCompare;
        private CollisionPair pActiveColPair;

        //---------------------------------------------------------------------------------------------------------
        //Private Constructor.
        //---------------------------------------------------------------------------------------------------------
        private CollisionPairManager(int initialReserveSize = 2, int newGrowthSize = 2)
        : base()     //Delegate.
        {
            // At this point collision pair manager is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            // no link... used in Process
            this.pActiveColPair = null;

            this.pDataCompare = new CollisionPair();            //Initialize compare image with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        //Singleton Static Method.
        //---------------------------------------------------------------------------------------------------------
        //Create new collision pair manager.
        public static void NewCollisionPair(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if collision pair manager is not present
            if (_pInstance == null)
            {
                //If not, create a new collision pair manager.
                _pInstance = new CollisionPairManager(initialReserveSize, newGrowthSize);
            }
        }

        //Add to active List.
        public static CollisionPair Add(CollisionPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            //Get collision pair manager instance.
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //else, add collision pair to active list.
            CollisionPair pTmp = (CollisionPair)pMan.BaseAdd();

            //Verify collision pair is added.
            Debug.Assert(pTmp != null);

            //Set new collision pair data.
            pTmp.SetCollisionPair(colpairName, treeRootA, treeRootB);

            //Print collision pair added.
            Debug.WriteLine("\n\n***Added Collision Pair:\"" + colpairName + "\" to Active List***");

            //Return added collision pair.
            return (pTmp);
        }

        //Search from active list.
        public static CollisionPair Search(CollisionPair.Name newName)
        {
            //Get collision pair manager instance.
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing collision pair with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing collision pair.
            CollisionPair pTmp = (CollisionPair)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(CollisionPair pNewData)
        {
            //Get collision pair manager instance.
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify collision pair is present to delete.
            Debug.Assert(pNewData != null);

            //Delete collision pair from active list.
            pMan.BaseDelete(pNewData);
        }

        public static void Process()
        {
            // get the singleton.
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            CollisionPair pColPair = (CollisionPair)pMan.BaseGetActive();

            while (pColPair != null)
            {
                // set the current active  <--- Key concept: set this before
                pMan.pActiveColPair = pColPair;

                // do the check for a single pair
                pColPair.Process();

                // advance to next
                pColPair = (CollisionPair)pColPair.pNext;
            }
        }

        //Print the manager details.
        public static void Print()
        {
            //Get collision pair manager instance.
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print collision pair manager.
            pMan.BasePrint("COLLISION PAIR");
        }

        public static void Destroy()
        {
            //Get collision pair manager instance.
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all collision pair data.
            Print();

            //Delete the collision pair manager.
            _pInstance = null;
        }

        static public CollisionPair GetActiveCollisionPair()
        {
            // get the singleton
            CollisionPairManager pMan = CollisionPairManager.GetPrivateInstance();

            return pMan.pActiveColPair;
        }

        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new collision pair.
            DLink pTmp = new CollisionPair();

            //Verify new collision pair is created.
            Debug.Assert(pTmp != null);

            //Return new collision pair.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            // This is used in baseSearch() 
            Debug.Assert(pCompareWith != null);
            Debug.Assert(pCompareTo != null);

            //Convert links to collision pair to compare.
            CollisionPair pTmp1 = (CollisionPair)pCompareWith;
            CollisionPair pTmp2 = (CollisionPair)pCompareTo;

            //Compare with names.
            if (pTmp1.ReturnName() == pTmp2.ReturnName())
            {
                return true;        //If name is equal.
            }

            return false;          //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            Debug.Assert(pResetLink != null);

            //Convert link to collision pair.
            CollisionPair pTmp = (CollisionPair)pResetLink;

            //Reset collision pair.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to collision pair.
            CollisionPair pTmp = (CollisionPair)pPrintLink;

            //Print collision pair details.
            pTmp.GetCollisionPair();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to collision pair.
            CollisionPair pTmp = (CollisionPair)pPrintLink;

            //Print collision pair name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static CollisionPairManager GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }
    }
}
