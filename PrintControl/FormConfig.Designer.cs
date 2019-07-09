namespace SmarteCOPrintControl
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OffsetMinMaxLabel2 = new System.Windows.Forms.Label();
            this.OffsetMinMaxLabel = new System.Windows.Forms.Label();
            this.txtWithLogoOffset = new System.Windows.Forms.TextBox();
            this.WithLogoOffset = new System.Windows.Forms.Label();
            this.cbPPWithLogo = new System.Windows.Forms.CheckBox();
            this.bntPPWithLogo = new System.Windows.Forms.Button();
            this.cbPPWithLogoForm = new System.Windows.Forms.ComboBox();
            this.lbPPWithLogo = new System.Windows.Forms.Label();
            this.txtWithoutLogoOffset = new System.Windows.Forms.TextBox();
            this.WithOutLogoOffset = new System.Windows.Forms.Label();
            this.lbResolution = new System.Windows.Forms.Label();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.cbPink = new System.Windows.Forms.CheckBox();
            this.cbWhite = new System.Windows.Forms.CheckBox();
            this.bntPink = new System.Windows.Forms.Button();
            this.bntWhite = new System.Windows.Forms.Button();
            this.cbPinkForm = new System.Windows.Forms.ComboBox();
            this.lbPinkForm = new System.Windows.Forms.Label();
            this.cbWhiteForm = new System.Windows.Forms.ComboBox();
            this.lbWhiteForm = new System.Windows.Forms.Label();
            this.cbPrinter = new System.Windows.Forms.ComboBox();
            this.lbPrinter = new System.Windows.Forms.Label();
            this.bntStart = new System.Windows.Forms.Button();
            this.bntExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OffsetMinMaxLabel2);
            this.groupBox1.Controls.Add(this.OffsetMinMaxLabel);
            this.groupBox1.Controls.Add(this.txtWithLogoOffset);
            this.groupBox1.Controls.Add(this.WithLogoOffset);
            this.groupBox1.Controls.Add(this.cbPPWithLogo);
            this.groupBox1.Controls.Add(this.bntPPWithLogo);
            this.groupBox1.Controls.Add(this.cbPPWithLogoForm);
            this.groupBox1.Controls.Add(this.lbPPWithLogo);
            this.groupBox1.Controls.Add(this.txtWithoutLogoOffset);
            this.groupBox1.Controls.Add(this.WithOutLogoOffset);
            this.groupBox1.Controls.Add(this.lbResolution);
            this.groupBox1.Controls.Add(this.cbResolution);
            this.groupBox1.Controls.Add(this.cbPink);
            this.groupBox1.Controls.Add(this.cbWhite);
            this.groupBox1.Controls.Add(this.bntPink);
            this.groupBox1.Controls.Add(this.bntWhite);
            this.groupBox1.Controls.Add(this.cbPinkForm);
            this.groupBox1.Controls.Add(this.lbPinkForm);
            this.groupBox1.Controls.Add(this.cbWhiteForm);
            this.groupBox1.Controls.Add(this.lbWhiteForm);
            this.groupBox1.Controls.Add(this.cbPrinter);
            this.groupBox1.Controls.Add(this.lbPrinter);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Printer Configuration";
            // 
            // OffsetMinMaxLabel2
            // 
            this.OffsetMinMaxLabel2.AutoSize = true;
            this.OffsetMinMaxLabel2.Location = new System.Drawing.Point(381, 177);
            this.OffsetMinMaxLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OffsetMinMaxLabel2.Name = "OffsetMinMaxLabel2";
            this.OffsetMinMaxLabel2.Size = new System.Drawing.Size(95, 13);
            this.OffsetMinMaxLabel2.TabIndex = 24;
            this.OffsetMinMaxLabel2.Text = "Min = -25, Max = 5";
            // 
            // OffsetMinMaxLabel
            // 
            this.OffsetMinMaxLabel.AutoSize = true;
            this.OffsetMinMaxLabel.Location = new System.Drawing.Point(381, 118);
            this.OffsetMinMaxLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OffsetMinMaxLabel.Name = "OffsetMinMaxLabel";
            this.OffsetMinMaxLabel.Size = new System.Drawing.Size(95, 13);
            this.OffsetMinMaxLabel.TabIndex = 23;
            this.OffsetMinMaxLabel.Text = "Min = -25, Max = 5";
            // 
            // txtWithLogoOffset
            // 
            this.txtWithLogoOffset.Location = new System.Drawing.Point(283, 116);
            this.txtWithLogoOffset.Name = "txtWithLogoOffset";
            this.txtWithLogoOffset.Size = new System.Drawing.Size(88, 20);
            this.txtWithLogoOffset.TabIndex = 22;
            this.txtWithLogoOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
            // 
            // WithLogoOffset
            // 
            this.WithLogoOffset.AutoSize = true;
            this.WithLogoOffset.Location = new System.Drawing.Point(204, 120);
            this.WithLogoOffset.Name = "WithLogoOffset";
            this.WithLogoOffset.Size = new System.Drawing.Size(68, 13);
            this.WithLogoOffset.TabIndex = 21;
            this.WithLogoOffset.Text = "Printer Offset";
            // 
            // cbPPWithLogo
            // 
            this.cbPPWithLogo.AutoSize = true;
            this.cbPPWithLogo.Location = new System.Drawing.Point(14, 90);
            this.cbPPWithLogo.Name = "cbPPWithLogo";
            this.cbPPWithLogo.Size = new System.Drawing.Size(127, 17);
            this.cbPPWithLogo.TabIndex = 20;
            this.cbPPWithLogo.Text = "PrePrinted(with Logo)";
            this.cbPPWithLogo.UseVisualStyleBackColor = true;
            this.cbPPWithLogo.CheckedChanged += new System.EventHandler(this.cbPPWithLogo_CheckedChanged);
            // 
            // bntPPWithLogo
            // 
            this.bntPPWithLogo.Location = new System.Drawing.Point(450, 86);
            this.bntPPWithLogo.Name = "bntPPWithLogo";
            this.bntPPWithLogo.Size = new System.Drawing.Size(75, 23);
            this.bntPPWithLogo.TabIndex = 19;
            this.bntPPWithLogo.Text = "Test Print";
            this.bntPPWithLogo.UseVisualStyleBackColor = true;
            this.bntPPWithLogo.Click += new System.EventHandler(this.bntPPWithLogo_Click);
            // 
            // cbPPWithLogoForm
            // 
            this.cbPPWithLogoForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPPWithLogoForm.FormattingEnabled = true;
            this.cbPPWithLogoForm.Location = new System.Drawing.Point(283, 88);
            this.cbPPWithLogoForm.Name = "cbPPWithLogoForm";
            this.cbPPWithLogoForm.Size = new System.Drawing.Size(153, 21);
            this.cbPPWithLogoForm.TabIndex = 18;
            this.cbPPWithLogoForm.SelectedIndexChanged += new System.EventHandler(this.cbPPWithLogoForm_SelectedIndexChanged);
            // 
            // lbPPWithLogo
            // 
            this.lbPPWithLogo.AutoSize = true;
            this.lbPPWithLogo.Location = new System.Drawing.Point(204, 93);
            this.lbPPWithLogo.Name = "lbPPWithLogo";
            this.lbPPWithLogo.Size = new System.Drawing.Size(64, 13);
            this.lbPPWithLogo.TabIndex = 17;
            this.lbPPWithLogo.Text = "Printer Tray:";
            // 
            // txtWithoutLogoOffset
            // 
            this.txtWithoutLogoOffset.Location = new System.Drawing.Point(283, 174);
            this.txtWithoutLogoOffset.Name = "txtWithoutLogoOffset";
            this.txtWithoutLogoOffset.Size = new System.Drawing.Size(88, 20);
            this.txtWithoutLogoOffset.TabIndex = 16;
            this.txtWithoutLogoOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
            // 
            // WithOutLogoOffset
            // 
            this.WithOutLogoOffset.AutoSize = true;
            this.WithOutLogoOffset.Location = new System.Drawing.Point(204, 174);
            this.WithOutLogoOffset.Name = "WithOutLogoOffset";
            this.WithOutLogoOffset.Size = new System.Drawing.Size(68, 13);
            this.WithOutLogoOffset.TabIndex = 15;
            this.WithOutLogoOffset.Text = "Printer Offset";
            // 
            // lbResolution
            // 
            this.lbResolution.AutoSize = true;
            this.lbResolution.Location = new System.Drawing.Point(248, 33);
            this.lbResolution.Name = "lbResolution";
            this.lbResolution.Size = new System.Drawing.Size(57, 13);
            this.lbResolution.TabIndex = 14;
            this.lbResolution.Text = "Resolution";
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(311, 30);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(126, 21);
            this.cbResolution.TabIndex = 13;
            this.cbResolution.SelectedIndexChanged += new System.EventHandler(this.cbResolution_SelectedIndexChanged);
            // 
            // cbPink
            // 
            this.cbPink.AutoSize = true;
            this.cbPink.Location = new System.Drawing.Point(14, 145);
            this.cbPink.Name = "cbPink";
            this.cbPink.Size = new System.Drawing.Size(75, 17);
            this.cbPink.TabIndex = 12;
            this.cbPink.Text = "PrePrinted";
            this.cbPink.UseVisualStyleBackColor = true;
            this.cbPink.CheckedChanged += new System.EventHandler(this.cbPink_CheckedChanged);
            // 
            // cbWhite
            // 
            this.cbWhite.AutoSize = true;
            this.cbWhite.Location = new System.Drawing.Point(14, 66);
            this.cbWhite.Name = "cbWhite";
            this.cbWhite.Size = new System.Drawing.Size(39, 17);
            this.cbWhite.TabIndex = 11;
            this.cbWhite.Text = "A4";
            this.cbWhite.UseVisualStyleBackColor = true;
            this.cbWhite.CheckedChanged += new System.EventHandler(this.cbWhite_CheckedChanged);
            // 
            // bntPink
            // 
            this.bntPink.Location = new System.Drawing.Point(450, 142);
            this.bntPink.Name = "bntPink";
            this.bntPink.Size = new System.Drawing.Size(75, 23);
            this.bntPink.TabIndex = 7;
            this.bntPink.Text = "Test Print";
            this.bntPink.UseVisualStyleBackColor = true;
            this.bntPink.Click += new System.EventHandler(this.bntPink_Click);
            // 
            // bntWhite
            // 
            this.bntWhite.Location = new System.Drawing.Point(450, 64);
            this.bntWhite.Name = "bntWhite";
            this.bntWhite.Size = new System.Drawing.Size(75, 23);
            this.bntWhite.TabIndex = 6;
            this.bntWhite.Text = "Test Print";
            this.bntWhite.UseVisualStyleBackColor = true;
            this.bntWhite.Click += new System.EventHandler(this.bntWhite_Click);
            // 
            // cbPinkForm
            // 
            this.cbPinkForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPinkForm.FormattingEnabled = true;
            this.cbPinkForm.Location = new System.Drawing.Point(283, 145);
            this.cbPinkForm.Name = "cbPinkForm";
            this.cbPinkForm.Size = new System.Drawing.Size(153, 21);
            this.cbPinkForm.TabIndex = 5;
            this.cbPinkForm.SelectedIndexChanged += new System.EventHandler(this.cbPinkForm_SelectedIndexChanged);
            // 
            // lbPinkForm
            // 
            this.lbPinkForm.AutoSize = true;
            this.lbPinkForm.Location = new System.Drawing.Point(204, 150);
            this.lbPinkForm.Name = "lbPinkForm";
            this.lbPinkForm.Size = new System.Drawing.Size(61, 13);
            this.lbPinkForm.TabIndex = 4;
            this.lbPinkForm.Text = "Printer Tray";
            // 
            // cbWhiteForm
            // 
            this.cbWhiteForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhiteForm.FormattingEnabled = true;
            this.cbWhiteForm.Location = new System.Drawing.Point(283, 66);
            this.cbWhiteForm.Name = "cbWhiteForm";
            this.cbWhiteForm.Size = new System.Drawing.Size(153, 21);
            this.cbWhiteForm.TabIndex = 3;
            this.cbWhiteForm.SelectedIndexChanged += new System.EventHandler(this.cbWhiteForm_SelectedIndexChanged);
            // 
            // lbWhiteForm
            // 
            this.lbWhiteForm.AutoSize = true;
            this.lbWhiteForm.Location = new System.Drawing.Point(204, 68);
            this.lbWhiteForm.Name = "lbWhiteForm";
            this.lbWhiteForm.Size = new System.Drawing.Size(64, 13);
            this.lbWhiteForm.TabIndex = 2;
            this.lbWhiteForm.Text = "Printer Tray:";
            // 
            // cbPrinter
            // 
            this.cbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrinter.FormattingEnabled = true;
            this.cbPrinter.Location = new System.Drawing.Point(75, 30);
            this.cbPrinter.Name = "cbPrinter";
            this.cbPrinter.Size = new System.Drawing.Size(158, 21);
            this.cbPrinter.TabIndex = 1;
            this.cbPrinter.SelectedIndexChanged += new System.EventHandler(this.cbPrinter_SelectedIndexChanged);
            // 
            // lbPrinter
            // 
            this.lbPrinter.AutoSize = true;
            this.lbPrinter.Location = new System.Drawing.Point(29, 33);
            this.lbPrinter.Name = "lbPrinter";
            this.lbPrinter.Size = new System.Drawing.Size(40, 13);
            this.lbPrinter.TabIndex = 0;
            this.lbPrinter.Text = "Printer:";
            // 
            // bntStart
            // 
            this.bntStart.Location = new System.Drawing.Point(405, 227);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(75, 23);
            this.bntStart.TabIndex = 11;
            this.bntStart.Text = "Start";
            this.bntStart.UseVisualStyleBackColor = true;
            this.bntStart.Click += new System.EventHandler(this.bntStart_Click);
            // 
            // bntExit
            // 
            this.bntExit.Location = new System.Drawing.Point(324, 227);
            this.bntExit.Name = "bntExit";
            this.bntExit.Size = new System.Drawing.Size(75, 23);
            this.bntExit.TabIndex = 12;
            this.bntExit.Text = "Exit";
            this.bntExit.UseVisualStyleBackColor = true;
            this.bntExit.Click += new System.EventHandler(this.bntExit_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 259);
            this.Controls.Add(this.bntExit);
            this.Controls.Add(this.bntStart);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartECO Print - Print Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbPrinter;
        private System.Windows.Forms.Label lbPrinter;
        private System.Windows.Forms.ComboBox cbPinkForm;
        private System.Windows.Forms.Label lbPinkForm;
        private System.Windows.Forms.ComboBox cbWhiteForm;
        private System.Windows.Forms.Label lbWhiteForm;
        private System.Windows.Forms.Button bntPink;
        private System.Windows.Forms.Button bntWhite;
        private System.Windows.Forms.CheckBox cbWhite;
        private System.Windows.Forms.Label lbResolution;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.CheckBox cbPink;
        private System.Windows.Forms.TextBox txtWithoutLogoOffset;
        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.Button bntExit;
        private System.Windows.Forms.CheckBox cbPPWithLogo;
        private System.Windows.Forms.Button bntPPWithLogo;
        private System.Windows.Forms.ComboBox cbPPWithLogoForm;
        private System.Windows.Forms.Label lbPPWithLogo;
        private System.Windows.Forms.TextBox txtWithLogoOffset;
        private System.Windows.Forms.Label WithLogoOffset;
        private System.Windows.Forms.Label WithOutLogoOffset;
        private System.Windows.Forms.Label OffsetMinMaxLabel;
        private System.Windows.Forms.Label OffsetMinMaxLabel2;
    }
}