using Microsoft.Win32;
using Note.Model;
using Note.View;
using Note.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Note
{   
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        DataOutput dataOutput;
        MainViewModel mvm;
        public string insertIntoPath { get; set; }
        
        public List<string> note;
        public Main()
        {
            InitializeComponent();
        }

        public void openMainWindow()
        {
            MainWindow window = new MainWindow(insertIntoPath, note);
            window.statusOne.Text = mvm.statusOneText;
            window.statusTwo.Text = mvm.statusTwoText;
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateDB create = new CreateDB();
            create.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(*.db)|*.db";
            openFile.Title = "Выберите файл";
            openFile.InitialDirectory = "";
            try
            {
                if (openFile.ShowDialog() == true)
                {
                    insertIntoPath = openFile.FileName;
                    
                    dataOutput = new DataOutput();
                    dataOutput.dbFile = openFile.FileName;
                    List<DataOutput> dot = dataOutput.ReadData();

                    note = new List<string>();
                    note = dataOutput.Columns;
                    note.RemoveAt(0);

                    mvm = new MainViewModel();
                    mvm.statusOneText = $"Открыт файл: {openFile.FileName}";

                    var fileInfo = new FileInfo(openFile.FileName);
                    double info = Convert.ToDouble(fileInfo.Length);
                    mvm.statusTwoText = $"Размер: {info} Кб";

                    openMainWindow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
