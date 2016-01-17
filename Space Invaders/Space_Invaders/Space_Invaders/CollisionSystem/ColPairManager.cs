using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Space_Invaders
{
    class ColPairManager : Manager
    {
        private static ColPairManager Instance;

        static public ColPairManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new ColPairManager();
                return Instance;
            }

            return Instance;
        }

        public void Collide()
        {
            int index = 0;

            ColPair Obj = (ColPair)List.getDatabyIndex(index);

            while (Obj != null)
            {
                Obj.CollideGroups();

                index++;
                Obj = (ColPair)List.getDatabyIndex(index);
            }
        }


        public void CreateCollisionPairs()
        {
            ///Alien WAll
            ColPair Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Alien),ColGroupManager.getInstance().find(ColGroupName.Wall));
            this.Add(Obj);

            ///Alien Missile
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Alien), ColGroupManager.getInstance().find(ColGroupName.Missile));
            this.Add(Obj);

            //Missile Wall
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Missile), ColGroupManager.getInstance().find(ColGroupName.Wall));
            this.Add(Obj);

            //Missile Shield
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Shield), ColGroupManager.getInstance().find(ColGroupName.Missile));
            this.Add(Obj);

            //Bomb Wall
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Bomb), ColGroupManager.getInstance().find(ColGroupName.Wall));
            this.Add(Obj);

            //Bomb Shield
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Shield), ColGroupManager.getInstance().find(ColGroupName.Bomb));
            this.Add(Obj);

            //Bomb Missile
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Bomb), ColGroupManager.getInstance().find(ColGroupName.Missile));
            this.Add(Obj);

            //Bomb Ship
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Bomb), ColGroupManager.getInstance().find(ColGroupName.Ship));
            this.Add(Obj);

            //Missile UFO
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Ufo), ColGroupManager.getInstance().find(ColGroupName.Missile));
            this.Add(Obj);

            //Alien Shield
            Obj = new ColPair(ColGroupManager.getInstance().find(ColGroupName.Alien), ColGroupManager.getInstance().find(ColGroupName.Shield));
            this.Add(Obj);

            
        }

        public ColPair Find(ColGroup inObj)
        {
            int index = 0;

            ColPair Obj = (ColPair)List.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.getColGroupA().Equals(inObj) || Obj.getColGroupB().Equals(inObj))
                    return Obj;

                index++;
                Obj = (ColPair)List.getDatabyIndex(index);
            }

            return null;
        }
    }
}
