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
    class AnimationManager: Manager
    {
        private static AnimationManager Instance;       

        static public AnimationManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new AnimationManager();
                return Instance;
            }

            return Instance;
        }


        public void Update(GameTime gameTime)
        {
        }

        public Animation Find(AnimName inName)
        {
            int index = 0;

            Animation Obj = (Animation)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.getName() == inName)
                    return Obj;

                index++;
                Obj = (Animation)List.getDatabyIndex(index);
            }

            return null;
        }

        public void CreateAnimations()
        {
            ///Create Bomb Animation
            Animation Bombs = new Animation(SpriteManager.getInstance().find(SpriteName.Bomb), 2, AnimSpeed.Medium, AnimName.BombAnim);
            Frame Bomb_Frame_1 = new Frame(SpriteName.Bomb,0);
            Frame Bomb_Frame_2 = new Frame(SpriteName.BombRev,1);
            
            Bombs.Add(Bomb_Frame_1);
            Bombs.Add(Bomb_Frame_2);
            
            AnimationManager.getInstance().Add(Bombs);            

            ///Create Explosion Animation (Crab)
            Animation Explosion = new Animation(SpriteManager.getInstance().find(SpriteName.Explosion), 1, AnimSpeed.Medium, AnimName.AlienDeath);
            Frame Explosion_Frame_1 = new Frame(SpriteName.Explosion, 0);

            Explosion.Add(Explosion_Frame_1);

            AnimationManager.getInstance().Add(Explosion);         
           
            ///Create Explosion Animation (Squid)
            ///Create Explosion Animation (Octopus)
            ///Create Explosion Animation (Ship)

            Animation Death = new Animation(SpriteManager.getInstance().find(SpriteName.Ship), 2, AnimSpeed.Medium, AnimName.ShipDeath);
            Frame Death_Frame_1 = new Frame(SpriteName.Explosion2, 0);
            Frame Death_Frame_2 = new Frame(SpriteName.Ship, 0);

            Death.Add(Death_Frame_1);
            Death.Add(Death_Frame_2);

            AnimationManager.getInstance().Add(Death);      
            
            ///Create Explosion Animation (UFO)
            
            Animation UfoExp = new Animation(SpriteManager.getInstance().find(SpriteName.Explosion3), 1, AnimSpeed.Medium, AnimName.UfoDeath);
            Frame UfoExp_Frame_1 = new Frame(SpriteName.Explosion3, 0);
            //Frame Bomb_Frame_2 = new Frame(SpriteName.BombRev, 1);

            UfoExp.Add(UfoExp_Frame_1);

            AnimationManager.getInstance().Add(UfoExp);

        }

    }
}
