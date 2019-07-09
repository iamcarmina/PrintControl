namespace SmarteCOPrintControl
{
    partial class FormException
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormException));
            this.label1 = new System.Windows.Forms.Label();
            this.lbExceptionMsg = new System.Windows.Forms.Label();
            this.txtExceptionStack = new System.Windows.Forms.TextBox();
            this.lbDetails = new System.Windows.Forms.Label();
            this.bntClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "We apologize that we have encountered an exception. Please retry the operation an" +
    "d if the operation persist, please copy the content and email to support-eco@vca" +
    "rgocloud.com";
            // 
            // lbExceptionMsg
            // 
            this.lbExceptionMsg.AutoSize = true;
            this.lbExceptionMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExceptionMsg.Location = new System.Drawing.Point(25, 52);
            this.lbExceptionMsg.Name = "lbExceptionMsg";
            this.lbExceptionMsg.Size = new System.Drawing.Size(20, 16);
            this.lbExceptionMsg.TabIndex = 1;
            this.lbExceptionMsg.Text = "...";
            // 
            // txtExceptionStack
            // 
            this.txtExceptionStack.Location = new System.Drawing.Point(28, 95);
            this.txtExceptionStack.Multiline = true;
            this.txtExceptionStack.Name = "txtExceptionStack";
            this.txtExceptionStack.ReadOnly = true;
            this.txtExceptionStack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtExceptionStack.Size = new System.Drawing.Size(307, 169);
            this.txtExceptionStack.TabIndex = 2;
            // 
            // lbDetails
            // 
            this.lbDetails.AutoSize = true;
            this.lbDetails.Location = new System.Drawing.Point(25, 79);
            this.lbDetails.Name = "lbDetails";
            this.lbDetails.Size = new System.Drawing.Size(39, 13);
            this.lbDetails.TabIndex = 3;
            this.lbDetails.Text = "Details";
            // 
            // bntClose
            // 
            this.bntClose.Location = new System.Drawing.Point(260, 270);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(75, 23);
            this.bntClose.TabIndex = 4;
            this.bntClose.Text = "Close";
            this.bntClose.UseVisualStyleBackColor = true;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // FormException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 305);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.lbDetails);
            this.Controls.Add(this.txtExceptionStack);
            this.Controls.Add(this.lbExceptionMsg);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormException";
            this.Text = "Exception";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbExceptionMsg;
        private System.Windows.Forms.TextBox txtExceptionStack;
        private System.Windows.Forms.Label lbDetails;
        private System.Windows.Forms.Button bntClose;
    }
}