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
    abstract class BaseTexture
    {

    }

    class SpriteTexture : BaseTexture
    {
        public Texture2D src;

        public SpriteTexture(Texture2D inSrc)
        {
            src = inSrc;
        }
    }

    class FontTexture : BaseTexture
    {
        public SpriteFont src;

        public FontTexture(SpriteFont inSrc)
        {
            src = inSrc;
        }
    }

    class ColTexture : BaseTexture
    {
        public Texture2D src;

        public ColTexture(GraphicsDevice GraphDev)
        {
            src = new Texture2D(GraphDev, 1, 1);
            src.SetData(new Color[] { Color.White });
        }    
    }
}
