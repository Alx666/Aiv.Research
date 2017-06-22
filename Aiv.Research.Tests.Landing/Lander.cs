using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Aiv.Research.Tests.Landing
{
    internal class Lander
    {
        private Segment[]       m_hSegments;
        private Box2            m_hBox;
        private Vector4         m_vColor;
        private Vector2         m_vVelocity;
        

        public Lander()
        {
            m_hBox          = new Box2(new Vector2(0, 0), new Vector2(50, 50));
            m_hSegments     = new Segment[4];
            m_vColor        = new Vector4(1f, 0f, 0f, 0f);

            m_hSegments[0]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hSegments[1]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hSegments[2]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hSegments[3]  = new Segment(0f, 0f, 0f, 0f, 2f);

            m_hBox.Translate(new Vector2(100, 100));           
        }

        public void Update()
        {            
            m_vVelocity.Y += 9.8f * Window.Current.deltaTime;

            if (Window.Current.GetKey(KeyCode.Space))
                m_vVelocity.Y += -22f * Window.Current.deltaTime;

            if (Window.Current.GetKey(KeyCode.Right))
                m_vVelocity.X += 10f * Window.Current.deltaTime;

            if (Window.Current.GetKey(KeyCode.Left))
                m_vVelocity.X += -10 * Window.Current.deltaTime;


            m_hBox.Translate(m_vVelocity);


            m_hSegments[0].Point1 = new Vector2(m_hBox.Left, m_hBox.Top);
            m_hSegments[0].Point2 = new Vector2(m_hBox.Right,m_hBox.Top);

            m_hSegments[1].Point1 = new Vector2(m_hBox.Left, m_hBox.Bottom);
            m_hSegments[1].Point2 = new Vector2(m_hBox.Right, m_hBox.Bottom);

            m_hSegments[2].Point1 = new Vector2(m_hBox.Left, m_hBox.Top);
            m_hSegments[2].Point2 = new Vector2(m_hBox.Left, m_hBox.Bottom);

            m_hSegments[3].Point1 = new Vector2(m_hBox.Right, m_hBox.Top);
            m_hSegments[3].Point2 = new Vector2(m_hBox.Right, m_hBox.Bottom);
        }

        public void Draw()
        {
            for (int i = 0; i < m_hSegments.Length; i++)
            {
                m_hSegments[i].DrawSolidColor(m_vColor);
            }
        }
    }
}
