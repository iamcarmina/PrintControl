using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintControl
{
    public partial class FormLogin : Form
    {
        private FormConfig config;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void bntSubmit_Click(object sender, EventArgs e)
        {
            String userId = this.txtUserID.Text;
            String password = this.txtPassword.Text;
            this.Hide();

            // Make web service call if the login is ok then show configuration box
            config = new FormConfig();
            config.ShowDialog();

            this.Show();
        }


    }
}
