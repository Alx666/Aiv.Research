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
        private Thread                  m_hThread;
        private ConcurrentBag<ICommand> m_hCommands;
        private Window                  m_hWnd;
        private Ground                  m_hGround;
        private LandingSite             m_hSite;
        private Lander                  m_hLander;
        private Recorder                m_hRecorder;

        public Controller()
        {
            m_hCommands = new ConcurrentBag<ICommand>();
            m_hThread   = new Thread(MainLoop);            
            m_hThread.Start();
        }

        private void MainLoop()
        {
            m_hWnd        = new Window(800, 600, "Lander");
            m_hGround     = new Ground();
            m_hSite       = new LandingSite(m_hGround.GroundLevel);
            //m_hLander   = new LanderHuman(m_hSite);
            //m_hRecorder = new Recorder(m_hLander);
            m_hLander     = new LanderAIX("../../../training_sets/Experiment6.net", m_hSite);

            //m_hLander     = new LanderAI4("../../../training_sets/Experiment4.net", m_hSite);
            //m_hLander = new LanderAI4("../../../training_sets/Experiment4.net", m_hSite);

            while (m_hWnd.IsOpened)
            {
                Input.Update(m_hWnd);

                //Command Dispatching
                ICommand hCmd;
                while (m_hCommands.TryTake(out hCmd))
                {
                    hCmd.Execute();
                }
                
                m_hGround.Update();
                m_hSite.Update();
                m_hLander.Update();

                m_hGround.Draw();
                m_hSite.Draw();
                m_hLander.Draw();

                //if (m_hRecorder.Started)
                //    m_hRecorder.Update();

                m_hWnd.Update();
            }
        }

        [ConsoleUIMethod]
        public void StartRecording()
        {
            m_hCommands.Add(new CommandRecordStart(m_hRecorder));
        }

        [ConsoleUIMethod]
        public void StopRecording()
        {
            m_hCommands.Add(new CommandRecordStop(m_hRecorder));
        }

        [ConsoleUIMethod]
        public void SetGravity(float fGrav)
        {
            m_hCommands.Add(new CommandSetGravity(m_hLander, fGrav));
        }

        [ConsoleUIMethod]
        public void SetAltitude(float fAltitude)
        {
            m_hCommands.Add(new CommandSetAltitude(m_hLander, fAltitude));
        }

        private interface ICommand
        {
            void Execute();
        }

        private class CommandSetGravity : ICommand
        {
            private Lander m_hLander;
            private float  m_fGravity;
            public CommandSetGravity(Lander hLander, float fGravity)
            {
                m_fGravity  = fGravity;
                m_hLander   = hLander;
            }

            public void Execute()
            {
                m_hLander.Gravity = m_fGravity;
            }
        }

        private class CommandSetAltitude : ICommand
        {
            private Lander m_hLander;
            private float m_fAltitude;
            public CommandSetAltitude(Lander hLander, float fAltitude)
            {
                m_fAltitude = fAltitude;
                m_hLander = hLander;
            }

            public void Execute()
            {
                m_hLander.TargetAltitude = m_fAltitude;
            }
        }

        private class CommandRecordStart : ICommand
        {
            private Recorder m_hRecorder;

            public CommandRecordStart(Recorder hRecorder)
            {
                m_hRecorder = hRecorder;
            }

            public void Execute()
            {
                m_hRecorder.Start("sample.xml");
            }
        }

        private class CommandRecordStop : ICommand
        {
            private Recorder m_hRecorder;

            public CommandRecordStop(Recorder hRecorder)
            {
                m_hRecorder = hRecorder;
            }

            public void Execute()
            {
                m_hRecorder.Stop();
            }
        }

    }
}
