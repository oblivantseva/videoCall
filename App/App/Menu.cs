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
                                {//изменила 4-2
                                    button2.Enabled = true;
                                }
                                else if (search == 3)
                                {
                                    button1.Enabled = true;
                                }
                                else if (search == 4)//изменила на 4
                                {//изменила 2-4
                                    button4.Enabled = true;
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
            
            string fio="";
            int idT = 0;
            int idM = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "SELECT * FROM dbo.staff  WHERE log = '" + login.Text + "' and pass='" + pass.Text + "'";
            cmd.ExecuteNonQuery();
                        SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    fio = reader[2].ToString()+" "+ reader[1].ToString()+" "+ reader[3].ToString();
                    int.TryParse(reader[8].ToString(), out idT);
                    int.TryParse(reader[0].ToString(), out idM);
                }
            }
            connection.Close();
            Moderation mod = new Moderation(fio,idT,idM);
            mod.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fio = "";
            int idT = 0;
            int idOp = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "SELECT * FROM dbo.staff  WHERE log = '" + login.Text + "' and pass='" + pass.Text + "'";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    fio = reader[2].ToString() + " " + reader[1].ToString() + " " + reader[3].ToString();
                    int.TryParse(reader[8].ToString(), out idT);
                    int.TryParse(reader[0].ToString(), out idOp);
                }
            }
            connection.Close();
            EfirAppeals ef = new EfirAppeals(fio, idT, idOp, this);
            ef.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fio = "";
            int idT = 0;
            int idOper = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "SELECT * FROM dbo.staff  WHERE log = '" + login.Text + "' and pass='" + pass.Text + "'";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    fio = reader[2].ToString() + " " + reader[1].ToString() + " " + reader[3].ToString();
                    int.TryParse(reader[8].ToString(), out idT);
                    int.TryParse(reader[0].ToString(), out idOper);
                }
            }
            connection.Close();
            ReceptionCalls reC = new ReceptionCalls(fio, idT, idOper, this);
       
            reC.Show();
        }
        public void ClearForm()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            pass.Clear();
            login.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Event st = new Event();//хз на какую форму надо?
            st.Show();
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
