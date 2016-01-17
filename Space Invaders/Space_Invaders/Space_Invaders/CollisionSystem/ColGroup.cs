using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{

    class ColGroup
    {
        LinkedList List;
        public ColGroupName Name;

        public ColGroup(ColGroupName inName)
        {
            Name = inName;
            List = new LinkedList(5, 3, NodeType.GameObj);
        }

        public void Add(GameObj inObj)
        {
            List.Add(inObj);
        }

        public ListNode getHead()
        {

            return  (ListNode)List.getActiveHead();
        }

        public LinkedList getList()
        {
            return List;
        }

        public void Kill(GameObj inObj)
        {
            ListNode inNode = (ListNode)List.Find(inObj);
            if (inNode != null)
                List.Remove(inNode);
        }

    }
}
