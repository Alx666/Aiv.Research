using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Tests.Landing
{
    class Program
    {
        private static  Window  m_hWnd;

        private static LandingSite  m_hSite;
        private static Ground       m_hGround;
        private static Lander       m_hLander;
        private static Recorder     m_hRecorder;

        static void Main(string[] args)
        {
            m_hWnd      = new Window(800, 600, "Lander");            
            m_hGround   = new Ground();
            m_hSite     = new LandingSite(m_hGround.GroundLevel);
            m_hLander   = new Lander(m_hSite);


            

            while (m_hWnd.IsOpened)
            {



                
                m_hRecorder?.Update();
                m_hGround.Update();
                m_hSite.Update();
                m_hLander.Update();

                m_hGround.Draw();
                m_hSite.Draw();
                m_hLander.Draw();

                m_hWnd.Update();
            }

            
        }
    }
}
