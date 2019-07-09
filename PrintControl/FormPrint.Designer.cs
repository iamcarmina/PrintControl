namespace SmarteCOPrintControl
{
    partial class FormPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrint));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CodataGridView = new System.Windows.Forms.DataGridView();
            this.ColQTranNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQCertNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bntClose = new System.Windows.Forms.Button();
            this.bntHide = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PrintedCOdataGrid = new System.Windows.Forms.DataGridView();
            this.ColTranNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCertNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNoOfOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNoOfCopies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.lblLastLogin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CodataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintedCOdataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(12, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(392, 211);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Certificate Print Queue";
            // 
            // CodataGridView
            // 
            this.CodataGridView.AllowUserToAddRows = false;
            this.CodataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CodataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColQTranNo,
            this.ColQCertNo,
            this.ColQRemarks});
            this.CodataGridView.Location = new System.Drawing.Point(12, 79);
            this.CodataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.CodataGridView.Name = "CodataGridView";
            this.CodataGridView.RowHeadersWidth = 62;
            this.CodataGridView.RowTemplate.Height = 28;
            this.CodataGridView.Size = new System.Drawing.Size(392, 182);
            this.CodataGridView.TabIndex = 6;
            // 
            // ColQTranNo
            // 
            this.ColQTranNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColQTranNo.HeaderText = "Transaction No.";
            this.ColQTranNo.MinimumWidth = 8;
            this.ColQTranNo.Name = "ColQTranNo";
            // 
            // ColQCertNo
            // 
            this.ColQCertNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColQCertNo.HeaderText = "Certificate No.";
            this.ColQCertNo.MinimumWidth = 8;
            this.ColQCertNo.Name = "ColQCertNo";
            // 
            // ColQRemarks
            // 
            this.ColQRemarks.HeaderText = "Remarks";
            this.ColQRemarks.Name = "ColQRemarks";
            // 
            // bntClose
            // 
            this.bntClose.Location = new System.Drawing.Point(817, 278);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(75, 23);
            this.bntClose.TabIndex = 3;
            this.bntClose.Text = "Close";
            this.bntClose.UseVisualStyleBackColor = true;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // bntHide
            // 
            this.bntHide.Location = new System.Drawing.Point(670, 278);
            this.bntHide.Name = "bntHide";
            this.bntHide.Size = new System.Drawing.Size(75, 23);
            this.bntHide.TabIndex = 4;
            this.bntHide.Text = "Hide";
            this.bntHide.UseVisualStyleBackColor = true;
            this.bntHide.Visible = false;
            this.bntHide.Click += new System.EventHandler(this.BntHide_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(424, 50);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(502, 211);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Certificate Printed";
            // 
            // PrintedCOdataGrid
            // 
            this.PrintedCOdataGrid.AllowUserToAddRows = false;
            this.PrintedCOdataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PrintedCOdataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTranNo,
            this.ColCertNo,
            this.ColNoOfOriginal,
            this.ColNoOfCopies,
            this.ColRemarks});
            this.PrintedCOdataGrid.Location = new System.Drawing.Point(424, 79);
            this.PrintedCOdataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.PrintedCOdataGrid.Name = "PrintedCOdataGrid";
            this.PrintedCOdataGrid.RowHeadersWidth = 62;
            this.PrintedCOdataGrid.RowTemplate.Height = 28;
            this.PrintedCOdataGrid.Size = new System.Drawing.Size(502, 182);
            this.PrintedCOdataGrid.TabIndex = 7;
            // 
            // ColTranNo
            // 
            this.ColTranNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColTranNo.HeaderText = "Transaction No.";
            this.ColTranNo.MinimumWidth = 8;
            this.ColTranNo.Name = "ColTranNo";
            this.ColTranNo.ReadOnly = true;
            // 
            // ColCertNo
            // 
            this.ColCertNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColCertNo.HeaderText = "Certificate No.";
            this.ColCertNo.MinimumWidth = 8;
            this.ColCertNo.Name = "ColCertNo";
            this.ColCertNo.ReadOnly = true;
            // 
            // ColNoOfOriginal
            // 
            this.ColNoOfOriginal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNoOfOriginal.FillWeight = 50F;
            this.ColNoOfOriginal.HeaderText = "Original";
            this.ColNoOfOriginal.MinimumWidth = 8;
            this.ColNoOfOriginal.Name = "ColNoOfOriginal";
            this.ColNoOfOriginal.ReadOnly = true;
            // 
            // ColNoOfCopies
            // 
            this.ColNoOfCopies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNoOfCopies.FillWeight = 50F;
            this.ColNoOfCopies.HeaderText = "Copies";
            this.ColNoOfCopies.MinimumWidth = 8;
            this.ColNoOfCopies.Name = "ColNoOfCopies";
            this.ColNoOfCopies.ReadOnly = true;
            // 
            // ColRemarks
            // 
            this.ColRemarks.HeaderText = "Remarks";
            this.ColRemarks.Name = "ColRemarks";
            this.ColRemarks.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Current user:";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Last log in date:";
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentUser.Location = new System.Drawing.Point(82, 9);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(11, 13);
            this.lblCurrentUser.TabIndex = 12;
            this.lblCurrentUser.Text = " ";
            // 
            // lblLastLogin
            // 
            this.lblLastLogin.AutoSize = true;
            this.lblLastLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLastLogin.Location = new System.Drawing.Point(510, 9);
            this.lblLastLogin.Name = "lblLastLogin";
            this.lblLastLogin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblLastLogin.Size = new System.Drawing.Size(11, 13);
            this.lblLastLogin.TabIndex = 13;
            this.lblLastLogin.Text = " ";
            // 
            // FormPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(938, 317);
            this.Controls.Add(this.lblLastLogin);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CodataGridView);
            this.Controls.Add(this.PrintedCOdataGrid);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.bntHide);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrint";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 10, 13);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartECO Print - PrintQueue";
            ((System.ComponentModel.ISupportInitialize)(this.CodataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintedCOdataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bntClose;
        private System.Windows.Forms.Button bntHide;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView CodataGridView;
        private System.Windows.Forms.DataGridView PrintedCOdataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTranNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCertNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNoOfOriginal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNoOfCopies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRemarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQTranNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQCertNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQRemarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label lblLastLogin;
    }
}