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
    class Missile: GameObj
    {
        bool status = false;
        public Missile(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite, ColGroupName inTempColGroupName)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            direction.Y -= 10;
            TempColGroupName = inTempColGroupName;

            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
            sprite.setPosition(Position.X, Position.Y);
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
            if (status == true)
                Position += direction;
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitMissile(this);
        }

        public override void VisitSuper(Super inSuper)
        {
            if (inSuper.CheckColumns(this))
                ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).ReloadMissile();
        }
    
        public override void VisitUfo(UFO inUfo)
        {            
            inUfo.Kill();
            ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).ReloadMissile();
        }

        public override void VisitShield(Shield inShield)
        {
            if (inShield.CheckParts(this))
                ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).ReloadMissile();
        }

        public override void VisitBomb(Bomb inBomb)
        {
            ((Super)(GameObjMananger.getInstance().Find(GameObjName.Super))).ReloadBomb(inBomb);
            ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).ReloadMissile();
        }
        public void setStatus(bool inStatus)
        {
            status = inStatus; 
        }

        public bool getStatus()
        {
            return status;
        }

    }
}
