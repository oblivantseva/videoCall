﻿using System;
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
    public partial class Moderation : Form
    {
        public Moderation(string fio)
        {
            InitializeComponent();
            // textBox3.Text = eventName;
            textBox1.Text = fio;
            //textBox2.Text = dolgn;

            {
                InitializeComponent();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Moderation_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Menu M = new Menu();
            M.Show();

        }
    }
    }


