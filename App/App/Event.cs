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
using System.Data.SqlTypes;
using System.Data.Sql;

namespace App
{
    public partial class Event : Form
    {

        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();

        public string stringPath = Properties.Settings.Default.stringPath;

        public Event()
        {
            InitializeComponent();
            connection.ConnectionString = stringPath;
            command.Connection = connection;
            federals_Load();
            quest_Load();
        }

        public void federals_Load()
        {
            string qs = "SELECT * FROM dbo.federal_districts";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "federal_districts";
            comboBox1.ValueMember = "Id_federal_districts";
        }

        public void quest_Load()
        {
            string qs = "SELECT * FROM dbo.message_categories";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox2.DataSource = tbl;
            comboBox2.DisplayMember = "message_categories";
            comboBox2.ValueMember = "Id_message_categories";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"1.mp4";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}