using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CollisionSystem
{
    public enum Combos
    {
        None = -2,
        Seek = -1,

        Punch = 0,
        Kick = 1,
        DoubleKick = 2,

    }
    public class Combo_Manager
    {
        static private Combo_Manager instance;
        private Inputs[] Prev_InputSequence;
        private Inputs[] Curr_InputSequence;
        private Inputs[][] CombosSequence;
        private int buffersize = 0;
        private Combos Current_Combo = Combos.None;

        public static Combo_Manager Instance()
        {
            if (instance == null)
            {
                instance = new Combo_Manager();
            }

            return instance;
        }


        public void Initialize(int size = 8)
        {
            buffersize = size;
            Prev_InputSequence = new Inputs[buffersize];
            Curr_InputSequence = new Inputs[buffersize];

            GenerateCombos();
        }
        private void GenerateCombos()
        {
            CombosSequence = new Inputs[10][];

            for (int i=0; i< 10; ++i)
                CombosSequence[i] = new Inputs[buffersize];


            ////////////////////////Creating Combo Sequences/////////////////////////////////
            //////////////////////////10 Combos with a Max of 8 Buttons each///////////////

            CombosSequence[0][0] = Inputs.A;
            CombosSequence[0][1] = Inputs.A;
            CombosSequence[0][2] = Inputs.B;

            CombosSequence[1][0] = Inputs.A;
            CombosSequence[1][1] = Inputs.A;
            CombosSequence[1][2] = Inputs.B;
            CombosSequence[1][3] = Inputs.B;

            CombosSequence[2][0] = Inputs.A;
            CombosSequence[2][1] = Inputs.Y;
            CombosSequence[2][2] = Inputs.X;
            CombosSequence[2][3] = Inputs.B;

            CombosSequence[3][0] = Inputs.A;
            CombosSequence[3][1] = Inputs.X;
            CombosSequence[3][2] = Inputs.B;
            CombosSequence[3][3] = Inputs.Y;

            CombosSequence[4][0] = Inputs.A;
            CombosSequence[4][1] = Inputs.X;
            CombosSequence[4][2] = Inputs.X;
            CombosSequence[4][3] = Inputs.B;
            CombosSequence[4][4] = Inputs.B;

            CombosSequence[5][0] = Inputs.A;
            CombosSequence[5][1] = Inputs.X;
            CombosSequence[5][2] = Inputs.X;
            CombosSequence[5][3] = Inputs.Y;
            CombosSequence[5][4] = Inputs.B;

            CombosSequence[6][0] = Inputs.Left;
            CombosSequence[6][1] = Inputs.Up;
            CombosSequence[6][2] = Inputs.X;
            CombosSequence[6][3] = Inputs.X;

            CombosSequence[7][0] = Inputs.Down;
            CombosSequence[7][1] = Inputs.Right;
            CombosSequence[7][2] = Inputs.Y;
            CombosSequence[7][3] = Inputs.Y;

            CombosSequence[8][0] = Inputs.Left;
            CombosSequence[8][1] = Inputs.Up;
            CombosSequence[8][2] = Inputs.Right;
            CombosSequence[8][3] = Inputs.Down;
            CombosSequence[8][4] = Inputs.A;
            CombosSequence[8][5] = Inputs.B;

            CombosSequence[9][0] = Inputs.A;
            CombosSequence[9][1] = Inputs.B;
            CombosSequence[9][2] = Inputs.X;
            CombosSequence[9][3] = Inputs.Y;
            CombosSequence[9][4] = Inputs.Left;
            CombosSequence[9][5] = Inputs.Up;
            CombosSequence[9][6] = Inputs.Right;
            CombosSequence[9][7] = Inputs.Down;
            
        }
        public bool Update(CircularList _Inputs)
        {
            bool ComboFound = false;
           
            for (int i = 0; i < _Inputs.Count; ++i)
            {
                
                Curr_InputSequence[i] = _Inputs.Retrieve(i).NodeContent;

                if (Curr_InputSequence[i] != Prev_InputSequence[i] && Curr_InputSequence[i] != 0 && Prev_InputSequence[i] != 0)
                    ComboFound = checkSequence();
            }

            resetCombo();

            return ComboFound;
        }

        private bool checkSequence()
        {
            Current_Combo = Combos.None;
            Combos HighestCombo = Combos.None;

            for (int i = 0; i < buffersize; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (Prev_InputSequence[i] == CombosSequence[j][0])
                    {
                        for (int k = 0; k < buffersize; ++k)
                        {
                            if (Prev_InputSequence[k] != CombosSequence[j][k])
                            {
                                Current_Combo = Combos.None;
                                break;
                            }

                            Current_Combo = (Combos)j;
                        }

                    }

                    if (Current_Combo > HighestCombo)
                        HighestCombo = Current_Combo;
                }
            }

            if (HighestCombo != Combos.None)
            {
                Current_Combo = HighestCombo;
            }

            if (Current_Combo == Combos.None || Current_Combo == Combos.Seek)
                return false;
            else
                return true;
        }

        private void resetCombo()
        {
            for (int i = 0; i < buffersize; ++i)
            {
                Prev_InputSequence[i] = Curr_InputSequence[i];
                Curr_InputSequence[i] = 0;
            }
        }
    }
}
