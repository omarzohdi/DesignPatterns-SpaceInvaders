using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    class InputManager : Manager
    {
        private static InputManager instance;

         private PlayerIndex PIndex;
         private GamePadState GPState;
         private GamePadState PrevGPState;
         private KeyboardState KBState;
         private KeyboardState PrevKBState;
         private CircularList _InputSequence;
         private int SequenceSize;

        public static InputManager Instance()
        {
            if (instance == null)
            {
                instance = new InputManager();
            }

            return instance;
        }

        public bool Initialize(int bufferSize = 8)
        {
            PIndex= PlayerIndex.One;
            
            _InputSequence = new CircularList();
            _InputSequence.Create(bufferSize);
            SequenceSize = bufferSize;

            initialized = true;
            return initialized;
        }
        
        public void Update(Screen CurrentScreen)
        {
            switch(CurrentScreen)
            {
                case Screen.Game:
                    getGameInput();
                    break;
                case Screen.MainMenu:
                    getMenuInput();
                    break;
                case Screen.Player:
                    getPlayerInput();
                    break;
                case Screen.GameOver:
                    getGOInput();
                    break;
            }

            _InputSequence.Update();

            _InputSequence.Kill();
        }

        private bool isStillPressed(Inputs Curr_Input)
        {
            switch(Curr_Input)
            {
                case Inputs.A:
                   return (PrevKBState.IsKeyDown(Keys.A) || PrevGPState.IsButtonDown(Buttons.A));
                case Inputs.B:
                   return (PrevKBState.IsKeyDown(Keys.B) || PrevGPState.IsButtonDown(Buttons.B));
                case Inputs.X:
                   return (PrevKBState.IsKeyDown(Keys.X) || PrevGPState.IsButtonDown(Buttons.X));
                case Inputs.Y:
                   return (PrevKBState.IsKeyDown(Keys.Y) || PrevGPState.IsButtonDown(Buttons.Y));
                case Inputs.LeftBumper:
                   return (PrevGPState.IsButtonDown(Buttons.LeftShoulder));
                case Inputs.RightBumper:
                   return (PrevGPState.IsButtonDown(Buttons.RightShoulder));
                case Inputs.Up:
                   return (PrevKBState.IsKeyDown(Keys.Up) || PrevGPState.IsButtonDown(Buttons.DPadUp));
                case Inputs.Down:
                   return (PrevKBState.IsKeyDown(Keys.Down) || PrevGPState.IsButtonDown(Buttons.DPadDown));
                case Inputs.Left:
                   return (PrevKBState.IsKeyDown(Keys.Left) || PrevGPState.IsButtonDown(Buttons.DPadLeft));
                case Inputs.Right:
                   return (PrevKBState.IsKeyDown(Keys.Right) || PrevGPState.IsButtonDown(Buttons.DPadRight));
                case Inputs.Space:
                   return (PrevKBState.IsKeyDown(Keys.Space));
                case Inputs.Start:
                   return (PrevKBState.IsKeyDown(Keys.Enter) || PrevGPState.IsButtonDown(Buttons.Start));
                case Inputs.Two:
                   return (PrevKBState.IsKeyDown(Keys.NumPad2) || PrevKBState.IsKeyDown(Keys.D2));
                case Inputs.One:
                   return (PrevKBState.IsKeyDown(Keys.NumPad1) || PrevKBState.IsKeyDown(Keys.D1));
            }

            return false;
        }

        void getGameInput()
        {
            KBState = Keyboard.GetState();
            GPState = GamePad.GetState(PIndex);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KBState.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Instance().CurrentScreen = Screen.GameOver;
                  GameManager.getInstance().Hit();
                  GameManager.getInstance().Hit();
                  GameManager.getInstance().Hit();
                ScreenManager.Instance().switchScreen();                
            }
                

            if (GameManager.getInstance().getCurrentPlayerStatus() == playerStatus.Live)
            {
                if (KBState.IsKeyDown(Keys.Up) || GPState.IsButtonDown(Buttons.DPadUp))
                    if (!isStillPressed(Inputs.Up))
                        _InputSequence.Add(Inputs.Up);

                if (KBState.IsKeyDown(Keys.Down) || GPState.IsButtonDown(Buttons.DPadDown))
                    if (!isStillPressed(Inputs.Down))
                        _InputSequence.Add(Inputs.Down);

                ////////////////////Movement////////////////////
                if (KBState.IsKeyDown(Keys.Left) || GPState.IsButtonDown(Buttons.DPadLeft))
                {
                    Ship Player = (Ship)GameObjMananger.getInstance().Find(GameObjName.Ship);
                    Player.MoveLeft();
                }

                if (KBState.IsKeyDown(Keys.Right) || GPState.IsButtonDown(Buttons.DPadRight))
                {
                    Ship Player = (Ship)GameObjMananger.getInstance().Find(GameObjName.Ship);
                    Player.MoveRight();
                }
                ///////////////////////////////////////////////////


                if (GPState.IsButtonDown(Buttons.RightShoulder))
                    _InputSequence.Add(Inputs.RightBumper);

                if (GPState.IsButtonDown(Buttons.LeftShoulder))
                    if (!isStillPressed(Inputs.LeftBumper))
                        _InputSequence.Add(Inputs.LeftBumper);

                if (KBState.IsKeyDown(Keys.B) || GPState.IsButtonDown(Buttons.B))
                    if (!isStillPressed(Inputs.B))
                        _InputSequence.Add(Inputs.B);

                if (KBState.IsKeyDown(Keys.X) || GPState.IsButtonDown(Buttons.X) || KBState.IsKeyDown(Keys.Space))
                    if (!isStillPressed(Inputs.X) && !isStillPressed(Inputs.Space))
                    {
                        ((Ship)GameObjMananger.getInstance().Find(GameObjName.Ship)).ShootMissile();
                    }
            }


            if (KBState.IsKeyDown(Keys.Y) || GPState.IsButtonDown(Buttons.Y))
                if (!isStillPressed(Inputs.Y))
                {
                    if (SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).isActive())
                        SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).DeActivate();
                    else if (!SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).isActive())
                        SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).Activate();
                }

            PrevKBState = KBState;
            PrevGPState = GPState;
        }

        void getMenuInput()
        {
            KBState = Keyboard.GetState();
            GPState = GamePad.GetState(PIndex);

            if (KBState.IsKeyDown(Keys.Enter) || GPState.IsButtonDown(Buttons.Start))
                if (!isStillPressed(Inputs.Start))
                {
                    ScreenManager.Instance().CurrentScreen = Screen.Player;
                    ScreenManager.Instance().switchScreen();
                }

            PrevKBState = KBState;
            PrevGPState = GPState;
        }

        void getPlayerInput()
        {
            KBState = Keyboard.GetState();
            GPState = GamePad.GetState(PIndex);

            if (KBState.IsKeyDown(Keys.NumPad1) || PrevKBState.IsKeyDown(Keys.D1))
                if (!isStillPressed(Inputs.Start))
                {
                    ScreenManager.Instance().CurrentScreen = Screen.Game;
                    ScreenManager.Instance().switchScreen();
                }

            if (KBState.IsKeyDown(Keys.NumPad2) || PrevKBState.IsKeyDown(Keys.D2))
                if (!isStillPressed(Inputs.Start))
                {
                    ScreenManager.Instance().CurrentScreen = Screen.Game;
                    ScreenManager.Instance().switchScreen();
                }

            PrevKBState = KBState;
            PrevGPState = GPState;
        }
      
        void getGOInput()
        {
            KBState = Keyboard.GetState();
            GPState = GamePad.GetState(PIndex);

            if (KBState.IsKeyDown(Keys.Enter) || GPState.IsButtonDown(Buttons.Start))
                if (!isStillPressed(Inputs.Start))
                {
                    ScreenManager.Instance().CurrentScreen = Screen.MainMenu;
                    ScreenManager.Instance().switchScreen();
                    GameManager.getInstance().Reset();
                }

            PrevKBState = KBState;
            PrevGPState = GPState;
        }
    }
}
