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
    class BaseSprite
    {
        protected Rectangle Rect;
        protected Vector2 Scale;
        protected float Rotation;
        protected float Depth;
    }

    class GameSprite: BaseSprite
    {
        protected Sprite _sprite;
        protected Texture.type type;

        public void setPosition(float inX, float inY)
        {
            Rect.X = (int)inX;
            Rect.Y = (int)inY;
        }

        public virtual void Draw(SpriteBatch spritebatch){}

        public void FlipSprite(SpriteName inImage)
        {
            _sprite = SpriteManager.getInstance().find(inImage);
        }
        public void setRect(Rectangle inRect)
        {
            Rect = inRect;
        }
        public Rectangle getRectangle()
        {
            return Rect;
        }

    }

    class TextureSprite : GameSprite
    {
        public TextureSprite(Rectangle inRect, Vector2 inScale, float inRot, float indepth, SpriteName inName)
        {
            Rect = inRect;
            Scale = inScale;
            Rotation = inRot;
            Depth = indepth;

            _sprite = SpriteManager.getInstance().find(inName);

            type = Texture.type.Text_Sprite;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteTexture Sprite = (SpriteTexture)_sprite.getXnaTexture();
            spriteBatch.Draw(Sprite.src, Rect,_sprite.getImage().getRectangle(),_sprite.getColor(),Rotation,Vector2.Zero,_sprite.getEffect(),Depth);
        }


    }

    class FontSprite : GameSprite
    {
        String Text;

        public FontSprite(Rectangle inRect, Vector2 inScale, float inRot, float indepth, SpriteName inName, String inText)
        {
            Rect = inRect;
            Scale = inScale;
            Rotation = inRot;
            Text = inText;
            _sprite = SpriteManager.getInstance().find(inName);
            Depth = indepth;

            type = Texture.type.Text_Font;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            FontTexture Sprite = (FontTexture)_sprite.getXnaTexture();
            spriteBatch.DrawString(Sprite.src, Text,new Vector2(Rect.X,Rect.Y), _sprite.getColor(), Rotation, Vector2.Zero, Scale, _sprite.getEffect(), Depth);
        }

        public void changeText(string inText)
        {
            Text = inText;
        }
    }

    class ColSprite : GameSprite
    {
        public ColSprite(Rectangle inRect, Vector2 inScale, float inRot, float indepth, SpriteName inName)
        {
            Rect = inRect;
            Scale = inScale;
            Rotation = inRot;
            Depth = indepth;

            _sprite = SpriteManager.getInstance().find(inName);

            type = Texture.type.Text_Col;
        }

        public void setRectangle(Rectangle inRect)
        {
            Rect = inRect;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            ColTexture colSprite = (ColTexture)_sprite.getXnaTexture();

            Texture2D text = colSprite.src;
            Color color = _sprite.getColor();

            int thickness = 1;

            spritebatch.Draw(text, new Rectangle(Rect.X, Rect.Y, Rect.Width, thickness), color);
            spritebatch.Draw(text, new Rectangle(Rect.X, Rect.Y, thickness, Rect.Height), color);
            spritebatch.Draw(text, new Rectangle((Rect.X + Rect.Width - thickness), Rect.Y, thickness, Rect.Height), color);
            spritebatch.Draw(text, new Rectangle(Rect.X, Rect.Y + Rect.Height - thickness, Rect.Width, thickness), color);
        }
    }
}
