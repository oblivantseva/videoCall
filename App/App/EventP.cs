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
    public partial class EventP : Form
    {
        public EventP(string nameEvent)
        {
            InitializeComponent();
            connection.ConnectionString = stringPath;
            command.Connection = connection;
            textBox1.Text = nameEvent;
           eventp_Load();
        }

        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();

        public string stringPath = Properties.Settings.Default.stringPath;

        public void eventp_Load()
        {
            string qs = "SELECT * FROM dbo.popular_group";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);//
            da.Fill(tbl);

            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "content";
            comboBox1.ValueMember = "Id_popular_group";
        }

        private void EventP_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                comboBox1.Enabled = true;
            else comboBox1.Enabled = false;
        }
    }
}
