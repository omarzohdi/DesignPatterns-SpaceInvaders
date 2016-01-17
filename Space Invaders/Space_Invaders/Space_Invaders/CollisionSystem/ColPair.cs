using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    class ColPair
    {
        ColGroup CollidingGroupA;
        ColGroup CollidingGroupB;

        public ColPair(ColGroup inColGrpA, ColGroup inColGrpB)
        {
            CollidingGroupA = inColGrpA;
            CollidingGroupB = inColGrpB;
        }

        public void CollideGroups()
        {
            ListNode ptrA = CollidingGroupA.getHead();
            ListNode ptrB = CollidingGroupB.getHead();
            bool Collide = false;

            while (ptrB != null)
            {
                GameObj ObjB = ((GameObj)(ptrB.getData()));
                ptrA = CollidingGroupA.getHead();

                while (ptrA != null)
                {
                    GameObj ObjA = ((GameObj)(ptrA.getData()));

                    if (ObjA != null && ObjB != null)
                        Collide = isCollide(ObjA, ObjB);

                    if (Collide == true)
                    {
                        ObjA.Accept(ObjB);
                        Collide = false;

                        return;
                    }

                    ptrA = (ListNode)ptrA.pNext;
                }

                ptrB = (ListNode)ptrB.pNext;
            }
        }

        public ColGroup getColGroupA()
        {
            return CollidingGroupA;
        }

        public ColGroup getColGroupB()
        {
            return CollidingGroupB;
        }

        public void ReplaceColGroup(ColGroup inColGroup)
        {
            if (inColGroup.Equals(CollidingGroupA))
            {
                CollidingGroupA = inColGroup;
            }

            if (inColGroup.Equals(CollidingGroupB))
            {
                CollidingGroupB = inColGroup;
            }
        }
        private bool isCollide(GameObj ObjA, GameObj ObjB)
        {
            Rectangle RectA = ObjA.getCollisionObjRectangle();
            Rectangle RectB = ObjB.getCollisionObjRectangle();

            return RectA.Intersects(RectB);
        }
    }
}
