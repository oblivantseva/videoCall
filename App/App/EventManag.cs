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
using System.Text.RegularExpressions;

namespace App
{
    public partial class EventManagement : Form
    {
        public DataSet DS;
        public SqlConnection connection = new SqlConnection(); int idAdmin;
        public SqlCommand command = new SqlCommand(); Menu f;
        public string stringPath = Properties.Settings.Default.stringPath;
        public EventManagement(string fio, int idType, int idModer, Menu form)

        {
            idAdmin = idModer;
            f = form;
            connection.ConnectionString = stringPath;
            InitializeComponent();
            textBox1.Text = fio;
            print_event(idModer);
            fillTypeStaff(idType); eventsss();
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
        public void print_event(int id)
        {
            //SqlCommand command2 = connection.CreateCommand();

            //command2.CommandText = "GETDATE ( )";

            SqlCommand cmd = new SqlCommand();
            //int Zaversh2 = cmd.ExecuteNonQuery();
            //SqlDataReader dr2 = cmd.ExecuteReader();
            //if (dr2.Read())
            //{
            //    var dat = Convert.ToInt32(dr2[0].ToString());
            //    dr2.Close();
            //}



            //d1.Date.Equals(d2.Date);
            cmd.Connection = connection;
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT events.name,events.datatime_end FROM dbo.events, dbo.staff_event where events.Id_events=staff_event.Id_staff_event_event and staff_event.Id_staff_event_staff='" + id + "'";
            SqlDataReader dr1 = command.ExecuteReader();
            if (dr1.Read())
            {
                textBox5.Text = dr1[0].ToString();
                var strDate1 = Convert.ToDateTime(dr1[1].ToString().Remove(11)).ToString("dd.MM.yyyy");
                string strDate2 = DateTime.Now.ToString("dd.MM.yyyy");
                DateTime d2 = DateTime.ParseExact(strDate2, "dd.MM.yyyy", null);
                //string strDate1 = "select convert(nvarchar(20), getdate(), 104)";
                DateTime d1 = DateTime.ParseExact(strDate1, "dd.MM.yyyy", null);

                if (d1.Date >= d2.Date)
                {
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                }
                else
                {
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                }

                dr1.Close();

            }
            connection.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {

            dataGridView1.Visible = false;
            groupBox1.Visible = true;
            groupBox1.Text = "Добавить";
            Staf();
            dol();

        }
        public void Staf()
        {

            SqlConnection conn1 = new SqlConnection(stringPath);
            conn1.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id_staff, (staff.first_name+' '+staff.second_name+' '+staff.patronymic) as fio FROM staff", conn1);
            SqlCommandBuilder command1 = new SqlCommandBuilder(adapter);
            DataTable ds = new DataTable();
            adapter.Fill(ds);
            comboBox2.ValueMember = "Id_staff";
            comboBox2.DisplayMember = "fio";
            comboBox2.DataSource = ds;
            conn1.Close();
            //string qs = "SELECT * FROM dbo.staff";
            //SqlCommand command = new SqlCommand(qs, connection);
            //System.Data.DataTable tbl = new System.Data.DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(tbl);

            //comboBox2.DataSource = tbl;
            //comboBox2.DisplayMember = "first_name";//+ " second_name "+ " patronymic";

            //comboBox2.ValueMember = "Id_staff";
        }
        public void eventsss()

        {
            string qs = "SELECT * FROM dbo.events";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "Id_events";

        }
        public void VivodD()
        {

            //            //string[] words = tbl.Rows[0][1].ToString().Split(new char[] { ' ' });
            dataGridView1.Visible = true;
            SqlConnection conn = new SqlConnection(stringPath);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id_staff, concat(staff.first_name,' ', staff.second_name ,' ', staff.patronymic) as ФИО,type_staff.type_staff as Должность ,events.name as 'Название мероприятия' FROM staff_event, staff,events,type_staff WHERE staff_event.Id_staff_event = staff.Id_staff and staff.Id_staff_type_staff =type_staff.Id_type_staff and staff_event.Id_staff_event_event= events.Id_events", conn);
            SqlCommandBuilder command = new SqlCommandBuilder(adapter);
            DataTable ds = new DataTable();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.Columns[0].Visible = false;
            conn.Close();
        }

        private void EventManagement_Load(object sender, EventArgs e)
        {
            VivodD();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            VivodD();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = "Update staff set online=0 Where Id_staff='" + idAdmin + "'";
            cmd.ExecuteNonQuery();
            cmd.Clone();
            connection.Close();
            this.Close();
            f.Activate();
            f.ClearForm();
        }
        public void dol()
        {
            string qs = "SELECT * FROM dbo.type_staff";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(tbl);

            comboBox3.DataSource = tbl;
            comboBox3.DisplayMember = "type_staff";
            comboBox3.ValueMember = "Id_type_staff";
        }


        private void button5_Click(object sender, EventArgs e)
        {
            {
                if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
                {

                    MessageBox.Show("Проверьте правильность введеных вами данных!", "Ошибка");
                    return;
                }
                SqlConnection conn = new SqlConnection(stringPath);
                conn.Open();
                dataGridView1.Visible = true;
                groupBox1.Visible = false;
                button1.Enabled = true;
                button2.Enabled = true;
                //SqlCommand ID = new SqlCommand(@"select IdMagazine from Magazine where IdMagazine = (select max(IdMagazine) from Magazine)", conn);//последний id
                //ID.ExecuteScalar();
                string sql = "INSERT INTO staff_event(Id_staff_event_staff,Id_staff_event_event) VALUES ('" + Convert.ToInt32(comboBox2.SelectedValue) + "','" + Convert.ToInt32(comboBox1.SelectedValue) + "')";// "','" + Convert.ToInt32(comboBox3.SelectedValue) + "')";
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                conn.Close();
                VivodD();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {

                if (dataGridView1.SelectedCells.Count != 0)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {

                        SqlConnection conn = new SqlConnection(stringPath);
                        conn.Open();
                        string sql = "DELETE FROM staff_event  WHERE Id_staff_event=" + dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value;
                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        conn.Close();
                        VivodD();
                    }
                }
            }
        }
    }
}




