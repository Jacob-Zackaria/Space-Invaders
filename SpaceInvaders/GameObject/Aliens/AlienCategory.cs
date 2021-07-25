using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            Squid,
            Crab,
            Octopus,
            Saucer,

            Column,
            Grid,

            Uninitialized
        }

        public AlienCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {

        }
    }
}
