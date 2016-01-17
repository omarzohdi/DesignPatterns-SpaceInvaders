using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{
    class LinkedList
    {
        private Node pHeadActive;
        private Node pHeadInActive;
        private int DeltaGrowth;
        private int BufferSize;
        private NodeType type;

        public Node getActiveHead()
        {
            return pHeadActive;
        }
        public Node getInActiveHead()
        {
            return pHeadInActive;
        }
        public int getSize()
        {
            return BufferSize;
        }

        public LinkedList(int TotalSize, int Delta, NodeType intype)
        {
            BufferSize = 0;
            Grow(TotalSize);
            DeltaGrowth = Delta;
            type = intype;
        }

        public void Grow(int Size)
        {
            BufferSize += Size;
            Node inNode;

            for (int i = 0; i < Size; ++i)
            {
                inNode = new ListNode();

                if (pHeadInActive == null)
                    pHeadInActive = inNode;
                else
                {
                    pHeadInActive.pPrev = inNode;
                    inNode.pNext = pHeadInActive;
                    pHeadInActive = inNode;
                }
            }
        }

        public void Add(Object Obj)
        {
            if (pHeadInActive == null)
                Grow(DeltaGrowth);

            Node inNode = pHeadInActive;
            ((ListNode)inNode).setData(Obj);

            pHeadInActive = inNode.pNext;
            inNode.pNext = null;
            
            if (pHeadInActive!= null)
                pHeadInActive.pPrev = null;
            
            if (pHeadActive == null)
            {
                pHeadActive = inNode;
            }
            else 
            {
                inNode.pNext = pHeadActive;    
                pHeadActive.pPrev = inNode;
                pHeadActive = pHeadActive.pPrev;
            }
        }

        public void Remove(Node inNode)
        {
            Node tempNode = inNode;

            if (inNode.pPrev == null)
            {
                if (pHeadActive == inNode)
                    pHeadActive = inNode.pNext;                
            }

            if (inNode.pNext != null)
            {
                tempNode = inNode.pNext;
                tempNode.pPrev = inNode.pPrev;
            }

            if (inNode.pPrev != null)
            {
                tempNode = inNode.pPrev;
                tempNode.pNext = inNode.pNext;
            }



            if (pHeadInActive == null)
                pHeadInActive = inNode;
            else
            {
                pHeadInActive.pPrev = inNode;
                inNode.pNext = pHeadInActive;
                pHeadInActive = inNode;
                pHeadInActive.pPrev = null;
            }
        }

        public Node Find(GameObj inObj)
        {
            Node ptr = pHeadActive;
            
            while(ptr != null)
            {
                if (((ListNode)ptr).getData().Equals(inObj))
                    return ptr;

                ptr = ptr.pNext;
            }

            return null;
        }

        public Object getDatabyIndex(int index)
        {
            int i = 0;
            Node tempActiveNode = pHeadActive;

            while (tempActiveNode != null)
            {
                if (index == i)
                    return ((ListNode)tempActiveNode).getData();

                tempActiveNode = tempActiveNode.pNext;
                i++;
            }

            return null;
        }

    }
}
