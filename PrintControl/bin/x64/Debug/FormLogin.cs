using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;

namespace PrintControl
{
    public partial class FormLogin : Form {

        #region Constants
        private const String GENERIC_ERROR = "-1";
        private const String CREDENTIALS_ERROR = "-2";
        private const String BLANK = "";
        #endregion

        #region Attributes
        private FormConfig config;
        protected String sessionId;
        protected WebPrintRestProxy restProxy;
        #endregion

        #region Constructor
        public FormLogin() {
            InitializeComponent();
            restProxy = new WebPrintRestProxy("");
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Event handler for focus leave user id text field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserID_Leave(object sender, EventArgs e)  {
            try {
                if (this.txtUserID.TextLength == 0) {
                    Alert(PrintControl.Properties.Resources.ERR_LOGIN_USER_ID);
                    this.txtUserID.Focus();
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// Event handler for focus leave password text field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_Leave(object sender, EventArgs e) {
            try {
                if (this.txtPassword.TextLength == 0) {
                    Alert(PrintControl.Properties.Resources.ERR_LOGIN_PASSWORD);
                    this.txtPassword.Focus();
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// Event handler for button submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void bntSubmit_Click(object sender, EventArgs e) {
            try {
                this.bntSubmit.Hide();
                if (this.txtUserID.TextLength == 0) this.txtUserID.Focus();
                if (this.txtPassword.TextLength == 0) this.txtPassword.Focus();

                //Added to upper to convert all the characters into uppercase
                String sessionId = await restProxy.DoLogin(this.txtUserID.Text.ToUpper() + ":" + this.txtPassword.Text);
                Console.WriteLine("sessionId: " + sessionId);

                switch (sessionId) {
                    case BLANK:
                    case GENERIC_ERROR:
                        Alert(Properties.Resources.ERR_LOGIN_EXCEPTION);
                        this.bntSubmit.Show();
                        return;
                    case CREDENTIALS_ERROR:
                        Alert(Properties.Resources.ERR_LOGIN_FAIL);
                        this.bntSubmit.Show();
                        return;
                    default:
                        break;
                }
                this.Hide();
                config = new FormConfig(sessionId);
                config.ShowDialog(this);
            } catch (Exception ex) {
                LogException(ex);
            }
            this.Close();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        protected void Alert(String msg) {
            try {
                if (null != msg && msg.Length > 0) MessageBox.Show(msg, Properties.Resources.FORM_ALERT_TITLE);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="ex"></param>
        protected void LogException(Exception ex) {
            Console.WriteLine(ex.StackTrace);
            FormException form = new FormException(ex);
            form.ShowDialog();
        }
        #endregion

    }
}
