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
    class Wall : GameObj
    {
        public Wall(GameObjName inName, ColObj inColObj, Vector2 inPos)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = null;

            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
        }

        public override void Update(GameTime gametime)
        {
            UpdateCollisionObject(gametime);
        }

        private void UpdateCollisionObject(GameTime gametime)
        {
            colObj.Update();
            colObj.UpdatePosition((int)(Position.X + Col_Off.X), (int)(Position.Y + Col_Off.Y));
        }

        public override void Accept(GameObj inObj)
        {
        }

        public override void VisitSuper(Super inSuper)
        {
            ReactToWall(inSuper);
        }
        
        public override void VisitMissile(Missile inMiss)
        {
            ReactToWall(inMiss);
        }
        public override void VisitBomb(Bomb inBomb)
        {
            ReactToWall(inBomb);
        }

        private void ReactToWall(Bomb inBomb)
        {
            ((Super)(GameObjMananger.getInstance().Find(GameObjName.Super))).ReloadBomb(inBomb);
        }
        private void ReactToWall(Missile inMiss)
        {
            ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).ReloadMissile();
        }
        private void ReactToWall(Super inSuper)
        {
            if (inSuper.Step)
            {
                inSuper.direction.X *= -1;
                inSuper.direction.Y = 5;
                inSuper.Step = false;
            }
        }
    }
}
