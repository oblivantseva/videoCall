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
            string qs= " ";
            qs = "SELECT       popular_group.Id_popular_group, popular_group.[content], messages.message_text, federal_districts.federal_districts, popular_messages.vote " +
                   "FROM popular_messages INNER JOIN " +
                   " messages ON popular_messages.Id_popular_messages_message = messages.Id_message INNER JOIN " +
                   " popular_group ON popular_messages.Id_popular_messages_popular_group = popular_group.Id_popular_group INNER JOIN " +
                   " [user] ON messages.Id_message_user = [user].Id_user INNER JOIN " +
                   " federal_districts ON[user].id_user_federal_districts = federal_districts.Id_federal_districts";

            if (comboBox1.SelectedIndex == -1)
            {
                
            }
            else { try
                {
                    qs = "SELECT       popular_group.Id_popular_group, popular_group.[content], messages.message_text, federal_districts.federal_districts, popular_messages.vote " +
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
           dataGridView1.Columns[0].HeaderText = "Популярная группа";
            dataGridView1.Columns[1].HeaderText = "Обращение";
            dataGridView1.Columns[2].HeaderText = "Федеральный округ";
            dataGridView1.Columns[3].HeaderText = "Голоса";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                comboBox1.Enabled = true;
            else { comboBox1.Enabled = false; comboBox1.SelectedIndex = -1; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  MessageBox.Show(comboBox1.SelectedValue.ToString());
           
   
               EventP_Load(sender, e);
        }
    }
}
