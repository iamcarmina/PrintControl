using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmarteCOPrintControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ceTe.DynamicPDF.Printing.PrintJob.AddLicense("PMG40NXDCDEHIOk6m1RC2l6e4X21k44IUxKr512MYnlbuoQjMgFcpn1T6CQEt6ZRYLnYFhQqZ5bMDlAVAZPjoeLEqfnLGFpnNhiQ");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}
