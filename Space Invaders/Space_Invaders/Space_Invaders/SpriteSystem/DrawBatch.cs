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
    public enum SpriteBatchLayers
    {
        Layer1,
        Layer2,
        Layer3,
        Count
    }

    public enum SpriteBatchName
    {
        Scene,
        Shields,
        Collisions,
        Start,
        GameOver,
        UI,
        Player,
        Credit
    }

    class DrawBatch
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicDevices;
        SpriteSortMode sortMode;
        BlendState blendState;
        int DeltaGrow;
        SpriteBatchName Name;
        bool _isActive;

        LinkedList GameSprites;

        public DrawBatch(SpriteBatch inspriteBatch, GraphicsDevice ingraphicDevice, SpriteSortMode inSort, BlendState inBlend, SpriteBatchName inName, int buffersize = 10, int delta = 3)
        {
            GameSprites = new LinkedList(buffersize, delta, NodeType.GameSpr);
            DeltaGrow = delta;

            spriteBatch = inspriteBatch;
            graphicDevices = ingraphicDevice;
            sortMode = inSort;
            blendState = inBlend;
            Name = inName;
            _isActive = true;
        }



        public void Add(GameSprite inGS)
        {
            GameSprites.Add(inGS);
        }

        public void Draw()
        {
            spriteBatch.Begin(sortMode, blendState);

            ListNode temp = (ListNode)GameSprites.getActiveHead();

            while (temp != null)
            {
                GameSprite Obj = (GameSprite)temp.getData();

                Obj.Draw(spriteBatch);

                temp = (ListNode)temp.pNext;
            }

            spriteBatch.End();
        }

        public SpriteBatchName getName()
        {
            return Name;
        }
        
        private ListNode Find(GameSprite inGSpr)
        {
            ListNode temp = (ListNode)GameSprites.getActiveHead();

            while (temp != null)
            {
                if (temp.getData().Equals(inGSpr))
                    return temp;

                temp = (ListNode)temp.pNext;
            }

            return null;
        }

        public void Kill(GameSprite inGspr)
        {
            ListNode KillNode = Find(inGspr);

            GameSprites.Remove(KillNode);
            KillNode.setData(null);
      
        }

        public bool isActive()
        {
            return _isActive;
        }
        public void Activate()
        {
            _isActive = true;
        }
        public void DeActivate()
        {
            _isActive = false;
        }
    }
}
