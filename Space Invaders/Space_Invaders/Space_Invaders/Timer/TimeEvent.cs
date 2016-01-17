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
    public delegate void ExecuteAction();

    public class TempTimerData
    {
        public TempTimerData(int initialCountValue, int deltaSeconds)
        {
            this.count = initialCountValue;
            this.targetTime = new TimeSpan();
            this.deltaTime = new TimeSpan(0, 0, 0, deltaSeconds, 0);
        }

        public int count;
        public TimeSpan targetTime; 
        public TimeSpan deltaTime;

    }
    class Actions
    {
        public static void Execute(ExecuteAction Action)
        {
            Action();
        }

        public static void Animate(Animation Anim, Bomb bomb)
        {
            bomb.FlipSprite(Anim.getNextFrame());

            if (bomb.getStatus())
            {
                TimeSpan FrameInterval = new TimeSpan(2000000);
                Animation An = AnimationManager.getInstance().Find(AnimName.BombAnim);
                TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, An, delegate { Actions.Animate(An, bomb); });
            }
        }

        public static void Animate(Animation Anim, Alien alien)
        {
            if (!alien.isDying)
            {
                alien.FlipSprite(Anim.getNextFrame());
                TimeSpan FrameInterval = new TimeSpan(1750000);
                TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, Anim, delegate { Actions.Animate(Anim, alien); });
                alien.isDying = true;
                
                Sound Effect = SoundManager.getInstance().Find(SoundName.AlienKill);
                Effect.PlaySound();
            }
            else
            {
                GameObjMananger.getInstance().KillObject(alien, SpriteBatchName.Scene);
                GameManager.getInstance().ScoreUp(alien.getScore());
                UI Score = (UI)GameObjMananger.getInstance().Find(GameObjName.Score);
                Score.ChangeText(GameManager.getInstance().getCurrentScore());
                GameManager.getInstance().AlienDead();
            }
            
        }
       
        public static void Animate(Animation Anim, UFO Ufo)
        {
            if (!Ufo.isDying)
            {
                Ufo.FlipSprite(Anim.getNextFrame());
                TimeSpan FrameInterval = new TimeSpan(1750000);
                TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, Anim, delegate { Actions.Animate(Anim, Ufo); });
                Ufo.isDying = true;

                Sound Effect = SoundManager.getInstance().Find(SoundName.UFOKill);
                Effect.PlaySound();
            }
            else
            {
                Ufo.Reset();
                GameManager.getInstance().ScoreUp(Ufo.getScore());
                UI Score = (UI)GameObjMananger.getInstance().Find(GameObjName.Score);
                Score.ChangeText(GameManager.getInstance().getCurrentScore());
            }

        }

        public static void Animate(Animation Anim, Ship player)
        {
            if (player.Status == playerStatus.Live)
            {
                player.FlipSprite(Anim.getNextFrame());
                TimeSpan FrameInterval = new TimeSpan(25000000);
                TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, Anim, delegate { Actions.Animate(Anim, player); });

                Sound Effect = SoundManager.getInstance().Find(SoundName.ShipDeath);
                Effect.PlaySound();

                player.Status = playerStatus.Hit;
            }
            else if (player.Status == playerStatus.Hit)
            {
                GameManager.getInstance().Hit();
                player.FlipSprite(Anim.getNextFrame());

                if (GameManager.getInstance().isDead())
                {
                    TimeSpan FrameInterval = new TimeSpan(0);
                    TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, Anim, delegate { Actions.Animate(Anim, player); });
                    player.Status = playerStatus.Dead;
                }
                else
                    player.Status = playerStatus.Live;

            }
            else if (player.Status == playerStatus.Dead)
            {
                player.score = GameManager.getInstance().getCurrentScore();
                player.lives = GameManager.getInstance().getCurrentLives();
            }

        }

        public static void Act(Super Supah)
        {
            Supah.Action();

            TimeSpan FrameInterval = new TimeSpan(0, 0, 0, 0, 1000 - GameManager.getInstance().getDifficulty());//new TimeSpan(5750000);
            TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, Supah, delegate { Actions.Act(Supah); });

            Sound Effect = (Sound)SoundManager.getInstance().Find(SoundName.AlienMove);

            Effect.PlaySoundLib();
        }

        public static void Act(UFO ufo)
        {
            ufo.setDir(new Vector2(2, 0));
            Sound Effect = SoundManager.getInstance().Find(SoundName.UFOMove);
            Effect.PlaySoundLooped();
        }
    }

    public class TimeEvent
    {
        public TimeEvent next;
        public TimeEvent prev;
        public TimeSpan targetTime;
        public Object data;
        public ExecuteAction Callback;

        public TimeEvent()
        {
            data = null;
            Callback = null;
        }

      
    }
}
