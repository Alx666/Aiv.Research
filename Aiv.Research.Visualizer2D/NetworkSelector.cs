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
            NetworkCreationConfig config = new NetworkCreationConfig();

            // stabiliamo connessione + download dati Training + enumeriamo
            client.Connect(appsettings.TrainingServiceAddress, appsettings.TrainingServicePort);
            
            IEnumerable<NetworkCreationConfig> hConfigs = client.ServiceInstance.EnumerateTrainingsCompleted();

            



            //creiamo colonne e righe
            


           

            


        }
        

       
        
    }

    class SomeClass
    {
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
    }

}
