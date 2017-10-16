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
namespace App
{
    public partial class Moderation : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();
        public string stringPath = Properties.Settings.Default.stringPath;
        public Moderation(string fio, int idType)
        {
            connection.ConnectionString = stringPath;
            InitializeComponent();
            process_Load();
            categor_Load();
            federals_Load();
            status_Load();
            textBox1.Text = fio;
            fillTypeStaff(idType);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            onlineTrue(idType);
        }
        public void onlineTrue(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online='" + "False" + "' Where Id_staff='" + id + "'";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            connection.Close();
        }
        public void fillTypeStaff(int idType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "SELECT * FROM type_staff  WHERE Id_type_staff= '" + idType + "'";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox2.Text = reader[1].ToString();

                }
            }
            connection.Close();
        }
        public Moderation()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Moderation_Load(object sender, EventArgs e)
        {

        }
        public void categor_Load()
        {
            string qs = "SELECT * FROM dbo.message_categories";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "message_categories";
            comboBox1.ValueMember = "Id_message_categories";

        }
        public void federals_Load()
        {
            string qs = "SELECT * FROM dbo.federal_districts";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox2.DataSource = tbl;
            comboBox2.DisplayMember = "federal_districts";
            comboBox2.ValueMember = "Id_federal_districts";
        }
        public void status_Load()
        {
            string qs = "SELECT * FROM dbo.status_message";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox3.DataSource = tbl;
            comboBox3.DisplayMember = "status_message";
            comboBox3.ValueMember = "Id_status_message";
        }
        public void process_Load()
        {
            string qs = "SELECT * FROM dbo.message_processing";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox4.DataSource = tbl;
            comboBox4.DisplayMember = "answer_comment";
            comboBox4.ValueMember = "Id_message_processing";
        }
    }
}
