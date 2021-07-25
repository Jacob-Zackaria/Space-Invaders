using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class FontMan_MLink : Manager
    {
        public Font_DLink poActive;
        public Font_DLink poReserve;
    }
    class FontManager : FontMan_MLink
    {
        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private static FontManager pInstance = null;
        private Font pRefNode;
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private FontManager(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.pRefNode = (Font)this.derivedCreateData();
        }
        ~FontManager()
        {

        }

        //----------------------------------------------------------------------
        // Static Manager methods can be implemented with base methods 
        // Can implement/specialize more or less methods your choice
        //----------------------------------------------------------------------
        public static void NewFont(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new FontManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {

        }
        public static Font Add(Font.Name name, SpriteBatch.Name SB_Name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            FontManager pMan = FontManager.GetPrivateInstance();

            Font pNode = (Font)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage, glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchManager.Search(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.pFontSprite != null);
            pSB.Attach(pNode.pFontSprite);

            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            GlyphManager.AddXml(glyphName, assetName, textName);
        }

        public static void Delete(Glyph pNode)
        {
            Debug.Assert(pNode != null);
            FontManager pMan = FontManager.GetPrivateInstance();
            pMan.BaseDelete(pNode);
        }
        public static Font Search(Font.Name name)
        {
            FontManager pMan = FontManager.GetPrivateInstance();

            // Compare functions only compares two Nodes
            pMan.pRefNode.name = name;

            Font pData = (Font)pMan.BaseSearch(pMan.pRefNode);
            return pData;
        }


        public static void Print()
        {
            FontManager pMan = FontManager.GetPrivateInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrint("FONT");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected Boolean derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Font pDataA = (Font)pLinkA;
            Font pDataB = (Font)pLinkB;

            Boolean status = false;

            if (pDataA.ReturnName() == pDataB.ReturnName())
            {
                status = true;
            }

            return status;
        }
        override protected DLink derivedCreateData()
        {
            DLink pNode = new Font();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected void derivedReset(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;
            pNode.Clean();
        }

        override protected void derivedPrint(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;

            Debug.Assert(pNode != null);
            pNode.GetFont();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to image.
            Font pTmp = (Font)pPrintLink;

            //Print image name.
            return (pTmp.ReturnName());
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static FontManager GetPrivateInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }       
    }
}
