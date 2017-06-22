using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Tests.Landing
{
    internal class Ground
    {
        private Segment m_hLine;
        private Vector4 m_vColor;
                
        public Ground()
        {            
            m_hLine     = new Segment(0f, Window.Current.Height - Window.Current.Height / 8, Window.Current.Width, Window.Current.Height - Window.Current.Height / 8, 1f);
            m_vColor    = new Vector4(1f, 1f, 1f, 0f);
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            m_hLine.DrawSolidColor(m_vColor);
        }
    }
}
