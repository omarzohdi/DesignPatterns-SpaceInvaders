using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CollisionSystem
{
    class Region
    {
        LinkedList ColObjects;
        Rectangle rect;

        public Region(int buffersize = 10, int delta = 3)
        {
            ColObjects = new LinkedList(buffersize, delta, NodeType.ColObj);
            rect.X = int.MaxValue;
            rect.Y = int.MaxValue;

            rect.Width = 0;
            rect.Height = 0;
        }

        public void UpdateSize()
        {
            int index = 0;

            ColObj Obj = (ColObj)ColObjects.getDatabyIndex(index);

            while (Obj != null)
            {
                Rectangle temp = Obj.getRect();
                
                if (temp.X < rect.X)
                    rect.X = temp.X;
                if (temp.Y < rect.Y)
                    rect.Y = temp.Y;

                if (rect.Width < temp.Width)
                {
                    if (rect.X == 0)
                        rect.Width = (temp.Width + temp.X);
                    else
                        rect.Width = (temp.Width);

                }
                if (rect.Height < temp.Height + temp.Y)
                {
                    if (rect.Y == 0)
                        rect.Height = (temp.Y + temp.Height);
                    else
                        rect.Height = temp.Height;
                }

                index++;
                Obj = (ColObj)ColObjects.getDatabyIndex(index);
            }
        }


        public void Add(Object inObj)
        {
            ColObjects.Add(inObj);
        }

        public void Remove(Node inNode)
        {
            ColObjects.Remove(inNode);
        }

        public void Draw(SpriteBatch spritebatch, GraphicsDevice GraphDev)
        {
            Texture2D text = new Texture2D(GraphDev, 1, 1);
            text.SetData(new Color[] { Color.White });
            Color color = Color.Black;

            int thickness = 1;

            spritebatch.Draw(text, new Rectangle(rect.X, rect.Y, rect.Width, thickness), color);
            spritebatch.Draw(text, new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);
            spritebatch.Draw(text, new Rectangle((rect.X + rect.Width - thickness), rect.Y, thickness, rect.Height), color);
            spritebatch.Draw(text, new Rectangle(rect.X, rect.Y + rect.Height - thickness, rect.Width, thickness), color);
        }
        

    }
}
