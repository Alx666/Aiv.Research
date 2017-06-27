using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Aiv.Research.Shared;
using Aiv.Fast2D;
using Aiv.Fast2D.Utils.Input;
using Aiv.Research.Shared.Data;
using System.Collections.Concurrent;

namespace Aiv.Research.Tests.Landing
{
    internal class Controller
    {
        private Thread m_hThread;
               
        public Controller()
        {
            m_hThread = new Thread(MainLoop);
            m_hThread.Start();
        }

        private void MainLoop()
        {
            Window      hWnd        = new Window(800, 600, "Lander");
            Ground      hGround     = new Ground();
            LandingSite hSite       = new LandingSite(hGround.GroundLevel);
            Lander      hLander     = new LanderAI("../../../training_sets/Experiment4.net", hSite);
            
            while (hWnd.IsOpened)
            {
                hGround.Update();
                hSite.Update();
                hLander.Update();

                hGround.Draw();
                hSite.Draw();
                hLander.Draw();
                
                hWnd.Update();
            }
        }

        [ConsoleUIMethod]
        public void Load(string sExperiment)
        {
        }


        [ConsoleUIMethod]
        public void Start(string sExperimentName)
        {
        }

        private class Command
        {

        }

    }
}
