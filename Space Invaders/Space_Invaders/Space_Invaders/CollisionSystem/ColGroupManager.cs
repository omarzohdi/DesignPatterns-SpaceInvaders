using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{
    enum ColGroupName
    {
        Alien,
        Wall,
        Shield,
        Missile,
        Bomb,
        Ufo,
        Ship
    }

    class ColGroupManager
    {
        static ColGroupManager Instance;
        LinkedList ColGroups;
        int DeltaGrow;
        bool initialized;

        static public ColGroupManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new ColGroupManager();
                return Instance;
            }

            return Instance;
        }

        public bool Initialize(int buffesize = 10, int delta = 3)
        {
            DeltaGrow = delta;
            ColGroups = new LinkedList(buffesize, delta, NodeType.ColGrp);
            initialized = true;

            return initialized;
        }

        public void Add(Object inObj)
        {
            ColGroups.Add(inObj);
        }

        public void Remove(Node inNode)
        {
            ColGroups.Remove(inNode);
        }

        public ColGroup find(ColGroupName inName)
        {
            int index = 0;

            ColGroup Obj = (ColGroup)ColGroups.getDatabyIndex(index);

            while (Obj != null)
            {
                if (Obj.Name.Equals(inName))
                    return Obj;

                index++;
                Obj = (ColGroup)ColGroups.getDatabyIndex(index);
            }

            return null;
        }

        public ColGroup Create()
        {
            ColGroup inColGroup = new ColGroup(ColGroupName.Missile);
            this.Add(inColGroup);

            inColGroup = new ColGroup(ColGroupName.Alien);
            this.Add(inColGroup);

            inColGroup = new ColGroup(ColGroupName.Wall);
            this.Add(inColGroup);

            inColGroup = new ColGroup(ColGroupName.Shield);
            this.Add(inColGroup);

            inColGroup = new ColGroup(ColGroupName.Ship);
            this.Add(inColGroup);

            inColGroup = new ColGroup(ColGroupName.Bomb);
            this.Add(inColGroup);

            inColGroup = new ColGroup(ColGroupName.Ufo);
            this.Add(inColGroup);
            
            return inColGroup;
        }
    }
}
