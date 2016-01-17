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
    class SpriteManager: Manager
    {
        private static SpriteManager Instance;

        static public SpriteManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new SpriteManager();
                return Instance;
            }

            return Instance;
        }

        public void Create(ImageTag inTag, SpriteEffects inSpEffect, Color inColor, SpriteName inName)
        {
            Sprite inSprite = new Sprite(inTag, inSpEffect, inColor, inName);
            this.Add(inSprite);
        }

        public Sprite find(SpriteName inName)
        {
            int index = 0;

            Sprite Obj = (Sprite)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.getName() == inName)
                    return Obj;

                index++;
                Obj = (Sprite)List.getDatabyIndex(index);
            }

            return null;
        }

        public void CreateAllSprites()
        {
            this.Create(ImageTag.BoundingBox, SpriteEffects.None, Color.White, SpriteName.BBox);
            
            this.Create(ImageTag.Crab, SpriteEffects.None, Color.White, SpriteName.Crab);
            this.Create(ImageTag.Squid, SpriteEffects.None, Color.White, SpriteName.Squid);
            this.Create(ImageTag.Octopus, SpriteEffects.None, Color.White, SpriteName.Octopus);
            
            this.Create(ImageTag.Ship, SpriteEffects.None, Color.Green, SpriteName.Ship);
            this.Create(ImageTag.Ufo, SpriteEffects.None, Color.Red, SpriteName.Ufo);

            this.Create(ImageTag.Missile, SpriteEffects.None, Color.White, SpriteName.Missile);
            this.Create(ImageTag.Bomb, SpriteEffects.None, Color.White, SpriteName.Bomb);
            this.Create(ImageTag.Bomb, SpriteEffects.FlipHorizontally, Color.White, SpriteName.BombRev);
            
            this.Create(ImageTag.Explosion, SpriteEffects.None, Color.White, SpriteName.Explosion);
            this.Create(ImageTag.Explosion2, SpriteEffects.None, Color.Green, SpriteName.Explosion2);
            this.Create(ImageTag.Explosion, SpriteEffects.None, Color.Red, SpriteName.Explosion3);

            this.Create(ImageTag.Shield, SpriteEffects.None, Color.Green, SpriteName.Shield);
            this.Create(ImageTag.ShieldPart, SpriteEffects.None, Color.Green, SpriteName.ShieldPart);
            this.Create(ImageTag.ShieldPart2, SpriteEffects.None, Color.Green, SpriteName.ShieldPart2);
            this.Create(ImageTag.ShieldPart3, SpriteEffects.None, Color.Green, SpriteName.ShieldPart3);

            this.Create(ImageTag.Score_Font, SpriteEffects.None, Color.White, SpriteName.Score_Font);
            this.Create(ImageTag.Score_Font, SpriteEffects.None, Color.White, SpriteName.Score_Val);
            this.Create(ImageTag.Score_Font, SpriteEffects.None, Color.White, SpriteName.HIScore_Val);
            
            this.Create(ImageTag.Menu, SpriteEffects.None, Color.White, SpriteName.Menu);
        }
    }
}
