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
    class TextureManager : Manager
    {        
        private static TextureManager Instance;
       
        static public TextureManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new TextureManager();
                return Instance;
            }

            return Instance;
        }

        public void Create(TexName Name, Texture.type type)
        {
            Texture inTexture = new Texture(Name, type);
            this.Add(inTexture);
        }
        public void Create(string Asset, TexName Name, Texture.type type)
        {
            Texture inTexture = new Texture(Asset,Name,type);
            this.Add(inTexture);
        }

        public void load(ContentManager Content,GraphicsDevice Graph)
        {
            int index = 0;

            Texture Obj = (Texture)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.Type == Texture.type.Text_Sprite)
                    Obj.setXnaTexture(new SpriteTexture(Content.Load<Texture2D>(Obj.AssetName)));
                else if (Obj.Type == Texture.type.Text_Font)
                    Obj.setXnaTexture(new FontTexture(Content.Load<SpriteFont>(Obj.AssetName)));
                else if (Obj.Type == Texture.type.Text_Col)
                    Obj.setXnaTexture(new ColTexture(Graph));

                index++;
                Obj = (Texture)List.getDatabyIndex(index);
            }    
        }

        public void unload(ContentManager Content)
        {
            Content.Unload();
        }

        public Texture find(TexName inFinder)
        {
            int index = 0;

            Texture Obj = (Texture)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.Name == inFinder)
                    return Obj;

                index++;
                Obj = (Texture)List.getDatabyIndex(index);
            }

            return null;
        }

        public void CreateGameTextures()
        {
            this.Create(TexName.BBox, Texture.type.Text_Col);
            this.Create("alienSSColored", TexName.Invaders, Texture.type.Text_Sprite);
            this.Create("Player_Missile", TexName.Missile, Texture.type.Text_Sprite);
            this.Create("Shield", TexName.Shield, Texture.type.Text_Sprite);
            this.Create("Shield_Full", TexName.ShieldPart, Texture.type.Text_Sprite);
            this.Create("Shield_Hit1", TexName.ShieldPart2, Texture.type.Text_Sprite);
            this.Create("Shield_Hit2", TexName.ShieldPart3, Texture.type.Text_Sprite);
            this.Create("UserInterface", TexName.Score_Font, Texture.type.Text_Font);
            this.Create("Menu", TexName.Menu, Texture.type.Text_Sprite);
        }
    }
}
