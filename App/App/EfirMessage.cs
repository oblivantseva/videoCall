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
        public SqlConnection connection = new SqlConnection();
        public SqlCommand command = new SqlCommand();
        public string stringPath = Properties.Settings.Default.stringPath;
        public EfirAppeals(string fio, int idType, int idOper)
        {
            connection.ConnectionString = stringPath;
            InitializeComponent();
            textBox1.Text = fio;
            fillTypeStaff(idType);
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
        private void EfirAppeals_Load(object sender, EventArgs e)
        {

        }
    }
}
