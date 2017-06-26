namespace Aiv.Research.Visualizer2D
{
    partial class NetworkSelector
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InputSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OutputSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Iterations = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ActivationTypeGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name,
            this.Id,
            this.InputSize,
            this.OutputSize,
            this.Iterations,
            this.ActivationTypeGuid});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(860, 388);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Name
            // 
            this.Name.Text = "Name";
            this.Name.Width = 113;
            // 
            // Id
            // 
            this.Id.Text = "Id";
            this.Id.Width = 90;
            // 
            // InputSize
            // 
            this.InputSize.Text = "InputSize";
            this.InputSize.Width = 106;
            // 
            // OutputSize
            // 
            this.OutputSize.Text = "OutputSize";
            this.OutputSize.Width = 130;
            // 
            // Iterations
            // 
            this.Iterations.Text = "Iterations";
            this.Iterations.Width = 117;
            // 
            // ActivationTypeGuid
            // 
            this.ActivationTypeGuid.Text = "ActivationTypeGuid";
            this.ActivationTypeGuid.Width = 140;
            // 
            // NetworkSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 388);
            this.Controls.Add(this.listView1);
            // this.Name = new System.Windows.Forms.ColumnHeader("NetworkSelector");
            this.Name.Name = "NetworkSelectorHeader";
            this.Text = "NetworkSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader InputSize;
        private System.Windows.Forms.ColumnHeader OutputSize;
        private System.Windows.Forms.ColumnHeader Iterations;
        private System.Windows.Forms.ColumnHeader ActivationTypeGuid;
    }
}