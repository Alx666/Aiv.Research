﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Aiv.Research.Tests.Landing
{
    class LanderSTM : Lander
    {
        public LanderSTM(LandingSite hSite) : base(hSite)
        {
           
        }

        public override void Update()
        {
            base.Update();

            if (IsGrounded)
                return;

            if (this.Position.Y > 200)
            {
                VertThrust = 1;
                m_vVelocity.Y += -22f * Window.Current.deltaTime;
            }
            else if (this.Position.Y < 200 && this.Position.Y > 125)
            {
                VertThrust = 0;
            }
            if (this.Position.Y < 125)
            {
                VertThrust = 0;
            }


            InHeight = this.Position.Y;
            //VelocityX = this.m_vVelocity.X;
            InVelocityY = this.m_vVelocity.Y;
            //VectorX = (m_hSite.Position - Position).X;
            //VectorY = (m_hSite.Position - Position).Y;
        }
    }
}
