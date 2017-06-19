using Aiv.Research.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D.Filters
{
    static class Filters
    {
        static Filters()
        {
            GaussianBlur3x3 =           new FilterGaussianBlur3x3();
            GaussianBlur5x5 =           new FilterGaussianBlur5x5();
            GaussianBlur7x7 =           new FilterGaussianBlur7x7();
            CenterOfMass    =           new FilterCenter();
            NumericIndipendentData =    new FilterNumericIndipendentData();
        }

        public static FilterGaussianBlur3x3        GaussianBlur3x3 { get; private set; }
        public static FilterGaussianBlur5x5        GaussianBlur5x5 { get; private set; }
        public static FilterGaussianBlur7x7        GaussianBlur7x7 { get; private set; }
        public static FilterCenter                 CenterOfMass    { get; private set; }
        public static FilterNumericIndipendentData NumericIndipendentData { get; private set; }

        public static void Apply(Filter hFilter, List<Sample> samples, float Rows, float Columns)
        {
            //2) prendi i campioni: 
            //3)Per ogni sample lo trasformi in InputInformation
            foreach (Sample item in samples)
            {
                InputInformation[,] hToEdit = new InputInformation[(int)Rows, (int)Columns];
                for (int i = 0; i < (int)Rows; i++)
                {
                    for (int k = 0; k < (int)Columns; k++)
                    {
                        hToEdit[i, k] = new InputInformation();
                    }
                }
                for (int x = 0; x < Rows; x++)
                {
                    for (int y = 0; y < Columns; y++)
                    {
                        hToEdit[x, y].Value = item.Values[x * hToEdit.GetLength(0) + y];
                    }
                }

                //Applicare il filtro
                hFilter.Apply(hToEdit, 1);

                for (int x = 0; x < Rows; x++)
                {
                    for (int y = 0; y < Columns; y++)
                    {
                        item.Values[x * hToEdit.GetLength(0) + y] = (float)hToEdit[x, y].Value;
                    }
                }
            }

        }

    }
}
