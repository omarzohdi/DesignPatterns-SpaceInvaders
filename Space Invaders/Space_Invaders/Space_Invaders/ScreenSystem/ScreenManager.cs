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
    enum Screen
    {
        MainMenu,
        Game,
        GameOver,
        Player,
        Credit
    }
    class ScreenManager: Manager
    {
        private static ScreenManager instance;

        public Screen CurrentScreen;

        public static ScreenManager Instance()
        {
            if (instance == null)
            {
                instance = new ScreenManager();
            }

            return instance;
        }


        public void switchScreen()
        {
            switch (CurrentScreen)
            {
                case Screen.MainMenu:
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Shields).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.GameOver).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Credit).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Player).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Start).Activate();
                    break;

                case Screen.Game:
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Shields).Activate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Activate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.GameOver).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Credit).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Start).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Player).DeActivate();
                    break;

                case Screen.Player:
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Shields).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.GameOver).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Credit).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Start).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Player).Activate();
                    break;

                case Screen.GameOver:
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Shields).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Collisions).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.GameOver).Activate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Credit).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Start).DeActivate();
                    SpriteBatchManager.getInstance().Find(SpriteBatchName.Player).DeActivate();
                    break;
            }
        }

    }
}
