using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D.Utils.Input;

namespace Aiv.Research.Tests.Landing
{
    class Program
    {
        private static  Window  m_hWnd;

        private static LandingSite  m_hSite;
        private static Ground       m_hGround;
        private static Lander       m_hLander;
        private static Recorder     m_hRecorder;
        private static int          m_iCounter;


        static void Main(string[] args)
        {
            m_hWnd      = new Window(800, 600, "Lander");            
            m_hGround   = new Ground();
            m_hSite     = new LandingSite(m_hGround.GroundLevel);
            m_hLander   = new LanderAI("Experimental0.net", m_hSite);
            m_hRecorder = new Recorder(m_hLander);


            while (m_hWnd.IsOpened)
            {
                Input.Update(m_hWnd);

                if (Input.IsKeyDown(KeyCode.R))
                {
                    if (!m_hRecorder.Started)
                    {
                        m_hRecorder.Start($"samples{m_iCounter}.xml");
                        m_iCounter++;
                    }
                    else
                    {
                        m_hRecorder.Stop();
                        Console.WriteLine("Recorder Stopped");
                    }
                }

                m_hGround.Update();
                m_hSite.Update();
                m_hLander.Update();

                m_hGround.Draw();
                m_hSite.Draw();
                m_hLander.Draw();

                if (m_hRecorder.Update())
                {
                    Console.WriteLine("Sample Added");
                }

                m_hWnd.Update();                
            }

            
        }
    }
}
