using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatch_Link : DLink
    {

    }
    public class SpriteBatch : SpriteBatch_Link
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        public SpriteBatch.Name name;
        private bool drawEnable;
        private readonly SpriteDataManager _pSpriteDataManager;

        //---------------------------------------------------------------------------------------------------------
        //Enum.
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Missile,
            Explosion,
            Saucer,
            Aliens,
            Bombs,
            Boxes,
            Texts,
            Shields,
            Player,

            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteBatch()
        :   base()
        {
            this.drawEnable = true;
            this.name = SpriteBatch.Name.Uninitialized;
            this._pSpriteDataManager = new SpriteDataManager();
            Debug.Assert(this._pSpriteDataManager != null);
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods.
        //---------------------------------------------------------------------------------------------------------
        //Set sprite data.
        public void Set(SpriteBatch.Name name, int reserveSize = 2, int growSize = 2)
        {
            //Assign name.
            this.name = name;

            //Set sprite data manager.
            this._pSpriteDataManager.Set(name, reserveSize, growSize);
        }

        //Attach Sprite.
        public void Attach(BaseSprite pNode)
        {
            //verify base sprite is present.
            Debug.Assert(pNode != null);

            //Attach sprite data.
            SpriteData pTmp = (SpriteData)this._pSpriteDataManager.Attach(pNode);

            //Verify data is attached.
            Debug.Assert(pTmp != null);

            //Set sprite data.
            pTmp.SetSpriteData(pNode, this._pSpriteDataManager);

            // Back pointer
            this._pSpriteDataManager.SetSpriteBatch(this);
        }

        public void SetDrawEnable(bool status)
        {
            this.drawEnable = status;
        }
        public bool GetDrawEnable()
        {
            return this.drawEnable;
        }

        //Set sprite batch name.
        public void SetName(SpriteBatch.Name newName)
        {
            this.name = newName;
        }

        //Return name.
        public SpriteBatch.Name ReturnName()
        {
            return (this.name);
        }

        //Return sprite batch data manager.
        public SpriteDataManager GetSBDataManager()
        {
            return (this._pSpriteDataManager);
        }

        public void GetSpriteBatch()
        {
            //Print name of sprite batch.
            Debug.WriteLine("\nName: {0}\n", this.ReturnName());

            //Check if previous sprite batch exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else    //else print the previous sprite batch name.
            {
                SpriteBatch pTmp = (SpriteBatch)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next sprite batch exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else    //else print the next sprite batch name.
            {
                SpriteBatch pTmp = (SpriteBatch)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }     
        }

        public void Clean()
        {
            
        }
    }
}
