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
    class Alien: GameObj
    {
        public bool isDying = false;
        int score;
        public Alien(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite,int inScore)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            score = inScore;

            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
            sprite.setPosition(Position.X, Position.Y);
        }

        public int getScore()
        {
            return score;
        }
        public override void Update(GameTime gametime)
        {
            UpdateSprite(gametime);
            UpdateCollisionObject();
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

        public void UpdateMovement(Vector2 direction)
        {
            Position += direction;
            UpdateCollisionObject();
        }

        private void UpdateMovement(GameTime gametime)
        {
            Position += direction;
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitAlien(this);
        }
        public Rectangle getRectangle()
        {
            return (new Rectangle((int)Position.X, (int)Position.Y, sprite.getRectangle().Width, sprite.getRectangle().Height));
        }

        public void Kill()
        {
            TimeSpan FrameInterval = new TimeSpan(0);
            Animation An = AnimationManager.getInstance().Find(AnimName.AlienDeath);
            TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, An, delegate { Actions.Animate(An, this); });
        }
    }
}
