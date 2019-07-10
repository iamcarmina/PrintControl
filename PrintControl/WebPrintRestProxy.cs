using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;

namespace SmarteCOPrintControl
{
    public class WebPrintRestProxy {

        #region Cosntants
        private const String GENERIC_ERROR = "-1";
        private const String CREDENTIALS_ERROR = "-2";
        private const String BLANK = "";
        public enum FormType { WHITE_FORM, PINK_FORM, PP_WITH_LOGO };
        #endregion

        #region Attributes
        private String sessionId;
        #endregion

        #region Constructor
        public WebPrintRestProxy(String sessionId) {
            this.sessionId = sessionId;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Invoke login to the WebPrint Proxy Server
        /// </summary>
        /// <param name="credentails"></param>
        /// <returns></returns>
        public async Task<string> DoLoginAsync(String credentails) {
            String sessionId = BLANK;
            try {
                String urlApi = Properties.Settings.Default.Host + "/print/co/login";
                HttpClient client = new HttpClient();
                String based64Credentials = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(credentails));
                Console.WriteLine("based64Credentials: " + based64Credentials);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", based64Credentials);
                HttpResponseMessage response = await client.GetAsync(urlApi);
                if (response.IsSuccessStatusCode) {
                    sessionId = await response.Content.ReadAsStringAsync();
                }
            } catch (Exception ex) {
                sessionId = GENERIC_ERROR;
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("sessionId: " + sessionId);
            return sessionId;
        }


        /// <summary>
        /// Test Print CO
        /// </summary>
        /// <param name="formType"></param>
        /// <returns></returns>
        public async Task<CoDoc> DoCOTestAsync(FormType formType, String offset) {
            CoDoc coDoc = new CoDoc();
            String content = "";
            try {
                String urlApi = Properties.Settings.Default.Host + "/print/co/test/" + formType.ToString() + "/" + offset;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                HttpResponseMessage response = await client.GetAsync(urlApi);
                if (response.IsSuccessStatusCode) {
                    content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("content: " + content);
                }
                coDoc = JsonConvert.DeserializeObject<CoDoc>(content);
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            Console.WriteLine("cDoc: " + coDoc.ToString());
            return coDoc;
        }


        public async Task<String> GetUserPrintOffsetAsync(String formType)
        {
            String content = "";
            try
            {
                String urlApi = Properties.Settings.Default.Host + "/print/co/printOffset/"+ formType;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                HttpResponseMessage response = await client.GetAsync(urlApi);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("content: " + content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            //Console.WriteLine("cDoc: " + coDoc.ToString());
            return content;
        }

        
        /**  
         * <summary>
         *  Calls {context}/co/list/{printMediaSelected} to retrieve the lists of COs based on the 
         *  user session and printMedia selection.
         *  </summary> 
         *  
         *  <param name="SelectedPrintMedia">
         *      Selected print media in comma-separated list.
         *  </param>
         *  
         *  <returns>Lists of doc nos.</returns>
         */
        public async Task<ArrayList> DoCOListAsync(String selectedPrintMedia) {
            Console.WriteLine("DoCOListAsync is called...");

            ArrayList docCONos = new ArrayList();
            String content = "";
            try {
                String urlApi = Properties.Settings.Default.Host + "/print/co/list/"+ selectedPrintMedia;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                HttpResponseMessage response = await client.GetAsync(urlApi);
                if (response.IsSuccessStatusCode) {
                    content = await response.Content.ReadAsStringAsync();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace.ToString());
            }

            Console.WriteLine("DoCOListAsync response content: " + content);
            docCONos = JsonConvert.DeserializeObject<ArrayList>(content);
            return docCONos;
        }

        /** 
         * <summary>
         *      Calls webservice API {context}/co/print/{coDocNo}. 
         * </summary>
         * <param name="coDocNo"> CO doc no.</param>
         * <returns>CoDoc details </returns>
        */
        public async Task<CoDoc> getCoDetailsAsync(String coDocNo) {
            CoDoc coDoc = new CoDoc();
            String content = "";
            try {
                String urlApi = Properties.Settings.Default.Host + "/print/co/details/" + coDocNo;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                HttpResponseMessage response = await client.GetAsync(urlApi);
                
                if (response.IsSuccessStatusCode) {
                    content = await response.Content.ReadAsStringAsync();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace.ToString());
            }

            Console.WriteLine("Awaited task has completed... printing content...");
            Console.WriteLine("content: " + content);

            try
            {
                coDoc = JsonConvert.DeserializeObject<CoDoc>(content);
            }
            catch(Exception e)
            {
                Console.WriteLine("EXCEPTION IN DESERLIAZING.... ");
                e.StackTrace.ToString();
            }
            

            Console.WriteLine("Afer deserializing... " + coDoc.ToString());
            return coDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coDoc"></param>
        /// <returns></returns>
        public async Task<CoDoc> DoCOUpdateAsync(CoDoc coDoc) {
            String content = "";
            try {
                String urlApi = Properties.Settings.Default.Host + "/print/co/update";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                String json = JsonConvert.SerializeObject(coDoc);
                Console.WriteLine("json: " + json);
                HttpContent postContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlApi, postContent);
                if (response.IsSuccessStatusCode) {
                    content = await response.Content.ReadAsStringAsync();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            Console.WriteLine("content: " + content);
            coDoc = JsonConvert.DeserializeObject<CoDoc>(content);
            Console.WriteLine(coDoc.ToString());
            return coDoc;
        }

        //updating offset in db
        public async Task<String> UpdateOffsetAsync(String formTypeString, String offset)
        {
            String content = "";
            try
            {
                String urlApi = Properties.Settings.Default.Host + "/print/co/updateOffset/" + formTypeString + "/" + offset;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                String json = offset;
                Console.WriteLine("json: " + json);
                HttpContent postContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlApi, postContent);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            Console.WriteLine("content: " + content);
            return content;
        }

        //get OffsetConfig from DB 
        public async Task<String> GetOffsetConfigValueAsync()
        {
            String content = "";
            try
            {
                String urlApi = Properties.Settings.Default.Host + "/print/co/offsetConfig/";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.sessionId);
                HttpResponseMessage response = await client.GetAsync(urlApi);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("content: " + content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
            Console.WriteLine("content: " + content);
            return content;
        }
    }

    #endregion
}
