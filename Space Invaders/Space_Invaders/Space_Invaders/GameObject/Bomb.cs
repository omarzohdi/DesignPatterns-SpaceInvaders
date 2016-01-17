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
    class Bomb : GameObj
    {
        public GameObjName ColumnName;
        bool Status = false;

        public Bomb(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite, ColGroupName inTempColGroupName)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            direction.Y += 5;
            TempColGroupName = inTempColGroupName;

            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
            sprite.setPosition(Position.X, Position.Y);
        }
        public void setRectangle(Rectangle inRect)
        {
            Position.X = inRect.X;
            Position.Y = inRect.Y;

            colObj.setRect(inRect);
            sprite.setRect(inRect);
        }

        public override void Update(GameTime gametime)
        {
            UpdateMovement(gametime);
            UpdateCollisionObject();
            UpdateSprite(gametime);
        }

        private void UpdateSprite(GameTime gametime)
        {
            sprite.setPosition(Position.X, Position.Y);
        }

        private void UpdateCollisionObject()
        {
            colObj.Update();
            colObj.UpdatePosition((int)(Position.X + Col_Off.X), (int)(Position.Y + Col_Off.Y));
        }

        private void UpdateMovement(GameTime gametime)
        {
            Position += direction;
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitBomb(this);
        }

        public override void VisitShield(Shield inShield)
        {
            if (inShield.CheckParts(this))
            {
                ((Super)(GameObjMananger.getInstance().Find(GameObjName.Super))).ReloadBomb(this);
            }
        }

        public void setColumnName(GameObjName inName)
        {
            ColumnName = inName;
        }
        public GameObjName getColumnName()
        {
            return ColumnName;
        }

        public void setStatus(bool inStatus)
        {
            Status = inStatus;
        }
        public bool getStatus()
        {
            return Status;
        }
    }
}
