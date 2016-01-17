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
    class Super : GameObj
    {
        public LinkedList Columns;
        public BombReserve BombPool;
        private bool AnimSwitch = false;
        public bool Step = false;

        public Super(GameObjName inName, ColObj inColObj, Vector2 inPos)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = null;
            direction = new Vector2(30, 0);

            Columns = new LinkedList(10, 3, NodeType.GameObj);
            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
        }

        public void ReloadBomb(Bomb inBomb)
        {
            BombPool.ReloadBomb(inBomb);
        }

        public override void Update(GameTime gametime)
        {
        }

        public void Action()
        {
            Step = true;
            int count = 0;

            UpdateMovement(ref count);

            Random R = new Random();
            int Chosen = R.Next(count);
            Column inCol = (Column)Columns.getDatabyIndex(Chosen);
            BombPool.Update(inCol);

            Rectangle tempRect = UpdateSize();

            UpdateCollisionObject(tempRect);
            UpdateAnimStep();

            if (this.getCollisionObjRectangle().Y + this.getCollisionObjRectangle().Height >= 715)
            {
                ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).Status = playerStatus.Dead;
                GameManager.getInstance().Hit();
                GameManager.getInstance().Hit();
                GameManager.getInstance().Hit();
            }
        }
        private void UpdateAnimStep()
        {
            if (!AnimSwitch)
            {
                SpriteManager.getInstance().find(SpriteName.Crab).setImage(ImageManager.getInstance().find(ImageTag.Crab2));
                SpriteManager.getInstance().find(SpriteName.Octopus).setImage(ImageManager.getInstance().find(ImageTag.Octopus2));
                SpriteManager.getInstance().find(SpriteName.Squid).setImage(ImageManager.getInstance().find(ImageTag.Squid2));
                AnimSwitch = true;
            }
            else
            {
                SpriteManager.getInstance().find(SpriteName.Crab).setImage(ImageManager.getInstance().find(ImageTag.Crab));
                SpriteManager.getInstance().find(SpriteName.Octopus).setImage(ImageManager.getInstance().find(ImageTag.Octopus));
                SpriteManager.getInstance().find(SpriteName.Squid).setImage(ImageManager.getInstance().find(ImageTag.Squid));
                AnimSwitch = false;
            }
        }
        private void UpdateCollisionObject(Rectangle tempRect)
        {
            colObj.Update();
            colObj.UpdatePosition((int)(Position.X + Col_Off.X), (int)(Position.Y + Col_Off.Y));
            colObj.UpdateSize(tempRect.Width, tempRect.Height);
        }

        private Rectangle UpdateSize()
        {
            if (Columns.getActiveHead() == null)
                return Rectangle.Empty;

            Rectangle tempRect = Rectangle.Empty;

            ListNode ptr = (ListNode)Columns.getActiveHead();

            while (ptr != null)
            {
                if (tempRect.IsEmpty)
                    tempRect = ((Column)ptr.getData()).getCollisionObjRectangle();
                else
                    tempRect = Rectangle.Union(((Column)ptr.getData()).getCollisionObjRectangle(), tempRect);

                ptr = (ListNode)ptr.pNext;
            }


            Position.X = tempRect.X;
            Position.Y = tempRect.Y;

            return tempRect;
        }

        public Vector2 CheckFuturePosition()
        {
            Vector2 NewDir = Vector2.Zero;

            if (Position.X + direction.X + this.getCollisionObjRectangle().Width > (1009))
            {
                int X = (int)(Position.X + direction.X) + this.getCollisionObjRectangle().Width - (1009);

                NewDir.X = (direction.X - X) + 1;

                return NewDir;
            }
            else if (Position.X + direction.X < 20)
            {
                int X = (int)(Position.X + direction.X) + (20);

                NewDir.X = -(Position.X - 20) - 1;

                return NewDir;
            }

            return direction;
        }
        private void UpdateMovement(ref int count)
        {
            Vector2 dir = CheckFuturePosition();

            if (dir.Y == 0)
                Position += dir;
            else
                dir.Y += dir.Y;

            ListNode temp = (ListNode)Columns.getActiveHead();

            while (temp != null)
            {
                count++;
                Column X = (Column)temp.getData();
                X.UpdateMovement(dir);

                temp = (ListNode)temp.pNext;

            }

            if (direction.Y > 0)
                direction.Y = 0;
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitSuper(this);
        }

        public bool CheckColumns(Missile inObj)
        {
            ListNode ptr = (ListNode)Columns.getActiveHead();
            while (ptr != null)
            {
                Column temp = (Column)ptr.getData();

                if (temp.iSCollide(inObj.getCollisionObjRectangle()))
                {
                    bool Hit = temp.CheckAliens(inObj);

                    if (((Column)ptr.getData()).isEmpty())
                    {
                        Columns.Remove(ptr);
                        GameObjMananger.getInstance().KillObject((Column)ptr.getData(), SpriteBatchName.Scene);
                    }

                    Rectangle X = UpdateSize();

                    if (X.Equals(Rectangle.Empty))
                    {
                        GameManager.getInstance().NextLevel();
                    }

                    return Hit;
                }

                ptr = (ListNode)ptr.pNext;
            }

            return false;
        }

        public bool CheckColumns(ShieldPart inObj)
        {
            ListNode ptr = (ListNode)Columns.getActiveHead();
            while (ptr != null)
            {
                Column temp = (Column)ptr.getData();

                if (temp.iSCollide(inObj.getCollisionObjRectangle()))
                {
                    return temp.CheckAliens(inObj);
                }

                ptr = (ListNode)ptr.pNext;
            }

            return false;
        }

        public void Create()
        {
            Vector2 ColumnPos = new Vector2(50 + GameManager.getInstance().getDifficulty() / 2, 200);
            NodeName PName = NodeName.Col1;
            GameObjName ColumnName = GameObjName.Column1;

            for (int i = 0; i < 11; ++i)
            {
                Column Obj = GameObjMananger.getInstance().CreateColumns(new Vector2(ColumnPos.X, ColumnPos.Y), ColumnName, PName);
                Columns.Add(Obj);
                GameObjMananger.getInstance().Add(Obj);
                ColumnPos.X += 50;
                PName++;
                ColumnName++;
            }
        }

        public void Purge()
        {
           ListNode ptr = null;

           do
           {
               ptr = (ListNode)Columns.getActiveHead();
               if (ptr != null)
               {
                   
                   Column temp = (Column)ptr.getData();
                   if (temp._bomb != null)
                        BombPool.ReloadBomb(temp._bomb);

                   temp.Purge();
                   Columns.Remove(ptr);
                   GameObjMananger.getInstance().KillObject((Column)ptr.getData(), SpriteBatchName.Scene);
               }

           } while (ptr != null);

        }

        public void reCreate()
        {
            Vector2 ColumnPos = new Vector2(50, 200);
            NodeName PName = NodeName.Col1;
            GameObjName ColumnName = GameObjName.Column1;

            for (int i = 0; i < 5; ++i)
            {
                Column Obj = GameObjMananger.getInstance().CreateColumns(new Vector2(ColumnPos.X, ColumnPos.Y), ColumnName, PName);
                Columns.Add(Obj);
                ColumnPos.X += 50;
                PName++;
                ColumnName++;
            }
        }

    }
}
