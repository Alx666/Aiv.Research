using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Aiv.Research.Shared.Data;
using Encog.Neural.Networks;

namespace Aiv.Research.Tests.Landing
{
    internal class Lander
    {
        private Segment[]       m_hStructure;
        private Segment         m_hLandingVector;

        private Box2            m_hBox;
        private Vector4         m_vColor;
        private Vector4         m_vColorGood;
        private Vector4         m_vColorBad;
        private Vector2         m_vVelocity;

        private LandingSite     m_hSite;

        public  bool            IsGrounded  { get; private set; }

        public Vector2          Position    { get; private set; }



        BasicNetwork m_hNetwork;

        [NeuralInput(0)]
        private double Height;
        [NeuralInput(1)]
        private double VelocityX;
        [NeuralInput(2)]
        private double VelocityY;
        [NeuralInput(3)]
        private double VectorX;
        [NeuralInput(4)]
        private double VectorY;

        [NeuralIdeal(0)]
        private double Adjustment;
        [NeuralIdeal(1)]
        private double Thrust;

        public Lander(LandingSite hSite)
        {
            m_hBox          = new Box2(new Vector2(0, 0), new Vector2(50, 50));
            m_hStructure    = new Segment[4];
            m_vColor        = new Vector4(1f, 1f, 1f, 0f);
            m_vColorGood    = new Vector4(0f, 1f, 0f, 0f);
            m_vColorBad     = new Vector4(1f, 0f, 0f, 0f);
            m_hSite         = hSite;

            m_hStructure[0]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hStructure[1]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hStructure[2]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hStructure[3]  = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hLandingVector = new Segment(0f, 0f, 0f, 0f, 2f);

            m_hBox.Translate(new Vector2(100, 100));            
        }

        public Lander(string sNetwork, LandingSite hSite) : this(hSite)
        {
            m_hNetwork = Encog.Persist.EncogDirectoryPersistence.LoadObject(new System.IO.FileInfo(sNetwork)) as BasicNetwork;
        }




        public void Update()
        {
            if (IsGrounded)
                return;

            Thrust      = 0;
            Adjustment  = 0;

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

            m_hBox.Translate(m_vVelocity);
            Position = new Vector2(m_hBox.Left + m_hBox.Width / 2, m_hBox.Top + m_hBox.Height / 2);

            m_hStructure[0].Point1 = new Vector2(m_hBox.Left, m_hBox.Top);
            m_hStructure[0].Point2 = new Vector2(m_hBox.Right,m_hBox.Top);

            m_hStructure[1].Point1 = new Vector2(m_hBox.Left, m_hBox.Bottom);
            m_hStructure[1].Point2 = new Vector2(m_hBox.Right, m_hBox.Bottom);

            m_hStructure[2].Point1 = new Vector2(m_hBox.Left, m_hBox.Top);
            m_hStructure[2].Point2 = new Vector2(m_hBox.Left, m_hBox.Bottom);

            m_hStructure[3].Point1 = new Vector2(m_hBox.Right, m_hBox.Top);
            m_hStructure[3].Point2 = new Vector2(m_hBox.Right, m_hBox.Bottom);

            m_hLandingVector.Point1 = Position;
            m_hLandingVector.Point2 = m_hSite.Position;



            Height      = this.Position.Y;
            VelocityX   = this.m_vVelocity.X;
            VelocityY   = this.m_vVelocity.Y;
            VectorX     = (m_hSite.Position - Position).X;
            VectorY     = (m_hSite.Position - Position).Y;
        }

        public void Draw()
        {
            for (int i = 0; i < m_hStructure.Length; i++)
            {
                m_hStructure[i].DrawSolidColor(m_vColor);
            }

            m_hLandingVector.DrawSolidColor(new Vector4(1f, 1f, 1f, 0f));
        }
    }
}
