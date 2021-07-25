using System.Diagnostics;

namespace SpaceInvaders
{
    public class CollisionObject
    {
        // -------------------------------------------------------
        // Data:
        // -------------------------------------------------------
        public BoxSprite pColSprite;
        public CollisionRectangle pColRect;

        // -------------------------------------------------------
        // Constructor:
        // -------------------------------------------------------
        public CollisionObject(ProxySprite pProxySprite)
        {
            Debug.Assert(pProxySprite != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            GameSprite pSprite = pProxySprite.pSprite;
            Debug.Assert(pSprite != null);

            // Origin is in the UPPER RIGHT 
            this.pColRect = new CollisionRectangle(pSprite.GetScreenRect());
            Debug.Assert(this.pColRect != null);

            // Create the sprite
            this.pColSprite = BoxSpriteManager.Add(BoxSprite.Name.Box, this.pColRect.x, this.pColRect.y, this.pColRect.width, this.pColRect.height);
            Debug.Assert(this.pColSprite != null);
            this.pColSprite.SetLineColor(1.0f, 0.0f, 0.0f);
        }

        public void UpdatePos(float x, float y)
        {
            this.pColRect.x = x;
            this.pColRect.y = y;

            this.pColSprite.x = this.pColRect.x;
            this.pColSprite.y = this.pColRect.y;

            this.pColSprite.SetScreenRect(this.pColRect.x, this.pColRect.y, this.pColRect.width, this.pColRect.height);
            this.pColSprite.Update();
        }

    }
}