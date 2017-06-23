using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using Encog.Neural.Networks;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Tests.Landing
{
    class LanderHuman : Lander
    {

        private static Window m_hWnd;
        
        private static Ground m_hGround;
        private static Lander m_hLander;
        private static Recorder m_hRecorder;

        private Segment[] m_hStructure;
        private Segment m_hLandingVector;

        private Box2 m_hBox;
        private Vector4 m_vColor;
        private Vector4 m_vColorGood;
        private Vector4 m_vColorBad;

        private LandingSite m_hSite;

        public bool IsGrounded { get; private set; }

        public Vector2 Position { get; private set; }

        int iCounter;

        public LanderHuman(LandingSite hSite) : base(hSite)
        {
            iCounter = 0;
        }
        override public void Update()
        {
            if (IsGrounded)
                return;

            Thrust = 0;
            Adjustment = 0;

            m_vVelocity.Y += 9.8f * Window.Current.deltaTime;

            if (Window.Current.GetKey(KeyCode.Space))
            {
                m_vVelocity.Y += -22f * Window.Current.deltaTime;
                Thrust = 1;
            }
            
            if (Window.Current.GetKey(KeyCode.Right))
            {
                m_vVelocity.X += 10f * Window.Current.deltaTime;
                Adjustment = 1;
            }
            
            if (Window.Current.GetKey(KeyCode.Left))
            {
                m_vVelocity.X += -10 * Window.Current.deltaTime;
                Adjustment = -1;
            }
            
            
            if (m_hBox.Bottom >= m_hSite.GroundLevel)
            {
                m_vColor = m_vVelocity.Length < 2.0f ? m_vColorGood : m_vColorBad;
                m_vVelocity = Vector2.Zero;
                IsGrounded = true;
            }


            if (m_hWnd.GetKey(KeyCode.R))
            {
                m_hRecorder?.Stop();
                m_hGround = new Ground();
                m_hSite = new LandingSite(m_hGround.GroundLevel);
                m_hLander = new LanderHuman(m_hSite);
                m_hRecorder = new Recorder(m_hLander);
                m_hRecorder.Start("Lander" + iCounter + ".xml");
                iCounter++;
            }



            m_hBox.Translate(m_vVelocity);
            Position = new Vector2(m_hBox.Left + m_hBox.Width / 2, m_hBox.Top + m_hBox.Height / 2);

            m_hStructure[0].Point1 = new Vector2(m_hBox.Left, m_hBox.Top);
            m_hStructure[0].Point2 = new Vector2(m_hBox.Right, m_hBox.Top);

            m_hStructure[1].Point1 = new Vector2(m_hBox.Left, m_hBox.Bottom);
            m_hStructure[1].Point2 = new Vector2(m_hBox.Right, m_hBox.Bottom);

            m_hStructure[2].Point1 = new Vector2(m_hBox.Left, m_hBox.Top);
            m_hStructure[2].Point2 = new Vector2(m_hBox.Left, m_hBox.Bottom);

            m_hStructure[3].Point1 = new Vector2(m_hBox.Right, m_hBox.Top);
            m_hStructure[3].Point2 = new Vector2(m_hBox.Right, m_hBox.Bottom);

            m_hLandingVector.Point1 = Position;
            m_hLandingVector.Point2 = m_hSite.Position;



            Height = this.Position.Y;
            VelocityX = this.m_vVelocity.X;
            VelocityY = this.m_vVelocity.Y;
            VectorX = (m_hSite.Position - Position).X;
            VectorY = (m_hSite.Position - Position).Y;
        }

    }
}
