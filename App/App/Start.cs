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

        public Start()
        {
            InitializeComponent();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\call2president.mdf;Integrated Security=True;";
            command.Connection = connection;
            events_Load();
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
            Event f1 = new Event();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s_login = login.Text;
            string s_pass = pass.Text;
            connection.Open();

            command.CommandText = "SELECT COUNT(*) FROM dbo.staff WHERE log = '" + s_login+"'";
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                int search = 0;
                search = Convert.ToInt32(dr[0].ToString());
                dr.Close();
                if (search != 0)
                {
                    command.CommandText = "SELECT COUNT(*) FROM dbo.staff WHERE log = '" + s_login + "' and pass='" + s_pass + "'";
                    SqlDataReader dr1 = command.ExecuteReader();
                    if (dr1.Read())
                    {
                        search = Convert.ToInt32(dr1[0].ToString());
                        dr1.Close();
                        if (search != 0)
                        {
                            MessageBox.Show("Вы в системе!");
                        }
                        else
                        {
                            MessageBox.Show("Неправильно указан логин/пароль.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Данный аккаунт не зарегестрирован.");
                }
            }
            connection.Close();
        }

        private void eventList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void eventList_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void eventList_KeyDown(object sender, KeyEventArgs e)
        {
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
                    }
                    else if(DateTime.Now > dr.GetDateTime(4))
                    {
                        status.Text = "Окончен";
                    }
                    else
                    {
                        status.Text = "Будет";
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

