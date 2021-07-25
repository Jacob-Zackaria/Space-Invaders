using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SaucerCategory : Leaf
    {
        public enum Type
        {
            Saucer,
            SaucerRoot,
            Unitialized
        }

        public SaucerCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {

        }
    }
}
