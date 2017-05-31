using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Encog.Neural.Networks;
using Aiv.Research.Shared;

namespace Aiv.Research.Visualizer2D
{
    class PenDrawer : IDisposable
    {
        private Pen                 m_hGreenPen;
        private Pen                 m_hRedPen;
        private Graphics            m_hGfx;
        private Panel               m_hPanel;
        private Point?              m_vStart;

        private InputInformation[]  m_hInputData;
        private RectangleF[]        m_hRectangles;
        private RectangleF[]        m_hNonFillables;
        private float               m_fSizeX;
        private float               m_fSizeY;
        private int                 m_iColumns ;
        private int                 m_iRows;
        public PenDrawer(Panel hPanel)
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
                    m_hInputData = new InputInformation[value.InputSize];

                    m_iColumns = (int)Math.Ceiling(Math.Sqrt(m_hInputData.Length));
                    m_iRows    = (int)Math.Ceiling((double)m_hInputData.Length / m_iColumns);

                    m_fSizeX   = (float)m_hPanel.Width  / m_iColumns;
                    m_fSizeY   = (float)m_hPanel.Height / m_iRows;


                    List<InputInformation> hTmp = new List<InputInformation>();

                    for (int i = 0; i < m_iRows; i++)
                    {
                        for (int k = 0; k < m_iColumns; k++)
                        {
                            InputInformation vRectData = new InputInformation();
                            vRectData.Area = new RectangleF(k * m_fSizeX, i * m_fSizeY, m_fSizeX, m_fSizeY);
                            hTmp.Add(vRectData);
                        }
                    }

                    m_hRectangles   = hTmp.Select(d => d.Area).ToArray();
                    m_hNonFillables = m_hRectangles.Skip(m_hInputData.Length).ToArray();
                    m_hInputData    = hTmp.ToArray();

                    this.m_hPanel.Invalidate();
                }
            }
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {            
            //Draw Grid
            e.Graphics.DrawRectangles(m_hRedPen, m_hRectangles);


            //Redraw Selected Cells
            for (int i = 0; i < m_hInputData.Length; i++)
            {
                if (m_hInputData[i].Value > 0.0f)
                {
                    e.Graphics.FillRectangle(Brushes.Green, m_hInputData[i].Area);
                }
            }
            
            //Redraw Non fillable cells
            if (m_hNonFillables.Length > 0)
                e.Graphics.FillRectangles(Brushes.Red, m_hNonFillables);
        }



        public void Begin(int iX, int iY)
        {
            m_vStart = new Point(iX, iY);

            int iIndex = ((int)(iY / m_fSizeY) * m_iColumns) + (int)(iX / m_fSizeX);

            if (iIndex + 1 < m_hNetwork.InputSize)
            {
                m_hInputData[iIndex].Value = 1.0f;            
                m_hGfx.FillRectangle(Brushes.Green, m_hInputData[iIndex].Area);
            }            
        }

        public void Update(int iX, int iY)
        {
            if (m_vStart.HasValue)
            {
                Point vNext = new Point(iX, iY);

                int iIndex = ((int)(iY / m_fSizeY) * m_iColumns) + (int)(iX / m_fSizeX);

                if (iIndex < m_hNetwork.InputSize)
                {
                    m_hInputData[iIndex].Value = 1.0f;
                    m_hGfx.FillRectangle(Brushes.Green, m_hInputData[iIndex].Area);
                }

                m_vStart = vNext;
            }
        }

        public void End()
        {
            m_vStart = null;            
        }

        public Bitmap Clear(out double[] hSamples)
        {
            //Bitmap hBmp = new Bitmap(m_hPanel.Width, m_hPanel.Height);

            //using (Graphics hBmpGfx = Graphics.FromImage(hBmp))
            //{
            //    hBmpGfx.CopyFromScreen(m_hPanel.PointToScreen(Point.Empty), Point.Empty, m_hPanel.Size);
            //    m_hGfx.Clear(Color.Black);

            //    //hSamples = this.m_hDataSpace.Clone() as double[,];
            //    return hBmp;
            //}
            hSamples = null;
            return null;
        }


        private class InputInformation
        {
            public double        Value;
            public RectangleF    Area;
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
