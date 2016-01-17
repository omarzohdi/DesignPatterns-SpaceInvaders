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
    public enum ImageTag
    {
        Squid,
        Squid2,
        Octopus,
        Octopus2,
        Crab,
        Crab2,
        Ufo,
        Ship,
        Missile,
        Bomb,
        Shield,
        ShieldPart,
        ShieldPart2,
        ShieldPart3,
        BoundingBox,
        Explosion,
        Explosion2,
        Score_Font,
        Menu,
        Count
    }

    class Image
    {
        private Rectangle Rect;
        public ImageTag Name;
        private Texture _texture;
        public Texture.type type;

        public Image(Rectangle inRect, TexName Tex_Name, ImageTag inName)
        {
            Rect = inRect;
            _texture = TextureManager.getInstance().find(Tex_Name);
            type = _texture.Type;
            Name = inName;
        }

        public Texture getTexture()
        {
            return _texture;
        }

        public Rectangle getRectangle()
        {
            return Rect;
        }
        public BaseTexture getXnaTexture()
        {
            return _texture.getXnaTexture();
        }
    }
}
