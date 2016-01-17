using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{
    enum NodeType
    {
        ColObj,
        GameObj,
        Image,
        Sprite,
        Texture,
        SprBatch,
        GameSpr,
        ColGrp,
        ColPrs,
        Shield,
        None,
    }
    enum NodeName
    {
        Super,
        Col1,
        Col2,
        Col3,
        Col4,
        Col5,
        Col6,
        Col7,
        Col8,
        Col9,
        Col10,
        Col11,
        Wall,
        Alien,
        Shield,
        Missile
    }

    abstract class Node
    {
        public Node pNext;
        public Node pPrev;
        protected Object data;
    }

    class ListNode: Node
    {
        public void setData(Object indata)
        {
            data = indata;
        }
        public Object getData()
        {
            return data;
        }
        public void purge()
        {
            data = null;
        }
    }

    class TreeNode: Node
    {
        public TreeNode pParent;
        public TreeNode pChild;
        public TreeNode pSibling;
        public NodeName Name;

        public TreeNode(Object indata, NodeName inName)
        {
            data = indata;
            Name = inName;
        }

        public void setData(Object indata)
        {
            data = indata;
        }
        public Object getData()
        {
            return data;
        }
        public void purge()
        {
            data = null;
 
        }
    }
}
