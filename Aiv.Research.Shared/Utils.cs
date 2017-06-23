using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Shared
{
    public static class Utils
    {
        /// <summary>
        /// Converte un array bidimensionale di un tipo ad un altro
        /// </summary>
        /// <typeparam name="TInput">Tipo di input</typeparam>
        /// <typeparam name="TOutput">Tipo di outpu</typeparam>
        /// <param name="array">Array di input</param>
        /// <param name="converter">Func di conversione</param>
        /// <returns></returns>
        public static TOutput[][] ConvertArray2D<TInput, TOutput>(TInput[][] array, Func<TInput, TOutput> converter)
        {
            int length0 = array.Length;

            TOutput[][] result = new TOutput[length0][];

            for (int i = 0; i < length0; i++)
            {
                int length1 = array[i].Length;
                result[i] = new TOutput[length1];


                for (int j = 0; j < length1; j++)
                {
                    if (result[i] == null)
                    {
                        result[i] = new TOutput[length0];
                    }
                    result[i][j] = converter(array[i][j]);
                }
            }

            return result;
        }
    }
}
