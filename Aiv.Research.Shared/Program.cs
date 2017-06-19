using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Shared
{
    class Program
    {
        static void Main(string[] args)
        {
            TrainingService service = new TrainingService(4);
            TrainingClient client = new TrainingClient();
        }

        public static TOutput[,] ConvertAll<TInput, TOutput>(TInput[,] array, Converter<TInput, TOutput> converter)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            int height = array.GetLength(0);
            int width = array.GetLength(1);
            TOutput[,] localArray = new TOutput[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    localArray[i, j] = converter(array[i, j]);
            }
            return localArray;
        }
    }
}
