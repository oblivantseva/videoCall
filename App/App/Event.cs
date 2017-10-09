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
            string filename = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Видео файлы(*.mp4)|*.mp4" };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                axWindowsMediaPlayer1.URL = filename;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fam.Text == "" || name.Text == "" || otch.Text == "" || tel.Text == "" || age.Text == "" || mail.Text == "" || comboBox1.Text == "")
            {

                label9.Text = "Проверьте правильность введеных вами данных!";
                return;
            }
            else
            {
                label9.Text = "";
            }
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
                    idO = Convert.ToInt32(dr1[0].ToString());
                    dr1.Close();
                    SqlCommand command2 = connection.CreateCommand();
                    command2.CommandText = @"INSERT INTO dbo.[messages](datatime, Id_message_user, Id_message_type_message, message_text, 
                                             Id_message_event, media_content, Id_message_message_categories) 
                                            VALUES ('" + DateTime.Now + "','" + idO + "', '4','" + quest.Text + 
                                            "','" + Convert.ToInt32(id.Text) + 
                                            "','" + axWindowsMediaPlayer1.URL + "','" + Convert.ToInt32(comboBox1.SelectedValue) + "')";
                    int Zaversh2 = command2.ExecuteNonQuery();
                    if (Zaversh2 != 0)
                    {
                        MessageBox.Show("Обращение отправлено.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при отправке обращения.");
                    }
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка при отправке обращения.");
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
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void mail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;   
        }
        
<<<<<<< HEAD

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                label9.Text = "Ошибка!";
                e.Handled = true;
            }
            else
            {
                label9.Text = "";
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        //private void button3_Click_1(object sender, EventArgs e)
        //{
        //    if (fam.Text == "" || name.Text == "" || otch.Text == "" || tel.Text == "" || age.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "")
        //    {

        //        label9.Text = "Проверьте правильность введеных вами данных!";
        //        return;
        //    }
        //    else
        //    {
        //        label9.Text = "";
        //    }
        //}

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                char l = e.KeyChar;
                if ((l < 'A' || l > 'Z') && l != '\b' && l != '.' && l != '@' && l != '_' && l != '-')
                {
                    label9.Text = "Проверьте правильность введеных вами данных!";
                    e.Handled = true;
                }
                else
                {
                    label9.Text = "";
                }
            }
        }
=======
>>>>>>> de0e0356c60644e9f9266073527cea649ac9e6bd
    }
}