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
    public partial class ReceptionCalls : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection(); Menu f;
        public SqlCommand command = new SqlCommand(); int idOp;
        public string stringPath = Properties.Settings.Default.stringPath;
        public ReceptionCalls(string fio, int idType, int idOper, Menu form)
        {
            connection.ConnectionString = stringPath;
            idOp = idOper;
            InitializeComponent();
            fillTypeStaff(idType);
            textBox1.Text = fio;
            print_event(idOper);
            onlineTrue(idOper);
            categor_Load(); federals_Load(); popular_groupLoad();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
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
                textBox9.Text = dr1[0].ToString();
                dr1.Close();
            }
            connection.Close();

        }
        public void onlineTrue(int id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online=1 Where Id_staff='" + id + "'";
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
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void ReceptionCalls_Load(object sender, EventArgs e)
        {
           id.Text = textBox9.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

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

            comboBox3.DataSource = tbl;
            comboBox3.DisplayMember = "content";
            comboBox3.ValueMember = "Id_popular_group";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online=0 Where Id_staff='" + idOp + "'";
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
                string qs = @"INSERT INTO dbo.[user](first_name, second_name, patronymic, telephone, age, id_user_federal_districts) VALUES('" + fam.Text + "', '" + name.Text + "','" + otch.Text + "', '" + telephone.Text + "','" + age.Text + "', '" + Convert.ToInt32(comboBox1.SelectedValue) + "')";
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
Id_message_event,  Id_message_message_categories) 
VALUES ('12.12.2017 0:00:00','4', '1','ляля','1','1')";
                    //                        command2.CommandText = @"INSERT INTO dbo.[messages](datatime, Id_message_user, Id_message_type_message, message_text, 
                    //Id_message_event,  Id_message_message_categories) 
                    //VALUES ('12.12.2017 0:00:00','" + idO + "', '1','" + quest.Text +
                    //                        "','" + Convert.ToInt32(id.Text) + "','" + Convert.ToInt32(comboBox2.SelectedValue) + "')";
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
            fam.Clear();name.Clear();otch.Clear();telephone.Clear();age.Clear();quest.Clear();
            button3.Text = "Указать как популярное";
            button3.Enabled = true;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = "Указано";
            button3.Enabled = false;
        }
    }
}
