using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ProxySprite_Base : BaseSprite
    {

    }
    public class ProxySprite : ProxySprite_Base
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private Name name;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public GameSprite pSprite;

        //---------------------------------------------------------------------------------------------------------
        //Enum.
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Proxy,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        //Constructor.
        //---------------------------------------------------------------------------------------------------------
        // Create a single sprite.
        public ProxySprite()
        :   base()
        {
            this.name = ProxySprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pSprite = null;
        }

        //---------------------------------------------------------------------------------------------------------
        //Specialized Constructor.
        //---------------------------------------------------------------------------------------------------------
        public ProxySprite(GameSprite.Name name)
        {
            //Assign name.
            this.name = ProxySprite.Name.Proxy;

            //Reset data.
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            //Search Game Sprite.
            this.pSprite = GameSpriteManager.Search(name);

            //Verify Sprite is found.
            Debug.Assert(this.pSprite != null);
        }

        //---------------------------------------------------------------------------------------------------------
        //Methods.
        //---------------------------------------------------------------------------------------------------------
        public void SetProxy(GameSprite.Name name)
        {
            //Assign name.
            this.name = ProxySprite.Name.Proxy;

            //Assign data.
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            //Search Game Sprite.
            this.pSprite = GameSpriteManager.Search(name);

            //Verify Sprite is found.
            Debug.Assert(this.pSprite != null);
        }

        public override void Update()
        {
            //Push data from proxy to real game sprite.
            this.PrivPushToReal();
            this.pSprite.Update();
        }

        private void PrivPushToReal()
        {
            //Push data from proxy to real game sprite.
            Debug.Assert(this.pSprite != null);

            //Assign sprite data with data.
            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        public override void Render()
        {
            //Push data from proxy to real game sprite.
            this.PrivPushToReal();

            //Update and render.
            this.pSprite.Update();
            this.pSprite.Render();
        }

        //Set name.
        public void SetName(Name newName)
        {
            this.name = newName;
        }

        //Return name.
        public Name ReturnName()
        {
            return (this.name);
        }

        //Return game sprite name.
        public GameSprite.Name ReturnSpriteName()
        {
            return (this.pSprite.ReturnName());
        }

        //Print proxy sprite details.
        public void GetProxy()
        {
            if(this.pSprite == null)
            {
                //Print name of proxy sprite.
                Debug.WriteLine("\nName: {0}  Sprite Name: null\n", this.ReturnName());
            }
            else
            {
                //Print name of proxy sprite, sprite name.
                Debug.WriteLine("\nName: {0}  Sprite Name: {1}\n", this.ReturnName(), this.pSprite.ReturnName());
            }
            

            //Check if previous sprite exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else    //else print the previous sprite name.
            {
                ProxySprite pTmp = (ProxySprite)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next sprite exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else    //else print the next sprite name.
            {
                ProxySprite pTmp = (ProxySprite)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        //Reset data.
        public new void ClearLink()
        {
            //Assign name.
            this.name = ProxySprite.Name.Uninitialized;

            //Reset data.
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            //Assign sprite.
            this.pSprite = null;
        }

        public void Clean()
        {
            base.ClearLink();
            this.ClearLink();
        }
    }
}
