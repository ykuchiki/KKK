using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KKK.Common
{


    public partial class FontSerializeUtils
    {
        [XmlIgnore]
        public Font MyFont
        {
            get; set;
        }

        public string MyFontText
        {
            get
            {
                return TypeDescriptor.GetConverter(typeof(Font)).ConvertToString(MyFont);
            }
            set
            {
                MyFont = (Font)TypeDescriptor.GetConverter(typeof(Font)).ConvertFromString(value);
            }
        }
    }
}
