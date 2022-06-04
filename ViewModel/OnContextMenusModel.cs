using Note.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModel
{
    internal class OnContextMenusModel : MainViewModel
    {
        private string _Title = "Редактор названий полей блокнота";

        public string Title
        {
            get { return _Title; }
            set => Set(ref _Title, value);
        }

    }
}
