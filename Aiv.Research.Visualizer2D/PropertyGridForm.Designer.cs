namespace Aiv.Research.Visualizer2D
{
    partial class PropertyGridForm
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
            this.m_hPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_hCancel = new System.Windows.Forms.Button();
            this.m_hButtonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_hPropertyGrid
            // 
            this.m_hPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_hPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.m_hPropertyGrid.Name = "m_hPropertyGrid";
            this.m_hPropertyGrid.Size = new System.Drawing.Size(632, 402);
            this.m_hPropertyGrid.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_hPropertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_hButtonOk);
            this.splitContainer1.Panel2.Controls.Add(this.m_hCancel);
            this.splitContainer1.Size = new System.Drawing.Size(632, 452);
            this.splitContainer1.SplitterDistance = 402;
            this.splitContainer1.TabIndex = 1;
            // 
            // m_hCancel
            // 
            this.m_hCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_hCancel.Location = new System.Drawing.Point(557, 0);
            this.m_hCancel.Name = "m_hCancel";
            this.m_hCancel.Size = new System.Drawing.Size(75, 46);
            this.m_hCancel.TabIndex = 0;
            this.m_hCancel.Text = "Cancel";
            this.m_hCancel.UseVisualStyleBackColor = true;
            this.m_hCancel.Click += new System.EventHandler(this.m_hCancel_Click);
            // 
            // m_hButtonOk
            // 
            this.m_hButtonOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_hButtonOk.Location = new System.Drawing.Point(482, 0);
            this.m_hButtonOk.Name = "m_hButtonOk";
            this.m_hButtonOk.Size = new System.Drawing.Size(75, 46);
            this.m_hButtonOk.TabIndex = 1;
            this.m_hButtonOk.Text = "Ok";
            this.m_hButtonOk.UseVisualStyleBackColor = true;
            this.m_hButtonOk.Click += new System.EventHandler(this.m_hButtonOk_Click);
            // 
            // PropertyGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 452);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertyGridForm";
            this.Text = "PropertyGridForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid m_hPropertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button m_hButtonOk;
        private System.Windows.Forms.Button m_hCancel;
    }
}