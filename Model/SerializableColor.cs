using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace Note.Model
{   
    [Serializable]
    public class SerializableColor
    {
        public Color colorProp { get; set; }
    }
}
