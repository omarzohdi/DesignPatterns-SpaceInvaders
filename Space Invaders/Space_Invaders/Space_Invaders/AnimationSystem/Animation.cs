using System;
using System.Diagnostics;
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
    enum AnimName
    {
        BombAnim,
        AlienDeath,
        ShipDeath,
        MissileDeath,
        UfoDeath,
        None
    }
    enum AnimSpeed
    {
        Dynamic = -1,
        Fast = 0,
        Medium,
        Slow,
        Paused
    }

    class Animation
    {
        public Sprite _sprite;
        public Frame[] _frames;
        public int FrameNum;
        public int CurrentFrame = 0;
        private TimeSpan FrameInterval;
        private TimeSpan LastInterval;
        public AnimSpeed Speed;
        public AnimName Name;

        public Animation(Sprite insprite, int numFrames, AnimSpeed speed, AnimName inName)
        {
            Name = inName;
            FrameNum = numFrames;
            _frames = new Frame[numFrames];
            _sprite = insprite;
            Speed = speed;
            FrameInterval = new TimeSpan(2750000);
            LastInterval = TimeEventManager.getInstance().GetCurrentTime();
        }

        public Animation(Sprite insprite, int numFrames, TimeSpan inFrame)
        {
            FrameNum = numFrames;
            _frames = new Frame[numFrames];
            _sprite = insprite;
            Speed = AnimSpeed.Dynamic;
            FrameInterval = inFrame;
            LastInterval = TimeEventManager.getInstance().GetCurrentTime();
        }

        private void setSpeed(AnimSpeed speed)
        {
            switch (speed)
            {
                case AnimSpeed.Fast:
                    FrameInterval = new TimeSpan(1750000);
                break;

                case AnimSpeed.Medium:
                    FrameInterval = new TimeSpan(2750000);
                break;

                case AnimSpeed.Slow:
                    FrameInterval = new TimeSpan(5750000);
                break;
                case AnimSpeed.Paused:
                    FrameInterval = TimeSpan.MaxValue;
                break;
            }
        }

        public SpriteName getNextFrame()
        {
            CurrentFrame++;

            if (CurrentFrame >= FrameNum)
                CurrentFrame = 0;

            return _frames[CurrentFrame].getSpriteName();
        }

        public AnimName getName()
        {
            return Name;
        }

        public void changespeed(AnimSpeed inSpeed)
        {
            Speed = inSpeed;
        }
        public void changespeed(TimeSpan inTimespan)
        {
            Speed = AnimSpeed.Dynamic;
            FrameInterval = inTimespan;
        }
        public void Add(Frame inFrame) ///Needs to be Ordered based on Frame.Order///
        {
            if (CurrentFrame < FrameNum)
            {
                _frames[CurrentFrame] = inFrame;
                CurrentFrame++;
            }
        }

        public void Animate(GameTime gameTime)
        {
           //setSpeed(Speed);

           //if (LastInterval <= TimeEventManager.getInstance().GetCurrentTime())
           //{
           //    //Debug.WriteLine(" currTime:{0} --> NextFrame CallBack():{1}", Timer.getInstance().GetCurrentTime(), Speed);
           //   // TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, this, delegate { Actions.Animate(this); });
           //    //LastInterval = TimeEventManager.getInstance().GetCurrentTime() + FrameInterval;
           //}
        }

    }
}
