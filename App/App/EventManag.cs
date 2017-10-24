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
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();
        public string stringPath = Properties.Settings.Default.stringPath;
        public EventManagement()

        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
          dataGridView1.Visible = false;
            groupBox1.Visible = true;
            groupBox1.Text = "Добавить";
        }
        public void VivodD()
        {

          //string[] words = tbl.Rows[0][1].ToString().Split(new char[] { ' ' });
            dataGridView1.Visible = true;
        SqlConnection conn = new SqlConnection(stringPath);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id_staff, concat(staff.first_name,' ', staff.second_name ,' ', staff.patronymic) as ФИО,type_staff.type_staff as Должность,events.name as 'Название мероприятия' FROM staff_event, staff,events,type_staff WHERE staff_event.Id_staff_event = staff.Id_staff and staff.Id_staff_type_staff =type_staff.Id_type_staff and staff_event.Id_staff_event_event= events.Id_events", conn);
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
    }
}

    

      