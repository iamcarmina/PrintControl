using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarteCOPrintControl
{
    public class PrinterConfiguration  {

        // Attributes
        /////////////
        private String name;
        private ArrayList paperSources;
        private ArrayList resolutions;
        private int whiteTray;
        private int pinkTray;
        private int ppWithLogoTray;
        private int resolution;
        private String printMediaSelected;

        /// <summary>
        /// Constructor
        /// </summary>
        public PrinterConfiguration()  {
            paperSources = new ArrayList();
            resolutions = new ArrayList();
            this.whiteTray =  this.pinkTray = this.ppWithLogoTray = this.resolution = -1;
        }

        #region Override Methods
        /// <summary>
        /// Return content of the instance in string format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "PrinterConfiguration [name=" + name + ", paperSources=" + paperSources + ", resolutions=" + resolutions
                    + ", whiteTray=" + whiteTray + ", pinkTray=" + pinkTray + ", resolution=" + resolution + "]";
        }
        #endregion

        // Properties
        //////////////
        public string Name { get => name; set => name = value; }
        public ArrayList PaperSources { get => paperSources; set => paperSources = value; }
        public ArrayList Resolutions { get => resolutions; set => resolutions = value; }
        public int WhiteTray { get => whiteTray; set => whiteTray = value; }
        public int PinkTray { get => pinkTray; set => pinkTray = value; }
        public int PPWithLogoTray { get => ppWithLogoTray; set => ppWithLogoTray = value; }
        public int Resolution { get => resolution; set => resolution = value; }
        public string PrintMediaSelected { get => printMediaSelected; set => printMediaSelected = value; }

    }
}
