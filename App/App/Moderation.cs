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
namespace App
{
    public partial class Moderation : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();
        public string stringPath = Properties.Settings.Default.stringPath;
        int idModeration;
        public Moderation(string fio, int idType, int idModer)
        {
            idModeration = idModer;
            connection.ConnectionString = stringPath;
            InitializeComponent();
            process_Load();
            categor_Load();
            federals_Load();
            status_Load();
            popular_groupLoad();
            textBox1.Text = fio;
            fillTypeStaff(idType);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            onlineTrue(idModer);
            messageList(idModer);


        }
        public void messageList(int idM)
        {
            textBox4.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            //command.CommandText = "SELECT COUNT(*) FROM dbo.staff WHERE staff.Id_staff = '" + idModeration + "'";
            //SqlDataReader dr = command.ExecuteReader();
            //if (dr.Read())
            //{
            //    int search = 0;
            //    search = Convert.ToInt32(dr[0].ToString());
            //    dr.Close();

            cmd.CommandText = "SELECT        staff.*, messages.message_text, messages.datatime, federal_districts.federal_districts, message_categories.message_categories, " +
            " status_message.status_message, message_processing.answer_comment" +
                    " FROM            message_categories INNER JOIN " +
                    " messages ON message_categories.Id_message_categories = messages.id_message_message_categories INNER JOIN " +
                    " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                    " federal_districts ON [user].id_user_federal_districts = federal_districts.Id_federal_districts INNER JOIN " +
                    " message_processing ON messages.Id_message = message_processing.Id_message_processing_message INNER JOIN " +
                    "   staff ON message_processing.Id_message_processing_staff = staff.Id_staff INNER JOIN " +
                    " status_message ON message_processing.Id_message_processing_status_message = status_message.Id_status_message " +
                    " WHERE staff.Id_staff ='" + idModeration + "'  ";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox4.Text += reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + "; \n"
                        + "Логин: " + reader[4].ToString() + ";\nEmail: " + reader[6].ToString() + "; \nТелефон: "
                        + reader[7].ToString() + "; \nТекст сообщения: " + reader[10].ToString() + "; \nДата/Время обращения: "
                        + reader[11].ToString() + "; \nФедеральный округ: " + reader[12].ToString() + "; \nКатегория обращения: "
                        + reader[13].ToString() + "; \n Статус: " + reader[14].ToString() + "; \n Процесс: " + reader[15].ToString() + ".\n______________________________";

                }
            }
            connection.Close();

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
        public void popular_groupLoad()
        {
            string qs = "SELECT * FROM popular_group";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox5.DataSource = tbl;
            comboBox5.DisplayMember = "content";
            comboBox5.ValueMember = "Id_popular_group";
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
        public void formedQuer(object sender, EventArgs e)
        {
            textBox4.Clear();
            string str = "";
            string[] z = new string[4];
            int count = 0;
            z[0] = ""; z[1] = ""; z[2] = ""; z[3] = "";
            if (checkBox1.Checked) { z[0] = "(message_categories.message_categories = N'" + comboBox1.Text + "')"; count++; }
            if (checkBox2.Checked) { z[1] = "( federal_districts.federal_districts = N'" + comboBox2.Text + "')"; count++; }
            if (checkBox3.Checked) { z[2] = "(status_message.status_message= N'" + comboBox3.Text + "')"; count++; }
            if (checkBox4.Checked) { z[3] = "(message_processing.answer_comment = N'" + comboBox4.Text + "')"; count++; }

            if (count == 0) str = "";
            else
            {
                string str_dop = "";
                foreach (string s in z)
                {
                    if (s != "")
                        str_dop += " AND " + s;
                }
                str = str_dop;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "SELECT        staff.*, messages.message_text, messages.datatime, federal_districts.federal_districts, message_categories.message_categories, " +
                " status_message.status_message, message_processing.answer_comment" +
                        " FROM            message_categories INNER JOIN " +
                        " messages ON message_categories.Id_message_categories = messages.id_message_message_categories INNER JOIN " +
                        " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                        " federal_districts ON [user].id_user_federal_districts = federal_districts.Id_federal_districts INNER JOIN " +
                        " message_processing ON messages.Id_message = message_processing.Id_message_processing_message INNER JOIN " +
                        "   staff ON message_processing.Id_message_processing_staff = staff.Id_staff INNER JOIN " +
                        " status_message ON message_processing.Id_message_processing_status_message = status_message.Id_status_message " +
                        " WHERE staff.Id_staff ='" + idModeration + "'  " + str + "";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox4.Text += reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + "; \n"
             + "Логин: " + reader[4].ToString() + ";\nEmail: " + reader[6].ToString() + "; \nТелефон: "
             + reader[7].ToString() + "; \nТекст сообщения: " + reader[10].ToString() + "; \nДата/Время обращения: "
             + reader[11].ToString() + "; \nФедеральный округ: " + reader[12].ToString() + "; \nКатегория обращения: "
             + reader[13].ToString() + "; \n Статус: " + reader[14].ToString() + "; \n Процесс: " + reader[15].ToString() + ".\n______________________________";

                }
            }
            connection.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
          
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                connection.Open();
                cmd.CommandText = "INSERT INTO popular_group (content,Id_popular_group_event) values(N'" + comboBox5.Text + "','" + 1 + "')";
                cmd.ExecuteNonQuery();
                connection.Close();





            //if (comboBox5.Text != "")
            //{
            //    connection.Open();
            //string qs = @"INSERT INTO dbo.popular_group(content,Id_popular_group_event) VALUES(N'" + comboBox5.Text + "','" + 1 + "')";
            //SqlCommand command = new SqlCommand(qs, connection);
            //int Zaversh = command.ExecuteNonQuery();
            //connection.Close();
            //    MessageBox.Show("Группа добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else MessageBox.Show("Заполните все поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            popular_groupLoad();
        }
    }
}
