namespace Aiv.Research.Visualizer2D
{
    partial class CreateNetworkForm
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
            this.m_hTextInputSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.m_hComboActivation = new System.Windows.Forms.ComboBox();
            this.m_hTextHL0Size = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_hTextHL1Size = new System.Windows.Forms.TextBox();
            this.m_hTextHL2Size = new System.Windows.Forms.TextBox();
            this.m_hTextOutputSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_hButtonCreate = new System.Windows.Forms.Button();
            this.m_hButtonCancel = new System.Windows.Forms.Button();
            this.m_hCheckVisualize = new System.Windows.Forms.CheckBox();
            this.m_hTextNSize = new System.Windows.Forms.TextBox();
            this.m_hTextHeight = new System.Windows.Forms.TextBox();
            this.m_hTextWidth = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_hTextInputSize
            // 
            this.m_hTextInputSize.Location = new System.Drawing.Point(92, 11);
            this.m_hTextInputSize.Name = "m_hTextInputSize";
            this.m_hTextInputSize.Size = new System.Drawing.Size(100, 20);
            this.m_hTextInputSize.TabIndex = 0;
            this.m_hTextInputSize.Text = "25";
            this.m_hTextInputSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input Size";
            // 
            // m_hComboActivation
            // 
            this.m_hComboActivation.FormattingEnabled = true;
            this.m_hComboActivation.Location = new System.Drawing.Point(269, 11);
            this.m_hComboActivation.Name = "m_hComboActivation";
            this.m_hComboActivation.Size = new System.Drawing.Size(187, 21);
            this.m_hComboActivation.TabIndex = 2;
            // 
            // m_hTextHL0Size
            // 
            this.m_hTextHL0Size.Location = new System.Drawing.Point(92, 37);
            this.m_hTextHL0Size.Name = "m_hTextHL0Size";
            this.m_hTextHL0Size.Size = new System.Drawing.Size(100, 20);
            this.m_hTextHL0Size.TabIndex = 3;
            this.m_hTextHL0Size.Text = "25";
            this.m_hTextHL0Size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "HL0 Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "HL1 Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "HL2 Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Output Size";
            // 
            // m_hTextHL1Size
            // 
            this.m_hTextHL1Size.Location = new System.Drawing.Point(92, 63);
            this.m_hTextHL1Size.Name = "m_hTextHL1Size";
            this.m_hTextHL1Size.Size = new System.Drawing.Size(100, 20);
            this.m_hTextHL1Size.TabIndex = 8;
            this.m_hTextHL1Size.Text = "0";
            this.m_hTextHL1Size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_hTextHL2Size
            // 
            this.m_hTextHL2Size.Location = new System.Drawing.Point(92, 89);
            this.m_hTextHL2Size.Name = "m_hTextHL2Size";
            this.m_hTextHL2Size.Size = new System.Drawing.Size(100, 20);
            this.m_hTextHL2Size.TabIndex = 9;
            this.m_hTextHL2Size.Text = "0";
            this.m_hTextHL2Size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_hTextOutputSize
            // 
            this.m_hTextOutputSize.Location = new System.Drawing.Point(92, 115);
            this.m_hTextOutputSize.Name = "m_hTextOutputSize";
            this.m_hTextOutputSize.Size = new System.Drawing.Size(100, 20);
            this.m_hTextOutputSize.TabIndex = 10;
            this.m_hTextOutputSize.Text = "5";
            this.m_hTextOutputSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(209, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Activation";
            // 
            // m_hButtonCreate
            // 
            this.m_hButtonCreate.Location = new System.Drawing.Point(300, 154);
            this.m_hButtonCreate.Name = "m_hButtonCreate";
            this.m_hButtonCreate.Size = new System.Drawing.Size(75, 23);
            this.m_hButtonCreate.TabIndex = 13;
            this.m_hButtonCreate.Text = "Create";
            this.m_hButtonCreate.UseVisualStyleBackColor = true;
            this.m_hButtonCreate.Click += new System.EventHandler(this.m_hButtonCreate_Click);
            // 
            // m_hButtonCancel
            // 
            this.m_hButtonCancel.Location = new System.Drawing.Point(381, 154);
            this.m_hButtonCancel.Name = "m_hButtonCancel";
            this.m_hButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.m_hButtonCancel.TabIndex = 14;
            this.m_hButtonCancel.Text = "Cancel";
            this.m_hButtonCancel.UseVisualStyleBackColor = true;
            this.m_hButtonCancel.Click += new System.EventHandler(this.m_hButtonCancel_Click);
            // 
            // m_hCheckVisualize
            // 
            this.m_hCheckVisualize.AutoSize = true;
            this.m_hCheckVisualize.Location = new System.Drawing.Point(345, 39);
            this.m_hCheckVisualize.Name = "m_hCheckVisualize";
            this.m_hCheckVisualize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_hCheckVisualize.Size = new System.Drawing.Size(111, 17);
            this.m_hCheckVisualize.TabIndex = 15;
            this.m_hCheckVisualize.Text = "Open In Visualizer";
            this.m_hCheckVisualize.UseVisualStyleBackColor = true;
            // 
            // m_hTextNSize
            // 
            this.m_hTextNSize.Location = new System.Drawing.Point(356, 115);
            this.m_hTextNSize.Name = "m_hTextNSize";
            this.m_hTextNSize.Size = new System.Drawing.Size(100, 20);
            this.m_hTextNSize.TabIndex = 21;
            this.m_hTextNSize.Text = "10";
            this.m_hTextNSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_hTextHeight
            // 
            this.m_hTextHeight.Location = new System.Drawing.Point(356, 89);
            this.m_hTextHeight.Name = "m_hTextHeight";
            this.m_hTextHeight.Size = new System.Drawing.Size(100, 20);
            this.m_hTextHeight.TabIndex = 20;
            this.m_hTextHeight.Text = "600";
            this.m_hTextHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_hTextWidth
            // 
            this.m_hTextWidth.Location = new System.Drawing.Point(356, 63);
            this.m_hTextWidth.Name = "m_hTextWidth";
            this.m_hTextWidth.Size = new System.Drawing.Size(100, 20);
            this.m_hTextWidth.TabIndex = 19;
            this.m_hTextWidth.Text = "800";
            this.m_hTextWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Neuron Size";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(270, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Window Height";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(273, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Window Width";
            // 
            // CreateNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 189);
            this.Controls.Add(this.m_hTextNSize);
            this.Controls.Add(this.m_hTextHeight);
            this.Controls.Add(this.m_hTextWidth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_hCheckVisualize);
            this.Controls.Add(this.m_hButtonCancel);
            this.Controls.Add(this.m_hButtonCreate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_hTextOutputSize);
            this.Controls.Add(this.m_hTextHL2Size);
            this.Controls.Add(this.m_hTextHL1Size);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_hTextHL0Size);
            this.Controls.Add(this.m_hComboActivation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_hTextInputSize);
            this.Name = "CreateNetworkForm";
            this.Text = "Create Network";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_hTextInputSize;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox m_hComboActivation;
        private System.Windows.Forms.TextBox m_hTextHL0Size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_hTextHL1Size;
        private System.Windows.Forms.TextBox m_hTextHL2Size;
        private System.Windows.Forms.TextBox m_hTextOutputSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button m_hButtonCreate;
        private System.Windows.Forms.Button m_hButtonCancel;
        private System.Windows.Forms.CheckBox m_hCheckVisualize;
        private System.Windows.Forms.TextBox m_hTextNSize;
        private System.Windows.Forms.TextBox m_hTextHeight;
        private System.Windows.Forms.TextBox m_hTextWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}