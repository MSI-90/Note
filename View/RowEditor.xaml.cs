using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Note.View
{
    public partial class RowEditor : Window
    {
        public string editText { get; set; } = "Доступные для редактирования поля";

        private ObservableCollection<string> rowContent;
        public ObservableCollection<string> RowContent
        {
            get { return rowContent; }
            set => Set(ref rowContent, value);
        }

        private List<string> dataColumns;
        public List<string> DataColumns
        {
            get { return dataColumns; }
            set => Set(ref dataColumns, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public RowEditor(ObservableCollection<string> list, List<string> dataCol)
        {   
            this.rowContent = list;
            this.dataColumns = dataCol;
            InitializeComponent();

            DataContext = this;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = cb.SelectedItem.ToString();
            string ss;
            int i = 0;
            foreach (var item in rowContent)
            {
                if (item.Equals(s))
                {
                    ss = item.ToString();
                    i = rowContent.IndexOf(ss);
                }
            }
            rowContent[i] = tb.Text;
            
            cb.ItemsSource = rowContent;
            foreach(var item in rowContent)
            {
                MessageBox.Show(item.ToString());
            }
        }
    }
}
