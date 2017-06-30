using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aiv.Research.Shared;

namespace Aiv.Research.Visualizer2D
{
    public partial class NetworkSelector : Form
    {
        private Settings m_hSettings;
        private ChannelFactory<ITrainingService> m_hFactory;
        public NetworkSelector(Settings appsettings)
        {
            InitializeComponent();
            m_hSettings = appsettings;
            NetTcpBinding hBinding = new NetTcpBinding(SecurityMode.None, true);
            hBinding.ReceiveTimeout = TimeSpan.MaxValue;
            hBinding.SendTimeout = TimeSpan.MaxValue;
            m_hFactory = new ChannelFactory<ITrainingService>(hBinding);

            TrainingClient client = new TrainingClient();
                   
            client.Connect(appsettings.TrainingServiceAddress, appsettings.TrainingServicePort);
            
            IEnumerable<NetworkCreationConfig> hConfigs = client.ServiceInstance.EnumerateTrainingsCompleted();
            listView1.FullRowSelect = true;
            listView1.Columns.Add("");
            ListViewExtender extender = new ListViewExtender(listView1);
            ListViewButtonColumn buttonAction = new ListViewButtonColumn(listView1.Columns.Count - 1);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;
            extender.AddColumn(buttonAction);

            foreach (var item in hConfigs)
            {
                ListViewItem lv1 = new ListViewItem(item.Name);
                lv1.SubItems.Add(item.Id.ToString());
                lv1.SubItems.Add(item.InputSize.ToString());
                lv1.SubItems.Add(item.OutputSize.ToString());
                lv1.SubItems.Add(item.Iterations.ToString());
                lv1.SubItems.Add(item.ActivationTypeGuid.ToString());
                lv1.SubItems.Add("Download_" + item.Id);
                listView1.Items.Add(lv1);
            }
        }
        private void OnButtonActionClick(object sender, ListViewColumnMouseEventArgs e)
        {
            int id = int.Parse(e.SubItem.Text.Split(new char[] {'_'})[1]);
            ITrainingService hService = m_hFactory.CreateChannel(new EndpointAddress($"net.tcp://{m_hSettings.TrainingServiceAddress}:{m_hSettings.TrainingServicePort}/Training/"));
            byte[] hData = hService.DownloadTrainingData(id);

            SaveFileDialog hDialog = new SaveFileDialog();
            using (MemoryStream hStream = new MemoryStream(hData))
            {
                object hObj = Encog.Persist.EncogDirectoryPersistence.LoadObject(hStream);
                
                if (hDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Stream hFs = File.OpenWrite(hDialog.FileName))
                    {
                        
                        Encog.Persist.EncogDirectoryPersistence.SaveObject(hFs, hObj);
                    }
                }
                
            }

            MessageBox.Show(this, @"Download " + e.SubItem.Text);
            //save file dialog
            //encong.persist.encogdirectorupersistence.loadobject
        }
    }
}
