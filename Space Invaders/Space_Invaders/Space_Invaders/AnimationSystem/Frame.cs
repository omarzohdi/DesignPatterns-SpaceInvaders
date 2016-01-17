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
    class Frame
    {
        private SpriteName SprName;
        private int Order;

        public Frame(SpriteName inSprName, int inOrder)
        {
            SprName = inSprName;
            Order = inOrder;
        }

        public SpriteName getSpriteName()
        {
            return SprName;
        }

        public int getOrder()
        {
            return Order;
        }
    }
}
