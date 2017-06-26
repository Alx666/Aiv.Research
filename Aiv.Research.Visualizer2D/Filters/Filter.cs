using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
{
    public abstract class Filter
    {
        public double[,] Matrix { get; protected set; }
        

        public Filter()
        {
        }

        public virtual void Apply(InputInformation[,] m_hInputData, float strenght)
        {
            // setting the Filter Matrix

            int centerPixelX = Matrix.GetLength(0) / 2;
            int centerPixelY = Matrix.GetLength(1) / 2;

            double[,] doubleArray = new double[m_hInputData.GetLength(0), m_hInputData.GetLength(1)];
            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (int j = 0; j < m_hInputData.GetLength(1); j++)
                {
                    doubleArray[i, j] = m_hInputData[i, j].Value;
                }
            }

            // FILTERING
            for (int x = 0; x < m_hInputData.GetLength(0); x++)
            {
                for (int y = 0; y < m_hInputData.GetLength(1); y++)
                {
                    //PIXEL
                    for (int iFilterX = 0 - centerPixelX; iFilterX < Matrix.GetLength(0) - centerPixelX; iFilterX++)
                    {
                        for (int iFilterY = 0 - centerPixelY; iFilterY < Matrix.GetLength(1) - centerPixelY; iFilterY++)
                        {

                            if (x + iFilterX >= 0 && x + iFilterX < m_hInputData.GetLength(0) && y + iFilterY >= 0 && y + iFilterY < m_hInputData.GetLength(1))
                            {
                                doubleArray[x + iFilterX, y + iFilterY] += (m_hInputData[x, y].Value - m_hInputData[x + iFilterX, y + iFilterY].Value) * Matrix[iFilterX + centerPixelX, iFilterY + centerPixelY] * strenght;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (int j = 0; j < m_hInputData.GetLength(1); j++)
                {
                    m_hInputData[i, j].Value = doubleArray[i, j];
                }
            }
        }
    }
}
