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
            

            listView1.Items.Add(new ListViewItem())

        }
        

       
        
    }
}
