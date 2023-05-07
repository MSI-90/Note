using Microsoft.Win32;
using Note.Model;
using Note.View;
using Note.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Note
{
    public delegate void MyColor (Color c);
    public partial class MainWindow : Window
    {
        MainViewModel mvm;
        DataOutput dataOutput;
        InsertIntoNote insertNote;
        CreateDB create = new CreateDB();
        MainViewModel mainViewModel;
        SerializableColor sc = new SerializableColor();

        SystemF systemFromWindows;

        public double systemWidth;
        public double systemHeight;
        public double mainWindowWidth;
        public string insertIntoPath { get; set; }
        //public DataOutput DataOutput { get; set; }
        public List<string> Notes;
        public MainWindow(string Path, List<string> list)
        {  
            this.insertIntoPath = Path;

            this.Notes = new List<string>();
            this.Notes = list;

            systemFromWindows = new SystemF();
            systemWidth = Convert.ToDouble(systemFromWindows.SystemWidth);
            systemHeight = Convert.ToDouble(systemFromWindows.SystemHeight);

            dataOutput = new DataOutput();

            InitializeComponent();
            
            ColorFromSerializable();
            dataOutput.dbFile = dbFile(insertIntoPath);
            mainDG();

            mainViewModel = new MainViewModel();
            statusOne.Text = mainViewModel.statusOneText;
            statusTwo.Text = mainViewModel.statusTwoText;
        }
        public string dbFile(string file)
        {
            file = insertIntoPath;
            return file;
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(*.db)|*.db";
            openFile.Title = "Выберите файл";
            openFile.InitialDirectory = "";
            try
            {
                if (openFile.ShowDialog() == true)
                {
                    Notes.Clear();
                    
                    this.insertIntoPath = openFile.FileName;
                    dataOutput.dbFile = openFile.FileName;
                    List<DataOutput> dot = dataOutput.ReadData();

                    Notes = new List<string>();

                    Notes = dataOutput.Columns;
                    Notes.RemoveAt(0);

                    mainDG();

                    mainViewModel.statusOneText = $"Открыт файл: {openFile.FileName}";
                    

                    var fileInfo = new FileInfo(openFile.FileName);
                    double info = Convert.ToDouble(fileInfo.Length);
                    mainViewModel.statusTwoText = $"Размер: {info} Кб";

                    statusOne.Text = mainViewModel.statusOneText;
                    statusTwo.Text = mainViewModel.statusTwoText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void mainDG()
        {
            mainDataGrid.ItemsSource = dataOutput.ReadData();
            string repl = "";

            for (int i = 0; i < dataOutput.Columns.Count; i++)
            {
                if (dataOutput.Columns[i].Contains("_"))
                {
                    repl = dataOutput.Columns[i].Replace("_", " ");
                }
                else
                {
                    repl = dataOutput.Columns[i];
                }
                mainDataGrid.Columns[i].Header = repl;
            }
            mainDataGrid.Width = 1200;
            //mainDataGrid.Columns[1].Width = dataOutput.sizeOfFirs*8;
            //mainDataGrid.Columns[2].Width = dataOutput.sizeOfTwo*2;
            mainDataGrid.Columns[3].Width = DataGridLength.Auto;
            //mainDataGrid.Columns[1].Width = 80;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mainDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {   
            FileInfo fileInfo;
            string path = "";
            string[] pathToBrowsr = new string[] 
            {
                @"C:\Program Files\Google\Chrome\Application\chrome.exe", 
                @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe",
                @"C:\Program Files\Mozilla Firefox\firefox.exe",
                @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                @"C:\Program Files\Mozilla Firefox\firefox.exe",
                @"C:\Program Files\Microsoft\Edge\Application\msedge.exe"
            };

            for (int i = 0; i < pathToBrowsr.Length; i++)
            {
                fileInfo = new FileInfo(pathToBrowsr[i]);
                if (fileInfo.Exists)
                {
                    path = pathToBrowsr[i];
                    MessageBox.Show("Сейчас будет инициализирован процесс перехода по вашей ссылке с помощью браузера");
                    break;
                }
            }

            if (path == "")
                MessageBox.Show("Необходимо установить один из предложенных браузеров:\nGoogle Chrome\nMozilla Firefox\nMicrosoft Edge");
            else
            {
                string pattern = @"\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>[^>\s]+))";
                List<string> ht = new List<string>() { "http", "https", "ftp", "www", "HTTP", "HTTPS", "WWW", "FTP" };
                List<string> ht2 = new List<string>() { "РФ", "рф", "com", "COM", "Com", "org", "net", "ru", "RU" };

                foreach (DataOutput dataOutput in mainDataGrid.Items)
                {
                    if (dataOutput == mainDataGrid.CurrentItem)
                    {
                        foreach (var str in ht)
                        {
                            if (dataOutput.Kategory.StartsWith(str))
                            {
                                System.Diagnostics.Process.Start(path, dataOutput.Kategory);
                                break;
                            }
                            else if (dataOutput.Title.StartsWith(str))
                            {
                                System.Diagnostics.Process.Start(path, dataOutput.Title);
                                break;
                            }
                            else if (dataOutput.Link.StartsWith(str))
                            {
                                System.Diagnostics.Process.Start(path, dataOutput.Link);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    if (dataOutput == mainDataGrid.CurrentItem)
                    {
                        foreach (var str2 in ht2)
                        {
                            if (dataOutput.Kategory.EndsWith(str2))
                            {
                                System.Diagnostics.Process.Start(path, dataOutput.Kategory);
                                break;
                            }
                            else if (dataOutput.Title.EndsWith(str2))
                            {
                                System.Diagnostics.Process.Start(path, dataOutput.Title);
                                break;
                            }
                            else if (dataOutput.Link.EndsWith(str2))
                            {
                                System.Diagnostics.Process.Start(path, dataOutput.Link);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                About about = new About();
                about.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                miniColorPicker mcp = new miniColorPicker(new MyColor(OnColor));
                mcp.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void OnColor(Color w)
        {
            Color selectedColor = w;
            try
            {
                this.Background = new SolidColorBrush(selectedColor);
                this.BorderBrush = new SolidColorBrush(selectedColor);
                ApplicationStatus.Background = this.Background;

                sc.colorProp = selectedColor;
                SaveInXmlFormat(Environment.CurrentDirectory + "\\" + "SerializableColor.xml", sc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveInXmlFormat(string fileName, object obj)
        {   
            XmlSerializer eraseSerialise = new XmlSerializer(typeof(SerializableColor));
            using (Stream fStream = new FileStream(fileName, FileMode.Truncate)) { }

            XmlSerializer serializer = new XmlSerializer(typeof(SerializableColor));
            using (Stream fStream = new FileStream(
            fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                serializer.Serialize(fStream, obj);
            }
        }
        public void ColorFromSerializable()
        {
            try
            {
                string programPath = Environment.CurrentDirectory;
                var mySerializer = new XmlSerializer(typeof(SerializableColor));

                if (!File.Exists(programPath + "\\" + "SerializableColor.xml"))
                {
                    //using var fileStream = new FileStream(programPath + "\\" + "SerializableColor.xml", FileMode.Create);
                    new FileInfo(programPath + "\\" + "SerializableColor.xml").Create();
                }
                else
                {
                    using var fileStream = new FileStream(programPath + "\\" + "SerializableColor.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                    var myObject = (SerializableColor)mySerializer.Deserialize(fileStream);
                    Color color = myObject.colorProp;
                    this.Background = new SolidColorBrush(color);
                    this.BorderBrush = new SolidColorBrush(color);
                    ApplicationStatus.Background = this.Background;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            CreateDB createDB = new CreateDB();
            createDB.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            insertNote = new InsertIntoNote(insertIntoPath, Notes);
            insertNote.Owner = this;
            insertNote.Show();
        }
        private void Find_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textBox = Find.Text;

            if (textBox != null)
            {
                try
                {
                    dataOutput.textFromTextBox = textBox;
                    mainDataGrid.ItemsSource = dataOutput.Search();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    mainDataGrid.ItemsSource = dataOutput.ReadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OnContextMenus cm = new OnContextMenus(insertIntoPath);
            cm.Owner = this;

            cm.listOfColumns.Foreground = new SolidColorBrush(Colors.Brown);
            cm.listOfColumns.FontSize = 12;
            cm.listOfColumns.FontWeight.Equals(FontWeights.Bold);

            cm.Background = this.Background;
            cm.ShowDialog();
           
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {   
            ObservableCollection<object> list = new ObservableCollection<object>();

            List<object> dataOutputColumns = new List<object>();
            foreach (var item in dataOutput.Columns)
            {
                dataOutputColumns.Add(item);
            }
            //dataOutputColumns.RemoveAt(0);

            foreach (DataOutput dataOutput in mainDataGrid.Items)
            {
                if (dataOutput == mainDataGrid.CurrentItem)
                {
                    list = dataOutput.GetListString();
                    //MessageBox.Show(list[0]+"\n"+list[1]+"\n"+list[2]);
                }
            }
            RowEditor re = new RowEditor(list, dataOutputColumns);
            re.Background = this.Background;
            re.Owner = this;
            re.ShowDialog();
        }
    } 
}