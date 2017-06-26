using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aiv.Research.Shared;
using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using Encog.Neural.Networks;
using System.IO;
using Aiv.Fast2D.Utils.Input;

namespace Aiv.Research.Tests.Landing
{
    internal class ConsoleController
    {
        private Thread m_hThread;
        private LanderAI m_hLander;

        public ConsoleController()
        {
            m_hThread = new Thread(ConsoleControllerRoutine);
        }

        private void ConsoleControllerRoutine()
        {
            
        }

        [ConsoleUIMethod]
        public void Start(string sNetwork)
        {
            Window  m_hWnd = new Window(800, 600, "Lander");

            Ground       m_hGround = new Ground();
            Lander       m_hLander;
            Recorder     m_hRecorder;
            int          m_iCounter = 0;
            LandingSite hSite = new LandingSite(m_hGround.GroundLevel);

            m_hLander = new LanderAI("../../../training_sets/Experiment4.net", hSite);
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

                if (Input.IsKeyDown(KeyCode.Q))
                {
                    m_hLander = new LanderAI("../../../training_sets/Experiment4.net", hSite);
                    m_hRecorder = new Recorder(m_hLander);
                }

                m_hGround.Update();
                hSite.Update();
                m_hLander.Update();

                m_hGround.Draw();
                hSite.Draw();
                m_hLander.Draw();

                Sample hLast = m_hRecorder.Update();

                if (hLast != null)
                {
                    Console.WriteLine(hLast.ToString());
                }

                m_hWnd.Update();
            }
        }

        [ConsoleUIMethod]
        public void SetReactorPower(float power)
        {

        }

        [ConsoleUIMethod]
        public void SetTargetAltitude(float altitude)
        {

        }
    }
}
