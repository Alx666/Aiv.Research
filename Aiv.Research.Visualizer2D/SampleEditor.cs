using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Encog.Neural.Networks;
using Aiv.Research.Shared;
using Aiv.Research.Visualizer2D.Filters;

namespace Aiv.Research.Visualizer2D
{
    class SampleEditor : IDisposable
    {
        private Pen                 m_hGreenPen;
        private Pen                 m_hRedPen;
        private Graphics            m_hGfx;
        private Panel               m_hPanel;
        private Point?              m_vStart;

        private InputInformation[,] m_hInputData;
        private RectangleF[]        m_hRectangles;
        private float               m_fSizeX;
        private float               m_fSizeY;
        private float               m_iColumns ;
        private float               m_iRows;
        public SampleEditor(Panel hPanel)
        {
            m_hGreenPen = new Pen(Color.Green,      1f);
            m_hRedPen   = new Pen(Color.DarkRed,    1f);

            m_hGfx      = hPanel.CreateGraphics();
            m_hPanel    = hPanel;
        }


        private NetworkCreationConfig m_hNetwork;
        public NetworkCreationConfig Network
        {
            get
            {
                return m_hNetwork;
            }

            set
            {                
                m_hNetwork = value;

                if (m_hNetwork != null)
                {
                    m_iColumns = (int)Math.Ceiling(Math.Sqrt(m_hNetwork.InputSize));
                    m_iRows = (int)Math.Ceiling((double)m_hNetwork.InputSize / m_iColumns);

                    m_hInputData = new InputInformation[(int)m_iRows, (int)m_iColumns];

                    m_fSizeX = (float)m_hPanel.Width / m_iColumns;
                    m_fSizeY = (float)m_hPanel.Height / m_iRows;

                    int iCounter = m_hNetwork.InputSize;
                    List<RectangleF> hTmp = new List<RectangleF>();

                    for (int i = 0; i < m_iRows; i++)
                    {
                        for (int k = 0; k < m_iColumns; k++)
                        {
                            RectangleF vRect = new RectangleF(k * m_fSizeX, i * m_fSizeY, m_fSizeX, m_fSizeY);
                            hTmp.Add(vRect);

                            if (iCounter > 0)
                                m_hInputData[i, k] = new InputInformation(vRect, true);
                            else
                                m_hInputData[i, k] = new InputInformation(vRect, false);

                            iCounter--;
                        }
                    }

                    m_hRectangles = hTmp.ToArray();
                    this.m_hPanel.Invalidate();
                }
                else
                {
                    m_iColumns = 0;
                    m_iRows = 0;
                    m_hInputData = null;
                    m_fSizeX = 0f;
                    m_fSizeY = 0f;
                    m_hRectangles = null;
                }
            }
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {            
            //Draw Grid
            e.Graphics.DrawRectangles(m_hRedPen, m_hRectangles);


            //Redraw Selected Cells
            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (int k = 0; k < m_hInputData.GetLength(1); k++)
                {
                    InputInformation hInfo = m_hInputData[i, k];

                    if (hInfo.InUse)
                    {
                        if (hInfo.Value != 0.0)
                            e.Graphics.FillRectangle(Brushes.Green, m_hInputData[i, k].Area);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Red, m_hInputData[i, k].Area);
                    }                        
                }
            }
        }

        //private Point Map(int iX, int iY)
        //{
        //    return new Point(iX / (int)m_fSpaceX, iY / (int)m_fSpaceY);
        //}

        public void Begin(int iX, int iY)
        {
            try
            {
                m_vStart = new Point(iX, iY);

                InputInformation hInput = m_hInputData[iY / (int)m_fSizeY, iX / (int)m_fSizeX];

                if (hInput.InUse)
                {
                    hInput.Value = 1.0f;
                    m_hGfx.FillRectangle(Brushes.Green, hInput.Area);
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        public void Update(int iX, int iY)
        {
            try
            {
                if (m_vStart.HasValue)
                {
                    Point vNext = new Point(iX, iY);

                    InputInformation hInput = m_hInputData[iY / (int)m_fSizeY, iX / (int)m_fSizeX];

                    if (hInput.InUse)
                    {
                        hInput.Value = 1.0f;
                        m_hGfx.FillRectangle(Brushes.Green, hInput.Area);
                    }

                    m_vStart = vNext;
                }
            }
            catch (IndexOutOfRangeException)
            {

            }
        }

        public void End()
        {
            m_vStart = null;            
        }

        public Bitmap Clear(out double[] hSamples)
        {
            Bitmap hBmp = new Bitmap(m_hInputData.GetLength(0), m_hInputData.GetLength(1));
            hSamples    = new double[m_hNetwork.InputSize];

            FilterCenter hCentering = new FilterCenter();
            FilterGaussianBlur7x7 hFilter = new FilterGaussianBlur7x7();

            //hCentering.Apply(m_hInputData, 0);
            //hFilter.Apply(m_hInputData, 1);

            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (int k = 0; k < m_hInputData.GetLength(1); k++)
                {                    
                    InputInformation hInfo = m_hInputData[i, k];

                    Color cColor = Color.FromArgb(0, (int)(255 * hInfo.Value), 0);


                    if (hInfo.InUse)
                    {
                        if(hInfo.Value != 0)
                            hBmp.SetPixel(k, i, cColor);

                        //Quando viene chiamato Clear bisogna mettere tutti i dati della matrice dentro un array 1d
                        //ordinati per riga.. l'array deve essere preallocato x essere sicuri che non sfori il size dell'input
                        
                        hSamples[i * m_hInputData.GetLength(0) + k] = m_hInputData[i, k].Value;
                    }
                    else
                    {
                        m_hPanel.Invalidate();
                        return hBmp;
                    }

                    hInfo.Value = 0.0;
                }
            }

            m_hPanel.Invalidate();
            return hBmp;
        }



        public void OnSampleSelected(Sample hSelected)
        {
            //è Stato caricato un sample, devi riempire inputdata con i values corretti (colorali opportunamente)
            for (int i = 0; i < m_hInputData.GetLength(0); i++)
            {
                for (int k = 0; k < m_hInputData.GetLength(1); k++)
                {
                    if (hSelected.Values.Length > i * m_hInputData.GetLength(0) + k)
                        m_hInputData[i, k].Value = hSelected.Values[i * m_hInputData.GetLength(0) + k];
                }
            }

            m_hPanel.Invalidate();
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_hGreenPen.Dispose();
                    m_hGfx.Dispose();
                }

                disposedValue = true;
            }
        }
                
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
