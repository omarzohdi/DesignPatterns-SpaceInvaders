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
    class Column: GameObj
    {
        public Bomb _bomb;
        public LinkedList Aliens;
        Rectangle tempRect;
        public bool Bomb_Active = false;

        public Column(GameObjName inName, ColObj inColObj, Vector2 inPos)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = null;

            Aliens = new LinkedList(5, 1, NodeType.GameObj);
            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
       } 
        public override void Update(GameTime gametime)
        {
            
            UpdateMovement(gametime);
            
        }
        private void UpdateCollisionObject(Rectangle tempRect)
        {
            
            colObj.Update();
            
            colObj.UpdatePosition((int)(Position.X + Col_Off.X), (int)(Position.Y + Col_Off.Y));
            colObj.UpdateSize(tempRect.Width,tempRect.Height);
        }
        public void UpdateMovement(Vector2 direction)
        {
            ListNode temp = (ListNode)Aliens.getActiveHead();

            while (temp != null)
            {
                Alien X = (Alien)temp.getData();
                X.UpdateMovement(direction);

                temp = (ListNode)temp.pNext;
            }

            Position += direction;
            tempRect = UpdateSize();
            UpdateCollisionObject(tempRect);
        }
        private void UpdateMovement(GameTime gametime)
        {
            Position += direction;
        }
       
        private Rectangle UpdateSize()
        {
            if (Aliens.getActiveHead() == null)
                return Rectangle.Empty;

            Rectangle tempRect = Rectangle.Empty;
           
            ListNode ptr = (ListNode)Aliens.getActiveHead();

            while (ptr != null)
            {
                if (tempRect.IsEmpty)
                    tempRect = ((Alien)ptr.getData()).getCollisionObjRectangle();
                else
                    tempRect = Rectangle.Union(((Alien)ptr.getData()).getCollisionObjRectangle(), tempRect);

                ptr = (ListNode) ptr.pNext;
            }


            Position.X = tempRect.X;
            Position.Y = tempRect.Y;

            return tempRect;
        }

        public bool CheckAliens(Missile inObj)
        {
            ListNode ptr = (ListNode)Aliens.getActiveHead();

            while (ptr != null)
            {
                Alien temp = (Alien)ptr.getData();

                if (temp.iSCollide(inObj.getCollisionObjRectangle()))
                {
                    Aliens.Remove(ptr);
                    UpdateSize();

                    ((Alien)(ptr.getData())).Kill();

                    ptr.purge();

                    return true;
                }

                ptr = (ListNode)ptr.pNext;
            }

            return false;
        }

        public bool CheckAliens(ShieldPart inObj)
        {
            ListNode ptr = (ListNode)Aliens.getActiveHead();

            while (ptr != null)
            {
                Alien temp = (Alien)ptr.getData();

                if (temp.iSCollide(inObj.getCollisionObjRectangle()))
                {
                    GameObjMananger.getInstance().KillObject(inObj, SpriteBatchName.Shields);
                    return true;
                }

                ptr = (ListNode)ptr.pNext;
            }

            return false;

        }
        public bool isEmpty()
        {
            if (Aliens.getActiveHead() == null)
                return true;
            else
                return false;
        }
        public void Create(NodeName PName)
        {
            Rectangle Rect = new Rectangle((int)Position.X, (int)Position.Y, 25, 25);
            Alien Obj = null;

            for (int i = 0; i < 5; ++i)
            {
                if (i == 0)
                    Obj = GameObjMananger.getInstance().CreateAliens(Rect, SpriteName.Squid, PName,30);
                else if (i <= 2)
                    Obj = GameObjMananger.getInstance().CreateAliens(Rect, SpriteName.Crab, PName, 20);
                else if (i > 2)
                    Obj = GameObjMananger.getInstance().CreateAliens(Rect, SpriteName.Octopus, PName,10);

                Aliens.Add(Obj);
                GameObjMananger.getInstance().Add(Obj);
                Obj = null;

                Rect.Y += (5 + Rect.Height); 
            }
        }

        public bool DropBomb(Bomb inBomb)
        {
            if (!Bomb_Active)
            {
                Bomb_Active = true;

                Alien _alien = (Alien)Aliens.getDatabyIndex(0);
                Rectangle temp = _alien.getRectangle();
                Rectangle inRect = new Rectangle(temp.X, temp.Y, 10, 10);
                _bomb = inBomb;
                _bomb.setRectangle(inRect);
                _bomb.setDir(new Vector2(0,5));
                _bomb.ColumnName = this.Name;
                _bomb.setStatus(Bomb_Active);
                
                return true;
            }

            return false;
        }
        public Bomb getAssignedBomb()
        {
            return _bomb;
        }


        public void Purge()
        {
            ListNode ptr = null;

            do
            {
                ptr = (ListNode)Aliens.getActiveHead();

                if (ptr != null)
                {
                    Alien temp = (Alien)ptr.getData();
                    Aliens.Remove(ptr);
                    GameObjMananger.getInstance().KillObject(temp, SpriteBatchName.Scene);
                }

            } while (ptr != null);

        }


        public void ReCreate(NodeName PName)
        {
            Rectangle Rect = new Rectangle((int)Position.X, (int)Position.Y, 25, 25);
            Alien Obj = null;

            for (int i = 0; i < 5; ++i)
            {
                if (i == 0)
                    Obj = GameObjMananger.getInstance().CreateAliens(Rect, SpriteName.Squid, PName, 30);
                else if (i <= 2)
                    Obj = GameObjMananger.getInstance().CreateAliens(Rect, SpriteName.Crab, PName, 20);
                else if (i > 2)
                    Obj = GameObjMananger.getInstance().CreateAliens(Rect, SpriteName.Octopus, PName, 10);

                Aliens.Add(Obj);
                Obj = null;

                Rect.Y += (5 + Rect.Height);
            }
        }
    }
}
