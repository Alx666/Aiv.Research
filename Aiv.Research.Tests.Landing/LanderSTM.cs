using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Aiv.Research.Tests.Landing
{
    class LanderSTM : Lander
    {
        private float Speed = 0.003f;
        //private float Speed = 10f;
        public LanderSTM(LandingSite hSite) : base(hSite)
        {
           
        }

        public override void Update()
        {
            base.Update();

            if (IsGrounded)
                return;

            if (this.Position.Y > TargetAltitude)
            {
                VertThrust = 1;
                m_vVelocity.Y += -22f * Window.Current.deltaTime;
            }
            else if (this.Position.Y < TargetAltitude && this.Position.Y > TargetAltitude - 75)
            {
                VertThrust = 0;
            }
            if (this.Position.Y < TargetAltitude - 75)
            {
                VertThrust = 0;
            }

            float effectiveSpeed = (this.Position - m_hSite.Position).Length * Speed;
            
            Console.WriteLine(TargetDestinationX + " - " + TargetDestinationY + " - " + new Vector2((float)TargetDestinationX, (float)TargetDestinationY).Length);
            if (this.Position.X > m_hSite.Position.X)
            {
                HorizThrust = 1;
                
                m_vVelocity.X -= effectiveSpeed * Window.Current.deltaTime;
            }
            else if (this.Position.X < m_hSite.Position.X && this.Position.X > m_hSite.Position.X )
            {
                HorizThrust = 0;
            }
            if (this.Position.X < m_hSite.Position.X)
            {
                HorizThrust = -1;
                m_vVelocity.X += effectiveSpeed * Window.Current.deltaTime;
            }


            InHeight = this.Position.Y;
            InWidth = this.Position.X;
            //VelocityX = this.m_vVelocity.X;
            InVelocityY = this.m_vVelocity.Y;
            InVelocityX = this.m_vVelocity.X;
            //VectorX = (m_hSite.Position - Position).X;
            //VectorY = (m_hSite.Position - Position).Y;
        }
    }
}
