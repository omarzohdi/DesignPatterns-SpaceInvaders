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
    class Sound
    {
        SoundName Name;
        SoundEffect Audio;
        SoundEffectInstance InstanceAudio;
        SoundEffect[] AudioLib;
        int maxSound;
        int currSound;
        String Asset;
        String[] AssetLib;

        public Sound(SoundName inName, string AssetName)
        {
            Name = inName;
            Asset = AssetName;
            maxSound = 0;
            currSound = 0;
        }
        public Sound(SoundName inName, string[] AssetName, int inMAx)
        {
            AudioLib = new SoundEffect[inMAx];
            AssetLib = new string[inMAx];
            Name = inName;

            for (int i= 0; i<inMAx; ++i)
                AssetLib[i] = AssetName[i];

            maxSound = inMAx;
            currSound = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            if (maxSound != 0)
            {
                for (int i = 0; i < maxSound; ++i)
                {
                    AudioLib[i] = Content.Load<SoundEffect>(AssetLib[i]);
                }
            }
            else
            {
                Audio = Content.Load<SoundEffect>(Asset);
            }
        }

        public SoundName getName()
        {
            return Name;
        }

        public void PlaySoundLib()
        {
            AudioLib[currSound].Play();

            currSound++;

            if (currSound >= maxSound)
                currSound = 0;
        }

        public void PlaySound()
        {
            Audio.Play();
        }

        public void PlaySoundLooped()
        {
            if (InstanceAudio == null)
            {
                InstanceAudio = Audio.CreateInstance();

                InstanceAudio.IsLooped = true;

                InstanceAudio.Play();
            }
            else
            {
                InstanceAudio.Resume();
            }

        }

        public void PauseSoundLooped()
        {
            InstanceAudio.Pause();
        }
    }
}
