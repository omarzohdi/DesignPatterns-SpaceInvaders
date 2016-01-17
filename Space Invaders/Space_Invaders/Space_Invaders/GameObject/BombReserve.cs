using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    class BombReserve
    {
        LinkedList Bombs;
        int usedBomb;
        int bufferSize;
        int MaxBomb;

        public BombReserve()
        {
            this.bufferSize = 11;
            Bombs = new LinkedList(bufferSize, 0, NodeType.GameObj);
            usedBomb = 0;
            MaxBomb = 1;
            Create();
        }

        public void Create()
        {
            for (int i = 0; i < bufferSize; ++i)
            {
                Bombs.Add(GameObjMananger.getInstance().CreateBomb(new Rectangle(-1, -1, 0, 0)));
            }
        }

        public void Update(Column inColumn)
        {
            if (usedBomb < MaxBomb)
            {
                bool drop = DropBomb(inColumn);

                if (drop)
                {
                    TimeSpan FrameInterval = new TimeSpan(1750000);
                    Animation An = AnimationManager.getInstance().Find(AnimName.BombAnim);
                    TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, An, delegate { Actions.Animate(An, inColumn.getAssignedBomb()); });
                }
            }
        }

        public void Add(Object inObj)
        {
            ((Bomb)(inObj)).setRectangle(new Rectangle(-1, -1, 0, 0));
            ((Bomb)(inObj)).setDir(new Vector2(0, 0));
            Bombs.Add(inObj);
        }

        public void Remove(Node inNode)
        {
            Bombs.Remove(inNode);
        }

        public bool DropBomb(Column inColumn)
        {
            if (inColumn != null)
            {
                ListNode ptr = (ListNode)Bombs.getActiveHead();
                if (ptr != null)
                {
                    bool isDrop = inColumn.DropBomb((Bomb)ptr.getData());
                    Remove(ptr);

                    if (isDrop)
                        usedBomb++;

                    return isDrop;
                }
            }

            return false;
        }

        public void ReloadBomb(Bomb inBomb)
        {
            usedBomb--;
            Column temp = ((Column)(GameObjMananger.getInstance().Find(inBomb.ColumnName)));
            
            if (temp != null)
                temp.Bomb_Active = false;
            
            inBomb.setStatus(false);
            Add(inBomb);
        }
    }
}
