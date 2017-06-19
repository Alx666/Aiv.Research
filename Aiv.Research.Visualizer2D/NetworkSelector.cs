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

            client.Connect(appsettings.TrainingServiceAddress, appsettings.TrainingServicePort);
            var a = client.ServiceInstance.DownloadTrainingData(config.Id).ToString();
            client.ServiceInstance.EnumerateTrainingsCompleted();


            listView1.GridLines = true;
            listView1.Sorting = SortOrder.Ascending;
           // listView1.Items[0].SubItems.Add() 
            ListViewItem item0 = new ListViewItem("qualcosa", 0);
            item0.SubItems.Add("1");

            


        }
        

       
        
    }

    class SomeClass
    {
        public int a { get; set; }
        public int b { get; set; }
        public int c { get; set; }
    }

}
