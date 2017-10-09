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
            string sql1 = "SELECT        SotrTable.ID_S, SkillsTable.NameSkill, Skill_Sotr.LevelSkill,SkillsTable.Id_Skill FROM            Skill_Sotr INNER JOIN " +
                    "  SkillsTable ON Skill_Sotr.FK_Skill = SkillsTable.Id_Skill INNER JOIN SotrTable ON Skill_Sotr.FK_Sotr = SotrTable.ID_S " +
                       "WHERE(SotrTable.ID_S = '" + (-1) + "') ";


            string qs = "SELECT dbo.popular_group.content,dbo.events.name,dbo.popular_messages.vote,dbo.federal_districts.federal_districts FROM dbo.popular_group INNER JOIN  dbo.events  "+
                "ON dbo.popular_group.Id_popular_group_event = dbo.events.Id_events INNER JOIN dbo.popular_messages ON  ";
            SqlCommand command = new SqlCommand(qs, connection);
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);//
            da.Fill(tbl);
            //dataGridView1.DataSource= tbl;
            //dataGridView1.Columns[0]= "Id_popular_group";



  using (SqlConnection connection = new SqlConnection(stringPath))
            {
                connection.Open();
                // Создаем объект DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(qs, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                // Отображаем данные
                dataGridView1.DataSource = ds.Tables[0];

            

        }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                comboBox1.Enabled = true;
            else comboBox1.Enabled = false;
        }
    }
}
