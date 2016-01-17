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
using System.Diagnostics;

namespace Space_Invaders
{
    class GameObjMananger : Manager
    {
        private static GameObjMananger Instance;
        
        static public GameObjMananger getInstance()
        {
            if (Instance == null)
            {
                Instance = new GameObjMananger();
                return Instance;
            }

            return Instance;
        }

        public void Update(GameTime gametime)
        {
            int index = 0;

            GameObj Obj = (GameObj)List.getDatabyIndex(index);

            while (Obj != null)
            {
                Obj.Update(gametime);
                index++;
                Obj = (GameObj)List.getDatabyIndex(index);
            }

        }

        public GameObj Find(GameObjName inName)
        {
            int index = 0;

            GameObj Obj = (GameObj)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.getName() == inName)
                    return Obj;

                index++;
                Obj = (GameObj)List.getDatabyIndex(index);
            }

            return null;
        }

        public Super CreateSuper()
        {
            Super temp = ((Super)(Find(GameObjName.Super)));

            if (temp != null)
            {
                ((Super)(Find(GameObjName.Super))).Purge();
            }
    
            ColObj _colobj = new ColObj(Rectangle.Empty,ColObjName.Super,SpriteName.BBox);
            ColObjManager.getInstance().Add(_colobj);
            Super Obj;

            if (temp != null)
            {
                Obj = temp;
                Obj.BombPool = temp.BombPool;
            }
            else
            {
                Obj = new Super(GameObjName.Super, _colobj, new Vector2(0, 0));
                Obj.BombPool = new BombReserve();
            
                ColGroup Invaders = ColGroupManager.getInstance().find(ColGroupName.Alien);
                Invaders.Add(Obj);

                GameObjMananger.getInstance().Add(Obj);
            
            }

            Obj.Create();
            

            return Obj;
        }

        public Alien CreateAliens(Rectangle inRect, SpriteName inSName, NodeName inColGrName, int score)
        {
            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, inSName);
            ColObj _colobj = new ColObj(inRect, ColObjName.Crab, SpriteName.BBox);
            Alien Obj = new Alien(GameObjName.Alien, _colobj, new Vector2(inRect.X, inRect.Y), Text, score);

            ColObjManager.getInstance().Add(_colobj);
            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);

            return Obj;
        }

        public Column CreateColumns(Vector2 Position, GameObjName inName, NodeName inColGrName)
        {
            ColObj _colobj = new ColObj(new Rectangle((int)Position.X,(int)Position.Y, 0,0), ColObjName.Column, SpriteName.BBox);
            Column Obj = new Column(inName, _colobj, Position);
            ColObjManager.getInstance().Add(_colobj);

            Obj.Create(inColGrName);
            
            return Obj;
        }

        public Wall CreateLevel(GraphicsDevice Graph)
        {
            //////////////////////////////////////////Walls
            ColGroup Walls = ColGroupManager.getInstance().find(ColGroupName.Wall);

            //Left Wall
            ColObj _colobj = new ColObj(new Rectangle(15,100, 5, Graph.Viewport.Height - 145), ColObjName.Wall, SpriteName.BBox);
            Wall Obj = new Wall(GameObjName.Wall, _colobj, new Vector2(15,100));
            ColObjManager.getInstance().Add(_colobj);
            ColGroupManager.getInstance().find(ColGroupName.Wall).Add(Obj);
            GameObjMananger.getInstance().Add(Obj);
            
            //Right Wall
            _colobj = new ColObj(new Rectangle(Graph.Viewport.Width-15, 100, 5, Graph.Viewport.Height - 145), ColObjName.Wall, SpriteName.BBox);
            Obj = new Wall(GameObjName.Wall, _colobj, new Vector2(Graph.Viewport.Width - 15, 100));
            ColObjManager.getInstance().Add(_colobj);
            ColGroupManager.getInstance().find(ColGroupName.Wall).Add(Obj);
            GameObjMananger.getInstance().Add(Obj);

            //Top Wall
            _colobj = new ColObj(new Rectangle(15, 100, Graph.Viewport.Width - 25, 5), ColObjName.Wall, SpriteName.BBox);
            Obj = new Wall(GameObjName.Wall, _colobj, new Vector2(15, 100));
            ColObjManager.getInstance().Add(_colobj);
            ColGroupManager.getInstance().find(ColGroupName.Wall).Add(Obj);
            GameObjMananger.getInstance().Add(Obj);

            //Bottom Wall
            _colobj = new ColObj(new Rectangle(15, Graph.Viewport.Height - 50, Graph.Viewport.Width - 25, 5), ColObjName.Wall, SpriteName.BBox);
            Obj = new Wall(GameObjName.Wall, _colobj, new Vector2(15, Graph.Viewport.Height - 50));
            ColObjManager.getInstance().Add(_colobj);
            ColGroupManager.getInstance().find(ColGroupName.Wall).Add(Obj);
            GameObjMananger.getInstance().Add(Obj);

            return Obj;
        }

        public Missile CreateMissile(Rectangle inRect)
        {
            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Missile);
            ColObj _colobj = new ColObj(inRect, ColObjName.Missile, SpriteName.BBox);
            Missile Obj = new Missile(GameObjName.Missile, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Missile);

            ColGroup X = ColGroupManager.getInstance().find(Obj.TempColGroupName);
            if ( X == null)
                ColGroupManager.getInstance().find(ColGroupName.Missile);

            ColObjManager.getInstance().Add(_colobj);
            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);

            ColGroupManager.getInstance().find(Obj.TempColGroupName).Add(Obj);

            GameObjMananger.getInstance().Add(Obj);
            Obj.setStatus(false);
            ((Ship)(GameObjMananger.getInstance().Find(GameObjName.Ship))).setMissile(Obj);

            return Obj;
        }

        public Bomb CreateBomb(Rectangle inRect)
        {
            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Bomb);
            ColObj _colobj = new ColObj(inRect, ColObjName.Missile, SpriteName.BBox);
            Bomb Obj = new Bomb(GameObjName.Missile, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Bomb);

            ColGroup X = ColGroupManager.getInstance().find(Obj.TempColGroupName);
            if (X == null)
                ColGroupManager.getInstance().find(ColGroupName.Bomb);

            ColObjManager.getInstance().Add(_colobj);
            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);

            ColGroupManager.getInstance().find(Obj.TempColGroupName).Add(Obj);

            GameObjMananger.getInstance().Add(Obj);

            return Obj;
        }

        public Shield CreateShield()
        {
            ColGroup Shields = ColGroupManager.getInstance().find(ColGroupName.Shield);
            Rectangle inRect = new Rectangle(150, 600, 100, 100);
            Rectangle inRectparts;

            //Shield 1
            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Shield);
            ColObj _colobj = new ColObj(inRect, ColObjName.Shield, SpriteName.BBox);
            Shield Obj = new Shield(GameObjName.Shield, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Shield);
            ColObjManager.getInstance().Add(_colobj);
           // SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);
            ColGroupManager.getInstance().find(ColGroupName.Shield).Add(Obj);

            inRectparts = new Rectangle(inRect.X, inRect.Y, 10,10);
            Obj.Create(inRectparts);

            GameObjMananger.getInstance().Add(Obj);


            inRect = new Rectangle(350, 600, 100, 100);
            //Shield 2
            Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Shield);
            _colobj = new ColObj(inRect, ColObjName.Shield, SpriteName.BBox);
            Obj = new Shield(GameObjName.Shield, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Shield);
            ColObjManager.getInstance().Add(_colobj);
           // SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);
            ColGroupManager.getInstance().find(ColGroupName.Shield).Add(Obj);

            inRectparts = new Rectangle(inRect.X, inRect.Y, 10, 10);
            Obj.Create(inRectparts);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(550, 600, 100, 100);
            //Shield 3
            Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Shield);
            _colobj = new ColObj(inRect, ColObjName.Shield, SpriteName.BBox);
            Obj = new Shield(GameObjName.Shield, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Shield);
            ColObjManager.getInstance().Add(_colobj);
            ColGroupManager.getInstance().find(ColGroupName.Shield).Add(Obj);

            inRectparts = new Rectangle(inRect.X, inRect.Y, 10, 10);
            Obj.Create(inRectparts);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(750, 600, 100, 100);
            //Shield 4
            Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Shield);
            _colobj = new ColObj(inRect, ColObjName.Shield, SpriteName.BBox);
            Obj = new Shield(GameObjName.Shield, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Shield);
            ColObjManager.getInstance().Add(_colobj);
            ColGroupManager.getInstance().find(ColGroupName.Shield).Add(Obj);

            inRectparts = new Rectangle(inRect.X, inRect.Y, 10, 10);
            Obj.Create(inRectparts);

            GameObjMananger.getInstance().Add(Obj);

            return Obj;
        }

        public Ship CreateShip()
        {
            Rectangle inRect = new Rectangle(476, 715, 58, 32);

            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Ship);
            ColObj _colobj = new ColObj(inRect, ColObjName.Ship, SpriteName.BBox);
            Ship Obj = new Ship(GameObjName.Ship, _colobj, new Vector2(inRect.X, inRect.Y), Text, ColGroupName.Ship);
            ColObjManager.getInstance().Add(_colobj);
            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);
            ColGroupManager.getInstance().find(ColGroupName.Ship).Add(Obj);

            GameObjMananger.getInstance().Add(Obj);

            this.CreateMissile(new Rectangle(inRect.X + inRect.Width / 2 - 10 / 2, inRect.Y-2, 10, 16));

            return Obj;
        }

        public UFO CreateUfo()
        {
            Rectangle inRect = new Rectangle(-70, 120, 95/2, 46/2);

            TextureSprite Text = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Ufo);
            ColObj _colobj = new ColObj(inRect, ColObjName.UFO, SpriteName.BBox);
            ColObjManager.getInstance().Add(_colobj);
            UFO Obj = new UFO(GameObjName.UFO, _colobj, new Vector2(inRect.X, inRect.Y),Text, 100);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);

            ColGroup Invaders = ColGroupManager.getInstance().find(ColGroupName.Ufo);
            Invaders.Add(Obj);

            GameObjMananger.getInstance().Add(Obj);

            return Obj;
        }
        public UI CreateUI()
        {

            Rectangle inRect = new Rectangle(825, 0, 100, 100);
            FontSprite Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "SCORE<2>");
            UI Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(875, 35, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Val, "0000");
            Obj = new UI(GameObjName.Score, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);


             inRect = new Rectangle(25, 0, 100, 100);
             Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font,"SCORE<1>");
             Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);
            
            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(75, 35, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Val, "0000");
            Obj = new UI(GameObjName.Score, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);


            inRect = new Rectangle(400, 0, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "HI-SCORE");
            Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(450, 35, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.HIScore_Val, "0000");
            Obj = new UI(GameObjName.HiScore, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);


            inRect = new Rectangle(25, 750, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "2");
            Obj = new UI(GameObjName.LivesUI, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Text);

            GameObjMananger.getInstance().Add(Obj);


            inRect = new Rectangle(1024 - 300, 750, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "CREDITS");
            Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(1024 - 100, 750, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "0");
            Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.UI).Add(Text);

            GameObjMananger.getInstance().Add(Obj);


            inRect = new Rectangle(1024 / 2 - 75, 800 / 2 -100, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "PUSH");
            Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.Player).Add(Text);

            GameObjMananger.getInstance().Add(Obj);

            inRect = new Rectangle(1024/2 - 250, 800/2 - 50, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "1 OR 2 PLAYERS BUTTON");
            Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);

            SpriteBatchManager.getInstance().Find(SpriteBatchName.Player).Add(Text);

            inRect = new Rectangle(1024 / 2 - 100, 800 / 2 - 50, 100, 100);
            Text = new FontSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Score_Font, "GAME OVER");
            Obj = new UI(GameObjName.None, new Vector2(inRect.X, inRect.Y), Text);


            this.CreateLives();


            SpriteBatchManager.getInstance().Find(SpriteBatchName.GameOver).Add(Text);

            GameObjMananger.getInstance().Add(Obj);

            return Obj;
        }

        public Lives CreateLives()
        {
            Rectangle inRect = new Rectangle(0, 0, 48, 32);
            TextureSprite Life = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Ship);
            Lives _Lives = new Lives(GameObjName.Life, new Vector2(75, 760), Life);
            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Life);

            GameObjMananger.getInstance().Add(_Lives);

            inRect = new Rectangle(0, 0, 48, 32);
            Life = new TextureSprite(inRect, new Vector2(1, 1), 0.0f, 1.0f, SpriteName.Ship);
            _Lives = new Lives(GameObjName.Life, new Vector2(145, 760), Life);
            SpriteBatchManager.getInstance().Find(SpriteBatchName.Scene).Add(Life);

            GameObjMananger.getInstance().Add(_Lives);

            return _Lives;
        }

        public void Kill(GameObj inObj)
        {
            ListNode KillNode = Find(inObj);

            if (KillNode != null)
                List.Remove(KillNode);
        }

        private ListNode Find(GameObj inObj)
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

        public void KillObject(GameObj inObj,SpriteBatchName inSpName)
        {
            GameSprite _gameSprite = inObj.getGameSprite();
            if (_gameSprite != null)
               SpriteBatchManager.getInstance().Kill(_gameSprite, inSpName);

            ColObj _colObj = inObj.getColObj();
            if (_colObj != null)
                ColObjManager.getInstance().Kill(_colObj);

            ColGroup _ColGroup = ColGroupManager.getInstance().find(inObj.TempColGroupName);
            _ColGroup.Kill(inObj);

            Kill(inObj);

        }

        public void ResetLevel()
        {
           this.CreateSuper();
           this.CreateLives();
        }
    }
}
