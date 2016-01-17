using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    class ShieldPart : GameObj
    {
        Rectangle tempRect;
        public int Health = 4;

        public ShieldPart(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
        }

        public override void Update(GameTime gametime)
        {
            UpdateCollisionObject(tempRect);
        }
        private void UpdateCollisionObject(Rectangle tempRect)
        {
            colObj.Update();

            colObj.UpdatePosition((int)(Position.X + Col_Off.X), (int)(Position.Y + Col_Off.Y));
            colObj.UpdateSize(tempRect.Width, tempRect.Height);
        }
    }
}
