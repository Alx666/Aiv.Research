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
            this.m_hToolStripTextQuantize = new System.Windows.Forms.ToolStripTextBox();
            this.m_hContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_hMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_hPanel
            // 
            this.m_hPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_hPanel.Location = new System.Drawing.Point(17, 27);
            this.m_hPanel.Name = "m_hPanel";
            this.m_hPanel.Size = new System.Drawing.Size(800, 600);
            this.m_hPanel.TabIndex = 0;
            this.m_hPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPanelPaint);
            this.m_hPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseDown);
            this.m_hPanel.MouseEnter += new System.EventHandler(this.OnPanelMouseEnter);
            this.m_hPanel.MouseLeave += new System.EventHandler(this.OnPanelMouseLeave);
            this.m_hPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseMove);
            this.m_hPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseUp);
            // 
            // m_hSamples
            // 
            this.m_hSamples.FormattingEnabled = true;
            this.m_hSamples.Location = new System.Drawing.Point(823, 25);
            this.m_hSamples.Name = "m_hSamples";
            this.m_hSamples.Size = new System.Drawing.Size(235, 602);
            this.m_hSamples.TabIndex = 1;
            // 
            // m_hMenuStrip
            // 
            this.m_hMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crateToolStripMenuItem});
            this.m_hMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_hMenuStrip.Name = "m_hMenuStrip";
            this.m_hMenuStrip.Size = new System.Drawing.Size(1070, 24);
            this.m_hMenuStrip.TabIndex = 2;
            this.m_hMenuStrip.Text = "menuStrip1";
            // 
            // crateToolStripMenuItem
            // 
            this.crateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.feedForwardToolStripMenuItem});
            this.crateToolStripMenuItem.Name = "crateToolStripMenuItem";
            this.crateToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.crateToolStripMenuItem.Text = "Options";
            // 
            // feedForwardToolStripMenuItem
            // 
            this.feedForwardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_hToolStripTextQuantize});
            this.feedForwardToolStripMenuItem.Name = "feedForwardToolStripMenuItem";
            this.feedForwardToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.feedForwardToolStripMenuItem.Text = "Quantize";
            // 
            // m_hToolStripTextQuantize
            // 
            this.m_hToolStripTextQuantize.Name = "m_hToolStripTextQuantize";
            this.m_hToolStripTextQuantize.Size = new System.Drawing.Size(100, 23);
            this.m_hToolStripTextQuantize.Text = "10";
            this.m_hToolStripTextQuantize.TextChanged += new System.EventHandler(this.OnQuantizeTextChanged);
            // 
            // m_hContextMenuStrip1
            // 
            this.m_hContextMenuStrip1.Name = "m_hContextMenuStrip1";
            this.m_hContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 634);
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
        private System.Windows.Forms.ToolStripTextBox m_hToolStripTextQuantize;
    }
}

