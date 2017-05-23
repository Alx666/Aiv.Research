namespace Aiv.Research.Visualizer2D
{
    partial class FormNNDrawer
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
            this.SuspendLayout();
            // 
            // m_hPanel
            // 
            this.m_hPanel.BackColor = System.Drawing.Color.Black;
            this.m_hPanel.Location = new System.Drawing.Point(12, 12);
            this.m_hPanel.Name = "m_hPanel";
            this.m_hPanel.Size = new System.Drawing.Size(429, 267);
            this.m_hPanel.TabIndex = 0;
            // 
            // FormNNDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 299);
            this.Controls.Add(this.m_hPanel);
            this.Name = "FormNNDrawer";
            this.Text = "Neural Network";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_hPanel;
    }
}