using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Encog.Neural.Networks;

namespace Aiv.Research.Visualizer2D
{
    class PenDrawer : IDisposable
    {
        private Pen m_hPen;
        private Graphics m_hGfx;
        private Panel m_hPanel;

        private Point?              m_vStart;
        private InputInformation[]  m_hInputData;





        private Rectangle[,] m_hQuantizedSpace;
        private double[,] m_hDataSpace;
        private float m_fSpaceX;
        private float m_fSpaceY;

        public PenDrawer(Color eColor, float fWidth, Panel hPanel)
        {
            m_hPen = new Pen(eColor, fWidth);
            m_hGfx = hPanel.CreateGraphics();
            m_hPanel = hPanel;
        }


        private BasicNetwork m_hNetwork;
        public BasicNetwork Network
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
                    m_hInputData = new InputInformation[value.GetLayerNeuronCount(0)];
                    this.m_hPanel.Invalidate();
                }
            }
        }


        public void OnPaint(object sender, PaintEventArgs e)
        {

        }


        //public Rectangle[,] QuantizedSpace
        //{
        //    get { return m_hQuantizedSpace; }
        //    set
        //    {
        //        m_hQuantizedSpace   = value;
        //        m_hDataSpace        = new double[value.GetLength(0), value.GetLength(1)];
        //        m_fSpaceX           = m_hPanel.Width / m_hDataSpace.GetLength(1);
        //        m_fSpaceY           = m_hPanel.Height / m_hDataSpace.GetLength(0);
        //    }
        //}



        private Point Map(int iX, int iY)
        {
            Point vPoint = new Point(iY / (int)m_fSpaceY, iX / (int)m_fSpaceX);
            return vPoint;
        }


        public void Begin(int iX, int iY)
        {
            //m_vStart    = new Point(iX, iY);
            //Point vIndices = this.Map(iX, iY);


            //m_hDataSpace[vIndices.X, vIndices.Y] = 1.0f;
            //m_hGfx.FillRectangle(Brushes.Green, QuantizedSpace[vIndices.X, vIndices.Y]);
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


        private struct InputInformation
        {
            public double       Value;
            public Rectangle    Area;
        }



        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_hPen.Dispose();
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
