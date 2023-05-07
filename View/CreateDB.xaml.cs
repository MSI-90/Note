using Microsoft.Win32;
using Note.Model;
using Note.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Note.View
{
    /// <summary>
    /// Логика взаимодействия для CreateDB.xaml
    /// </summary>
    public partial class CreateDB : Window
    {
        MainWindow window;
        MainViewModel mvm;
        private SQLiteConnection SqliteConnection;
        private SQLiteCommand SQLcommand;
        public string NameDB { get; set; }
        public string insertIntoPath { get; set; }
        public CreateDB()
        {   
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            SqliteConnection = new SQLiteConnection();
            SQLcommand = new SQLiteCommand();

            string dataBasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            insertIntoPath = dataBasePath + "/" + dbName.Text + ".db";
            if (!File.Exists(insertIntoPath))
            {
                try
                {
                    if (Kategory.Text == "" || Name.Text == "" || Link.Text == "")
                    {
                        MessageBox.Show("Не все поля заполнены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Kategory.Text = Kategory.Text.Replace(' ', '_');
                        Name.Text = Name.Text.Replace(' ', '_');
                        Link.Text = Link.Text.Replace(' ', '_');
                        try
                        {
                            SQLiteConnection.CreateFile(insertIntoPath);
                            SqliteConnection = new SQLiteConnection("Data Source=" + insertIntoPath + "; Version=3;");
                            SqliteConnection.Open();

                            SQLcommand.Connection = SqliteConnection; // Connection to DataBase

                            SQLcommand = SqliteConnection.CreateCommand();
                            SQLcommand.CommandText = "create table if not exists Data (id integer primary key autoincrement, " + Kategory.Text + ", " +
                                "" + Name.Text + "," + Link.Text + ")";

                            SQLcommand.ExecuteNonQuery();
                            SqliteConnection.Close();

                            MessageBox.Show($"Создан блокнот/журнал, именуемый - {dbName.Text} \nМожно добавлять поля");

                            List<string> Fields = new List<string>()
                            {
                                Kategory.Text,
                                Name.Text,
                                Link.Text,
                            };

                            mvm = new MainViewModel();
                            var fileInfo = new FileInfo(insertIntoPath);
                            double info = Convert.ToDouble(fileInfo.Length);
                            mvm.statusOneText = $"Открыт файл: {insertIntoPath}";
                            mvm.statusTwoText = $"Размер: {info} Кб";

                            this.Close();

                            window = new MainWindow(insertIntoPath, Fields);
                            window.statusOne.Text = mvm.statusOneText;
                            window.statusTwo.Text = mvm.statusTwoText;
                            window.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка выполнения операции, проверьте еще раз вводимые данные");
                        }
                        //InsertIntoNote iIn = new InsertIntoNote(insertIntoPath, Fields);
                        //iIn.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show($"В вашей директории уже имеется список, именуемый как: {dbName.Text} \n\nПереименуйте свой новый блокнот, либо откройте существующий", 
                    "ВНИМАНИЕ!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void OpenFileDialog()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(*.db)|*.db";
            openFile.Title = "Выберите файл";
            openFile.InitialDirectory = "";
            try
            {
                if (openFile.ShowDialog() == true)
                {
                    //Notes.Clear();

                    //this.insertIntoPath = openFile.FileName;
                    //dataOutput.dbFile = openFile.FileName;
                    //List<DataOutput> dot = dataOutput.ReadData();

                    //Notes = new List<string>();

                    //Notes = dataOutput.Columns;
                    //Notes.RemoveAt(0);

                    //mainDG();

                    //mainViewModel.statusOneText = $"Открыт файл: {openFile.FileName}";


                    //var fileInfo = new FileInfo(openFile.FileName);
                    //double info = Convert.ToDouble(fileInfo.Length);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
