using Note.Model;
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
        DataOutput data;
        public string editText { get; set; } = "Доступные для редактирования поля";
        public int index { get; set; }

        private ObservableCollection<object> rowContent;
        public ObservableCollection<object> RowContent
        {
            get { return rowContent; }
            set => Set(ref rowContent, value);
        }

        private List<object> dataColumns;
        public List<object> DataColumns
        {
            get { return dataColumns; }
            set => Set(ref dataColumns, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public RowEditor(ObservableCollection<object> list, List<object> dataCol)
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

        private void NoteUpdate(string dataTextForUpdate)
        {
            string dataBaseName = (Owner as MainWindow).insertIntoPath;

            data = new DataOutput();
            data.dbFile = dataBaseName;

            int _id = Convert.ToInt32(rowContent[0]);

            data.ReadData();
            data.UpdateFields(_id, dataTextForUpdate, this.index);

            (this.Owner as MainWindow).mainDG();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            string s = cb.SelectedItem.ToString();

            string ss;

            if (tb.Text.Equals("")) MessageBox.Show("Вы не указали новое значение выбранного выше поля данных");
            else
            {
                foreach (var item in rowContent)
                {
                    if (item.Equals(s))
                    {
                        ss = item.ToString();
                        this.index = rowContent.IndexOf(ss);
                    }
                }
                rowContent[this.index] = tb.Text;

                string dataForUpdate = tb.Text;

                NoteUpdate(dataForUpdate);

                cb.ItemsSource = rowContent;

            }
            
        }
    }
}
