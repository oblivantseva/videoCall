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
    public partial class Moderation : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();Menu f;
        public string stringPath = Properties.Settings.Default.stringPath;
        int idModeration;
        public int message;
        public Moderation(string fio, int idType, int idModer,Menu form)
        {
            f = form;
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
            print_event(idModer);
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
                textBox3.Text = dr1[0].ToString();
                dr1.Close();
            }
            connection.Close();

        }
        public void messageList(int idM)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();

            string sql = "SELECT        [user].*, messages.message_text, messages.datatime, federal_districts.federal_districts, message_categories.message_categories, " +
                 " status_message.status_message, message_processing.answer_comment ,messages.processed, messages.Id_message,messages.Id_message_message_categories, messages.media_content" +
                         " FROM            message_categories INNER JOIN " +
                         " messages ON message_categories.Id_message_categories = messages.id_message_message_categories INNER JOIN " +
                         " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                         " federal_districts ON [user].id_user_federal_districts = federal_districts.Id_federal_districts INNER JOIN " +
                         " message_processing ON messages.Id_message = message_processing.Id_message_processing_message INNER JOIN " +
                         "   staff ON message_processing.Id_message_processing_staff = staff.Id_staff INNER JOIN " +
                         " status_message ON message_processing.Id_message_processing_status_message = status_message.Id_status_message " +
                         " WHERE staff.Id_staff  ='" + idModeration + "' ";
            // cmd.ExecuteNonQuery();
            using (SqlConnection connection = new SqlConnection(stringPath))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataTable dt in ds.Tables)
                {
                    // перебор всех столбцов
                    // foreach (DataColumn column in dt.Columns)
                    // перебор всех строк таблицы
                    foreach (DataRow row in dt.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        row[1] = "ФИО:" + cells[1] + " " + cells[2] + " " + cells[3] + "; Телефон: " + cells[4] + "\n;email:" + cells[5] + "\n Возвраст:" + cells[6];
                    }
                }
                dataGridView1.DataSource = ds.Tables[0];
                connection.Close();
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[1].HeaderText = "ФИО пользователя";
            dataGridView1.Columns[11].HeaderText = "Категория";
            dataGridView1.Columns[9].HeaderText = "Дата";
            dataGridView1.Columns[10].HeaderText = "Федеральный округ";
            dataGridView1.Columns[14].HeaderText = "Процесс";
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
            connection.Open();
            string qs = @"INSERT INTO dbo.[message_processing](Id_message_processing_staff, Id_message_processing_message, Id_message_processing_status_message) VALUES('" + idModeration + "', '" + message + "','" + 2 + "')";
            SqlCommand command = new SqlCommand(qs, connection);
            int Zaversh = command.ExecuteNonQuery();
            connection.Close();
            label10.Text = "Обращение добавлено в эфир";
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
            //SqlCommand cmd = new SqlCommand();
            // cmd.Connection = connection;
            connection.Open();
            string sql = "SELECT        [user].*, messages.message_text, messages.datatime, federal_districts.federal_districts, message_categories.message_categories, " +
                " status_message.status_message, message_processing.answer_comment,messages.processed, messages.Id_message, messages.Id_message_message_categories, messages.media_content" +
                        " FROM            message_categories INNER JOIN " +
                        " messages ON message_categories.Id_message_categories = messages.id_message_message_categories INNER JOIN " +
                        " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                        " federal_districts ON [user].id_user_federal_districts = federal_districts.Id_federal_districts INNER JOIN " +
                        " message_processing ON messages.Id_message = message_processing.Id_message_processing_message INNER JOIN " +
                        "   staff ON message_processing.Id_message_processing_staff = staff.Id_staff INNER JOIN " +
                        " status_message ON message_processing.Id_message_processing_status_message = status_message.Id_status_message " +
                        " WHERE staff.Id_staff  ='" + idModeration + "'  " + str + "";
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
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;

                    dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[15].Visible = false;
                dataGridView1.Columns[16].Visible = false;
                dataGridView1.Columns[17].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО пользователя";
                dataGridView1.Columns[11].HeaderText = "Категория";
                dataGridView1.Columns[9].HeaderText = "Дата";
                dataGridView1.Columns[10].HeaderText = "Федеральный округ";
                dataGridView1.Columns[14].HeaderText = "Процесс";

                connection.Close();
            }
            connection.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                connection.Open();
               // cmd.CommandText = "INSERT INTO popular_group (content,Id_popular_group_event) values(N'" + comboBox5.Text + "','" + 5 + "')";
                
            cmd.CommandText = "INSERT INTO popular_group (content,Id_popular_group_event) values(N'" + comboBox5.Text + "','" + 6 + "')";
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = connection;
                //connection.Open();
                //cmd.CommandText = "INSERT INTO popular_group (content,Id_popular_group_event) values(N'" + comboBox5.Text + "','" + 5 + "')";
                

            cmd.ExecuteNonQuery();
            connection.Close();
            popular_groupLoad();
            }
            catch (Exception ex)
            {
                connection.Close();
            }


        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            popular_groupLoad();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online=0 Where Id_staff='" + idModeration + "'";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            connection.Close();
            this.Close();
            f.Activate();
            f.ClearForm();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            string qs = @"INSERT INTO dbo.[message_processing](Id_message_processing_staff, Id_message_processing_message, Id_message_processing_status_message) VALUES('" + idModeration + "', '" + message + "','" + 4 + "')";
            SqlCommand command = new SqlCommand(qs, connection);
            int Zaversh = command.ExecuteNonQuery();
            connection.Close();
            label10.Text = "Обращение добавлено в обработку";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            string qs = @"INSERT INTO dbo.[message_processing](Id_message_processing_staff, Id_message_processing_message, Id_message_processing_status_message) VALUES('" + idModeration + "', '" + message + "','" + 1 + "')";
            SqlCommand command = new SqlCommand(qs, connection);
            int Zaversh = command.ExecuteNonQuery();
            connection.Close();
            label10.Text = "Обращение отклонено";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            string qs = @"INSERT INTO dbo.[message_processing](Id_message_processing_staff, Id_message_processing_message, Id_message_processing_status_message) VALUES('" + idModeration + "', '" + message + "','" + 3 + "')";
            SqlCommand command = new SqlCommand(qs, connection);
            int Zaversh = command.ExecuteNonQuery();
            connection.Close();
            label10.Text = "Обращение завершено";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            string qs = @"INSERT INTO dbo.[popular_messages](Id_popular_messages_message, Id_popular_messages_popular_group, vote) VALUES('" + message + "', '" + Convert.ToInt32(id.Text) + "','" + 0 + "')";
            SqlCommand command = new SqlCommand(qs, connection);
            int Zaversh = command.ExecuteNonQuery();
            connection.Close();
            label10.Text = "Обращение добавлено в популярные обращения";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox6.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[8].Value.ToString();
                message = Convert.ToInt32(row.Cells[15].Value.ToString());
                id.Text = row.Cells[16].Value.ToString();
                axWindowsMediaPlayer1.URL = row.Cells[17].Value.ToString();
            }
        }
    }
}


