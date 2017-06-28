using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D.Filters
{
    class FilterCenter : Filter
    {
        public FilterCenter()
        {

        }

        public override void Apply(InputInformation[,] m_hInputData, float strenght)
        {
            float iMinX = -1;
            float iMaxX = -1;
            float iMinY = -1;
            float iMaxY = -1;

            float centerX;
            float centerY;

            float distanceCenterX;
            float distanceCenterY;

            // BINDING BOX
            int j = 0;
            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (j = 0; j < m_hInputData.GetLength(1); j++)
                {
                    if (m_hInputData[i, j].Value > 0)
                    {
                        #region MinMaxXY
                        if (iMinX != -1)
                        {
                            if (iMinX > i)
                                iMinX = i;
                        }
                        else
                            iMinX = i;

                        if (iMaxX != -1)
                        {
                            if (iMaxX < i)
                                iMaxX = i;
                        }
                        else
                            iMaxX = i;

                        if (iMinY != -1)
                        {
                            if (iMinY > j)
                                iMinY = j;
                        }
                        else
                            iMinY = j;

                        if (iMaxY != -1)
                        {
                            if (iMaxY < j)
                                iMaxY = j;
                        }
                        else
                            iMaxY = j;
                        #endregion
                    }
                }
            }

            // finding the center X and the center Y and distance from the center of the array
            centerX = (iMinX + iMaxX) / 2;
            centerY = (iMinY + iMaxY) / 2;
            distanceCenterX = m_hInputData.GetLength(0) / 2 - centerX;
            distanceCenterY = m_hInputData.GetLength(1) / 2 - centerY;

            double[,] doubleArray = new double[m_hInputData.GetLength(0), m_hInputData.GetLength(1)];

            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (j = 0; j < m_hInputData.GetLength(1); j++)
                {
                    if (m_hInputData[i, j].Value > 0)
                        doubleArray[i + (int)distanceCenterX, j + (int)distanceCenterY] = m_hInputData[i, j].Value;
                }
            }
            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (j = 0; j < m_hInputData.GetLength(1); j++)
                {
                    m_hInputData[i, j].Value = doubleArray[i, j];
                }
            }
        }

    }
}
