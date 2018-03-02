using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using OutputClass;

namespace Output
{
    public partial class Form1 : Form
    {
        public DataSet dataSet = new DataSet();
        //public string serverName;

        public Form1()
        {
            InitializeComponent();

            SqlConnection MeinConnection = Mein.CreateConnection("master");
            MeinConnection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;

            #region Не валидное имя сервера
            //sqlCommand = new SqlCommand($"SELECT @@servername", MeinConnection);
            //sqlDataReader = sqlCommand.ExecuteReader();
            //while (sqlDataReader.Read())
            //{
            //    serverName = (string)sqlDataReader.GetValue(0);
            //    Console.WriteLine(serverName);
            //}
            //sqlCommand.Cancel();
            //sqlDataReader.Close();
            #endregion

            //Вывод всех таблиц в dataGridView
            sqlDataAdapter = new SqlDataAdapter("EXEC sp_helpdb", MeinConnection);
            //sqlDataAdapter = new SqlDataAdapter($"EXEC sp_Databases", MeinConnection);
            DataTable table = new DataTable();
            sqlDataAdapter.Fill(table);
            dataGridView1.DataSource = table;

            Console.WriteLine(MeinConnection.Database);
            label1.Text = MeinConnection.Database;

            sqlCommand = new SqlCommand("EXEC sp_helpdb", MeinConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while(sqlDataReader.Read())
            {
                comboBox1.Items.Add(sqlDataReader.GetValue(0));
            }




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            dataSet.Tables.Clear();

            SqlConnection MeinConnection = Mein.CreateConnection((string)comboBox1.SelectedItem);
            MeinConnection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;

            sqlCommand = new SqlCommand($"SELECT TABLE_NAME FROM {comboBox1.SelectedItem}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", MeinConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                comboBox2.Items.Add(sqlDataReader.GetValue(0));
                dataSet.Tables.Add((string)sqlDataReader.GetValue(0));
            }
            sqlCommand.Cancel();
            sqlDataReader.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection MeinConnection = Mein.CreateConnection((string)comboBox1.SelectedItem);
            MeinConnection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM {dataSet.Tables[i].TableName}", MeinConnection);
                sqlDataAdapter.Fill(dataSet.Tables[i]);
            }
            dataGridView1.DataSource = dataSet.Tables[comboBox2.SelectedIndex];
        }
    }
}
