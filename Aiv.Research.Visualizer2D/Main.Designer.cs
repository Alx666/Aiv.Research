namespace Aiv.Research.Visualizer2D
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_hPanel = new System.Windows.Forms.Panel();
            this.m_hSamples = new System.Windows.Forms.ListBox();
            this.m_hMenuStrip = new System.Windows.Forms.MenuStrip();
            this.crateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feedForwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localBackPropagationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteBackPropagationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCustomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeCenterOfMassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianBlur3x3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianBlur5x5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianBlur7x7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_hContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_hSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_hOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_hStatusBar = new System.Windows.Forms.StatusStrip();
            this.m_hProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.m_hWorker = new System.ComponentModel.BackgroundWorker();
            this.m_hMenuStrip.SuspendLayout();
            this.m_hStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_hPanel
            // 
            this.m_hPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_hPanel.Location = new System.Drawing.Point(45, 64);
            this.m_hPanel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.m_hPanel.Name = "m_hPanel";
            this.m_hPanel.Size = new System.Drawing.Size(1707, 1526);
            this.m_hPanel.TabIndex = 0;
            this.m_hPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPanelPaint);
            this.m_hPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseDown);
            this.m_hPanel.MouseLeave += new System.EventHandler(this.OnPanelMouseLeave);
            this.m_hPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseMove);
            this.m_hPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseUp);
            // 
            // m_hSamples
            // 
            this.m_hSamples.FormattingEnabled = true;
            this.m_hSamples.ItemHeight = 31;
            this.m_hSamples.Location = new System.Drawing.Point(1768, 62);
            this.m_hSamples.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.m_hSamples.Name = "m_hSamples";
            this.m_hSamples.Size = new System.Drawing.Size(620, 1523);
            this.m_hSamples.TabIndex = 1;
            this.m_hSamples.SelectedIndexChanged += new System.EventHandler(this.OnSamplesSelectedIndexChanged);
            // 
            // m_hMenuStrip
            // 
            this.m_hMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.m_hMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crateToolStripMenuItem,
            this.trainingToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.dataSetToolStripMenuItem});
            this.m_hMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_hMenuStrip.Name = "m_hMenuStrip";
            this.m_hMenuStrip.Padding = new System.Windows.Forms.Padding(16, 5, 0, 5);
            this.m_hMenuStrip.Size = new System.Drawing.Size(2427, 58);
            this.m_hMenuStrip.TabIndex = 2;
            this.m_hMenuStrip.Text = "menuStrip1";
            // 
            // crateToolStripMenuItem
            // 
            this.crateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.feedForwardToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.downloadToolStripMenuItem});
            this.crateToolStripMenuItem.Name = "crateToolStripMenuItem";
            this.crateToolStripMenuItem.Size = new System.Drawing.Size(143, 48);
            this.crateToolStripMenuItem.Text = "Network";
            // 
            // feedForwardToolStripMenuItem
            // 
            this.feedForwardToolStripMenuItem.Name = "feedForwardToolStripMenuItem";
            this.feedForwardToolStripMenuItem.Size = new System.Drawing.Size(269, 46);
            this.feedForwardToolStripMenuItem.Text = "Create";
            this.feedForwardToolStripMenuItem.Click += new System.EventHandler(this.MenuItemCreate);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(269, 46);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.MenuItemSave);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(269, 46);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.MenuItemLoad);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(269, 46);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.MenuItemClose);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(269, 46);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.OnNetworkDownload);
            // 
            // trainingToolStripMenuItem
            // 
            this.trainingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localBackPropagationToolStripMenuItem,
            this.remoteBackPropagationToolStripMenuItem});
            this.trainingToolStripMenuItem.Name = "trainingToolStripMenuItem";
            this.trainingToolStripMenuItem.Size = new System.Drawing.Size(134, 48);
            this.trainingToolStripMenuItem.Text = "Training";
            // 
            // localBackPropagationToolStripMenuItem
            // 
            this.localBackPropagationToolStripMenuItem.Name = "localBackPropagationToolStripMenuItem";
            this.localBackPropagationToolStripMenuItem.Size = new System.Drawing.Size(468, 46);
            this.localBackPropagationToolStripMenuItem.Text = "Local BackPropagation";
            this.localBackPropagationToolStripMenuItem.Click += new System.EventHandler(this.MenuItemBackpropagationTrain);
            // 
            // remoteBackPropagationToolStripMenuItem
            // 
            this.remoteBackPropagationToolStripMenuItem.Name = "remoteBackPropagationToolStripMenuItem";
            this.remoteBackPropagationToolStripMenuItem.Size = new System.Drawing.Size(468, 46);
            this.remoteBackPropagationToolStripMenuItem.Text = "Remote BackPropagation";
            this.remoteBackPropagationToolStripMenuItem.Click += new System.EventHandler(this.OnRemoteBackPropagation);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(99, 48);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(239, 46);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OnOptionsClick);
            // 
            // dataSetToolStripMenuItem
            // 
            this.dataSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem1,
            this.loadCustomToolStripMenuItem,
            this.normalizeCenterOfMassToolStripMenuItem,
            this.normalizeValuesToolStripMenuItem,
            this.gaussianBlur3x3ToolStripMenuItem,
            this.gaussianBlur5x5ToolStripMenuItem,
            this.gaussianBlur7x7ToolStripMenuItem});
            this.dataSetToolStripMenuItem.Name = "dataSetToolStripMenuItem";
            this.dataSetToolStripMenuItem.Size = new System.Drawing.Size(141, 48);
            this.dataSetToolStripMenuItem.Text = "Data Set";
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(498, 46);
            this.loadToolStripMenuItem1.Text = "Load MNIST";
            this.loadToolStripMenuItem1.Click += new System.EventHandler(this.OnDataLoad);
            // 
            // loadCustomToolStripMenuItem
            // 
            this.loadCustomToolStripMenuItem.Name = "loadCustomToolStripMenuItem";
            this.loadCustomToolStripMenuItem.Size = new System.Drawing.Size(498, 46);
            this.loadCustomToolStripMenuItem.Text = "Load Custom";
            this.loadCustomToolStripMenuItem.Click += new System.EventHandler(this.OnLoadCustomData);
            // 
            // normalizeCenterOfMassToolStripMenuItem
            // 
            this.normalizeCenterOfMassToolStripMenuItem.Name = "normalizeCenterOfMassToolStripMenuItem";
            this.normalizeCenterOfMassToolStripMenuItem.Size = new System.Drawing.Size(498, 46);
            this.normalizeCenterOfMassToolStripMenuItem.Text = "Normalize (Center Of Mass)";
            this.normalizeCenterOfMassToolStripMenuItem.Click += new System.EventHandler(this.OnNormalizeCenterOfMass);
            // 
            // normalizeValuesToolStripMenuItem
            // 
            this.normalizeValuesToolStripMenuItem.Name = "normalizeValuesToolStripMenuItem";
            this.normalizeValuesToolStripMenuItem.Size = new System.Drawing.Size(498, 46);
            this.normalizeValuesToolStripMenuItem.Text = "Normalize Values";
            this.normalizeValuesToolStripMenuItem.Click += new System.EventHandler(this.OnNormalizeValues);
            // 
            // gaussianBlur3x3ToolStripMenuItem
            // 
            this.gaussianBlur3x3ToolStripMenuItem.Name = "gaussianBlur3x3ToolStripMenuItem";
            this.gaussianBlur3x3ToolStripMenuItem.Size = new System.Drawing.Size(498, 46);
            this.gaussianBlur3x3ToolStripMenuItem.Text = "Gaussian Blur 3x3";
            this.gaussianBlur3x3ToolStripMenuItem.Click += new System.EventHandler(this.OnGaussianBlur3x3);
            // 
            // gaussianBlur5x5ToolStripMenuItem
            // 
            this.gaussianBlur5x5ToolStripMenuItem.Name = "gaussianBlur5x5ToolStripMenuItem";
            this.gaussianBlur5x5ToolStripMenuItem.Size = new System.Drawing.Size(498, 46);
            this.gaussianBlur5x5ToolStripMenuItem.Text = "Gaussian Blur 5x5";
            this.gaussianBlur5x5ToolStripMenuItem.Click += new System.EventHandler(this.OnGaussianBlur5x5);
            // 
            // gaussianBlur7x7ToolStripMenuItem
            // 
            this.gaussianBlur7x7ToolStripMenuItem.Name = "gaussianBlur7x7ToolStripMenuItem";
            this.gaussianBlur7x7ToolStripMenuItem.Size = new System.Drawing.Size(498, 46);
            this.gaussianBlur7x7ToolStripMenuItem.Text = "Gaussian Blur 7x7";
            this.gaussianBlur7x7ToolStripMenuItem.Click += new System.EventHandler(this.OnGaussianBlur7x7);
            // 
            // m_hContextMenuStrip1
            // 
            this.m_hContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.m_hContextMenuStrip1.Name = "m_hContextMenuStrip1";
            this.m_hContextMenuStrip1.Size = new System.Drawing.Size(98, 4);
            // 
            // m_hSaveFileDialog
            // 
            this.m_hSaveFileDialog.Filter = "*.xml|Allfiles";
            // 
            // m_hStatusBar
            // 
            this.m_hStatusBar.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.m_hStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_hProgressBar});
            this.m_hStatusBar.Location = new System.Drawing.Point(0, 1620);
            this.m_hStatusBar.Name = "m_hStatusBar";
            this.m_hStatusBar.Padding = new System.Windows.Forms.Padding(3, 0, 37, 0);
            this.m_hStatusBar.Size = new System.Drawing.Size(2427, 44);
            this.m_hStatusBar.TabIndex = 3;
            this.m_hStatusBar.Text = "statusStrip1";
            // 
            // m_hProgressBar
            // 
            this.m_hProgressBar.Name = "m_hProgressBar";
            this.m_hProgressBar.Size = new System.Drawing.Size(2389, 38);
            // 
            // m_hWorker
            // 
            this.m_hWorker.WorkerReportsProgress = true;
            this.m_hWorker.WorkerSupportsCancellation = true;
            this.m_hWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDoWork);
            this.m_hWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnProgressChanged);
            this.m_hWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnRunWorkerCompleted);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2427, 1664);
            this.Controls.Add(this.m_hStatusBar);
            this.Controls.Add(this.m_hSamples);
            this.Controls.Add(this.m_hPanel);
            this.Controls.Add(this.m_hMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.m_hMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Main";
            this.Text = "Sample Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnFormKeyDown);
            this.m_hMenuStrip.ResumeLayout(false);
            this.m_hMenuStrip.PerformLayout();
            this.m_hStatusBar.ResumeLayout(false);
            this.m_hStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel m_hPanel;
        private System.Windows.Forms.ListBox m_hSamples;
        private System.Windows.Forms.MenuStrip m_hMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem crateToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip m_hContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem feedForwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog m_hSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog m_hOpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localBackPropagationToolStripMenuItem;
        private System.Windows.Forms.StatusStrip m_hStatusBar;
        private System.Windows.Forms.ToolStripProgressBar m_hProgressBar;
        private System.ComponentModel.BackgroundWorker m_hWorker;
        private System.Windows.Forms.ToolStripMenuItem remoteBackPropagationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem normalizeCenterOfMassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalizeValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianBlur3x3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianBlur5x5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianBlur7x7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCustomToolStripMenuItem;
    }
}

