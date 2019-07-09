using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintControl {
    public partial class FormTest : Form {
        public FormTest() {
            InitializeComponent();
        }

        private void bntSubmit_Click(object sender, EventArgs e) {
            MessageBox.Show(this.txtName.Text);
        }

        private void txtName_Leave(object sender, EventArgs e) {
            Console.WriteLine(this.txtName.Text);
            this.txtName.Text = "Hello Mars";
        }
    }
}
