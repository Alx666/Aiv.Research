﻿using Aiv.Fast2D;
using Aiv.Research.Shared.Data;
using Encog.Neural.Networks;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Tests.Landing
{
    class LanderHuman : Lander
    {
        private bool start;
        private bool getKeyDown;

        private int iCounter;
        private Recorder m_hRecorder;

        public LanderHuman(LandingSite hSite) : base(hSite)
        {
            iCounter = 0;
            m_hRecorder = new Recorder(this);

            start = true;
            getKeyDown = false;
        }
        public override void Update()
        {
            base.Update();

            if (IsGrounded)
                return;

            if (Window.Current.GetKey(KeyCode.Space))
            {
                m_vVelocity.Y += -22f * Window.Current.deltaTime;
                Thrust = 1;
            }
            
            if (Window.Current.GetKey(KeyCode.Right))
            {
                m_vVelocity.X += 10f * Window.Current.deltaTime;
                Adjustment = 1;
            }
            
            if (Window.Current.GetKey(KeyCode.Left))
            {
                m_vVelocity.X += -10 * Window.Current.deltaTime;
                Adjustment = -1;
            }

            if (Window.Current.GetKey(KeyCode.R) && getKeyDown == false)
            {
                if(start == true)
                {
                    m_hRecorder.Start("Lander" + iCounter + ".xml");
                    start = false;
                }
                if (start == false)
                {
                    m_hRecorder?.Stop();
                    start = true;
                }
                iCounter++;
                getKeyDown = true;
            }
            else
            {
                getKeyDown = false;
            }


            Height = this.Position.Y;
            VelocityX = this.m_vVelocity.X;
            VelocityY = this.m_vVelocity.Y;
            VectorX = (m_hSite.Position - Position).X;
            VectorY = (m_hSite.Position - Position).Y;
        }

    }
}
