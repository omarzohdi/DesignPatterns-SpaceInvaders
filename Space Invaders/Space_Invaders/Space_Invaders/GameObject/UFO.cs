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
    class UFO: GameObj
    {
        public bool isDying = false;

        int score;
        public UFO(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite, int inScore)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            score = inScore;
            direction.X = 0;

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
            UpdateMovement(gametime);
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
            if (isDying != true)
            {
                Vector2 Bounds = GameManager.getInstance().getScreenSize();

                if (Position.X < Bounds.X)
                {
                    Position += direction;
                }
                else
                {
                    Reset();
                }
            }
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitUfo(this);
        }
        public Rectangle getRectangle()
        {
            return (new Rectangle((int)Position.X, (int)Position.Y, sprite.getRectangle().Width, sprite.getRectangle().Height));
        }

        public void Kill()
        {
            TimeSpan FrameInterval = new TimeSpan(0);
            Animation An = AnimationManager.getInstance().Find(AnimName.UfoDeath);
            TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, An, delegate { Actions.Animate(An, this); });
        }

        public void Reset()
        {
            this.FlipSprite(SpriteName.Ufo);
            isDying = false;

            this.direction.X = 0;
            this.Position.X = -70;

            Random X = new Random();
            int s = X.Next(3);

            switch (s)
            {
                case 0:
                    score = 50;
                    break;
                case 1:
                    score = 100;
                    break;
                case 2:
                    score = 150;
                    break;
            }

            Sound Effect = SoundManager.getInstance().Find(SoundName.UFOMove);
            Effect.PauseSoundLooped();

            TimeSpan FrameInterval = new TimeSpan(0, 0, 20);
            UFO ufo = (UFO)GameObjMananger.getInstance().Find(GameObjName.UFO);
            TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, ufo, delegate { Actions.Act(ufo); });
        }
    }
}
