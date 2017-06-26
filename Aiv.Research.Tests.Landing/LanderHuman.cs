using Aiv.Fast2D;
using Aiv.Fast2D.Utils.Input;
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
        public LanderHuman(LandingSite hSite) : base(hSite)
        {
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
              //77  Adjustment = 1;
            }
            
            if (Window.Current.GetKey(KeyCode.Left))
            {
                m_vVelocity.X += -10 * Window.Current.deltaTime;
               // Adjustment = -1;
            }

            Height      = this.Position.Y;
            //pVelocityX   = this.m_vVelocity.X;
            VelocityY   = this.m_vVelocity.Y;
           //VectorX     = (m_hSite.Position - Position).X;
         //   VectorY     = (m_hSite.Position - Position).Y;
        }

    }
}
