namespace Aiv.Research.Visualizer2D
{
    partial class IdealInputForm
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
            this.m_hTable = new System.Windows.Forms.TableLayoutPanel();
            this.m_hButtonOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_hTable
            // 
            this.m_hTable.ColumnCount = 2;
            this.m_hTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_hTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_hTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_hTable.Location = new System.Drawing.Point(0, 0);
            this.m_hTable.Name = "m_hTable";
            this.m_hTable.RowCount = 1;
            this.m_hTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_hTable.Size = new System.Drawing.Size(663, 47);
            this.m_hTable.TabIndex = 0;
            // 
            // m_hButtonOk
            // 
            this.m_hButtonOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_hButtonOk.Location = new System.Drawing.Point(588, 47);
            this.m_hButtonOk.Name = "m_hButtonOk";
            this.m_hButtonOk.Size = new System.Drawing.Size(75, 37);
            this.m_hButtonOk.TabIndex = 1;
            this.m_hButtonOk.Text = "Cancel";
            this.m_hButtonOk.UseVisualStyleBackColor = true;
            this.m_hButtonOk.Click += new System.EventHandler(this.m_hButtonDiscard_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(513, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.m_hButtonOk_Click);
            // 
            // IdealInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 84);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_hButtonOk);
            this.Controls.Add(this.m_hTable);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IdealInputForm";
            this.Text = "IdealInputForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel m_hTable;
        private System.Windows.Forms.Button m_hButtonOk;
        private System.Windows.Forms.Button button1;
    }
}