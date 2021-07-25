using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ImageMan_Link : Manager
    {
        public Image_Link poActive;
        public Image_Link poReserve;
    }
    public class ImageManager : ImageMan_Link
    {
        //---------------------------------------------------------------------------------------------------------
        //Static Data.
        //---------------------------------------------------------------------------------------------------------
        private static ImageManager _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private readonly Image pDataCompare;

        //---------------------------------------------------------------------------------------------------------
        //Private Constructor.
        //---------------------------------------------------------------------------------------------------------
        private ImageManager(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new Image();            //Initialize compare image with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        //Singleton Static Method.
        //---------------------------------------------------------------------------------------------------------
        //Create new image manager.
        public static void NewImage(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if image manager is not present
            if (_pInstance == null)
            {
                //If not, create a new image manager.
                _pInstance = new ImageManager(initialReserveSize, newGrowthSize);
            }

            //Null object image.
            Image pImage = ImageManager.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 128, 128);
            Debug.Assert(pImage != null);

            //Default image.
            pImage = ImageManager.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 128, 128);
            Debug.Assert(pImage != null);
        }

        //Add to active List.
        public static Image Add(Image.Name newName, Texture.Name pTextureName, float x, float y, float width, float height)
        {
            //Get image manager instance.
            ImageManager pMan = ImageManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //else, add image to active list.
            Image pTmp = (Image)pMan.BaseAdd();

            //Verify image is added.
            Debug.Assert(pTmp != null);

            //Search the texture manager for corresponding texture.
            Texture pTexture = TextureManager.Search(pTextureName);
            Debug.Assert(pTexture != null);

            //Set new image data.
            pTmp.SetImage(newName, pTexture, x, y, width, height);

            //Print image added.
            Debug.WriteLine("\n\n***Added Image:\"" + newName + "\" to Active List***");

            //Return added image.
            return (pTmp);
        }

        //Search from active list.
        public static Image Search(Image.Name newName)
        {
            //Get image manager instance.
            ImageManager pMan = ImageManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing texture with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing texture.
            Image pTmp = (Image)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(Image pNewData)
        {
            //Get image manager instance.
            ImageManager pMan = ImageManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify image is present to delete.
            Debug.Assert(pNewData != null);

            //Delete image from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get image manager instance.
            ImageManager pMan = ImageManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print image manager.
            pMan.BasePrint("IMAGE");
        }

        public static void Destroy()
        {
            //Get image manager instance.
            ImageManager pMan = ImageManager.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all image data.
            Print();

            //Delete the image manager.
            _pInstance = null;
        }

        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new image.
            DLink pTmp = new Image();

            //Verify new image is created.
            Debug.Assert(pTmp != null);

            //Return new image.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            // This is used in baseSearch() 
            Debug.Assert(pCompareWith != null);
            Debug.Assert(pCompareTo != null);

            //Convert links to image to compare.
            Image pTmp1 = (Image)pCompareWith;
            Image pTmp2 = (Image)pCompareTo;

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

            //Convert link to image.
            Image pTmp = (Image)pResetLink;

            //Reset image.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to image.
            Image pTmp = (Image)pPrintLink;

            //Print image details.
            pTmp.GetImage();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to image.
            Image pTmp = (Image)pPrintLink;

            //Print image name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static ImageManager GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }
    }
}
