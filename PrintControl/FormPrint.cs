using ceTe.DynamicPDF.Printing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmarteCOPrintControl
{
    public partial class FormPrint : Form {

        #region Constants
        private enum PrintType { ORIGINAL, COPY};
        #endregion

        #region Attributes
        private String sessionId;
        private WebPrintRestProxy restProxy;
        private PrinterConfiguration printerConfiguration;
        private static EventWaitHandle waitHandle;
        private System.Windows.Forms.Timer timer;
        private FormConfig config;

        public ArrayList printCompletedCo = new ArrayList();
        
        //Holds the  value that is currently currCoPrinting
        private String currCoPrinting = null;
       
        // private CoDoc coDoc = null;
        private bool isPrintInitiated = false;
        private bool isPrintSuccess = false;

        
        #endregion

        #region Constructor
        public FormPrint(String sessionId,  PrinterConfiguration printerConfiguration) {
            try {
                InitializeComponent();
                this.sessionId = sessionId;
                this.printerConfiguration = printerConfiguration;
                this.restProxy = new WebPrintRestProxy(sessionId);
                this.isPrintInitiated = false;
                this.lblCurrentUser.Text = Properties.Settings.Default.UserUid;
                this.lblLastLogin.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                waitHandle = new AutoResetEvent(false);
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 5000;
                timer.Tick += Timer_TickAsync;
                timer.Start();
            } catch (Exception ex) {
                LogException(ex);
            }
        }
        #endregion

        #region Event Handler

        //for closing application on Exit
        private void bntClose_Click(object sender, EventArgs e)
        {
            try
            {
                timer.Dispose();
                this.Hide();
                config = new FormConfig(sessionId);
                config.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        #endregion

        #region Timer Handler
        private async void Timer_TickAsync(object sender, EventArgs e) {
            try {

                Console.WriteLine("Timer_TickAsync");
                //If print process is currently running? 
                if (this.isPrintInitiated)
                {
                    return;
                }

                //May return null
                //String coDocNo = await GetCOListingAsync();
                String coDoc = await GetCOListingAsync();
                
                //if Printing has already started
                if (coDoc != currCoPrinting)
                {
                    currCoPrinting = coDoc; 
                   
                }

                if (!String.IsNullOrEmpty(coDoc))
                {
                    ExecutePrintAsync(coDoc);
                }

                //If coDocNo is null it won't execute below
                /*if (!String.IsNullOrEmpty(coDocNo))
                {
                    //If currCoPrinting is empty and it's not equal to the coDocNo retrieved from DoList()
                    if (String.IsNullOrEmpty(currCoPrinting) && currCoPrinting != coDocNo)
                    {
                        currCoPrinting = coDocNo;
                    }
                    
                    //If currCoPrinting now has value, then DoCoPrint
                    if(!String.IsNullOrEmpty(currCoPrinting))
                    {
                        //Do the print for coDocNo returned from DoList()
                        ExecutePrintAsync(coDocNo);
                    }
                }*/
                
               
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Processing Methods
        /** 
         * <summary>
         *      Does the call to web service API for listing all the CO's that are in Printing state and the print media is 
         *      one of the selected printMedias.
         * </summary>
         * <returns>The next CO to be printed or null if CO list is empty</returns>
         */
        
        private async Task<String> GetCOListingAsync()
        {
            try {

                //Retrieves the lists of COs based on the print media selected.
                ArrayList coDocNosList = await restProxy.DoCOListAsync (this.printerConfiguration.PrintMediaSelected);
                //this.lstCOQueue.Items.Clear();
                CodataGridView.Rows.Clear();
                CodataGridView.Refresh();

                //Make sure there is coDocNosList retrieved
                if (coDocNosList != null && coDocNosList.Count > 0)
                {
                    //populate the data grid with the lists of COs retrieved and sets the "currCoPrinting"

                    //check if the currCOPrinting is in the coDocNosList and remove it
                   PopulateDataGridQueueCO(coDocNosList);
                   
                    Console.WriteLine("Current CO for printing is " + currCoPrinting);

                    //If currCoPrinting has value?
                    //if (!String.IsNullOrEmpty(currCoPrinting))
                    //{
                        //if coDocNosList size is zero or it contains texts similar to currCoPrinting? 
                    //    if (coDocNosList.Contains(currCoPrinting))
                    //    {
                            //If isPrintInitiated is false and isPrintSuccess is true
                    //        if (!isPrintInitiated && isPrintSuccess)
                    //        {
                    //            printCompletedCo.Add(currCoPrinting);
                    //            PopulateDataGridCompletedCO(printCompletedCo);
                    //        }
                            
                    //    }
                    //}

                    /* if (!(coDocNos.Contains(currCoPrinting)) || coDocNos.Count == 0)
                    {
                        if (currCoPrinting != null)
                        {
                            printCompletedCo.Add(currCoPrinting);
                            PopulateDataGridCompletedCO(printCompletedCo);
                        }

                    }*/

                   return currCoPrinting;

                }
                //else if(coDocNosList.Count == 0 && String.IsNullOrEmpty(currCoPrinting))
                //{
                //    currCoPrinting = null;
                //    Alert("Please select document(s) for currCoPrinting via SmarteCO Portal.");
                // }
                //else if(coDocNosList.Count == 0 && !String.IsNullOrEmpty(currCoPrinting))
                //else
                //{
                //    currCoPrinting = null;
                //    timer.Stop();
                //    Alert("Please collect printed documents from printer.");
                //}

              
            } catch (Exception ex) {
                LogException(ex);
            }

            return null;
        }

        
        private void PopulateDataGridCompletedCO(ArrayList coDoc)
        {
            Console.WriteLine("PopulateDataGridCompletedCO called");
            //CodataGridView.

            PrintedCOdataGrid.Rows.Clear();
            PrintedCOdataGrid.Refresh();
            if (coDoc.Count == 0) return;
            foreach (String doc in coDoc)
            {
                char[] sep = { ':' };
                String[] arrcoDocNo = doc.Split(sep);

                //Search in the  CodataGridView and invalidate the row
                //Removed below 
                foreach (DataGridViewRow row in CodataGridView.Rows)
                {
                    Console.WriteLine("Removing the row for " + arrcoDocNo);
                    if (row.Cells[0].Value.ToString().Equals(arrcoDocNo[0]))
                    {
                        Console.WriteLine("Found yah!");
                        //int rowIndex = row.Index;
                        CodataGridView.Rows.Remove(row);
                        CodataGridView.Refresh();
                        break;
                    }
                }

                int n = PrintedCOdataGrid.Rows.Add();
                PrintedCOdataGrid.Rows[n].Cells[0].Value = arrcoDocNo[0];
                PrintedCOdataGrid.Rows[n].Cells[1].Value = arrcoDocNo[1];
                PrintedCOdataGrid.Rows[n].Cells[2].Value = arrcoDocNo[2];
                PrintedCOdataGrid.Rows[n].Cells[3].Value = arrcoDocNo[3];
                PrintedCOdataGrid.Rows[n].Cells[4].Value = arrcoDocNo[4];
            }

        }

        /** Populates the left part of the print queue and sets "currCoPrinting".*/
        private void PopulateDataGridQueueCO(ArrayList coDocList) {
            if (coDocList.Count <= 0)
                return;

            foreach(String doc in coDocList) {

                if (String.IsNullOrEmpty(currCoPrinting))
                {
                    currCoPrinting = doc;
                }

                char[] sep = { ':' };
                String[] arrcoDocNo = doc.Split(sep);

                int n = CodataGridView.Rows.Add();
                //If it's a request the cell[0] is the requestNo, otherwise it's the docNo
                CodataGridView.Rows[n].Cells[0].Value = arrcoDocNo[0];
                CodataGridView.Rows[n].Cells[1].Value = arrcoDocNo[1];

                //For remarks
                CodataGridView.Rows[n].Cells[2].Value = arrcoDocNo[4];
            }
        }

        /**
         * <summary>
         *      Performs the currCoPrinting of specific coDocNo by calling DocCoPrint method that calls to webservice.<br></br>
         *      It splits the coDocNo passed by colon.
         * </summary>
         * 
         * <param name="coDocNo">CO docNo and cert no. concatenated by colon (:)</param>
         */
        private async void ExecutePrintAsync(String coDocNo) {
            try {
               
                
                char[] sep = { ':' };
                if (null != coDocNo && coDocNo.Length > 0)
                {
                    String[] arrcoDocNo = coDocNo.Split(sep);
                    String coDocNoSplit = arrcoDocNo[0];

                    Console.WriteLine("ExecutePrintAsync called for " + coDocNoSplit);

                    //set the bProcess to true for what?
                    isPrintInitiated = true;

                    //Calls the getCoDetailsAsync from server
                    CoDoc coDoc = await restProxy.getCoDetailsAsync(coDocNoSplit);
                    
                    if (null != coDoc)
                    {
                        Console.WriteLine("CODoc found: " + coDoc.ToString());
                        PrintType printType = PrintType.COPY;

                        //if errCode is 0, no issue?
                        if (coDoc.errCode.Equals("0"))
                        {
                           // isPrintSuccess = false;
                            //if nothing has been printed yet and coOriginal is greater than 0 or = 1, set the printType to ORIGINAL
                            if ((coDoc.coDBPrintedCopies == 0 && coDoc.coOriginal > 0) || coDoc.docReqtype.Equals("CO_TESTPRINT"))
                            {
                                printType = PrintType.ORIGINAL;
                            }
                                
                            //Do the actual currCoPrinting
                            SendCOToPrinter(printType, coDoc);
                            CoDoc responseCO = null;
                            
                            //Waits for the current task to finish 
                            waitHandle.WaitOne();

                            if (isPrintSuccess)
                            {
                                Console.WriteLine("Printed successfully....");
                                coDoc.coPrintedCopies = coDoc.coDBPrintedCopies + 1;
                                Console.WriteLine("coDoc.coDBPrintedCopies  " + coDoc.coDBPrintedCopies);
                                responseCO = await restProxy.DoCOUpdateAsync(coDoc);
                                if(responseCO.errCode != "0")
                                {
                                    Console.WriteLine("coDoc.errCode: " + responseCO.errCode);
                                    Alert("Error occured while updating CO record. Error Code "+ responseCO.errCode);
                                    //stop timing and notify User to call Support and inform the error received

                                    //need to check if we can do this or not
                                    //LogException(new Exception("Non Zero Error code received from the Server. Error COde + "+ responseCO.errCode));
                                }

                                //only move the to completed listing if values below are equal
                                if((coDoc.coPrintedCopies == (coDoc.coOriginal + coDoc.coCopies)) || (coDoc.docReqtype.Equals("CO_TESTPRINT") || coDoc.docReqtype.Equals("CO_CHAMBER_COPY")))
                                {
                                    //move to the other side
                                    printCompletedCo.Add(coDocNo);
                                    PopulateDataGridCompletedCO(printCompletedCo);
                                    currCoPrinting = null;
                                }
                               


                            } else
                            {
                                //just making sure it's really set to false
                                isPrintSuccess = false;
                                
                                Console.WriteLine("Printed fail....");
                            }
                        } else {
                            Console.WriteLine("coDoc.errCode: " + coDoc.errCode);
                        }
                    }
                }
            } catch (Exception ex) {
                LogException(ex);
            }

            isPrintInitiated = false;
        }
        #endregion

        #region Print CO
        /**
         * <summary>
         *      Executes the actual currCoPrinting.
         * </summary>
         * <param name="printType">Can be ORIGINAL or COPY</param>
         * <param name="coDoc">Contains all related details of the CO</param>
        */
        private void SendCOToPrinter(PrintType printType, CoDoc coDoc) {
            try {
                
                //If no printerConfiguration object is found.
                if (null == this.printerConfiguration) {
                    return;
                }

                //TestPrintJob_Succeeded();
                String tempFile = Path.GetTempFileName();
                byte[] pdfContent = null;
                switch (printType)
                {
                    case PrintType.ORIGINAL:
                        pdfContent = System.Convert.FromBase64String(coDoc.orignal);
                        break;

                    case PrintType.COPY:
                        pdfContent = System.Convert.FromBase64String(coDoc.copy);
                        break;
                }

                File.WriteAllBytes(tempFile, pdfContent);
                PrintJob printJob = new PrintJob(this.printerConfiguration.Name, tempFile);
                printJob.DocumentName = "Letter Portrait";
                
                //If printer is colored
                if (printJob.Printer.Color)
                {
                    printJob.PrintOptions.Color = true;
                }
                
                printJob.PrintOptions.Copies = 1;
                //?
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
                switch (coDoc.coMediaType) {
                    case "PrePrinted":
                        if (0 != this.printerConfiguration.PinkTray) printJob.PrintOptions.PaperSource = printJob.Printer.PaperSources[this.printerConfiguration.PinkTray];
                        break;

                    case "A4":
                        if (0 != this.printerConfiguration.WhiteTray) printJob.PrintOptions.PaperSource = printJob.Printer.PaperSources[this.printerConfiguration.WhiteTray];
                        break;

                    case "PrePrintedWithLogo":
                        if (0 != this.printerConfiguration.PPWithLogoTray) printJob.PrintOptions.PaperSource = printJob.Printer.PaperSources[this.printerConfiguration.PPWithLogoTray];
                        break;
                }
                printJob.PrintOptions.PrintAnnotations = false;
                printJob.PrintOptions.Resolution = printJob.Printer.Resolutions[this.printerConfiguration.Resolution];
                printJob.PrintOptions.VerticalAlign = VerticalAlign.Top;
                printJob.Succeeded += PrintJob_Succeeded;
                printJob.Failed += PrintJob_Failed;
                printJob.Updated += PrintJob_Updated;
                //code to counter the non select print media type. to stop them currCoPrinting from random tray
                if(printJob.PrintOptions.PaperSource.Name == "Automatically Select")
                {
                    throw new Exception("Please select printer tray for media type" + coDoc.coMediaType);
                }

                //TestPrintJob_Succeeded();
               printJob.Print();
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// When page is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintJob_Updated(object sender, PrintJobEventArgs e) {
            Console.WriteLine("PrintJob_Updated");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintJob_Failed(object sender, PrintJobFailedEventArgs e) {
            Console.WriteLine("PrintJob_Failed");
            isPrintSuccess = false;
            waitHandle.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintJob_Succeeded(object sender, PrintJobEventArgs e) {
            Console.WriteLine("PrintJob_Succeeded");
            isPrintSuccess = true;
            waitHandle.Set();
        }

        private void TestPrintJob_Succeeded()
        {
            Console.WriteLine("PrintJob_Succeeded");
            isPrintSuccess = true;
            waitHandle.Set();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        protected void Alert(String msg) {
            try {
                if (null != msg && msg.Length > 0 && msg.Contains("Please select document for currCoPrinting."))
                {
                    timer.Dispose();
                    MessageBox.Show(msg, Properties.Resources.FORM_ALERT_TITLE);
                    //if no document selected for currCoPrinting redirecting user to form config page
                    this.Hide();
                    config = new FormConfig(sessionId);
                    config.ShowDialog(this);
                }else if (null != msg && msg.Length > 0 && msg.Contains("Error occured while updating CO record."))
                {
                    timer.Dispose();
                    MessageBox.Show(msg, Properties.Resources.FORM_ALERT_TITLE);
                    //if no document selected for currCoPrinting redirecting user to form config page
                    
                    this.Show();
                }
                else
                {
                    MessageBox.Show(msg, Properties.Resources.FORM_ALERT_TITLE);
                    this.Show();
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="ex"></param>
        protected void LogException(Exception ex) {

            if(ex.Message.Contains( "No Printer tray selected for the Print media type"))
            {
                timer.Dispose();
                this.Hide();
                Console.WriteLine(ex.StackTrace);
                FormException form = new FormException(ex);
                form.ShowDialog();
                config = new FormConfig(sessionId);
                config.ShowDialog(this);
            }else if (ex.Message == "Non Zero Error code received from the Server. Error COde ")
            {
                timer.Dispose();
                Console.WriteLine(ex.StackTrace);
                FormException form = new FormException(ex);
                form.ShowDialog();
            }

        }

        #endregion

        private void BntHide_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
