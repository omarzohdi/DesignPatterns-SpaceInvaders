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
    enum SoundName
    {
        AlienMove = 0,
        AlienMove2,
        AlienMove3,
        AlienMove4,
        AlienKill,
        Missile,
        UFOMove,
        UFOKill,
        ShipDeath,

    }

    class SoundManager:Manager
    {
        private static SoundManager Instance;

        static public SoundManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new SoundManager();
                return Instance;
            }

            return Instance;
        }

        public void LoadContent(ContentManager Content)
        {
           ListNode ptr = (ListNode)List.getActiveHead();

           while (ptr != null)
           {
               Sound Obj = (Sound)ptr.getData();

               Obj.LoadContent(Content);

               ptr = (ListNode)ptr.pNext;
           }
        }

        public Sound Find(SoundName inName)
        {
            ListNode ptr = (ListNode)List.getActiveHead();

            while (ptr != null)
            {
                Sound Obj = (Sound)ptr.getData();

                if (Obj.getName().Equals(inName))
                    return Obj;

                ptr = (ListNode)ptr.pNext;
            }

            return null;
        }

        public void CreateSounds()
        {
            String[] AlienMove = new string[4];
            AlienMove[0] = "Audio/A";
            AlienMove[1] = "Audio/B";
            AlienMove[2] = "Audio/C";
            AlienMove[3] = "Audio/D";

            Sound Obj = new Sound(SoundName.AlienMove, AlienMove, 4);
            Add(Obj);

            Obj = new Sound(SoundName.ShipDeath, "Audio/explosion");
            Add(Obj);

            Obj = new Sound(SoundName.Missile, "Audio/shoot");
            Add(Obj);

            Obj = new Sound(SoundName.UFOMove, "Audio/ufo_highpitch");
            Add(Obj);

            Obj = new Sound(SoundName.UFOKill, "Audio/ufo_lowpitch");
            Add(Obj);

            Obj = new Sound(SoundName.AlienKill, "Audio/invaderkilled");
            Add(Obj);

        }
    }
}
