using Aiv.Fast2D;
using Encog.Neural.Networks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Tests.Landing
{
    class LanderAI : Lander
    {
        private BasicNetwork m_hNetwork;
        private double[] m_hInput;
        private double[] m_hOutput;

        public LanderAI(string sNetwork, LandingSite hSite) : base(hSite)
        {
            m_hNetwork  = Encog.Persist.EncogDirectoryPersistence.LoadObject(new FileInfo(sNetwork)) as BasicNetwork;
            m_hInput    = new double[5];
            m_hOutput   = new double[2];
        }

        public override void Update()
        {
            base.Update();
            m_hInput[0] = Height;
            m_hInput[1] = VelocityX;
            m_hInput[2] = VelocityY;
            m_hInput[3] = VectorX;
            m_hInput[4] = VectorY;

            m_hNetwork.Compute(m_hInput, m_hOutput);


            if(m_hOutput[1] > 0.5)
                m_vVelocity.Y += -22f * Window.Current.deltaTime;
        }
    }
}
