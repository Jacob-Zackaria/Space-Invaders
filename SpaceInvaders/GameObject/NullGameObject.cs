﻿using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class NullGameObject : Leaf
    {
        public NullGameObject()
        :   base(GameObject.Name.NullObject, GameSprite.Name.NullObject)
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an NullGameObject
            // Call the appropriate collision reaction            
            other.VisitNullGameObject(this);

        }
        public override void Update()
        {
            // do nothing - its a null object
        }
    }
}
