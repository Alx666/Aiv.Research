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

                    int iColumns = (int)Math.Ceiling(Math.Sqrt(m_hInputData.Length));
                    int iRows    = (int)Math.Ceiling((double)m_hInputData.Length / iColumns);

                    m_fSizeX   = (float)m_hPanel.Width  / iColumns;
                    m_fSizeY   = (float)m_hPanel.Height / iRows;


                    List<InputInformation> hTmp = new List<InputInformation>();

                    for (int i = 0; i < iRows; i++)
                    {
                        for (int k = 0; k < iColumns; k++)
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
            e.Graphics.DrawRectangles(m_hRedPen, m_hRectangles);


            e.Graphics.FillRectangles(Brushes.Green, m_hInputData.Select(x => x.Area).Skip(5).Take(5).ToArray());

            if (m_hNonFillables.Length > 0)
                e.Graphics.FillRectangles(Brushes.Red, m_hNonFillables);
        }



        private Point Map(int iX, int iY)
        {
            Point vPoint = new Point(iX / (int)m_fSizeX, iY / (int)m_fSizeY);
            return vPoint;
        }

        public void Begin(int iX, int iY)
        {
            m_vStart        = new Point(iX, iY);
            Point vIndices  = this.Map(iX, iY);


            m_hInputData[vIndices.X * vIndices.Y].Value = 1.0f;            
            m_hGfx.FillRectangle(Brushes.Green, m_hInputData[vIndices.X * vIndices.Y].Area);
        }

        public void Update(int iX, int iY)
        {
            //if (m_vStart.HasValue)
            //{
            //    Point vNext = new Point(iX, iY);


            //    Point vIndices = this.Map(iX, iY);
            //    m_hDataSpace[vIndices.X, vIndices.Y] = 1.0f;
            //    m_hGfx.FillRectangle(Brushes.Green, QuantizedSpace[vIndices.X, vIndices.Y]);
            //    m_vStart = vNext;
            //}
        }

        public void End()
        {
            //m_vStart = null;            
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
