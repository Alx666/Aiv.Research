using Aiv.Fast2D;
using OpenTK;
using System;

namespace Aiv.Research.Tests.Landing
{
    public class LandingSite
    {
        private Segment m_hLine;
        private Vector4 m_vColor;
        private Random  m_hRand;
        private const int Length = 200;

        public float GroundLevel { get; private set; }
        public Vector2 Position { get; private set; }

        public LandingSite(float fGroundLevel)
        {
            m_hRand = new Random();
            int iPosition = m_hRand.Next(0, Window.Current.Width - Length);

            GroundLevel = fGroundLevel;
            Position = new Vector2(iPosition + Length / 2, GroundLevel);

            m_hLine     = new Segment(iPosition, GroundLevel, iPosition + Length, GroundLevel, 3f);
            m_vColor    = new Vector4(0.2f, 1f, 0.2f, 0f);
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