using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Note;
using Note.View;
using Note.ViewModel;

namespace Note.Model
{   
    public class DataOutput
    {
        private SQLiteConnection SqliteConnection; // connect to DataBase
        private SQLiteCommand SQLcommand; //Commant to SQL
        private SQLiteDataReader Reader;
        public string dbFile { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Kategory { get; set; }
        public string Link { get; set; }
        public List<string> Columns { get; set; }
        public string textFromTextBox { get; set; }

        public ObservableCollection<object> GetListString()
        {
            ObservableCollection<object> list = new ObservableCollection<object>()
            {
                {ID},   
                {Kategory},
                {Title},
                {Link}
            };
            return list;
        }

        public List<DataOutput> ReadData() 
        {   
            List<DataOutput> dataOutput = new List<DataOutput>();

            List<string> columns;

            this.SQLcommand = new SQLiteCommand();

            this.dbFile = dbFile;
            this.SqliteConnection = new SQLiteConnection("Data Source="+dbFile+";Version=3;");
            SqliteConnection.Open();

            SQLcommand.Connection = SqliteConnection; // Connection to DataBase

            SQLcommand = SqliteConnection.CreateCommand();
            SQLcommand.CommandText = "SELECT * FROM Data";
            Reader = SQLcommand.ExecuteReader();

            columns = new List<string>();
            for (int i = 0; i < Reader.FieldCount; i++)
            {   
                columns.Add(Reader.GetName(i));
            }
            Columns = columns;


            while (Reader.Read())
            {
                dataOutput.Add(new DataOutput()
                {
                    ID = Convert.ToInt32(Reader["ID"]),
                    Kategory = Reader[columns[1]].ToString(),
                    Title = Reader[columns[2]].ToString(),
                    Link = Reader[columns[3]].ToString(),
                });
            }
            SqliteConnection.Close();
            return dataOutput;
        }

        public List<DataOutput> Search()
        {
            List<DataOutput> dataOutput = new List<DataOutput>();

            this.SQLcommand = new SQLiteCommand();

            this.dbFile = dbFile;
            this.SqliteConnection = new SQLiteConnection("Data Source=" + dbFile + ";Version=3;");
            SqliteConnection.Open();

            SQLcommand.Connection = SqliteConnection; // Connection to DataBase

            SQLcommand = SqliteConnection.CreateCommand();
            SQLcommand.CommandText = "SELECT * FROM Data WHERE " + Columns[1] + " GLOB '" + "*" + textFromTextBox + "*" + "' OR " + Columns[2] + " GLOB '" + "*" + textFromTextBox + "*" + "' " +
                "OR " + Columns[3] + " GLOB '" + "*" + textFromTextBox + "*" + "'";
            Reader = SQLcommand.ExecuteReader();

            while (Reader.Read())
            {
                dataOutput.Add(new DataOutput()
                {
                    ID = Convert.ToInt32(Reader["ID"]),
                    Kategory = Reader[Columns[1]].ToString(),
                    Title = Reader[Columns[2]].ToString(),
                    Link = Reader[Columns[3]].ToString(),
                });
            }
            SqliteConnection.Close();
            return dataOutput;
        }

        public List<DataOutput> RenameColumn(string columnOldName, string columnNewName)
        {   
            if (columnNewName.Contains(" "))
            {
                columnNewName = columnNewName.Replace(" ","_");
            }

            List<DataOutput> dataOutput = new List<DataOutput>();

            this.SQLcommand = new SQLiteCommand();

            this.dbFile = dbFile;
            this.SqliteConnection = new SQLiteConnection("Data Source=" + dbFile + ";Version=3;");
            SqliteConnection.Open();

            SQLcommand.Connection = SqliteConnection; // Connection to DataBase

            SQLcommand = SqliteConnection.CreateCommand();
            SQLcommand.CommandText = "ALTER TABLE Data RENAME COLUMN '"+columnOldName+"' TO '"+columnNewName+"' ";
            
            Reader = SQLcommand.ExecuteReader();
            while (Reader.Read())
            {
                dataOutput.Add(new DataOutput()
                {
                    ID = Convert.ToInt32(Reader["ID"]),
                    Kategory = Reader[Columns[1]].ToString(),
                    Title = Reader[Columns[2]].ToString(),
                    Link = Reader[Columns[3]].ToString(),
                });
            }
            SqliteConnection.Close();

            return dataOutput;
        }

        public void UpdateFields(int _id, string dataList, int index)
        {

            int _idField = _id; // номер порядковый, id поля

            string transerToColumn="";

            transerToColumn = Columns[index];


            this.SQLcommand = new SQLiteCommand();

            this.dbFile = dbFile;
            this.SqliteConnection = new SQLiteConnection("Data Source=" + dbFile + ";Version=3;");
            SqliteConnection.Open();

            SQLcommand.Connection = SqliteConnection; // Connection to DataBase

            SQLcommand = SqliteConnection.CreateCommand();
            SQLcommand.CommandText = "Update Data SET '" + transerToColumn + "' = '" + dataList + "' WHERE Id = " + _idField + " ";
            SQLcommand.ExecuteNonQuery();

            SqliteConnection.Close();
        }
    }
}