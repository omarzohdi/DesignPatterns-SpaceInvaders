using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Space_Invaders
{
    public enum Inputs
    {
        A = 1,
        B,
        X,
        Y,

        Left,
        Right,
        Up,
        Down,

        LeftBumper,
        RightBumper,
        RightTrigger,
        LeftTrigger,

        Space,
        Start,

        Two,
        One

    }

    public class CircularList
    {
        public static int TIMER = 45;

        public class Node
        {
            public Inputs NodeContent;
            public Node Next;
            public Node Prev;
            public int timer;

            public Node()
            {
                timer = TIMER;
            }
        }

        private Node head;
        private Node current;
        private Node tail;
        private int size;
        public int Count
        {
            get
            {
                return size;
            }
        }

        public CircularList()
        {
            size = 0;
            head = null;
        }

        public void Create(int buffersize)
        {
            size = buffersize;

            for (int i = 0; i < size; ++i)
            {
                Node _input = new Node();

                if (head == null)
                {
                    head = _input;
                    current = head;
                }
                else
                {
                    current.Next = _input;
                    _input.Prev = current;
                    current = _input;
                }
            }

            tail = current;
            tail.Next = head;
            head.Prev = head;
            current = head;
        }

        public void Add(Inputs _input)
        {
            current.NodeContent = _input;
            current.timer = TIMER;
            current = current.Next;
        }

        public void Kill()
        {
            Node TempN = head;

            for (int i = 0; i < size; ++i)
            {
                TempN.NodeContent = 0;
                TempN.timer = 0;

                TempN = TempN.Next;
            }
            current = head;
        }

        public void Update()
        {
            Node tempNode = head;
            int count = 0;

            while (count < 8)
            {
                if (tempNode.timer == 0 && tempNode.NodeContent != 0)
                {
                    tempNode.NodeContent = (Inputs)0;
                    head = head.Next;
                    tail = tail.Next;
                }
                else
                {
                    if (tempNode.timer > 0)
                        tempNode.timer--;
                }

               count++;
               tempNode = tempNode.Next;
           }
        }

        public Node Retrieve(int Position)
        {
            Node tempNode = head;
            Node retNode = null;
 
            for (int i = 0; i < size; ++i)
            {
                if (i == Position)
                {
                    retNode = tempNode;
                    break;
                }

                tempNode = tempNode.Next;
            }

            return retNode;
        }
    }
}
