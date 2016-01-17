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
    class Manager
    {
        protected LinkedList List;
        protected int deltaGrow;
        protected bool initialized = false;
        protected int Totalnum = 0;

        public virtual bool Initialize(int BufferSize = 10, int delta = 3)
        {
            deltaGrow = delta;
            List = new LinkedList(BufferSize, delta, NodeType.None);
            initialized = true;

            return initialized;
        }

        public void Add(Object inObj)
        {
            List.Add(inObj);
        }
        public void Remove(Node inNode)
        {
            List.Remove(inNode);
        }
    }
}
