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
    public partial class EfirAppeals : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection();Menu f;
        public SqlCommand command = new SqlCommand(); int idO;
        public string stringPath = Properties.Settings.Default.stringPath;
        public EfirAppeals(string fio, int idType, int idOper,Menu form)
        {
            f = form;
            idO = idOper;
            connection.ConnectionString = stringPath;
            InitializeComponent();
            textBox1.Text = fio;
            tableLoad();
            fillTypeStaff(idType);
            categor_Load(); federals_Load(); popular_groupLoad();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            print_event(idOper); onlineTrue(idOper);
        }
        public void federals_Load()
        {
            string qs = "SELECT * FROM dbo.federal_districts";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox3.DataSource = tbl;
            comboBox3.DisplayMember = "federal_districts";
            comboBox3.ValueMember = "Id_federal_districts";
        }
        public void tableLoad()
        {
            connection.Open();
            string sql = "SELECT         [user].*,messages.message_text, popular_group.[content], messages.datatime, message_categories.message_categories, status_message.status_message,messages.Id_message " +

                            "FROM events INNER JOIN " +
                         "messages ON events.Id_events = messages.Id_message_event INNER JOIN " +
                         "message_categories ON messages.id_message_message_categories = message_categories.Id_message_categories INNER JOIN " +
                         "message_processing ON messages.Id_message = message_processing.Id_message_processing_message INNER JOIN " +
                         "status_message ON message_processing.Id_message_processing_status_message = status_message.Id_status_message INNER JOIN " +
                         "[user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                         "popular_messages ON messages.Id_message = popular_messages.Id_popular_messages_message INNER JOIN " +
                        " popular_group ON popular_messages.Id_popular_messages_popular_group = popular_group.Id_popular_group " +
"WHERE(message_processing.Id_message_processing_status_message = 2)";
            // cmd.ExecuteNonQuery();

            using (SqlConnection connection = new SqlConnection(stringPath))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                string query = "";
                foreach (DataTable dt in ds.Tables)
                {
                    // перебор всех столбцов
                    // foreach (DataColumn column in dt.Columns)
                    // перебор всех строк таблицы
                    foreach (DataRow row in dt.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        row[1] = "ФИО:" + cells[1] + " " + cells[2] + " " + cells[3] + ";  Телефон: " + cells[4] + "\n;  email:" + cells[5] + "\n;   Возвраст:" + cells[6];
                        query = "select media_content from dbo.messages where messages.Id_message_user='" + cells[0] + "'";
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                //нужно поменять название видео файлов,поэтому заккоментила
                //SqlDataReader dr1 = command.ExecuteReader();
                //if (dr1.Read())
                //{
                //    axWindowsMediaPlayer1.URL = dr1[0].ToString();
                //}
                dataGridView1.DataSource = ds.Tables[0];
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[8].Width = 200;
            dataGridView1.Columns[9].Width = 200;
            dataGridView1.Columns[10].Width = 80;
            dataGridView1.Columns[1].HeaderText = "Информация пользователя";
            dataGridView1.Columns[8].HeaderText = "Текст сообщения";
            dataGridView1.Columns[9].HeaderText = "Популярные группы";
            dataGridView1.Columns[10].HeaderText = "Время обращения";
            dataGridView1.Columns[11].HeaderText = "Категория сообщения";

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
        public void categor_Load()
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
        public void popular_groupLoad()
        {
            string qs = "SELECT * FROM popular_group";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "content";
            comboBox1.ValueMember = "Id_popular_group";
        }
        private void EfirAppeals_Load(object sender, EventArgs e)
        {

        }
        public void formedQuer(object sender, EventArgs e)
        {
            string str = "";
            string[] z = new string[3];
            int count = 0;
            z[0] = ""; z[1] = ""; z[2] = "";
          
           
            if (checkBox2.Checked) { z[1] = "(message_categories.message_categories = N'" + comboBox2.Text + "')"; count++; }
            if (checkBox1.Checked) { z[0] = "(popular_group.[content]= N'" + comboBox1.Text + "')"; count++; }
            if (checkBox3.Checked) { z[2] = "( federal_districts.federal_districts = N'" + comboBox3.Text + "')"; count++; }
            //  if (checkBox4.Checked) { z[3] = "(message_processing.answer_comment = N'" + comboBox4.Text + "')"; count++; }

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
            //SqlCommand cmd = new SqlCommand();
            // cmd.Connection = connection;
            connection.Open();
            string sql = "SELECT         [user].*,messages.message_text, popular_group.[content], messages.datatime, message_categories.message_categories, status_message.status_message,messages.Id_message " +

                            "FROM events INNER JOIN " +
                         "messages ON events.Id_events = messages.Id_message_event INNER JOIN " +
                         "message_categories ON messages.id_message_message_categories = message_categories.Id_message_categories INNER JOIN " +
                         "message_processing ON messages.Id_message = message_processing.Id_message_processing_message INNER JOIN " +
                         "status_message ON message_processing.Id_message_processing_status_message = status_message.Id_status_message INNER JOIN " +
                         "[user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                         "popular_messages ON messages.Id_message = popular_messages.Id_popular_messages_message INNER JOIN " +
                        " popular_group ON popular_messages.Id_popular_messages_popular_group = popular_group.Id_popular_group " +
"WHERE(message_processing.Id_message_processing_status_message = 2)  " + str + "";
            // cmd.ExecuteNonQuery();

            using (SqlConnection connection = new SqlConnection(stringPath))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                string query = "";
                foreach (DataTable dt in ds.Tables)
                {
                    // перебор всех столбцов
                    // foreach (DataColumn column in dt.Columns)
                    // перебор всех строк таблицы
                    foreach (DataRow row in dt.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        row[1] = "ФИО:" + cells[1] + " " + cells[2] + " " + cells[3] + ";  Телефон: " + cells[4] + "\n;  email:" + cells[5] + "\n;   Возвраст:" + cells[6];
                        query = "select media_content from dbo.messages where messages.Id_message_user='" + cells[0] + "'";
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                //нужно поменять название видео файлов,поэтому заккоментила
                //SqlDataReader dr1 = command.ExecuteReader();
                //if (dr1.Read())
                //{
                //    axWindowsMediaPlayer1.URL = dr1[0].ToString();
                //}
                dataGridView1.DataSource = ds.Tables[0];
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[8].Width = 200;
            dataGridView1.Columns[9].Width = 200;
            dataGridView1.Columns[10].Width = 80;
            dataGridView1.Columns[1].HeaderText = "Информация пользователя";
            dataGridView1.Columns[8].HeaderText = "Текст сообщения";
            dataGridView1.Columns[9].HeaderText = "Популярные группы";
            dataGridView1.Columns[10].HeaderText = "Время обращения";
            dataGridView1.Columns[11].HeaderText = "Категория сообщения";

            connection.Close();
        }
        public void onlineTrue(int id)
        {
            //SqlCommand cmd1 = new SqlCommand();
            //cmd1.Connection = connection;
            //connection.Open();
            //cmd1.CommandText = "SELECT * FROM staff  WHERE Id_staff= '" + id + "'";
            //cmd1.ExecuteNonQuery();
            //SqlDataReader reader = cmd1.ExecuteReader();
            //if (reader != null)
            //{
            //    if (reader.Read())
            //    {
            //        MessageBox.Show(reader[9].ToString());

            //    }
            //}
            //connection.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online=1 Where Id_staff='" + id + "'";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            connection.Close();
            //SqlCommand cmd2 = new SqlCommand();
            //cmd2.Connection = connection;
            //connection.Open();
            //cmd2.CommandText = "SELECT * FROM staff  WHERE Id_staff= '" + id + "'";
            //cmd2.ExecuteNonQuery();
            //SqlDataReader reader1 = cmd2.ExecuteReader();
            //if (reader1 != null)
            //{
            //    if (reader1.Read())
            //    {
            //        MessageBox.Show(reader1[9].ToString());

            //    }
            //}
            //connection.Close();
        }
        public void print_event(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT events.name FROM dbo.events, dbo.staff_event where events.Id_events=staff_event.Id_staff_event_event and staff_event.Id_staff_event_staff='" + id + "'";
            SqlDataReader dr1 = command.ExecuteReader();
            if (dr1.Read())
            {
                textBox4.Text = dr1[0].ToString();
                dr1.Close();
            }
            connection.Close();

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
           // textBox5.Text = dataGridView1[8, dataGridView1.CurrentRow.Index].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online=0 Where Id_staff='" + idO + "'";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            connection.Close();
            this.Close();
            f.Activate();
            f.ClearForm();
        }


        //- нажатие кнопки «пометить как отвечен» меняет статус обращения на «отвечен 
        //    Президентом» - с соответствующей записью в таблицу базы данных «message_processing»;
        private void button3_Click(object sender, EventArgs e)
        {
            int idM = Convert.ToInt32(dataGridView1[13, dataGridView1.CurrentRow.Index].Value);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update message_processing set Id_message_processing_status_message=5 Where Id_message_processing_message='" + idM + "'";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            connection.Close();
            this.Close();
            label9.Text = "Сообщение помечено, как Отвечен Презедентом";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableLoad();
        }
    }
}
