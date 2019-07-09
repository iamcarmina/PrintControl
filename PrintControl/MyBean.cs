using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintControl {
    public class MyBean {
        #region Attributes
        private String name;
        private String address;
        #endregion

        #region Properties
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        #endregion

    }
}
