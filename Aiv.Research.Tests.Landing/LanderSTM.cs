using System;
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
                Thrust = 1;
                m_vVelocity.Y += -18f * Window.Current.deltaTime;
            }
            else if (this.Position.Y < 200 && this.Position.Y > 125)
            {
                if (VelocityY == 0)
                {
                    Thrust = 1;
                }
                else if (VelocityY >= 1)
                {
                    m_vVelocity.Y += -11f * Window.Current.deltaTime;
                    Thrust = 1;
                }
            }
            if (this.Position.Y < 125)
            {
                Thrust = 0;
            }

            if (this.Position.X > m_hSite.Position.X + 50)
            {
                m_vVelocity.X = -11f * Window.Current.deltaTime;
                Adjustment = -1;
            }
            else if (this.Position.X < m_hSite.Position.X - 50)
            {
                m_vVelocity.X = 11f * Window.Current.deltaTime;
                Adjustment = 1;
            }


            Height = this.Position.Y;
            //VelocityX = this.m_vVelocity.X;
            VelocityY = this.m_vVelocity.Y;
            //VectorX = (m_hSite.Position - Position).X;
            //VectorY = (m_hSite.Position - Position).Y;
        }
    }
}
