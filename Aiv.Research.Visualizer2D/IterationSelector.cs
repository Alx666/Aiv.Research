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
    public partial class IterationSelector : Form
    {
        public double Value { get; private set; }

        public IterationSelector()
        {
            InitializeComponent();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Enter && e.KeyChar != (Char)Keys.Back && !Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void m_hButtonOk_Click(object sender, EventArgs e)
        {
            Value = double.Parse(m_hTextNumber.Text);

            this.DialogResult = DialogResult.OK;
        }

        private void m_hButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
