using Note.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
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
    public partial class InsertIntoNote : Window
    {
        MainWindow window;
        DataOutput dataOutput;
        private SQLiteDataReader Reader;
        public string dbFile { get; set; }
        private SQLiteConnection SqliteCon;
        private SQLiteCommand SqlCMD;
        public List<string> Notes;
        public InsertIntoNote(string data, List<string> list )
        {   
            this.dbFile = data;
            this.Notes = list;

            InitializeComponent();
            Header.Text = "Набор полей: " + Notes[0] + ", " + Notes[1] + ", " + Notes[2];
            one.Text = "Поле - " + Notes[0];
            two.Text = "Поле - " + Notes[1];
            three.Text = "Поле - " + Notes[2];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqliteCon = new SQLiteConnection();
            SqlCMD = new SQLiteCommand();

            dbFile = this.dbFile;

            if (Kategory.Text == "" || Name.Text == "" || Link.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                SqliteCon = new SQLiteConnection("Data Source =" + dbFile + ";Version=3;");
                SqliteCon.Open();

                SqlCMD = SqliteCon.CreateCommand();

                SqlCMD.CommandText = "Insert into Data ('" + Notes[0] + "', '" + Notes[1] + "', '" + Notes[2] + "') values" +
                    "('" + Kategory.Text + "', '" + Name.Text + "', '" + Link.Text + "')";

                SqlCMD.ExecuteNonQuery();
                SqliteCon.Close();

                MessageBox.Show("Данные успешно добавлены");

                (this.Owner as MainWindow).mainDG();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (deleteText.Text == "")
            {
                MessageBox.Show("Вы не указали поле ID");
            }
            else
            {   
                List<int> list = new List<int>();
                bool flag = false;
                SqliteCon = new SQLiteConnection();
                SqlCMD = new SQLiteCommand();

                dbFile = this.dbFile;

                SqliteCon = new SQLiteConnection("Data Source =" + dbFile + ";Version=3;");
                SqliteCon.Open();

                SqlCMD = SqliteCon.CreateCommand();
                SqlCMD.CommandText = "SELECT * FROM Data";
                Reader = SqlCMD.ExecuteReader();
                while (Reader.Read())
                {
                    list.Add(Convert.ToInt32(Reader["id"]));
                }

                foreach (int id in list)
                {
                    if (id == Convert.ToInt32(deleteText.Text))
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    SqlCMD = SqliteCon.CreateCommand();
                    SqlCMD.CommandText = "DELETE FROM Data where id = '" + Convert.ToInt32(deleteText.Text) + "' ";
                    SqlCMD.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно удалены");
                    (this.Owner as MainWindow).mainDG();
                }
                else
                {
                    MessageBox.Show("В блокноте нет указанного Вами идентификатора поля\nВозможно поле уже было удалено?\n" +
                    "Возможно в данном блокноте нет поля с данным идентификатором?");
                }

                SqliteCon.Close();
            }
        }
    }
}

