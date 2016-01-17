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

    class TimeEventManager : Manager
    {
        private TimeEvent Active;
        private TimeEvent inActive;
        private int buffersize;
        private TimeSpan CurrentTime;
        private ExecuteAction Callback;

        private static TimeEventManager Instance;

        static public TimeEventManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new TimeEventManager();
                return Instance;
            }

            return Instance;
        }

        public override bool Initialize(int inReserve = 10, int inDelta = 3)
        {
            buffersize = inReserve;
            deltaGrow = inDelta;
            this.Create(buffersize);
            initialized = true;

            return initialized;
        }
        public void Update(GameTime Time)
        {
            CurrentTime = Time.TotalGameTime;
            Process(Time);
        }

        public TimeSpan GetCurrentTime()
        {
            return CurrentTime;
        }

        private void Create(int amount)
        {
            for (int i=0; i<amount; ++i)
            {
                TimeEvent TpNode = new TimeEvent();

                if (inActive != null)
                {
                    TimeEvent current = inActive;
                    TpNode.next = current;
                    current.prev = TpNode;
                    inActive = TpNode;
                }
                else
                {
                    inActive = TpNode;
                }  
            }
        }

        public void Destroy()
        {
            Active = null;
            inActive = null;
        }

        private void Process(GameTime Time)
        {
            TimeEvent Acurrent = Active;

            while (Acurrent != null)
            {
                if (Acurrent.targetTime <= Time.TotalGameTime)
                {
                    Callback = Acurrent.Callback;
                    Acurrent.Callback();

                    TimeEvent temp = Acurrent.next;

                    Remove(Acurrent);

                    Acurrent = temp; 
                }
                else
                {
                    break;
                }
            }
        }

        public void Add(TimeSpan TargetTime, Object obj, ExecuteAction inCallBack)
        {
            if (inActive == null)
                Create(deltaGrow);

            TimeEvent current = inActive;
            current.targetTime = TargetTime;
            current.data = obj;
            current.Callback = inCallBack;

            if (Active != null)
            {
                TimeEvent Acurrent = Active;
                inActive = current.next;

                if (Acurrent.targetTime > current.targetTime)
                {
                    Active = current;
                    Active.next = Acurrent;
                    Acurrent.prev = Active;
                    Active.prev = null;
                }
                else
                {
                    while (Acurrent != null)
                    {
                        if (Acurrent.next != null)
                        {
                            if (Acurrent.targetTime > current.targetTime)
                            {
                                current.next = Acurrent;
                                current.prev = Acurrent.prev;

                                Acurrent.prev.next = current;
                                Acurrent.prev = current;

                                break;
                            }
                        }
                        else
                        {
                            current.next = null;
                            current.prev = Acurrent;

                            Acurrent.next = current;
                            Acurrent = current;
                            break;
                        }

                        Acurrent = Acurrent.next;
                    }
                }
            }
            else
            {
                inActive = current.next;

                Active = current;
                Active.next = null;
                Active.prev = null;
            }  
        }

        public void Remove(TimeEvent Event)
        {
            TimeEvent current = Event;
            TimeEvent IAcurrent = inActive;

            if (inActive == null)
            {
                if (current.prev != null)
                    current.prev.next = Event.next;
                if (current.next != null)
                    current.next.prev = Event.prev;

                inActive = Event;
                Active = Event.next;

                Event.prev = null;
                Event.next = null;
            }
            else
            {
                if (current.prev != null)
                    current.prev.next = Event.next;
                if (current.next != null)
                    current.next.prev = Event.prev;

                Active = Event.next;
                Event.next = inActive;
                inActive.prev = Event;
                Event.prev = null;
                inActive = Event;
            }
        }


        public void StartTimedEvents()
        {
            TimeSpan FrameInterval = new TimeSpan(5750000);
            Super Super = (Super)GameObjMananger.getInstance().Find(GameObjName.Super);
            TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, Super, delegate { Actions.Act(Super); });

            FrameInterval = new TimeSpan(0, 0, 15);
            UFO ufo = (UFO)GameObjMananger.getInstance().Find(GameObjName.UFO);
            TimeEventManager.getInstance().Add(TimeEventManager.getInstance().GetCurrentTime() + FrameInterval, ufo, delegate { Actions.Act(ufo); });
        }
    }
}
