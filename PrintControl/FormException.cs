using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmarteCOPrintControl
{
    public partial class FormException : Form
    {
        public FormException(Exception ex)
        {
            InitializeComponent();
            PopulateForm(ex);
        }



        #region Helper Methods
        /// <summary>
        /// Populate the form
        /// </summary>
        /// <param name="ex"></param>
        private void PopulateForm(Exception ex) {
            try {
                if (null != ex) {
                    this.lbDetails.Text = ex.Message;
                    this.txtExceptionStack.Text = ex.StackTrace;
                }
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// Event handler for close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        #endregion

    }
}
