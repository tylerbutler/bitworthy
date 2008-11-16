using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Lights_Out.Library;

namespace Lights_Out.ContentExtensionLibrary
{
    [ContentTypeWriter]
    public class PuzzleContentWriter : ContentTypeWriter<Puzzle>
    {
        protected override void Write(
                ContentWriter output,
                Puzzle value)
        {
            output.Write(value.PuzzleNumber);
            output.Write(value.SizeAcross);
            output.Write(value.SizeDown);

            bool[,] array = value.State;

            for (int i = 0; i < value.SizeAcross; i++)
            {
                output.WriteObject<bool[]>(getSingleArray(ref array, i));
            }

            for (int i = 0; i < value.SizeAcross; i++)
            {
                for (int j = 0; j < value.SizeDown; j++)
                {
                    output.Write(value.State[i, j]);
                }
            }
        }

        private bool[] getSingleArray(ref bool[,] array, int row)
        {
            bool[] toReturn = new bool[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                toReturn[i] = array[i, row];
            }
            return toReturn;
        }

        public override string GetRuntimeReader(
            TargetPlatform targetPlatform)
        {
            //return typeof(PuzzleContentReader).AssemblyQualifiedName;
            return typeof(Lights_Out.Library.PuzzleContentReader).AssemblyQualifiedName;
        }

    }
}

