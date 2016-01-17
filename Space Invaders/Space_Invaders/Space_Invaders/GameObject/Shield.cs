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
    class Shield : GameObj
    {
        private LinkedList ShieldParts;

        public Shield(GameObjName inName, ColObj inColObj, Vector2 inPos, GameSprite inSprite, ColGroupName inTempColGroupName)
        {
            Name = inName;
            Position = inPos;
            colObj = inColObj;
            sprite = inSprite;
            direction = new Vector2(0, 0);
            TempColGroupName = inTempColGroupName;

            ShieldParts = new LinkedList(10, 3, NodeType.GameObj);
            Rectangle tempRect = inColObj.getRect();
            Col_Off = new Vector2(Position.X - tempRect.X, Position.Y - tempRect.Y);
            sprite.setPosition(Position.X, Position.Y);
        }

        public override void Update(GameTime gametime)
        {
            Rectangle tempRect = UpdateSize();
            UpdateCollisionObject(gametime, tempRect);
            UpdateSprite(gametime);
        }

        private void UpdateCollisionObject(GameTime gametime, Rectangle tempRect)
        {
            colObj.Update();
            colObj.UpdatePosition((int)(Position.X + Col_Off.X), (int)(Position.Y + Col_Off.Y));
            colObj.UpdateSize(tempRect.Width, tempRect.Height);
        }

        private Rectangle UpdateSize()
        {
            if (ShieldParts.getActiveHead() == null)
                return Rectangle.Empty;

            Rectangle tempRect = Rectangle.Empty;

            ListNode ptr = (ListNode)ShieldParts.getActiveHead();

            while (ptr != null)
            {
                if (tempRect.IsEmpty)
                    tempRect = ((ShieldPart)ptr.getData()).getCollisionObjRectangle();
                else
                    tempRect = Rectangle.Union(((ShieldPart)ptr.getData()).getCollisionObjRectangle(), tempRect);

                ptr = (ListNode)ptr.pNext;
            }


            Position.X = tempRect.X;
            Position.Y = tempRect.Y;

            return tempRect;
        }

        private void UpdateSprite(GameTime gametime)
        {
            sprite.setPosition(Position.X, Position.Y);
        }

        public override void Accept(GameObj inObj)
        {
            inObj.VisitShield(this);
        }

        public override void VisitSuper(Super inSuper)
        {
            if (CheckParts(inSuper))
            {

            }
        } 

        public void Create(Rectangle inRect)
        {
            Rectangle tempRect = inRect;
            bool toDraw = false;;
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i != 7 && i !=8 && i!=9)
                        toDraw = true;
                    else
                        if (j != 3 && j != 4 && j!=5 && j!=6)
                            toDraw = true;

                    if ((i == 0 && j == 0) || (i == 0 && j == 9))
                    {
                        toDraw = false;
                    }
                    if ((i == 0 && j == 1) || ( i == 0 && j == 8))
                    {
                        toDraw = false;
                    }
                    if ((i == 1 && j == 0) || (i == 1 && j == 9))
                    {
                        toDraw = false;
                    }

                    if (toDraw)
                    {
                        TextureSprite inSprite = new TextureSprite(tempRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.ShieldPart);
                        ColObj _colobj = new ColObj(tempRect, ColObjName.ShieldPart, SpriteName.BBox);
                        ShieldPart Obj = new ShieldPart(GameObjName.ShieldPart, _colobj, new Vector2(tempRect.X, tempRect.Y), inSprite);
                        ColObjManager.getInstance().Add(_colobj);
                        SpriteBatchManager.getInstance().Find(SpriteBatchName.Shields).Add(inSprite);
                        GameObjMananger.getInstance().Add(Obj);

                        ShieldParts.Add(Obj);
                    }

                    tempRect.X += 10;
                    toDraw = false;

                }
                tempRect.X = inRect.X;
                tempRect.Y += 10;
            }
        }

        public bool CheckParts(GameObj inObj)
        {
            ListNode ptr = (ListNode)ShieldParts.getActiveHead();
            while (ptr != null)
            {
                ShieldPart temp = (ShieldPart)ptr.getData();

                if (temp.iSCollide(inObj.getCollisionObjRectangle()))
                {
                    temp.Health--;
                    switch(temp.Health)
                    {
                        case 3:
                            temp.FlipSprite(SpriteName.ShieldPart2);
                            break;
                        case 2:
                            temp.FlipSprite(SpriteName.ShieldPart3);
                            break;

                        default:
                            ShieldParts.Remove(ptr);
                            GameObjMananger.getInstance().KillObject((ShieldPart)ptr.getData(), SpriteBatchName.Shields);

                            UpdateSize();
                        break;
                    }
                    return true;
                }

                ptr = (ListNode)ptr.pNext;
            }
            return false;
        }

        public bool CheckParts(Super inSuper)
        {
            ListNode ptr = (ListNode)ShieldParts.getActiveHead();
            while (ptr != null)
            {
                ShieldPart temp = (ShieldPart)ptr.getData();

                if (inSuper.CheckColumns(temp))
                {
                    ListNode inTemp = (ListNode)ptr.pNext;
                    ShieldParts.Remove(ptr);
                    ptr = inTemp;
                }
                else
                {
                    ptr = (ListNode)ptr.pNext;
                }
            }

            return true;
        }
    }
}
