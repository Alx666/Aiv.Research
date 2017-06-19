using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D.Filters
{
    class FilterNumericIndipendentData : Filter
    {
        public FilterNumericIndipendentData()
        {

        }

        public override void Apply(InputInformation[,] m_hInputData, float strenght)
        {
            int counter = 0;
            double averageValue = 0;
            for (int x = 0; x < m_hInputData.GetLength(0); x++)
            {
                for (int y = 0; y < m_hInputData.GetLength(1); y++)
                {
                    averageValue += m_hInputData[x, y].Value;
                    counter++;
                }
            }

            averageValue = averageValue / counter;

            double[,] deviationArray = new double[m_hInputData.GetLength(0), m_hInputData.GetLength(1)];
            for (int x = 0; x < m_hInputData.GetLength(0); x++)
            {
                for (int y = 0; y < m_hInputData.GetLength(1); y++)
                {
                    double rawValue = m_hInputData[x, y].Value - averageValue;
                    deviationArray[x, y] = rawValue * rawValue;
                }
            }

            double variance = 0;
            for (int x = 0; x < m_hInputData.GetLength(0); x++)
            {
                for (int y = 0; y < m_hInputData.GetLength(1); y++)
                {
                    variance = deviationArray[x, y];
                }
            }

            variance = variance / counter;
            double standardDeviation = Math.Sqrt(variance);

            for (int x = 0; x < m_hInputData.GetLength(0); x++)
            {
                for (int y = 0; y < m_hInputData.GetLength(1); y++)
                {
                    m_hInputData[x, y].Value = (m_hInputData[x, y].Value - averageValue) / standardDeviation;
                }
            }

        }

    }
}
