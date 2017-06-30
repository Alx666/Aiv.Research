using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using OpenTK;

namespace Aiv.Research.Tests.Landing
{
    internal abstract class Lander : IEntity
    {
        public bool     IsGrounded      { get; private set; }
        public Vector2  Position        { get; private set; }
        public float    Gravity         { get; set; }
        public float    TargetAltitude  { get; set; }

        private Segment[]       m_hStructure;
        private Segment         m_hLandingVector;
        private Segment         m_hTargetHeight;
        private Box2            m_hBox;
        private Vector4         m_vColor;
        private Vector4         m_vColorGood;
        private Vector4         m_vColorBad;

        protected Vector2       m_vVelocity;
        protected LandingSite   m_hSite;

        
        protected const float   REACTOR_POWER   = 12;

        [NeuralInput(0)]
        protected double InHeight;
        [NeuralInput(1)]
        protected double InWidth;
        [NeuralInput(2)]
        protected double InVelocityY;
        [NeuralInput(3)]
        protected double InVelocityX;
        [NeuralInput(4)]
        protected double InGravity;
        [NeuralInput(5)]
        protected double TargetDestinationX;
        [NeuralInput(6)]
        protected double TargetDestinationY;

        [NeuralIdeal(0)]
        protected double VertThrust;
        [NeuralIdeal(1)]
        protected double HorizThrust;

        public Lander(LandingSite hSite)
        {
            TargetAltitude      = 200f;
            m_hBox              = new Box2(new Vector2(0, 0), new Vector2(50, 50));
            m_hStructure        = new Segment[4];
            m_vColor            = new Vector4(1f, 1f, 1f, 0f);
            m_vColorGood        = new Vector4(0f, 1f, 0f, 0f);
            m_vColorBad         = new Vector4(1f, 0f, 0f, 0f);
            m_hSite             = hSite;

            m_hStructure[0]     = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hStructure[1]     = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hStructure[2]     = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hStructure[3]     = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hLandingVector    = new Segment(0f, 0f, 0f, 0f, 2f);
            m_hTargetHeight     = new Segment(0f, TargetAltitude, Window.Current.Width, TargetAltitude, 1f);

            m_hBox.Translate(new Vector2(100, 100));

            Gravity = 9.8f;
        }
       

        virtual public void Update()
        {
            if (IsGrounded)
                return;

            m_vVelocity.Y += Gravity * Window.Current.deltaTime;


            if(m_hBox.Bottom >= m_hSite.GroundLevel)
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

            
            TargetDestinationX = this.Position.X - m_hSite.Position.X;
            TargetDestinationY = this.Position.Y - TargetAltitude;

            InHeight    = TargetAltitude        - this.Position.Y;
            InWidth     = m_hSite.Position.X    - this.Position.X;
            InVelocityX = m_vVelocity.X;
            InVelocityY = m_vVelocity.Y;
            InGravity   = Gravity;
        }

        virtual public void Draw()
        {
            for (int i = 0; i < m_hStructure.Length; i++)
            {
                m_hStructure[i].DrawSolidColor(m_vColor);
            }

            m_hTargetHeight.Point1 = new Vector2(0, TargetAltitude);
            m_hTargetHeight.Point2 = new Vector2(Window.Current.Width, TargetAltitude);

            m_hTargetHeight.DrawSolidColor(new Vector4(0.2f, 0.2f, 0.2f, 0f));
            m_hLandingVector.DrawSolidColor(new Vector4(1f, 1f, 1f, 0f));
        }
    }
}
