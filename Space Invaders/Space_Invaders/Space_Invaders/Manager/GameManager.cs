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
    class GameManager:Manager
    {
        /// <summary>
        /// Player Variables
        /// </summary>
        private playerStatus currentPlayerStatus;
        private int currentPlayerLives;
        private int currentPlayerScore;
        private int currentHiScore;
        private int CurrLevel;
        private int Difficulty;

        /// <summary>
        /// Game Variables
        /// </summary>
        /// 
        Vector2 ScreenSize = new Vector2(1024, 800);

        private static GameManager Instance;

        static public GameManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new GameManager();
                return Instance;
            }

            return Instance;
        }

        public Vector2 getScreenSize()
        {
            return ScreenSize;
        }
        public int getCurrentLives()
        {
            return currentPlayerLives;
        }
        public int getCurrentScore()
        {
            return currentPlayerScore;
        }
        public playerStatus getCurrentPlayerStatus()
        {
            return currentPlayerStatus;
        }
        public void oneUp()
        {
            currentPlayerLives++;
        }
        public void Hit()
        {
            currentPlayerLives--;

            if (currentPlayerLives >= 0)
            {
                ((UI)(GameObjMananger.getInstance().Find(GameObjName.LivesUI))).ChangeText(currentPlayerLives);

                Lives l = (Lives)GameObjMananger.getInstance().Find(GameObjName.Life);
                if (l != null)
                {
                    GameObjMananger.getInstance().KillObject(l, SpriteBatchName.Scene);
                }
            }
        }
        public bool isDead()
        {
            if (currentPlayerLives == 0)
                return true;
            else
                return false;
        }
        public void ScoreUp(int inScore)
        {
            currentPlayerScore += inScore;
        }
        public void AlienDead()
        {
            Difficulty += 15;
        }
        public void NextLevel()
        {
            CurrLevel++;
            Difficulty = CurrLevel * 50;
            if (Difficulty > 200)
                Difficulty = 200;
            
           Super temp = GameObjMananger.getInstance().CreateSuper();

           temp.direction.Y = 50;
            
        }
        public int getDifficulty()
        {
            return Difficulty;
        }

        public void GAMEOVER()
        {
            ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).score = currentPlayerScore;
            currentPlayerScore = 0;

            ScreenManager.Instance().CurrentScreen = Screen.GameOver;
            ScreenManager.Instance().switchScreen();
        }

        public void Initialize(GraphicsDeviceManager graphics, SpriteBatch spritebatch)
        {
            TextureManager.getInstance().Initialize();
            TextureManager.getInstance().CreateGameTextures();

            SoundManager.getInstance().Initialize();
            SoundManager.getInstance().CreateSounds();

            ImageManager.getInstance().Initialize();
            ImageManager.getInstance().CreateGameImages();

            SpriteManager.getInstance().Initialize();
            SpriteManager.getInstance().CreateAllSprites();

            AnimationManager.getInstance().Initialize();
            AnimationManager.getInstance().CreateAnimations();

            SpriteBatchManager.getInstance().Initialize();
            SpriteBatchManager.getInstance().CreateAllSpriteBatches(graphics.GraphicsDevice);
            SpriteBatchManager.getInstance().CreateMenu(graphics.GraphicsDevice);

            ColObjManager.getInstance().Initialize();
            GameObjMananger.getInstance().Initialize();

            ColGroupManager.getInstance().Initialize();
            ColGroupManager.getInstance().Create();

            ColPairManager.getInstance().Initialize();
            ColPairManager.getInstance().CreateCollisionPairs();

            GameObjMananger.getInstance().CreateSuper();
            GameObjMananger.getInstance().CreateLevel(graphics.GraphicsDevice);
            GameObjMananger.getInstance().CreateShield();
            GameObjMananger.getInstance().CreateShip();
            GameObjMananger.getInstance().CreateUI();
            GameObjMananger.getInstance().CreateUfo();

            InputManager.Instance().Initialize();
            TimeEventManager.getInstance().Initialize();
            TimeEventManager.getInstance().StartTimedEvents();

            ScreenManager.Instance().CurrentScreen = Screen.MainMenu;
            ScreenManager.Instance().switchScreen();

            getCurrentPlayerData();

            CurrLevel = 0;
            Difficulty = CurrLevel * 2;
        }

        private void getCurrentPlayerData()
        {
            currentPlayerStatus = ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).Status;
            currentPlayerScore = ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).score;
            currentPlayerLives = ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).lives;
        }
        public void Update(GameTime gameTime)
        {
            currentPlayerStatus = ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).Status;

            InputManager.Instance().Update(ScreenManager.Instance().CurrentScreen);

            if (currentPlayerStatus != playerStatus.Dead &&  ScreenManager.Instance().CurrentScreen == Screen.Game)
            {
                ColPairManager.getInstance().Collide();
                GameObjMananger.getInstance().Update(gameTime);
                TimeEventManager.getInstance().Update(gameTime);
                AnimationManager.getInstance().Update(gameTime);
                UpdateHIScore();
            }

            if (currentPlayerStatus == playerStatus.Dead && ScreenManager.Instance().CurrentScreen == Screen.Game)
            {
                GAMEOVER();
            }
        }

        public void UpdateHIScore()
        {
            if (currentPlayerScore > currentHiScore)
            {
                currentHiScore = currentPlayerScore;

                ((UI)(GameObjMananger.getInstance().Find(GameObjName.HiScore))).ChangeText(currentHiScore);
            }
        }

        public void Reset()
        {
            Sound Effect = SoundManager.getInstance().Find(SoundName.UFOMove);
            Effect.PauseSoundLooped();

            CurrLevel = 0;
            Difficulty = CurrLevel * 2;

            ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).Reset();
            ((UI)(GameObjMananger.getInstance().Find(GameObjName.Score))).ChangeText(0);
            getCurrentPlayerData();
            GameObjMananger.getInstance().ResetLevel();
        }
    }
}
