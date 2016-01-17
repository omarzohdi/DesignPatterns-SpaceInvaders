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
    class ImageManager: Manager
    {
        private static ImageManager Instance;

        static public ImageManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new ImageManager();
                return Instance;
            }

            return Instance;
        }

        public void Create(Rectangle inRect, TexName inName, ImageTag inTag)
        {
            Image inImage = new Image(inRect, inName, inTag);
            this.Add(inImage);
        }

        public Image find(ImageTag inFinder)
        {
            int index = 0;

            Image Obj = (Image)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.Name == inFinder)
                    return Obj;

                index++;
                Obj = (Image)List.getDatabyIndex(index);
            }

            return null;
        }

        public void CreateGameImages()
        {
            this.Create(new Rectangle(0, 0, 1, 1), TexName.BBox, ImageTag.BoundingBox);

            this.Create(new Rectangle(0, 0, 216, 199), TexName.Shield, ImageTag.Shield);
            this.Create(new Rectangle(0, 0, 10, 10), TexName.ShieldPart, ImageTag.ShieldPart);
            this.Create(new Rectangle(0, 0, 10, 10), TexName.ShieldPart2, ImageTag.ShieldPart2);
            this.Create(new Rectangle(0, 0, 10, 10), TexName.ShieldPart3, ImageTag.ShieldPart3);

            this.Create(new Rectangle(0, 0, 24, 33), TexName.Missile, ImageTag.Missile);
            this.Create(new Rectangle(439, 490, 24, 39), TexName.Invaders, ImageTag.Bomb);
            
            this.Create(new Rectangle(136, 66, 86, 64), TexName.Invaders, ImageTag.Crab);
            this.Create(new Rectangle(253, 66, 86, 64), TexName.Invaders, ImageTag.Crab2);

            this.Create(new Rectangle(560, 66, 94, 62), TexName.Invaders, ImageTag.Octopus);
            this.Create(new Rectangle(28, 202, 94, 62), TexName.Invaders, ImageTag.Octopus2);

            this.Create(new Rectangle(370, 66, 63, 63), TexName.Invaders, ImageTag.Squid);
            this.Create(new Rectangle(465, 66, 63, 63), TexName.Invaders, ImageTag.Squid2);

            this.Create(new Rectangle(242, 495, 58, 42), TexName.Invaders, ImageTag.Ship);
            this.Create(new Rectangle(121, 495, 95, 46), TexName.Invaders, ImageTag.Ufo);

            this.Create(new Rectangle(477, 488, 86, 64), TexName.Invaders, ImageTag.Explosion);
            this.Create(new Rectangle(330, 491, 82, 47), TexName.Invaders, ImageTag.Explosion2);

            this.Create(new Rectangle(0, 0, 100, 100), TexName.Score_Font, ImageTag.Score_Font);

            this.Create(new Rectangle(0, 0, 815, 653), TexName.Menu, ImageTag.Menu);
         
        }
    }
}
