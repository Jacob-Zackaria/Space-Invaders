using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationSprite : Command
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private readonly GameSprite pSprite;
        private SLink pCurrImage;
        private SLink pFirstImage;
        private int i = 0;

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public AnimationSprite(GameSprite.Name spriteName)
        {
            //Get the game sprite.
            this.pSprite = GameSpriteManager.Search(spriteName);

            //Verify game sprite is added.
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrImage = null;

            // list
            this.pFirstImage = null;
            this.i = 0;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods.
        //---------------------------------------------------------------------------------------------------------


        public void Attach(Image.Name imageName)
        {
            //Search for image.
            Image pImage = ImageManager.Search(imageName);

            //Verify image.
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            SLink.AddToFront(ref this.pFirstImage, pImageHolder);

            // Set the first one to this image
            this.pCurrImage = pImageHolder;
        }

        /*public void PauseAnim(bool val)
        {
            this.timePause = val;
        }*/
        public override void Execute(float deltaTime)
        {
            if(i == 0)
            {
                SoundManager.PlaySound(SoundSource.Name.AlienMarch1, false, false, false);
            }
            else if(i == 1)
            {
                SoundManager.PlaySound(SoundSource.Name.AlienMarch2, false, false, false);
            }
            else if (i == 2)
            {
                SoundManager.PlaySound(SoundSource.Name.AlienMarch3, false, false, false);
            }
            else if (i == 3)
            {
                SoundManager.PlaySound(SoundSource.Name.AlienMarch4, false, false, false);
                i = 0;
            }
            i++;

            // Move the grid
            AlienGrid pGrid = (AlienGrid)GameObjectManager.Search(GameObject.Name.AlienGrid);
            pGrid.MoveGrid();

            // advance to next image 
            ImageHolder pImageHolder = (ImageHolder)this.pCurrImage.pSNext;

            // if at end of list, set to first
            if (pImageHolder == null)
            {
                pImageHolder = (ImageHolder)pFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrImage = pImageHolder;

            // change image
            this.pSprite.SwapImage(pImageHolder.pImage);

            //SPEED CONTROL>
            deltaTime = ((pGrid.GetAlienCount() + (deltaTime * 25.0f)) / 150.0f);
            if (deltaTime < 0.05f)
            {
                deltaTime = 0.05f;
            }

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.SpriteAnimation, this, deltaTime);
        }
    }
}

