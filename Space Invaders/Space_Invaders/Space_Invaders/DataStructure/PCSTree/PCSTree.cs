using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    class PCSTree
    {
        TreeNode root;

        public PCSTree(){}

        public TreeNode getRoot()
        {
            return root;
        }

        public void AddNode(Object inData, NodeName inName, NodeName parentName)
        {
            TreeNode inNode = new TreeNode(inData,inName);

            if (root == null)
            {
                root = inNode;
            }
            else 
            {
                TreeNode ptr = root;

               if (ptr.Name == parentName)
               {
                   AddChild(root,inNode);
               }
               else
               {
                   TreeNode parent = findNode(parentName);
                   AddChild(parent, inNode);
               }
            }

        }

        private void AddChild(TreeNode parent,TreeNode Child)
        {
            TreeNode ptr = parent;

            if (ptr.pChild == null)
            {
                ptr.pChild = Child;
                Child.pParent = parent;
            }
            else
            {
                ptr = ptr.pChild;
                if (ptr.pSibling != null)
                {
                    while (ptr.pSibling != null)
                    {
                        ptr = ptr.pSibling;
                    }
                }

                ptr.pSibling = Child;
                Child.pParent = parent;
            }
        }

        public TreeNode getNode(NodeName _name)
        {
            TreeNode node = findNode(_name);
            return node;
        }

        private TreeNode findNode(NodeName inName)
        {
            TreeNode ptr = root;
            if (ptr.Name == inName)
                return ptr;

            while (ptr != null)
            {
                if (ptr.pChild != null)
                {
                    ptr = ptr.pChild;

                    while (ptr != null)
                    {
                         if (ptr.Name == inName)
                               return ptr;

                        if (ptr.pSibling != null)
                            ptr = ptr.pSibling;
                        else
                            break;
                    }

                    ptr = ptr.pParent;
                    ptr = ptr.pSibling;

                    if (ptr.Name == inName)
                        return ptr;
                }
            }

            return ptr;
        }

        public TreeNode Find(GameObj inObj)
        {
            TreeNode ptr = root;
            if (ptr.getData().Equals(inObj))
                return ptr;

            while (ptr != null)
            {
                if (ptr.pChild != null)
                {
                    ptr = ptr.pChild;

                    while (ptr != null)
                    {
                        if (ptr.getData().Equals(inObj))
                            return ptr;

                        if (ptr.pSibling != null)
                            ptr = ptr.pSibling;
                        else
                            break;
                    }

                    ptr = ptr.pParent;
                    ptr = ptr.pSibling;

                    if (ptr.getData().Equals(inObj))
                        return ptr;
                }
            }

            return ptr;
        }

        public void Remove(TreeNode inNode)
        {
            TreeNode Next;
            TreeNode Prev;

            if (root == inNode)
                root = null;

            Prev = inNode.pParent.pChild;
            Next = inNode.pSibling;

            while (Prev.pChild != null)
            {
                if (Prev.pSibling == inNode)
                {
                    break;
                }

                Prev = Prev.pSibling;
            }

            inNode.pParent = null;
            Prev.pSibling = Next;
        }
    }
}
