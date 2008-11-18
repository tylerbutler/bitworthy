using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Xml;

namespace Lights_Out
{
    public class Puzzle
    {
        int puzzleNumber;
        int sizeAcross, sizeDown;
        bool[,] puzzle;

        public Puzzle(int puzzleNumberIn, int numTilesAcross, int numTilesDown)
        {
            PuzzleNumber = puzzleNumberIn;
            SizeAcross = numTilesAcross;
            SizeDown = numTilesDown;
            puzzle = new bool[SizeAcross, SizeDown];
            //for (int i = 0; i < SizeAcross; i++)
            //{
            //    for (int j = 0; j < SizeDown; j++)
            //    {
            //        puzzle[i,j] = 
            //    }
            //}          
        }

        public Puzzle() { }

        [ContentSerializerIgnore]
        public bool[,] State
        {
            get { return puzzle; }
            set { puzzle = value; }
        }

        public int SizeAcross
        {
            get { return sizeAcross; }
            set { sizeAcross = value; }
        }

        public int SizeDown
        {
            get { return sizeDown; }
            set { sizeDown = value; }
        }

        public int PuzzleNumber
        {
            get { return puzzleNumber; }
            set { puzzleNumber = value; }
        }
    }

    public class PuzzleContentReader : ContentTypeReader<Puzzle>
    {
        protected override Puzzle Read(ContentReader input, Puzzle existingInstance)
        {
            int puzzleNumber = input.ReadInt32();
            int sizeAcross = input.ReadInt32();
            int sizeDown = input.ReadInt32();

            Puzzle p = new Puzzle(puzzleNumber, sizeAcross, sizeDown);

            //bool[,] array = new bool[sizeAcross, sizeDown];
            //for (int i = 0; i < sizeAcross; i++)
            //{
            //    addRow(ref array, input.ReadObject<bool[]>(), i);
            //}

            return p;
        }

        private void addRow(ref bool[,] multiArray, bool[] array, int row)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                multiArray[i, row] = array[i];
            }
        }
    }
}