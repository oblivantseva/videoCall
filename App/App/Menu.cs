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
    public partial class Menu : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();


        public string stringPath = Properties.Settings.Default.stringPath;

        public Menu()
        {
            connection.ConnectionString = stringPath;
            command.Connection = connection;
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s_login = login.Text;
            string s_pass = pass.Text;
            connection.Open();

            command.CommandText = "SELECT COUNT(*) FROM dbo.staff WHERE log = '" + s_login + "'";
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
                            command.CommandText = "SELECT Id_staff_type_staff FROM dbo.staff WHERE log = '" + s_login + "' and pass='" + s_pass + "'";
                            SqlDataReader dr2 = command.ExecuteReader();
                            if (dr2.Read())
                            {
                                search = Convert.ToInt32(dr2[0].ToString());
                                dr2.Close();
                                if (search == 1)
                                {
                                    button5.Enabled = true;

                                }
                                else if (search == 2)
                                {
                                    button4.Enabled = true;
                                }
                                else if (search == 3)
                                {
                                    button1.Enabled = true;
                                }
                                else if (search == 5)
                                {
                                    button2.Enabled = true;
                                }
                            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            //string qs = "SELECT * FROM dbo.events Where ";
            //SqlCommand command = new SqlCommand(qs, connection);
            //System.Data.DataTable tbl = new System.Data.DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(tbl);
            //string ev = "name";
            string qs = "SELECT * FROM dbo.staff  WHERE log = '" + login.Text + "' and pass='" + pass.Text + "'";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);
           // string fio= tbl[2] + " "+ tbl[1] + " "+ tbl[3];
           // string d =;
            //Moderation mod = new Moderation(fio);
            Moderation mod = new Moderation();
            mod.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Start st = new Start();
            st.Show();
        }

        private void login_Click(object sender, EventArgs e)
        {
            login.Clear();
        }

        private void pass_Click(object sender, EventArgs e)
        {
            pass.Clear();
        }
    }
}
