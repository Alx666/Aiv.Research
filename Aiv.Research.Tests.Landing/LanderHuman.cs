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

            if(Input.IsJoystickButtonDown(JoystickButton.ShoulderLeft, JoystickIndex.One))
            {
                m_hSite.Randomize();
            }


            Vector2 vAxis;//  = Input.JoystickAxisLeft(JoystickIndex.One);
            vAxis.Y = -Input.JoystickTriggerRight(JoystickIndex.One);
            vAxis.X =  Input.JoystickAxisRight(JoystickIndex.One).X;


            m_vVelocity.Y += REACTOR_POWER * vAxis.Y * Window.Current.deltaTime;
            m_vVelocity.X += REACTOR_POWER * 0.8f *  vAxis.X * Window.Current.deltaTime;

            VertThrust  = m_vVelocity.Y;
            HorizThrust = m_vVelocity.X;
        }

    }
}
