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
    public partial class Start : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();


        public string stringPath = Properties.Settings.Default.stringPath;

        public Start()
        {
            InitializeComponent();
            connection.ConnectionString = stringPath;
            command.Connection = connection;
            events_Load();
            button1.Enabled = false;
            button2.Enabled = false;

        }

        public void events_Load()
    {
        string qs = "SELECT * FROM dbo.events";
        SqlCommand command = new SqlCommand(qs, connection);
        System.Data.DataTable tbl = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(tbl);

        eventList.DataSource = tbl;
        eventList.DisplayMember = "name";
            eventList.ValueMember = "Id_events";
    }
    private void button1_Click(object sender, EventArgs e)
        {
            Event f1 = new Event();//переделала с Form1 на Event потому что ругалось
            f1.id.Text = eventList.SelectedValue.ToString();
            f1.eventbox.Text = eventList.Text;
            f1.Show();
        }

        

        private void eventList_KeyPress(object sender, KeyPressEventArgs e)
        {
            connection.Open();
            if (eventList.SelectedIndex != -1)
            {
                string qs = "SELECT * FROM dbo.events where Id_events='" + eventList.SelectedValue + "'";
                SqlCommand command = new SqlCommand(qs, connection);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    name.Text = dr.GetString(1);
                    description.Text = dr.GetString(2);
                    if(DateTime.Now > dr.GetDateTime(3) && DateTime.Now < dr.GetDateTime(4))
                    {
                        status.Text = "Проходит";
                        button1.Enabled = true;
                        button2.Enabled = true;

                    }
                    else if(DateTime.Now > dr.GetDateTime(4))
                    {
                        status.Text = "Окончен";
                        button1.Enabled = false;
                        button2.Enabled = true;
                    }
                    else
                    {
                        status.Text = "Будет";
                        button1.Enabled = false;
                        button2.Enabled = false;
                    }
                }
                dr.Close();
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EventP f = new EventP();
            f.Show();
        }
    }
}

