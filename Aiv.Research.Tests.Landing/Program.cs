using Aiv.Fast2D;
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

        private static  Ground  m_hGround;
        private static  Lander  m_hLander;

        static void Main(string[] args)
        {
            m_hWnd      = new Window(800, 600, "Lander");            
            m_hLander   = new Lander();
            m_hGround   = new Ground();

            while (m_hWnd.IsOpened)
            {
                m_hGround.Update();
                m_hLander.Update();

                m_hGround.Draw();
                m_hLander.Draw();

                m_hWnd.Update();
            }
        }
    }
}
