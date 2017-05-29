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
            this.trainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localBackPropagationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_hContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_hSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_hOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_hMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_hPanel
            // 
            this.m_hPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_hPanel.Location = new System.Drawing.Point(17, 27);
            this.m_hPanel.Name = "m_hPanel";
            this.m_hPanel.Size = new System.Drawing.Size(800, 800);
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
            this.m_hSamples.Location = new System.Drawing.Point(823, 25);
            this.m_hSamples.Name = "m_hSamples";
            this.m_hSamples.Size = new System.Drawing.Size(235, 797);
            this.m_hSamples.TabIndex = 1;
            // 
            // m_hMenuStrip
            // 
            this.m_hMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crateToolStripMenuItem,
            this.trainingToolStripMenuItem});
            this.m_hMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_hMenuStrip.Name = "m_hMenuStrip";
            this.m_hMenuStrip.Size = new System.Drawing.Size(1070, 24);
            this.m_hMenuStrip.TabIndex = 2;
            this.m_hMenuStrip.Text = "menuStrip1";
            // 
            // crateToolStripMenuItem
            // 
            this.crateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.feedForwardToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.crateToolStripMenuItem.Name = "crateToolStripMenuItem";
            this.crateToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.crateToolStripMenuItem.Text = "Network";
            // 
            // feedForwardToolStripMenuItem
            // 
            this.feedForwardToolStripMenuItem.Name = "feedForwardToolStripMenuItem";
            this.feedForwardToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.feedForwardToolStripMenuItem.Text = "Create";
            this.feedForwardToolStripMenuItem.Click += new System.EventHandler(this.MenuItemCreate);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.MenuItemSave);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.MenuItemLoad);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.MenuItemClose);
            // 
            // trainingToolStripMenuItem
            // 
            this.trainingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localBackPropagationToolStripMenuItem});
            this.trainingToolStripMenuItem.Name = "trainingToolStripMenuItem";
            this.trainingToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.trainingToolStripMenuItem.Text = "Training";
            // 
            // localBackPropagationToolStripMenuItem
            // 
            this.localBackPropagationToolStripMenuItem.Name = "localBackPropagationToolStripMenuItem";
            this.localBackPropagationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.localBackPropagationToolStripMenuItem.Text = "Local BackPropagation";
            this.localBackPropagationToolStripMenuItem.Click += new System.EventHandler(this.MenuItemBackpropagationTrain);
            // 
            // m_hContextMenuStrip1
            // 
            this.m_hContextMenuStrip1.Name = "m_hContextMenuStrip1";
            this.m_hContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // m_hSaveFileDialog
            // 
            this.m_hSaveFileDialog.Filter = "*.xml|Allfiles";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 830);
            this.Controls.Add(this.m_hSamples);
            this.Controls.Add(this.m_hPanel);
            this.Controls.Add(this.m_hMenuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.m_hMenuStrip;
            this.Name = "Main";
            this.Text = "Sample Manager";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnFormKeyDown);
            this.m_hMenuStrip.ResumeLayout(false);
            this.m_hMenuStrip.PerformLayout();
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
    }
}

