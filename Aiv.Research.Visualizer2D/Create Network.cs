using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Encog.Engine.Network.Activation;
using System.Reflection;
using Aiv.Research.Shared;

namespace Aiv.Research.Visualizer2D
{
    public partial class CreateNetworkForm : Form
    {
        public NetworkCreationConfig Config { get; private set; }

        public CreateNetworkForm()
        {
            InitializeComponent();

            var hActivationFuncs = from t in Assembly.Load("encog-core-cs").GetTypes()
                                   from i in t.GetInterfaces()
                                   where i.Name == "IActivationFunction"
                                   select new ActivationFuncSelect(t.Name, Activator.CreateInstance(t) as IActivationFunction);

            hActivationFuncs.ToList().ForEach(a => m_hComboActivation.Items.Add(a));
            m_hComboActivation.SelectedIndex = 10;
        }


        private struct ActivationFuncSelect
        {
            public ActivationFuncSelect(string sName, IActivationFunction hFunc)
            {
                Name = sName;
                Func = hFunc;
            }

            public string Name;
            public IActivationFunction Func;

            public override string ToString() => Name;
        }

        private void m_hButtonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Config = new NetworkCreationConfig();
                Config.InputSize = int.Parse(m_hTextInputSize.Text);
                double dSqrt = Math.Sqrt(Config.InputSize);

                //if (Math.Ceiling(dSqrt) != dSqrt)
                //    throw new ApplicationException("Input Size must have an integer Sqrt");

                Config.OutputSize = int.Parse(m_hTextOutputSize.Text);

                int iHl0Size = 0;
                int iHl1Size = 0;
                int iHl2Size = 0;


                if (int.TryParse(m_hTextHL0Size.Text, out iHl0Size))
                    Config.HL0Size = iHl0Size;

                if (int.TryParse(m_hTextHL1Size.Text, out iHl1Size))
                    Config.HL1Size = iHl0Size;

                if (int.TryParse(m_hTextHL2Size.Text, out iHl2Size))
                    Config.HL2Size = iHl0Size;


                //Config.Activation = ((ActivationFuncSelect)m_hComboActivation.SelectedItem).Func;


                if (m_hCheckVisualize.Checked)
                {
                    Config.Visualize    = m_hCheckVisualize.Checked;
                    Config.Width        = int.Parse(m_hTextWidth.Text);
                    Config.Height       = int.Parse(m_hTextHeight.Text);
                    Config.NeuronSize   = int.Parse(m_hTextNSize.Text);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            catch (Exception)
            {

            }
        }

        private void m_hButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
