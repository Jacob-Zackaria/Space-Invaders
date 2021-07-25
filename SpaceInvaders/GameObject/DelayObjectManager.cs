﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DelayedObjectMan
    {
        // -------------------------------------------
        // Data: 
        // -------------------------------------------

        private CollisionObserver head;
        private static DelayedObjectMan instance = null;
        static public void Attach(CollisionObserver observer)
        {
            // protection
            Debug.Assert(observer != null);

            DelayedObjectMan pDelayMan = DelayedObjectMan.PrivGetInstance();

            // add to front
            if (pDelayMan.head == null)
            {
                pDelayMan.head = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }
            else
            {
                observer.pNext = pDelayMan.head;
                observer.pPrev = null;
                pDelayMan.head.pPrev = observer;
                pDelayMan.head = observer;
            }
        }
        private void PrivDetach(CollisionObserver node, ref CollisionObserver head)
        {
            // protection
            Debug.Assert(node != null);

            if (node.pPrev != null)
            {	// middle or last node
                node.pPrev.pNext = node.pNext;
            }
            else
            {  // first
                head = (CollisionObserver)node.pNext;
            }

            if (node.pNext != null)
            {	// middle node
                node.pNext.pPrev = node.pPrev;
            }
        }
        static public void Process()
        {
            DelayedObjectMan pDelayMan = DelayedObjectMan.PrivGetInstance();

            CollisionObserver pNode = pDelayMan.head;

            while (pNode != null)
            {
                // Fire off listener
                pNode.Execute();

                pNode = (CollisionObserver)pNode.pNext;
            }


            // remove
            pNode = pDelayMan.head;
            CollisionObserver pTmp = null;

            while (pNode != null)
            {
                pTmp = pNode;
                pNode = (CollisionObserver)pNode.pNext;

                // remove
                pDelayMan.PrivDetach(pTmp, ref pDelayMan.head);
            }
        }
        private DelayedObjectMan()
        {
            this.head = null;
        }
        private static DelayedObjectMan PrivGetInstance()
        {
            // Do the initialization
            if (instance == null)
            {
                instance = new DelayedObjectMan();
            }

            // Safety - this forces users to call create first
            Debug.Assert(instance != null);

            return instance;
        }   
    }
}
