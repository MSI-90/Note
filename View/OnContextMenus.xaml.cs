using Note.Model;
using Note.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// <summary>
    /// Логика взаимодействия для OnContextMenus.xaml
    /// </summary>
    public partial class OnContextMenus : Window
    {
        MainWindow window;
        DataOutput dataOutput;
        private string _Title = "Редактор названий полей блокнота";
        public string Title
        {
            get { return _Title; }
            set => Set(ref _Title, value);
        }
        public string dbFile { get; set; }
        public string oldColumn { get; set; }
        public string select;
        public string Select
        {
            get { return select; }
            set => Set(ref select, value);
        }
        public ObservableCollection<string> issues;
        public ObservableCollection<string> Issues
        {
            get { return issues; }
            set => Set(ref issues, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public OnContextMenus(string data)
        {   
            this.dbFile = data;
            
            this.issues = getCol(dataOutput);

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

        public ObservableCollection<string> getCol(DataOutput data)
        {
            this.issues = new ObservableCollection<string>();

            data = new DataOutput();
            data.dbFile = this.dbFile;
            data.ReadData();

            foreach (string item in data.Columns)
            {
                issues.Add(item);
            }
            select = issues[1];
            issues.RemoveAt(0);

            return issues;
        }

        private void listOfColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            ComboBox cb = sender as ComboBox;
            this.oldColumn = (string)cb.SelectedItem;
            renameBtn.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (newCaptionTB.Text != "")
            {
                dataOutput = new DataOutput();
                dataOutput.dbFile = dbFile;
                dataOutput.ReadData();
                dataOutput.RenameColumn(this.oldColumn, newCaptionTB.Text);

                dataOutput.dbFile = dbFile;
                dataOutput.ReadData();

                (this.Owner as MainWindow).mainDG();

                this.issues = getCol(dataOutput);
                listOfColumns.ItemsSource = issues;

                MessageBox.Show($"Заголовок {oldColumn} переименован как {newCaptionTB.Text}");
            }

        }
    }
}
