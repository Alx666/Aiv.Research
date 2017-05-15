using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aiv.Research.Visualizer2D
{
    public partial class Main : Form
    {
        private IDrawer m_hDrawer;

        private PenDrawer m_hPenDrawer;
        private NullDrawer m_hNullDrawer;
        private Graphics m_hGfx;

        public Main()
        {
            InitializeComponent();

            m_hGfx = m_hPanel.CreateGraphics();

            m_hPenDrawer = new PenDrawer(Color.Green, 1f, m_hGfx);
            m_hNullDrawer = new NullDrawer();
            m_hDrawer = m_hNullDrawer;
        }

        #region Panel Event Handlers

        private void OnPanelMouseEnter(object sender, EventArgs e) => m_hDrawer = m_hPenDrawer;
        private void OnPanelMouseLeave(object sender, EventArgs e)
        {
            m_hDrawer.End();
            m_hDrawer = m_hNullDrawer;
        }


        private void OnPanelMouseDown(object sender, MouseEventArgs e)
        {
            m_hDrawer.Begin(e.X, e.Y);
        }

        private void OnPanelMouseMove(object sender, MouseEventArgs e)
        {
            m_hDrawer.Update(e.X, e.Y);
        }

        private void OnPanelMouseUp(object sender, MouseEventArgs e)
        {
            m_hDrawer.End();
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Modifiers == Keys.Control)
            {
                Sample hSample = new Sample();
                hSample.Number = m_hSamples.Items.Count;
                m_hSamples.Items.Add(hSample);
                m_hGfx.Clear(Color.Black);
            }
        }

        #endregion


    }
}
