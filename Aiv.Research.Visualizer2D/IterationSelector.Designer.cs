namespace Aiv.Research.Visualizer2D
{
    partial class IterationSelector
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
            this.m_hTextNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_hButtonCancel = new System.Windows.Forms.Button();
            this.m_hButtonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_hTextNumber
            // 
            this.m_hTextNumber.Location = new System.Drawing.Point(128, 12);
            this.m_hTextNumber.Name = "m_hTextNumber";
            this.m_hTextNumber.Size = new System.Drawing.Size(151, 20);
            this.m_hTextNumber.TabIndex = 0;
            this.m_hTextNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number Of Iterations:";
            // 
            // m_hButtonCancel
            // 
            this.m_hButtonCancel.Location = new System.Drawing.Point(204, 46);
            this.m_hButtonCancel.Name = "m_hButtonCancel";
            this.m_hButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.m_hButtonCancel.TabIndex = 2;
            this.m_hButtonCancel.Text = "Cancel";
            this.m_hButtonCancel.UseVisualStyleBackColor = true;
            this.m_hButtonCancel.Click += new System.EventHandler(this.m_hButtonCancel_Click);
            // 
            // m_hButtonOk
            // 
            this.m_hButtonOk.Location = new System.Drawing.Point(123, 46);
            this.m_hButtonOk.Name = "m_hButtonOk";
            this.m_hButtonOk.Size = new System.Drawing.Size(75, 23);
            this.m_hButtonOk.TabIndex = 3;
            this.m_hButtonOk.Text = "Ok";
            this.m_hButtonOk.UseVisualStyleBackColor = true;
            this.m_hButtonOk.Click += new System.EventHandler(this.m_hButtonOk_Click);
            // 
            // IterationSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 81);
            this.Controls.Add(this.m_hButtonOk);
            this.Controls.Add(this.m_hButtonCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_hTextNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IterationSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_hTextNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_hButtonCancel;
        private System.Windows.Forms.Button m_hButtonOk;
    }
}