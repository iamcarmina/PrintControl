using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarteCOPrintControl
{
    public class CoDoc
    {
        [Newtonsoft.Json.JsonProperty("docReqtype")]
        public String docReqtype { get; set; }

        [Newtonsoft.Json.JsonProperty("coDocno")]
        public String coDocno { get; set; }

        [Newtonsoft.Json.JsonProperty("coDocRefNo")]
        public String coDocRefNo { get; set; }

        [Newtonsoft.Json.JsonProperty("coPrintedBy")]
        public String coPrintedBy { get; set; }

        [Newtonsoft.Json.JsonProperty("coMediaType")]
        public String coMediaType { get; set; }

        [Newtonsoft.Json.JsonProperty("coOriginal")]
        public Int32 coOriginal { get; set; }

        [Newtonsoft.Json.JsonProperty("coCopies")]
        public Int32 coCopies { get; set; }

        [Newtonsoft.Json.JsonProperty("coDBPrintedCopies")]
        public Int32 coDBPrintedCopies { get; set; }

        [Newtonsoft.Json.JsonProperty("coPrintedCopies")]
        public Int32 coPrintedCopies { get; set; }

        [Newtonsoft.Json.JsonProperty("orignal")]
        public String orignal { get; set; }

        [Newtonsoft.Json.JsonProperty("copy")]
        public String copy { get; set; }

        [Newtonsoft.Json.JsonProperty("errCode")]
        public String errCode { get; set; }



        public override string ToString() {
            return "CoDoc [docReqtype=" + docReqtype + ", coDocno=" + coDocno + ", coPrintedBy=" + coPrintedBy + ", coMediaType=" + coMediaType
                 + ", coOriginal=" + coOriginal + ", coCopies=" + coCopies + ", coDBPrintedCopies=" + coDBPrintedCopies
                 + ", coPrintedCopies=" + coPrintedCopies + ", orignal=" +  (orignal.Length > 10 ? orignal.Substring(0,10)+"..." : "") + ", copy=" + (copy.Length > 10 ? copy.Substring(0,10)+"..." : "")+ ", errCode="
                 + errCode + "]";
        }

    }
}