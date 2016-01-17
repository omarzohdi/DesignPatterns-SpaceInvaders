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
    class ColObjManager: Manager
    {
        private static ColObjManager Instance;
        
        static public ColObjManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new ColObjManager();
                return Instance;
            }

            return Instance;
        }

        public void Kill(ColObj inObj)
        {
            SpriteBatchName inSpName = SpriteBatchName.Collisions;

            GameSprite _gameSprite = inObj.getColSprite();
            SpriteBatchManager.getInstance().Kill(_gameSprite, inSpName);
            ListNode KillNode = Find(inObj);

            if (KillNode != null)
                List.Remove(KillNode);
        }
        private ListNode Find(ColObj inObj)
        {
            ListNode temp = (ListNode)List.getActiveHead();

            while (temp != null)
            {
                if (temp.getData().Equals(inObj))
                    return temp;

                temp = (ListNode)temp.pNext;
            }

            return null;
        }

    }
}
