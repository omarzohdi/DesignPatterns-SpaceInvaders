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
    enum TexName
    {
        Invaders,
        Missile,
        BBox,
        Shield,
        ShieldPart,
        ShieldPart2,
        ShieldPart3,
        Score_Font,
        Menu,
        None,
        Count
    }

    class Texture
    {
        public enum type
        {
            Text_Font,
            Text_Sprite,
            Text_Col
        }

        public string AssetName;
        public TexName Name;
        public Texture.type Type;

        private BaseTexture _texture;

        int height;
        int width;

        public Texture(TexName inTextName, Texture.type intype)
        {
            Name = inTextName;
            Type = intype;

            height = 0;
            width = 0;

            AssetName = "";
        }

        public Texture(string inName, TexName inTextName, Texture.type intype, int inheight = 1, int inwidth = 1)
        {
            AssetName = inName;
            Name = inTextName;
            
            Type = intype;

            height = inheight;
            width = inwidth;
        }

        public BaseTexture getXnaTexture()
        {
            return _texture;
        }
        public void setXnaTexture(BaseTexture inTexture)
        {
            _texture = inTexture;
        }
    }
}
