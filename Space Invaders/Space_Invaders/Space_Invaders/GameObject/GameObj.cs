using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    interface ICollidable
    {
        void Accept(GameObj inObj);

    }
    enum GameObjName
    {
        Alien,
        Ship,
        UFO,
        Wall,
        Super,
        Column1,
        Column2,
        Column3,
        Column4,
        Column5,
        Column6,
        Column7,
        Column8,
        Column9,
        Column10,
        Column11,
        Missile,
        Bomb,
        Shield,
        ShieldPart,
        Life,
        LivesUI,
        Score,
        HiScore,
        None
    }
    class GameObj
    {
        protected Vector2 Col_Off;
        public Vector2 Position;
        protected GameObjName Name;
        protected ColObj colObj;
        protected GameSprite sprite;
        public Vector2 direction;
        public ColGroupName TempColGroupName;

        protected GameObj()
        {
            Name = GameObjName.None;
            Position = Vector2.Zero;
            colObj = null;
            sprite = null;
            direction = Vector2.Zero;

            Rectangle tempRect = Rectangle.Empty;
            Col_Off = Vector2.Zero;
        }
        public GameObj(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            direction = Vector2.Zero;

            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
            sprite.setPosition(Position.X, Position.Y);
        }

        public GameObjName getName()
        {
            return Name;
        }
        public ColObj getColObj()
        {
            return colObj;
        }
        public GameSprite getGameSprite()
        {
            return sprite;
        }
        public void FlipSprite(SpriteName inImage)
        {
            sprite.FlipSprite(inImage);
        }

        public virtual void Update(GameTime gametime){}

        public void setDir(Vector2 inDir)
        {
            direction = inDir;
        }

        public Rectangle getCollisionObjRectangle()
        {
            return colObj.getRect();
        }

        public bool iSCollide(Rectangle inRect)
        {
            return inRect.Intersects(this.getCollisionObjRectangle());
        }

        public virtual void VisitWall(Wall inWall) {}
        public virtual void VisitAlien(Alien inAlien) {}
        public virtual void VisitSuper(Super inSuper) {}
        public virtual void VisitShip(Ship inShip) {}
        public virtual void VisitMissile(Missile inMissile) {}
        public virtual void VisitShield(Shield inShield) {}
        public virtual void VisitBomb(Bomb inBomb) {}
        public virtual void VisitUfo(UFO inUfo) {}
        public virtual void Accept(GameObj inObj) {}
    }
}
