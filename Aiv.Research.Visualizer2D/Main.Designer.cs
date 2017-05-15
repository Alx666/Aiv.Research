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
            this.m_hPanel = new System.Windows.Forms.Panel();
            this.m_hSamples = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // m_hPanel
            // 
            this.m_hPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_hPanel.Location = new System.Drawing.Point(12, 12);
            this.m_hPanel.Name = "m_hPanel";
            this.m_hPanel.Size = new System.Drawing.Size(800, 600);
            this.m_hPanel.TabIndex = 0;
            this.m_hPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseDown);
            this.m_hPanel.MouseEnter += new System.EventHandler(this.OnPanelMouseEnter);
            this.m_hPanel.MouseLeave += new System.EventHandler(this.OnPanelMouseLeave);
            this.m_hPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseMove);
            this.m_hPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPanelMouseUp);
            // 
            // m_hSamples
            // 
            this.m_hSamples.FormattingEnabled = true;
            this.m_hSamples.Location = new System.Drawing.Point(823, 12);
            this.m_hSamples.Name = "m_hSamples";
            this.m_hSamples.Size = new System.Drawing.Size(235, 602);
            this.m_hSamples.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 626);
            this.Controls.Add(this.m_hSamples);
            this.Controls.Add(this.m_hPanel);
            this.KeyPreview = true;
            this.Name = "Main";
            this.Text = "Main";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnFormKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_hPanel;
        private System.Windows.Forms.ListBox m_hSamples;
    }
}

