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
    enum playerStatus
    {
        Live,
        Dead,
        Hit
    }

    class Ship : GameObj
    {
        Missile _missile = null;
        public playerStatus Status = playerStatus.Live;
        public int lives;
        public int score;

        public Ship(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite, ColGroupName inTempColGroupName)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            TempColGroupName = inTempColGroupName;
            lives = 2;
            score = 0;

            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
            sprite.setPosition(Position.X, Position.Y);
        }

        public Rectangle getRectangle()
        {
            Rectangle temp = sprite.getRectangle();
            return new Rectangle((int)Position.X, (int)Position.Y, temp.Width, temp.Height);
        }
        public override void Update(GameTime gametime)
        {
            UpdateMovement(gametime);
            UpdateSprite(gametime);
            UpdateCollisionObject();
            UpdateMissile();
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
            this.Position += this.direction;
            
            this.direction = Vector2.Zero;
        }
        private void UpdateMissile()
        {
            if (!_missile.getStatus())
            {
                _missile.Position.X = Position.X + sprite.getRectangle().Width / 2 - 10 / 2;
                _missile.Position.Y = 850;
            }
        }
        public void MoveLeft()
        {
            if ((this.Position.X - 4) >= 20)
            {
                this.direction.X -= 4;
                if (!_missile.getStatus())
                    _missile.Position.X -= 4;
            }

        }
        public void MoveRight()
        {
            if ((this.Position.X + 4) <= 952)
            {
                this.direction.X += 4;
                if (!_missile.getStatus())
                    _missile.Position.X += 4;
            }
        }
        public void setMissile(Missile inMissile)
        {
            _missile = inMissile;
        }
        public bool ShootMissile()
        {
            if (_missile == null)
            {
                Rectangle inRect = ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).getRectangle();
                _missile = GameObjMananger.getInstance().CreateMissile(new Rectangle(inRect.X + inRect.Width / 2 - 10 / 2, inRect.Y, 10, 16));
                _missile.setStatus(false);

                Sound Effect = SoundManager.getInstance().Find(SoundName.Missile);
                Effect.PlaySound();

                return true;
            }
            else
            {
                if (!_missile.getStatus())
                {
                    _missile.Position.Y = Position.Y - 2;
                    _missile.setStatus(true);


                    Sound Effect = SoundManager.getInstance().Find(SoundName.Missile);
                    Effect.PlaySound();

                    return true;

                }
            }

            return false;
        }
        public void ReloadMissile()
        {
            _missile.Position.X = Position.X + sprite.getRectangle().Width / 2 - 10 / 2;
            _missile.Position.Y = 850;
            _missile.setStatus(false);
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitShip(this);
        }
       
        public override void VisitBomb(Bomb inBomb)
        {
            ((Super)(GameObjMananger.getInstance().Find(GameObjName.Super))).ReloadBomb(inBomb);

            if (Status == playerStatus.Live)
            {
                TimeSpan FrameInterval = new TimeSpan(0);
                Animation An = AnimationManager.getInstance().Find(AnimName.ShipDeath);
                TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, An, delegate { Actions.Animate(An, this); });
            }
        }

        public void Reset()
        {
            Status = playerStatus.Live;
            lives = 2;
            score = 0;

            ((UI)(GameObjMananger.getInstance().Find(GameObjName.LivesUI))).ChangeText(lives);
        }
    }
}