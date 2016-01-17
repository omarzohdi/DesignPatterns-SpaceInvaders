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
    enum ColObjName
    {
        Super,
        Column,
        Wall,
        Crab,
        Squid,
        Ship,
        Shield,
        ShieldPart,
        Missile,
        UFO,
        Count
    }

    class ColObj
    {
        ColObjName Name;
        Rectangle rect;
        ColSprite _ColSprite;

        public ColObj (Rectangle inRect, ColObjName inCName, SpriteName inSName)
        {
            Name = inCName;
            rect = inRect;
            _ColSprite = new ColSprite(Rectangle.Empty, new Vector2(1, 1), 0.0f, 1.0f, inSName);
            if (_ColSprite != null)
                SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).Add(_ColSprite);
        }

        public Rectangle getRect()
        {
            return rect;
        }

        public ColSprite getColSprite()
        {
            return _ColSprite;
        }

        public void setRect(Rectangle inRect)
        {
            rect = inRect;
        }

        public void Update()
        {
            if (_ColSprite != null)
                _ColSprite.setRectangle(rect);
        }

        public void UpdatePosition(int X, int Y)
        {
            rect.X = X;
            rect.Y = Y;

            if (_ColSprite != null)
                _ColSprite.setRectangle(rect);
        }

        public void UpdateSize(int Width, int Height)
        {
            rect.Width = Width;
            rect.Height = Height;

            if (_ColSprite != null)
                _ColSprite.setRectangle(rect);
        }
    }
}
