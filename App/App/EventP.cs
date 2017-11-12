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

        public int id_mes;
        public EventP(string nameEvent)
        {
            InitializeComponent();
            connection.ConnectionString = stringPath;
            command.Connection = connection;
            textBox1.Text = nameEvent;
            eventp_Load();
            comboBox1.SelectedIndex = -1;
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

            formLoad();
        }

        private void formLoad()
        {
            string qs = " ";
            qs = "SELECT       popular_group.Id_popular_group, popular_group.[content], messages.message_text, federal_districts.federal_districts, popular_messages.vote, popular_messages.Id_popular_messages, messages.media_content  " +
                   "FROM popular_messages INNER JOIN " +
                   " messages ON popular_messages.Id_popular_messages_message = messages.Id_message INNER JOIN " +
                   " popular_group ON popular_messages.Id_popular_messages_popular_group = popular_group.Id_popular_group INNER JOIN " +
                   " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                   " federal_districts ON[user].id_user_federal_districts = federal_districts.Id_federal_districts";

            if (comboBox1.SelectedIndex == -1)
            {

            }
            else
            {
                try
                {


                    qs = "SELECT       popular_group.Id_popular_group, popular_group.[content], messages.message_text, federal_districts.federal_districts, popular_messages.vote, popular_messages.Id_popular_messages, messages.media_content   " +

              "FROM popular_messages INNER JOIN " +
              " messages ON popular_messages.Id_popular_messages_message = messages.Id_message INNER JOIN " +
              " popular_group ON popular_messages.Id_popular_messages_popular_group = popular_group.Id_popular_group INNER JOIN " +
              " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
              " federal_districts ON[user].id_user_federal_districts = federal_districts.Id_federal_districts" +
              "  WHERE(popular_group.Id_popular_group =" + Convert.ToInt32(comboBox1.SelectedValue) + ")";
                }
                catch { }
            }

            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);//
            da.Fill(tbl);

            using (SqlConnection connection = new SqlConnection(stringPath))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(qs, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Популярная группа";
            dataGridView1.Columns[2].HeaderText = "Обращение";
            dataGridView1.Columns[3].HeaderText = "Федеральный округ";
            dataGridView1.Columns[4].HeaderText = "Голоса";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int vote = 0;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT vote FROM dbo.popular_messages where Id_popular_messages='" + id_mes + "'";
            SqlDataReader dr1 = command.ExecuteReader();
            if (dr1.Read())
            {
                vote = Convert.ToInt32(dr1[0].ToString());
                dr1.Close();
            }
            vote++;
            string qs = "Update dbo.popular_messages set vote='" + vote + "' Where Id_popular_messages ='" + id_mes + "'";
            command = new SqlCommand(qs, connection);
            int Zaversh = command.ExecuteNonQuery();
            connection.Close();
            label5.Text = "Вы проголосовали за данное обращение";
            dataGridView1.Refresh();
            formLoad();
            button1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                comboBox1.Enabled = true;
            else { comboBox1.Enabled = false; comboBox1.SelectedIndex = -1; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
               EventP_Load(sender, e);
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
                id_mes = Convert.ToInt32(row.Cells[5].Value.ToString());
                axWindowsMediaPlayer1.URL = row.Cells[6].Value.ToString();
                button1.Enabled = true;

            }
        }
    }
}
