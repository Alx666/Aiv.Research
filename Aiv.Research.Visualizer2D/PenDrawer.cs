using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Aiv.Research.Visualizer2D
{
    class PenDrawer : IDrawer, IDisposable
    {
        private Pen         m_hPen;
        private Graphics    m_hGfx;
        private Panel       m_hPanel;

        private Point?      m_vStart;


        public PenDrawer(Color eColor, float fWidth, Panel hPanel)
        {
            m_hPen          = new Pen(eColor, fWidth);
            m_hGfx          = hPanel.CreateGraphics();
            m_hPanel        = hPanel;
        }
        

        public void Begin(int iX, int iY)
        {
            m_vStart    = new Point(iX, iY);            
        }

        public void Update(int iX, int iY)
        {
            if (m_vStart.HasValue)
            {
                Point vNext = new Point(iX, iY);
                m_hGfx.DrawLine(m_hPen, m_vStart.Value, vNext);
                m_vStart = vNext;
            }
        }

        public void End()
        {
            m_vStart = null;            
        }

        public Bitmap Clear()
        {            
            Bitmap hBmp = new Bitmap(m_hPanel.Width, m_hPanel.Height);

            using (Graphics hBmpGfx = Graphics.FromImage(hBmp))
            {
                hBmpGfx.CopyFromScreen(m_hPanel.PointToScreen(Point.Empty), Point.Empty, m_hPanel.Size);
                m_hGfx.Clear(Color.Black);
                return hBmp;
            }                
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

    class NullDrawer : IDrawer
    {
        public void Begin(int iX, int iY)
        {            
        }

        public void End()
        {            
        }

        public void Update(int iX, int iY)
        {            
        }

        public Bitmap Clear()
        {
            return null;
        }
    }
}
