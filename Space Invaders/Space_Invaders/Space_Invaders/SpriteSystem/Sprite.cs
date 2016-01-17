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
    public enum SpriteName
    {
        Crab,
        Squid,
        Octopus,
        Ship,
        Ufo,
        BBox,
        Missile,
        Bomb,
        BombRev,
        Shield,
        ShieldPart,
        ShieldPart2,
        ShieldPart3,
        Explosion,
        Explosion2,
        Explosion3,
        Score_Font,
        HIScore_Val,
        Score_Val,
        Menu,
        Count
    }

    class Sprite
    {
        private Image _image;
        private SpriteEffects Effect;
        private Color color;
        private SpriteName Name;

        public Sprite(ImageTag inTag, SpriteEffects inEffect, Color inColor, SpriteName inName)
        {
            _image = ImageManager.getInstance().find(inTag);
            Effect = inEffect;
            color = inColor;
            Name = inName;
        }
        
        public BaseTexture getXnaTexture()
        {
            return _image.getTexture().getXnaTexture();
        }
        public Texture getTexture()
        {
            return _image.getTexture();
        }
        public Image getImage()
        {
            return _image;
        }
        public SpriteEffects getEffect()
        {
            return Effect;
        }
        public Color getColor()
        {
            return color;
        }
        public SpriteName getName()
        {
            return Name;
        }
        public void setImage(Image inImage)
        {
            _image = inImage;
        }
    }
}
