using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aiv.Research.Shared;

namespace Aiv.Research.Visualizer2D
{
    public partial class NetworkSelector : Form
    {
        public NetworkSelector(Settings appsettings)
        {
            InitializeComponent();
            TrainingClient client = new TrainingClient();
                   
            client.Connect(appsettings.TrainingServiceAddress, appsettings.TrainingServicePort);
            
            IEnumerable<NetworkCreationConfig> hConfigs = client.ServiceInstance.EnumerateTrainingsCompleted();
            
            foreach (var item in hConfigs)
            {
                ListViewItem lv1 = new ListViewItem(item.Name);
                lv1.SubItems.Add(item.Id.ToString());
                lv1.SubItems.Add(item.InputSize.ToString());
                lv1.SubItems.Add(item.OutputSize.ToString());
                lv1.SubItems.Add(item.Iterations.ToString());
                lv1.SubItems.Add(item.ActivationTypeGuid.ToString());
                listView1.Items.Add(lv1);
            }
        }       
    }
}
