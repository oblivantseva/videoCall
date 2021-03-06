﻿using System;
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
using System.Text.RegularExpressions;

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
            string filename = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Видео файлы(*.mp4)|*.mp4" };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                axWindowsMediaPlayer1.URL = filename;
            }
        }

        public bool RegTrue(string mail, string reg)
        {
            return Regex.IsMatch(mail, reg);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!RegTrue(mail.Text, @"\A[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\z"))
            {
                label9.Text = "Не правильно введен адрес почты. Повторите попытку.";
                return;
            }
            else
            {
                connection.Open();
                string qs = @"INSERT INTO dbo.[user](first_name, second_name, patronymic, telephone, email,age, id_user_federal_districts) VALUES('" + fam.Text + "', '" + name.Text + "','" + otch.Text + "', '" + tel.Text + "','" + mail.Text + "','" + age.Text + "', '" + Convert.ToInt32(comboBox1.SelectedValue) + "')";
                SqlCommand command = new SqlCommand(qs, connection);
                int Zaversh = command.ExecuteNonQuery();
                connection.Close();
                int idO;
                if (Zaversh != 0)
                {
                    connection.Open();
                    command.CommandText = "SELECT TOP 1 Id_user FROM dbo.[user] ORDER BY Id_user DESC";
                    SqlDataReader dr1 = command.ExecuteReader();
                    if (dr1.Read())
                    {
                        Char delimiter = '/';
                        String[] substrings = axWindowsMediaPlayer1.URL.Split(delimiter);

                        idO = Convert.ToInt32(dr1[0].ToString());
                        dr1.Close();
                        SqlCommand command2 = connection.CreateCommand();
                        command2.CommandText = @"INSERT INTO dbo.[messages](datatime, Id_message_user, Id_message_type_message, message_text, 
                        Id_message_event, media_content, Id_message_message_categories) 
                        VALUES (GETDATE (),'" + idO + "', '4','" + quest.Text +
                        "','" + Convert.ToInt32(id.Text) +
                        "','" + substrings[substrings.Length-1] + "','" + Convert.ToInt32(comboBox1.SelectedValue) + "')";
                        int Zaversh2 = command2.ExecuteNonQuery();
                        if (Zaversh2 != 0)
                        {
                            label9.Text = "Обращение отправлено.";
                        }
                        else
                        {
                            label9.Text = "Ошибка при отправке обращения.";
                        }

                        command.CommandText = "SELECT TOP 1 Id_message FROM dbo.[messages] ORDER BY Id_message DESC";
                        SqlDataReader dr2 = command.ExecuteReader();
                        if (dr2.Read())
                        {
                            int idM = Convert.ToInt32(dr2[0].ToString());
                            dr2.Close();
                            command2.CommandText = @"INSERT INTO dbo.message_processing(Id_message_processing_staff, Id_message_processing_message, Id_message_processing_status_message) 
                                                    VALUES (2,'" + idM + "', '4')";
                            Zaversh2 = command2.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
                else
                {
                    label9.Text = "Ошибка при отправке обращения.";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Видео файлы(*.mp4)|*.mp4" };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                axWindowsMediaPlayer1.URL = filename;
            }
        }

        private void tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    label9.Text = "Неверный ввод";
                    e.Handled = true;
                }
                else
                {
                    label9.Text = "";
                }
        }
        private void mail_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string c = e.KeyChar.ToString();
            //if (!Regex.Match(c, @"\p{IsCyrillic}|\p{IsCyrillicSupplement}").Success)//запрет русских букв
            //{
            //    label9.Text = "Неверный ввод";
            //    e.Handled = true;
            //}
            //else
            //{
            //    label9.Text = "";
            //}
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (tel.Text == "" || fam.Text == "" || name.Text == "" || otch.Text == "" || mail.Text == "" || age.Text == "" || comboBox1.Text == "")
            {

                label9.Text = "Проверьте правильность введеных вами данных!";
                return;
            }
            else
            {
                label9.Text = "";
            }
        }

        private void Event_Load(object sender, EventArgs e)
        {

        }

        private void age_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    label9.Text = "Неверный ввод";
                    e.Handled = true;
                }
                else
                {
                    label9.Text = "";
                }
            }
        }

        private void tel_TextChanged(object sender, EventArgs e)
        {
            string ans = WriteTel(tel.Text);
            if (ans == null)
            {
                label9.Text = "Номер телефона должен состоять из 11 цифр. Попробуйте ввести данные еще раз.";
                tel.ReadOnly = false;
                tel.Clear();
            }
        }

        public string WriteTel (string tel)
        {
            if (tel.Length < 11)
                return null;
            else
               return "1";
        }

    }
}


