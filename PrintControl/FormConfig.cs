namespace SmarteCOPrintControl
{
    using ceTe.DynamicPDF.Printing;
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Net.Http;
    using System.Text;
    using System.Collections;
    using Newtonsoft.Json;
    using System.IO;
    using SmarteCOPrintControl;

    /// <summary>
    /// Defines the <see cref="FormConfig" />
    /// </summary>
    public partial class FormConfig : Form  {

        #region Constants
        private enum ConfigState { INIT, SELECT_PRINTER, SELECT_RESOLUTION, CHECK_PINK, UNCHECK_PINK, SELECT_TRAY_PINK, SELECT_TRAY_WHITE, CHECK_WHITE, UNCHECK_WHITE,SELECT_TRAY_PPWITHLOGO, CHECK_PPWITHLOGO, UNCHECK_PPWITHLOGO, TEST_PRINT_PINK, TEST_PRINT_WHITE, TEST_PRINT_PP_WITHLOGO, TEST_PRINT_COMPLETE }
        #endregion

        #region Attributes
        protected String sessionId;
        protected WebPrintRestProxy restProxy;
        private PrinterConfiguration printerConfiguration;
        private FormPrint printForm;
        private String WithLogOffset;
        private String WithOutLogOffset;
        private String OffsetConfig;
        private String minOffset;
        private bool flag = true;
        private String maxOffset;

        public enum FormType { WHITE_FORM, PINK_FORM, PP_WITH_LOGO };

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="restProxy"></param>
        public FormConfig(String sessionId)  {
            try {
                InitializeComponent();

                this.sessionId = sessionId;
                this.restProxy = new WebPrintRestProxy(sessionId);
                this.GetOffsetConfig();
                this.UpdateControls(ConfigState.INIT);
                this.InitPrinters();

                if (Settings1.Default.PinkCb && Settings1.Default.LoggedIn) this.cbPink.Checked = true;
                if (Settings1.Default.A4Checkbox && Settings1.Default.LoggedIn) this.cbWhite.Checked = true;
                if (Settings1.Default.PPWithLogoCb && Settings1.Default.LoggedIn) this.cbPPWithLogo.Checked = true;

            }
            catch (Exception ex) {
                LogException(ex);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// The cbPrinter_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void cbPrinter_SelectedIndexChanged(object sender, EventArgs e)  {
            try {
                if(this.cbPrinter.Text != Settings1.Default.Printer_name)
                {
                    Settings1.Default.LoggedIn = false;
                }

                Printer printer = new Printer(cbPrinter.Text);
                this.printerConfiguration = new PrinterConfiguration();
                this.printerConfiguration.Name = cbPrinter.Text;

                foreach (PaperSource paperSource in printer.PaperSources) this.printerConfiguration.PaperSources.Add(paperSource.Name);
                foreach (Resolution resolution in printer.Resolutions) this.printerConfiguration.Resolutions.Add(resolution.HorizontalDpi + " x " + resolution.VerticalDpi);

                this.UpdateControls(ConfigState.SELECT_PRINTER);
                InitPrintConfiguration();
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// Printer resolutions change handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (null == this.printerConfiguration) return;

                this.printerConfiguration.Resolution = cbResolution.SelectedIndex;
                this.UpdateControls(ConfigState.SELECT_RESOLUTION);
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// White form selection check handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbWhite_CheckedChanged(object sender, EventArgs e)  {
            try {
                if (this.cbWhite.Checked) {
                    UpdateControls(ConfigState.CHECK_WHITE);
                } else {
                    UpdateControls(ConfigState.UNCHECK_WHITE);
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// White form paper selection handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbWhiteForm_SelectedIndexChanged(object sender, EventArgs e)  {
            try {
                if (null == this.printerConfiguration) return;

                this.printerConfiguration.WhiteTray = cbWhiteForm.SelectedIndex;
                this.UpdateControls(ConfigState.SELECT_TRAY_WHITE);
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// White form test print handler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private async void bntWhite_Click(object sender, EventArgs e) {
            try {
                if (null == this.printerConfiguration) return;

                CoDoc coDoc = await restProxy.DoCOTestAsync(WebPrintRestProxy.FormType.WHITE_FORM, "0");
                if (null != coDoc) {
                    Console.WriteLine(coDoc.ToString());
                    if (!coDoc.errCode.Equals("0")) throw new Exception("CO Retrieve Error " + coDoc.errCode);

                    this.UpdateControls(ConfigState.TEST_PRINT_WHITE);
                    TestPrintCO(WebPrintRestProxy.FormType.WHITE_FORM, coDoc);
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }


        private async void cbPPWithLogo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                String formType = FormConfig.FormType.PP_WITH_LOGO.ToString();
                if (this.cbPPWithLogo.Checked)
                {
                    this.UpdateControls(ConfigState.CHECK_PPWITHLOGO);
                    WithLogOffset = await restProxy.GetUserPrintOffsetAsync(formType);
                    if (WithLogOffset != null)
                    {
                        this.txtWithLogoOffset.Text = WithLogOffset;
                    }
                }
                else
                {
                    this.UpdateControls(ConfigState.UNCHECK_PPWITHLOGO);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }


        private void cbPPWithLogoForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (null == this.printerConfiguration) return;

                this.printerConfiguration.PPWithLogoTray = cbPPWithLogoForm.SelectedIndex;
                this.UpdateControls(ConfigState.SELECT_TRAY_PPWITHLOGO);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }


        private async void bntPPWithLogo_Click(object sender, EventArgs e)
        {
            try
            {
                if (null == this.printerConfiguration) return;

                int value = 0;
                if (!int.TryParse(this.txtWithLogoOffset.Text, out value))
                {
                    Alert("Please enter a Number for margin offset.");
                    this.txtWithLogoOffset.Focus();
                    return;
                }

                CoDoc coDoc = await restProxy.DoCOTestAsync(WebPrintRestProxy.FormType.PP_WITH_LOGO, this.txtWithLogoOffset.Text);
                if (null != coDoc)
                {
                    Console.WriteLine(coDoc.ToString());
                    if (!coDoc.errCode.Equals("0")) throw new Exception("CO Retrieve Error " + coDoc.errCode);

                    this.UpdateControls(ConfigState.TEST_PRINT_PP_WITHLOGO);
                    TestPrintCO(WebPrintRestProxy.FormType.PP_WITH_LOGO, coDoc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Pink form selection check handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cbPink_CheckedChanged(object sender, EventArgs e) {
            try {
                String formType = FormConfig.FormType.PINK_FORM.ToString();
                if (this.cbPink.Checked) {
                    this.UpdateControls(ConfigState.CHECK_PINK);
                    WithOutLogOffset = await restProxy.GetUserPrintOffsetAsync(formType);
                    if (WithOutLogOffset != null)
                    {
                        this.txtWithoutLogoOffset.Text = WithOutLogOffset;
                    }
                } else {
                    this.UpdateControls(ConfigState.UNCHECK_PINK);
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// Pink form paper selection handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPinkForm_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (null == this.printerConfiguration) return;

                this.printerConfiguration.PinkTray = cbPinkForm.SelectedIndex;
                this.UpdateControls(ConfigState.SELECT_TRAY_PINK);
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOffset_TextChanged(object sender, EventArgs e) {
            try {
                this.bntStart.Enabled = true;
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// Pink form test print handler
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private async void bntPink_Click(object sender, EventArgs e) {
            try {
                if (null == this.printerConfiguration) return;

                int value = 0;
                if (!int.TryParse(this.txtWithoutLogoOffset.Text, out value)) {
                    Alert("Please enter a numeric integer for offset");
                    this.txtWithoutLogoOffset.Focus();
                    return;
                }
                CoDoc coDoc = await restProxy.DoCOTestAsync(WebPrintRestProxy.FormType.PINK_FORM, this.txtWithoutLogoOffset.Text);
                if (null != coDoc) {
                    Console.WriteLine(coDoc.ToString());
                    if (!coDoc.errCode.Equals("0")) throw new Exception("CO Retrieve Error " + coDoc.errCode);

                    this.UpdateControls(ConfigState.TEST_PRINT_PINK);
                    TestPrintCO(WebPrintRestProxy.FormType.PINK_FORM, coDoc);
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntStart_Click(object sender, EventArgs e) {
            this.flag = true;
            try {
                if (!ValidateCheckBoxAndTray())
                {
                    return;
                }

                if (this.cbPPWithLogo.Checked == true || this.cbPink.Checked == true)
                {
                    if (offsetText_check())
                    {
                        return;
                    }
                }

                int value;
                if (int.TryParse(this.txtWithLogoOffset.Text, out value) && cbPPWithLogo.Checked == true)
                {
                    if (this.txtWithLogoOffset.Text != WithLogOffset)
                    {
                        DialogBox(FormConfig.FormType.PP_WITH_LOGO, this.txtWithLogoOffset.Text);
                    }
                    if (!this.flag) return;
                }
                if (int.TryParse(this.txtWithoutLogoOffset.Text, out value) && cbPink.Checked == true)
                {
                    if (this.txtWithoutLogoOffset.Text != WithOutLogOffset) {
                        DialogBox(FormConfig.FormType.PINK_FORM, this.txtWithoutLogoOffset.Text);
                    }
                    if (!this.flag) return;
                }
                this.printerConfiguration.PrintMediaSelected = null;
                if (this.cbWhite.Checked)
                {
                    this.printerConfiguration.PrintMediaSelected = "A4";
                    Settings1.Default.MediaSelected = "A4";
                }
                    
                if (this.cbPink.Checked) {
                    if (this.printerConfiguration.PrintMediaSelected == null)
                    {
                        this.printerConfiguration.PrintMediaSelected = "PrePrinted";
                        Settings1.Default.MediaSelected = "Pre-Printed";
                    }
                    else
                    {
                        this.printerConfiguration.PrintMediaSelected = this.printerConfiguration.PrintMediaSelected + ",PrePrinted";
                        Settings1.Default.MediaSelected = Settings1.Default.MediaSelected + ", Pre-Printed";
                    }
                }

                if (this.cbPPWithLogo.Checked)
                {
                    if (this.printerConfiguration.PrintMediaSelected == null)
                    {
                        this.printerConfiguration.PrintMediaSelected = "PrePrintedWithLogo";
                        Settings1.Default.MediaSelected = "Pre-Printed (with Logo)";
                    }
                    else
                    {
                        this.printerConfiguration.PrintMediaSelected = this.printerConfiguration.PrintMediaSelected + ",PrePrintedWithLogo";
                        Settings1.Default.MediaSelected = Settings1.Default.MediaSelected + ", Pre-Printed (with Logo)";
                    }
                }

                Console.WriteLine("Settings 1 Default MediaSelected: " + Settings1.Default.MediaSelected);
                Settings1.Default.Save();

                Console.WriteLine("PrintMediaSelected :- " + this.printerConfiguration.PrintMediaSelected);
                Settings1.Default.Printer_name = this.cbPrinter.Text;
                Settings1.Default.Resolution = this.cbResolution.Text;
                Settings1.Default.A4Checkbox = this.cbWhite.Checked;
                Settings1.Default.PPWithLogoCb = this.cbPPWithLogo.Checked;
                Settings1.Default.PinkCb = this.cbPink.Checked;
                Settings1.Default.PinkTray = this.cbPinkForm.SelectedIndex;
                Settings1.Default.WhiteTray = this.cbWhiteForm.SelectedIndex;
                Settings1.Default.PPWithLogotray = this.cbPPWithLogoForm.SelectedIndex;
                Settings1.Default.LoggedIn = true;
                Settings1.Default.Save();
                this.Hide();
                printForm = new FormPrint(this.sessionId, this.printerConfiguration);
                printForm.ShowDialog(this);
            } catch (Exception ex) {
                LogException(ex);
            }
            this.Close();
        }

        private bool ValidateCheckBoxAndTray()
        {
            bool output = true;
            if (cbPink.Checked == false && cbWhite.Checked == false && cbPPWithLogo.Checked == false)
            {
                Alert("Please select at least one form and tray.");
                return output = false;
            }

            if (cbPink.Checked && cbPinkForm.SelectedIndex == 0)
            {
                Alert("Please select printer tray for Pre-Printed.");
                return output = false;
            }

            if (cbWhite.Checked && cbWhiteForm.SelectedIndex == 0)
            {
                Alert("Please select printer tray for A4.");
                return output = false;
            }


            if (cbPPWithLogo.Checked && cbPPWithLogoForm.SelectedIndex == 0)
            {
                Alert("Please select printer tray for Pre-Printed (With Logo).");
                return output = false;
            }


            if (cbPink.Checked && cbPinkForm.SelectedIndex != 0)
            {
                if(cbWhite.Checked || cbPPWithLogo.Checked)
                {
                    if((this.printerConfiguration.WhiteTray == this.printerConfiguration.PinkTray) ||
                    (this.printerConfiguration.WhiteTray == this.printerConfiguration.PPWithLogoTray)
                    || (this.printerConfiguration.PinkTray == this.printerConfiguration.PPWithLogoTray))
                    {
                        Console.WriteLine(this.printerConfiguration.WhiteTray + " " + this.printerConfiguration.PinkTray + " " + this.printerConfiguration.PPWithLogoTray);
                        Alert("Please select different printer tray for different paper type.");
                        return output = false;
                    }
                }

            }

            if (cbWhite.Checked && cbWhiteForm.SelectedIndex != 0)
            {
                if (cbPink.Checked || cbPPWithLogo.Checked)
                {
                    if ((this.printerConfiguration.WhiteTray == this.printerConfiguration.PinkTray) ||
                    (this.printerConfiguration.WhiteTray == this.printerConfiguration.PPWithLogoTray)
                    || (this.printerConfiguration.PinkTray == this.printerConfiguration.PPWithLogoTray))
                    {
                        Console.WriteLine(this.printerConfiguration.WhiteTray + " " + this.printerConfiguration.PinkTray + " " + this.printerConfiguration.PPWithLogoTray);
                        Alert("Please select different printer tray for different paper type.");
                        return output = false;
                    }
                }

            }

            if (cbPPWithLogo.Checked && cbPPWithLogoForm.SelectedIndex != 0)
            {
                if (cbPink.Checked || cbWhite.Checked)
                {
                    if ((this.printerConfiguration.WhiteTray == this.printerConfiguration.PinkTray) ||
                    (this.printerConfiguration.WhiteTray == this.printerConfiguration.PPWithLogoTray)
                    || (this.printerConfiguration.PinkTray == this.printerConfiguration.PPWithLogoTray))
                    {
                        Console.WriteLine(this.printerConfiguration.WhiteTray + " " + this.printerConfiguration.PinkTray + " " + this.printerConfiguration.PPWithLogoTray);
                        Alert("Please select different printer tray for different paper type.");
                        return output = false;
                    }
                }

            }

            return output;
        }

        //for closing application on Exit
        private void bntExit_Click(object sender, EventArgs e)
        {
            try
            {
                Settings1.Default.Printer_name = this.cbPrinter.Text;
                Settings1.Default.Resolution = this.cbResolution.Text;
                Settings1.Default.A4Checkbox = this.cbWhite.Checked;
                Settings1.Default.PPWithLogoCb = this.cbPPWithLogo.Checked;
                Settings1.Default.PinkCb = this.cbPink.Checked;
                Settings1.Default.PinkTray = this.cbPinkForm.SelectedIndex;
                Settings1.Default.WhiteTray = this.cbWhiteForm.SelectedIndex;
                Settings1.Default.PPWithLogotray = this.cbPPWithLogoForm.SelectedIndex;
                Settings1.Default.LoggedIn = true;
                Settings1.Default.Save();
                Application.Exit();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        #endregion

        #region Printer Control
        /// <summary>
        /// Initializes the printers
        /// </summary>
        private void InitPrinters() {
            try {
                ArrayList printerNames = GetPrinters();
                if (printerNames.Count == 0) {
                    Alert(SmarteCOPrintControl.Properties.Resources.ERR_CONFIG_NO_PRINTER);
                    this.Close();
                }
                this.cbPrinter.Items.Clear();
                if ((Settings1.Default.Printer_name !=null || Settings1.Default.Printer_name.ToString() != "") && Settings1.Default.LoggedIn)
                {
                    if (printerNames.Contains(Settings1.Default.Printer_name)) {
                        this.cbPrinter.Items.Add(Settings1.Default.Printer_name);
                        foreach (string printerName in printerNames)
                        {
                            if (Settings1.Default.Printer_name != printerName)
                                this.cbPrinter.Items.Add(printerName);
                        }
                    } else
                    {
                        foreach (string printerName in printerNames) this.cbPrinter.Items.Add(printerName);
                    }
                }else
                {
                    foreach (string printerName in printerNames)  this.cbPrinter.Items.Add(printerName);
                }
                this.cbPrinter.SelectedIndex = 0;
            } catch (Exception ex) {
                LogException(ex);
                Alert(SmarteCOPrintControl.Properties.Resources.ERR_CONFIG_NO_PRINTER);
            }
        }

        /// <summary>
        /// Get the list of valid printers
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPrinters()  {
            ArrayList printerNames = new ArrayList();
            try {
                foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters) {
                    Printer printer = new Printer(printerName);
                    //To remove PDF
                    if (printer.PaperSources.Count  < 1 || printer.Name.ToLower().Contains("pdf")) continue;
                    printerNames.Add(printerName);
                }
            } catch (Exception ex) {
                LogException(ex);
            }
            return printerNames;
        }

        /// <summary>
        /// Initialize the selection configuration for currCoPrinting
        /// </summary>
        private void InitPrintConfiguration()  {
            try {
                if (null == this.printerConfiguration) {
                    Alert(SmarteCOPrintControl.Properties.Resources.ERR_CONFIG_INVALID_PRINTER);
                    return;
                }

                //Clears the comboboxes for the printer trays of each print media
                this.cbWhiteForm.Items.Clear(); this.cbWhite.Enabled = this.cbWhiteForm.Enabled = false;
                this.cbPinkForm.Items.Clear(); this.cbPink.Enabled = this.cbPinkForm.Enabled = false;
                this.cbPPWithLogoForm.Items.Clear(); this.cbPPWithLogo.Enabled = this.cbPPWithLogoForm.Enabled = false;

                //At least 1 paper tray available for printers
                if (this.printerConfiguration.PaperSources.Count >= 1) {
                    //populate paper tray for A4/whiteform combobox
                    this.cbWhiteForm.Items.AddRange(this.printerConfiguration.PaperSources.ToArray());
                    if (Settings1.Default.WhiteTray > 0 && Settings1.Default.LoggedIn) {
                        this.printerConfiguration.WhiteTray = this.cbWhiteForm.SelectedIndex = Settings1.Default.WhiteTray;

                    } else {
                        //default to index 0
                        this.printerConfiguration.WhiteTray = this.cbWhiteForm.SelectedIndex = 0;
                    }

                    this.cbWhite.Enabled = true;

                    //populate tray for Pink form without logo
                    this.cbPinkForm.Items.AddRange(this.printerConfiguration.PaperSources.ToArray());
                    if (Settings1.Default.PinkTray > 0 && Settings1.Default.LoggedIn) {
                        this.printerConfiguration.PinkTray = this.cbPinkForm.SelectedIndex = Settings1.Default.PinkTray;

                    } else {
                        this.printerConfiguration.PinkTray = this.cbWhiteForm.SelectedIndex = 0;
                    }

                    this.cbPink.Enabled = true;

                    //populate tray for Pink form without logo
                    this.cbPPWithLogoForm.Items.AddRange(this.printerConfiguration.PaperSources.ToArray());
                    if (Settings1.Default.PPWithLogotray > 0 && Settings1.Default.LoggedIn) {
                        this.printerConfiguration.PPWithLogoTray = this.cbPPWithLogoForm.SelectedIndex = Settings1.Default.PPWithLogotray;

                    } else {
                        this.printerConfiguration.PPWithLogoTray = this.cbWhiteForm.SelectedIndex = 0;
                    }

                    //this.printerConfiguration.PPWithLogoTray = this.cbPPWithLogoForm.SelectedIndex = 0;
                    this.cbPPWithLogo.Enabled = true;
                }

                this.cbResolution.Items.Clear();
                if (this.printerConfiguration.Resolutions.Count >= 1) {
                    this.cbResolution.Items.AddRange(this.printerConfiguration.Resolutions.ToArray());
                    this.cbResolution.SelectedIndex = 0;
                }
            } catch (Exception ex) {
                LogException(ex);
            }

        }
        #endregion

        #region PDF Print
        /// <summary>
        /// Test print the CO
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="coDoc"></param>
        private void TestPrintCO(WebPrintRestProxy.FormType formType, CoDoc coDoc) {
            try {
                if (null == this.printerConfiguration) {
                    Alert(SmarteCOPrintControl.Properties.Resources.ERR_CONFIG_INVALID_CONFIG);
                    return;
                }
                if(this.cbPPWithLogo.Checked==true || this.cbPink.Checked == true )
                {
                    offsetText_check();
                }
                String tempFile = Path.GetTempFileName();
                Console.WriteLine("tempFile: " + tempFile);
                byte[] originPDF = System.Convert.FromBase64String(coDoc.orignal);
                File.WriteAllBytes(tempFile, originPDF);
                PrintJob printJob = new PrintJob(this.printerConfiguration.Name, tempFile);
                printJob.DocumentName = "Letter Portrait";
                if (printJob.Printer.Color)
                {
                    printJob.PrintOptions.Color = true;
                }
                    
                printJob.PrintOptions.Copies = 1;
                //settin paper size to A4 #Saurabh
                PaperSize paperSize = printJob.Printer.PaperSizes.A4;
                if (paperSize != null)
                {
                    printJob.PrintOptions.PaperSize = paperSize;
                }

                printJob.PrintOptions.HorizontalAlign = HorizontalAlign.Center;
                printJob.PrintOptions.Orientation.Type = OrientationType.Portrait;

                AutoPageScaling scaling = new AutoPageScaling(ScaleTo.PagePrintableArea, true, true);
                printJob.PrintOptions.Scaling = PageScaling.ActualSize;
                printJob.PrintOptions.Scaling = scaling;
                switch (formType) {
                    case WebPrintRestProxy.FormType.PINK_FORM:
                        if (-1 != this.printerConfiguration.PinkTray) printJob.PrintOptions.PaperSource = printJob.Printer.PaperSources[this.printerConfiguration.PinkTray];
                        break;

                    case WebPrintRestProxy.FormType.WHITE_FORM:
                        if (-1 != this.printerConfiguration.WhiteTray) printJob.PrintOptions.PaperSource = printJob.Printer.PaperSources[this.printerConfiguration.WhiteTray];
                        break;
                }
                printJob.PrintOptions.PrintAnnotations = false;
                printJob.PrintOptions.Resolution = printJob.Printer.Resolutions[this.printerConfiguration.Resolution];
                printJob.PrintOptions.VerticalAlign = VerticalAlign.Top;
                printJob.Starting += PrintJob_Starting;
                printJob.Succeeded += PrintJob_Succeeded;
                printJob.Failed += PrintJob_Failed;
                printJob.Updated += PrintJob_Updated;
                printJob.Print();
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        #endregion

        #region Printer Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintJob_Starting(object sender, PrintJobStartingEventArgs e) {
            Console.WriteLine("Printing Starting..." );
        }

        /// <summary>
        /// The PrintJob_Updated
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="PrintJobEventArgs"/></param>
        private void PrintJob_Updated(object sender, PrintJobEventArgs e)  {
            Console.WriteLine("Printing Page: " + e.PrintJob.PagesPrinted);
        }

        /// <summary>
        /// The PrintJob_Failed
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="PrintJobFailedEventArgs"/></param>
        private void PrintJob_Failed(object sender, PrintJobFailedEventArgs e)  {
            Console.WriteLine("Failed");
        }

        /// <summary>
        /// The PrintJob_Succeeded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="PrintJobEventArgs"/></param>
        private void PrintJob_Succeeded(object sender, PrintJobEventArgs e)   {
            Console.WriteLine("Succedded");
            if (bntPink.InvokeRequired) {
                bntPink.Invoke(new MethodInvoker(() => { UpdateControls(ConfigState.TEST_PRINT_COMPLETE); }));
                Console.WriteLine("Completed!!!");
            }
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
        //
        private bool offsetText_check()
        {
            bool flag = false;
            try
            {
                int value = 0;
                if (this.cbPPWithLogo.Checked)
                {
                    if (!int.TryParse(this.txtWithLogoOffset.Text, out value))
                    {
                        Alert("Please enter a numeric integer for offset");
                        this.txtWithLogoOffset.Focus();
                        flag = true;
                    }
                }
                if (this.cbPink.Checked)
                {
                    if (!int.TryParse(this.txtWithoutLogoOffset.Text, out value))
                    {
                        Alert("Please enter a numeric integer for offset");
                        this.txtWithoutLogoOffset.Focus();
                        flag = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return flag;
        }


        //Get offset Min and max from database
        protected async void GetOffsetConfig()
        {
            try
            {
                OffsetConfig =  await restProxy.GetOffsetConfigValueAsync();
                if (OffsetConfig != null)
                {
                    string[] config = OffsetConfig.Split(':');
                    this.minOffset = config[0];
                    if(this.minOffset==null && this.minOffset == "")
                    {
                        this.minOffset = "-30";
                    }
                    this.maxOffset = config[1];
                    if (this.minOffset == null && this.minOffset == "")
                    {
                        this.maxOffset = "4";
                    }
                }
                this.OffsetMinMaxLabel.Text = this.OffsetMinMaxLabel2.Text = "Min : " + this.minOffset + ", Max : " + this.maxOffset;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Dialogue box for saving the value of offset #Saurabh
        protected async void  DialogBox(FormType formType, String msg)
        {

            try
            {
                int MsgInt= 0;
                Int32.TryParse(msg, out MsgInt);
                int MinOffsetInt = 0;
                Int32.TryParse(this.minOffset, out MinOffsetInt);
                int MaxOffsetInt = 0;
                Int32.TryParse(this.maxOffset, out MaxOffsetInt);
                String formTypeString = formType.ToString();
                DialogResult dialogResult = MessageBox.Show("Margin offset = " + msg + "!"+"\n\nWould you like to save this value for offset?", "Message Box", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) { 

                    if (MsgInt >= MinOffsetInt && MsgInt <= MaxOffsetInt) {
                        //if the value is different from the one in db it will be changed in db
                        await restProxy.UpdateOffsetAsync(formTypeString, msg);

                    }else
                    {
                        Alert("Please choose offSet value in between Min : " + this.minOffset + ", Max : " + maxOffset);
                        flag = false;
                    }
                }
                if (dialogResult == DialogResult.No)
                {
                    //nothing will be changed
                    //Alert("offset not saved");
                }


            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// State control method
        /// </summary>
        /// <param name="configState"></param>
        private void UpdateControls(ConfigState configState) {
            try {
                switch (configState) {
                    case ConfigState.INIT:
                        this.cbPrinter.Enabled = true;
                        this.cbWhite.Enabled = false;
                        this.cbWhiteForm.Enabled = false;
                        this.bntWhite.Enabled = false;
                        this.cbPink.Enabled = false;
                        this.cbPinkForm.Enabled = false;
                        this.bntPink.Enabled = false;
                        this.bntPPWithLogo.Enabled = false;
                        this.cbPPWithLogo.Enabled = false;
                        this.cbPPWithLogoForm.Enabled = false;
                        break;

                    case ConfigState.SELECT_PRINTER:
                        this.cbPrinter.Enabled = true;
                        this.cbWhite.Enabled = true;
                        this.cbWhite.Checked = false;
                        this.cbWhiteForm.Enabled = false;
                        this.bntWhite.Enabled = false;
                        this.cbPink.Enabled = true;
                        this.cbPink.Checked = false;
                        this.cbPinkForm.Enabled = false;
                        this.bntPink.Enabled = false;
                        this.cbPPWithLogo.Enabled = true;
                        this.cbPPWithLogo.Checked = false;
                        this.cbPPWithLogoForm.Enabled = false;
                        this.bntPPWithLogo.Enabled = false;
                        break;

                    case ConfigState.SELECT_RESOLUTION:
                        break;

                    case ConfigState.CHECK_PINK:
                        this.cbPinkForm.Enabled = true;
                        this.bntPink.Enabled = true;
                        if (Settings1.Default.PinkTray > 0 && Settings1.Default.LoggedIn)
                        {
                            this.cbPinkForm.SelectedIndex =this.printerConfiguration.PinkTray= Settings1.Default.PinkTray;
                        }
                        else
                        {
                            this.cbPinkForm.SelectedIndex = this.printerConfiguration.PinkTray = 0;
                        }
                        //this.cbPPWithLogo.Checked = false;
                        //this.cbWhite.Checked = false;
                        //this.cbPPWithLogoForm.SelectedIndex = 0;
                        //this.cbWhiteForm.SelectedIndex = 0;
                        break;

                    case ConfigState.UNCHECK_PINK:
                        this.cbPinkForm.Enabled = false;
                        this.cbPinkForm.SelectedIndex = this.printerConfiguration.PinkTray= 0;
                        this.bntPink.Enabled = false;
                        this.txtWithoutLogoOffset.Text = "";
                        break;


                    case ConfigState.SELECT_TRAY_PINK:
                        break;

                    case ConfigState.CHECK_WHITE:
                        this.cbWhiteForm.Enabled = true;
                        if (Settings1.Default.WhiteTray > 0 && Settings1.Default.LoggedIn)
                        {
                            this.cbWhiteForm.SelectedIndex = this.printerConfiguration.WhiteTray = Settings1.Default.WhiteTray;
                        }
                        else
                            this.cbWhiteForm.SelectedIndex = this.printerConfiguration.WhiteTray = 0;
                        this.bntWhite.Enabled = true;
                        //this.cbPPWithLogo.Checked = false;
                        //this.cbPink.Checked = false;
                        //this.cbPPWithLogoForm.SelectedIndex = 0;
                        //this.cbPinkForm.SelectedIndex = 0;
                        break;

                    case ConfigState.UNCHECK_WHITE:
                        this.cbWhiteForm.Enabled = false;
                        this.cbWhiteForm.SelectedIndex = this.printerConfiguration.WhiteTray = 0;
                        this.bntWhite.Enabled = false;
                        break;

                    case ConfigState.SELECT_TRAY_WHITE:
                        break;


                    case ConfigState.CHECK_PPWITHLOGO:
                        this.cbPPWithLogoForm.Enabled = true;
                        if (Settings1.Default.PPWithLogotray > 0 && Settings1.Default.LoggedIn)
                        {
                            this.cbPPWithLogoForm.SelectedIndex=this.printerConfiguration.PPWithLogoTray = Settings1.Default.PPWithLogotray;
                        }
                        else
                            this.cbPPWithLogoForm.SelectedIndex = this.printerConfiguration.PPWithLogoTray = 0;
                        this.bntPPWithLogo.Enabled = true;
                        //this.cbWhite.Checked = false;
                        //this.cbPink.Checked = false;
                        //this.cbWhiteForm.SelectedIndex = 0;
                        //this.cbPinkForm.SelectedIndex = 0;
                        break;

                    case ConfigState.UNCHECK_PPWITHLOGO:
                        this.cbPPWithLogoForm.Enabled = false;
                        this.cbPPWithLogoForm.SelectedIndex = this.printerConfiguration.PPWithLogoTray = 0;
                        this.bntPPWithLogo.Enabled = false;
                        this.txtWithLogoOffset.Text = "";
                        break;

                    case ConfigState.SELECT_TRAY_PPWITHLOGO:
                        break;



                    case ConfigState.TEST_PRINT_WHITE:
                        if (cbPink.Checked) {
                            this.cbPinkForm.Enabled = false;
                            this.bntPink.Enabled = false;
                        }
                        this.cbPinkForm.Enabled = false;
                        this.bntPink.Enabled = false;

                        if (cbPPWithLogo.Checked)
                        {
                            this.cbPPWithLogoForm.Enabled = false;
                            this.bntPPWithLogo.Enabled = false;
                        }
                        this.cbPPWithLogoForm.Enabled = false;
                        this.bntPPWithLogo.Enabled = false;

                        break;

                    case ConfigState.TEST_PRINT_PINK:
                        if (cbWhite.Checked) {
                            this.cbWhiteForm.Enabled = false;
                            this.bntWhite.Enabled = false;
                        }
                        this.cbWhiteForm.Enabled = false;
                        this.bntWhite.Enabled = false;

                        if (cbPPWithLogo.Checked)
                        {
                            this.cbPPWithLogoForm.Enabled = false;
                            this.bntPPWithLogo.Enabled = false;
                        }
                        this.cbPPWithLogoForm.Enabled = false;
                        this.bntPPWithLogo.Enabled = false;
                        
                        break;

                    case ConfigState.TEST_PRINT_PP_WITHLOGO:
                        if (cbWhite.Checked)
                        {
                            this.cbWhiteForm.Enabled = false;
                            this.bntWhite.Enabled = false;
                        }
                        this.cbWhiteForm.Enabled = false;
                        this.bntWhite.Enabled = false;

                        if (cbPink.Checked)
                        {
                            this.cbPinkForm.Enabled = false;
                            this.bntPink.Enabled = false;
                        }
                        this.cbPinkForm.Enabled = false;
                        this.bntPink.Enabled = false;
                        break;

                    case ConfigState.TEST_PRINT_COMPLETE:
                        this.bntPink.Enabled = true;
                        this.bntWhite.Enabled = true;
                        this.bntPPWithLogo.Enabled = true;
                        break;
                }
            } catch (Exception ex) {
                LogException(ex);
            }
        }

        public void ResetSetting()
        {
            Settings1.Default.Printer_name = null;
            Settings1.Default.Resolution = null;
            Settings1.Default.A4Checkbox = false;
            Settings1.Default.PPWithLogoCb = false;
            Settings1.Default.PinkCb = false;
            Settings1.Default.PinkTray = 0;
            Settings1.Default.WhiteTray = 0;
            Settings1.Default.PPWithLogotray = 0;
            Settings1.Default.LoggedIn = false ;
        }
        #endregion
    }

}
