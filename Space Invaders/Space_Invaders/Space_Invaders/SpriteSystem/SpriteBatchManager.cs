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
    class SpriteBatchManager : Manager
    {
        private static SpriteBatchManager Instance;

        static public SpriteBatchManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new SpriteBatchManager();
                return Instance;
            }

            return Instance;
        }

        public void Draw()
        {
            int index = 0;

            DrawBatch Obj = (DrawBatch)List.getDatabyIndex(index);

            while (Obj != null)
            {
               if (Obj.isActive())
                    Obj.Draw();
                index++;
                Obj = (DrawBatch)List.getDatabyIndex(index);
            }
        }

        public DrawBatch Find(SpriteBatchName inName)
        {
            int index = 0;

            DrawBatch Obj = (DrawBatch)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.getName() == inName)
                    return Obj;

                index++;
                Obj = (DrawBatch)List.getDatabyIndex(index);
            }

            return null;
        }

        public void Kill(GameSprite inGSprite, SpriteBatchName inSpName)
        {
            DrawBatch DB  = this.Find(inSpName);
            DB.Kill(inGSprite);
        }

        public void CreateAllSpriteBatches(GraphicsDevice graphicsDev)
        {
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDev);
            DrawBatch Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SpriteBatchName.Collisions);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.Shields);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.Scene);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.Start);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.GameOver);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.Credit);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.UI);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);

            spriteBatch = new SpriteBatch(graphicsDev);
            Sprite_Batch = new DrawBatch(spriteBatch, graphicsDev, SpriteSortMode.Deferred, BlendState.AlphaBlend, SpriteBatchName.Player);
            SpriteBatchManager.getInstance().Add(Sprite_Batch);
        }

        public void CreateMenu(GraphicsDevice graphicsDev)
        {
            Rectangle inRect = new Rectangle(graphicsDev.Viewport.Width/2 - 250, graphicsDev.Viewport.Height/2-150, 500, 300);
            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Menu);
            Find(SpriteBatchName.Start).Add(Text);
        }
    }
}
