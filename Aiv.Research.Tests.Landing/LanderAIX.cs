using Aiv.Fast2D;
using Encog.Neural.Networks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aiv.Research.Tests.Landing
{
    class LanderAIX : Lander
    {
        private BasicNetwork m_hNetwork;
        private double[] m_hInput;
        private double[] m_hOutput;

        public LanderAIX(string sNetwork, LandingSite hSite) : base(hSite)
        {
            m_hNetwork = Encog.Persist.EncogDirectoryPersistence.LoadObject(new FileInfo(sNetwork)) as BasicNetwork;
            m_hInput   = new double[6];
            m_hOutput  = new double[2];
        }

        public override void Update()
        {
            base.Update();

            m_hInput[0] = InHeight * 1000;
            m_hInput[1] = InWidth  * 1000;
            m_hInput[2] = InVelocityX;
            m_hInput[3] = InVelocityY;
            m_hInput[4] = InGravity;
            m_hInput[5] = InTargetDistance;

            m_hNetwork.Compute(m_hInput, m_hOutput);

            float fIncrementY = (float)(-REACTOR_POWER * m_hOutput[0] * Window.Current.deltaTime);
            float fIncrementX = (float)( REACTOR_POWER * 0.5f * m_hOutput[1] * Window.Current.deltaTime);

            //Console.WriteLine(m_hInput[0]);

            m_vVelocity.Y -= fIncrementY;
            m_vVelocity.X += fIncrementX;

            //Console.WriteLine(InWidth + " " + fIncrementX);
        }
    }
}
