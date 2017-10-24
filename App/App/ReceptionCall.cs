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
            f = form;
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
            command.CommandText = "SELECT events.name,Id_events FROM dbo.events, dbo.staff_event where events.Id_events=staff_event.Id_staff_event_event and staff_event.Id_staff_event_staff='" + id + "'";
            SqlDataReader dr1 = command.ExecuteReader();
            if (dr1.Read())
            {
                textBox9.Text = dr1[0].ToString();
                idTb.Text = dr1[1].ToString();
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
            if (comboBox1.Text != "" && quest.Text != "" && comboBox2.Text != "")
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
                    Id_message_event,  Id_message_message_categories,processed) 
                    VALUES (GETDATE ( ),'" + idO + "', '1', N'" + quest.Text +
                        "','" + Convert.ToInt32(idTb.Text) + "','" + Convert.ToInt32(comboBox2.SelectedValue) + "',1)";
                        int Zaversh2 = command2.ExecuteNonQuery();
                        if (Zaversh2 != 0)
                        {
                            connection.Close();
                            ModerOnline(); 

                            MessageBox.Show("Обращение отправлено.");
                        }
                        else
                        {
                            connection.Close();

                            MessageBox.Show("Ошибка при отправке обращения.");
                        }
                    
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при отправке обращения.");
                }
                connection.Close();
                //
                button3.Text = "Указать как популярное";
                button3.Enabled = true;

                label11.Text = "";
            }
            else { label11.Text = "Федеральный округ,текст сообщения и категория сообщения-поля обязательные для заполнения!!! "; }

        }
        public void ModerOnline()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            int idM = 0;
            connection.Open();
            cmd.CommandText = "SELECT TOP 1 Id_message FROM dbo.[messages] ORDER BY Id_message DESC";
            cmd.ExecuteNonQuery();
            SqlDataReader dr1 = cmd.ExecuteReader();
            if (dr1.Read())
            {
                idM = Convert.ToInt32(dr1[0].ToString());
                dr1.Close();
                connection.Close();
               // SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                int idS = 0;
                connection.Open();
                cmd.CommandText = "SELECT TOP 1 Id_staff FROM dbo.[staff] Where Id_staff_type_staff='"+3+"' And online='true'";
                cmd.ExecuteNonQuery();
                SqlDataReader dr2 = cmd.ExecuteReader();
                if (dr2.Read())
                {
                    idS = Convert.ToInt32(dr2[0].ToString());
                    dr2.Close();
                    connection.Close();
                }
                else
                {
                    idS = 4;
                    dr2.Close();
                    connection.Close();
                }
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.CommandText = "INSERT INTO message_processing (Id_message_processing_staff,Id_message_processing_message,Id_message_processing_status_message) values('" + idM + "','" + idM + "','" + 2 + "')";

                    cmd.ExecuteNonQuery();
                    connection.Close();
                    //button3.Text = "Указано";
                    //button3.Enabled = false;
                label11.Text = "";
                    //fam.Clear(); name.Clear(); otch.Clear(); telephone.Clear(); age.Clear(); quest.Clear();
                    //comboBox1.SelectedIndex = -1;
                    //comboBox2.SelectedIndex = -1;
                    //comboBox3.SelectedIndex = -1;
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (comboBox3.Text!="")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                int idM=0;
                connection.Open();
                cmd.CommandText = "SELECT TOP 1 Id_message FROM dbo.[messages] ORDER BY Id_message DESC";
                cmd.ExecuteNonQuery();
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read())
                {
                    idM = Convert.ToInt32(dr1[0].ToString());
                    dr1.Close();
                    connection.Close();
                
                cmd.Connection = connection;
                connection.Open();
                cmd.CommandText = "INSERT INTO popular_messages (Id_popular_messages_message,Id_popular_messages_popular_group,vote) values('" + idM+ "','" + Convert.ToInt32(comboBox3.SelectedValue) + "','" + 1 + "')";

                cmd.ExecuteNonQuery();
                connection.Close();
                button3.Text = "Указано";
                button3.Enabled = false;label11.Text = "";
                fam.Clear(); name.Clear(); otch.Clear(); telephone.Clear(); age.Clear(); quest.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                }
            }
            else { label11.Text = "Выберите популярную группу"; }
     
        }

        private void ReceptionCalls_Load(object sender, EventArgs e)
        {

        }
    }
}
