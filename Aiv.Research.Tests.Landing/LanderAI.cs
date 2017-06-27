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
    class LanderAI : Lander
    {
        private BasicNetwork m_hNetwork;
        private double[] m_hInput;
        private double[] m_hOutput;

        public LanderAI(string sNetwork, LandingSite hSite) : base(hSite)
        {
            m_hNetwork  = Encog.Persist.EncogDirectoryPersistence.LoadObject(new FileInfo(sNetwork)) as BasicNetwork;
            m_hInput    = new double[2];
            m_hOutput   = new double[1];
        }

        public override void Update()
        {
            
            base.Update();
            m_hInput[0] = TargetAltitude - Height;
            m_hInput[1] = VelocityY;
            //m_hInput[1] = VelocityX;
            //m_hInput[3] = VectorX;
            //m_hInput[4] = VectorY;

            m_hNetwork.Compute(m_hInput, m_hOutput);     
            float fIncrement = (float)(-REACTOR_POWER * m_hOutput[0] * Window.Current.deltaTime);
            m_vVelocity.Y += fIncrement;

            //string sLine = $"{m_hOutput[0]}";
            //Thread.Sleep(500);
            //Console.WriteLine(sLine);
        }
    }
}
