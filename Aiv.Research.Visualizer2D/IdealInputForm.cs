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
    public partial class IdealInputForm : Form
    {
        private List<TextBox> m_hTextboxes;
        public double[] Ideal { get; set; }
        
        public IdealInputForm(int iIdealCount)
        {
            InitializeComponent();
            m_hTextboxes = new List<TextBox>();

            m_hTable.ColumnCount = iIdealCount;
            m_hTable.ColumnStyles.Clear();
            m_hTable.RowCount = 1;
            m_hTable.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            for (int i = 0; i < m_hTable.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100 / m_hTable.ColumnCount);
                m_hTable.ColumnStyles.Add(cs);

                TextBox hTextBox = new TextBox();
                hTextBox.Text = "0.0";
                hTextBox.Dock = DockStyle.Fill;
                m_hTable.Controls.Add(hTextBox, i, 0);
                m_hTextboxes.Add(hTextBox);
            }            
        }

        private void m_hButtonOk_Click(object sender, EventArgs e)
        {
            Ideal = new double[m_hTextboxes.Count];

            for (int i = 0; i < m_hTextboxes.Count; i++)
            {
                try
                {
                    Ideal[i] = double.Parse(m_hTextboxes[i].Text);
                }
                catch (Exception)
                {
                    Ideal[i] = 0;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_hButtonDiscard_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
