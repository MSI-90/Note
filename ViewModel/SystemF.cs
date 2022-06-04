using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Note.ViewModel
{
    internal class SystemF
    {
        internal double SystemWidth 
        {
            get 
            {
                return System.Windows.SystemParameters.PrimaryScreenWidth;
            } 
        }
        internal double SystemHeight 
        {
            get 
            {
                return System.Windows.SystemParameters.PrimaryScreenHeight;
            }
        }
    }
}
