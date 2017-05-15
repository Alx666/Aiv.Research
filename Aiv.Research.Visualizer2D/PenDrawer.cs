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
        private Pen      m_hPen;
        private Graphics m_hGfx;

        private Point?   m_vStart;

        public PenDrawer(Color eColor, float fWidth, Graphics hGfx)
        {
            m_hPen      = new Pen(eColor, fWidth);
            m_hGfx      = hGfx;            
        }

        public void Begin(int iX, int iY)
        {
            m_vStart = new Point(iX, iY);
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
    }
}
