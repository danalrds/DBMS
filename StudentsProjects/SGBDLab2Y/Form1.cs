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

namespace SGBDLab2Y
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter daMembers, daTasks;
        DataSet ds;
        SqlCommandBuilder cmdBuilder;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Successfully to the Database");
            daTasks.Update(ds, "Tasks");
            Form1_Load(null, null);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS; Initial Catalog=StudentsProjects; Integrated Security=true;");
            ds = new DataSet();
            daMembers = new SqlDataAdapter("SELECT * FROM Members", conn);
            daTasks = new SqlDataAdapter("SELECT * FROM Tasks", conn);
            daMembers.Fill(ds, "Members");
            daTasks.Fill(ds, "Tasks");
            cmdBuilder = new SqlCommandBuilder(daTasks);

            DataRelation dr = new DataRelation("FK_Members_Taks", ds.Tables["Members"].Columns["mid"], ds.Tables["Tasks"].Columns["memberid"]);
            ds.Relations.Add(dr);

            DataTable dt = new DataTable();
            daMembers.Fill(dt);

            comboBox1.DataSource = ds.Tables["Members"];
            comboBox1.DisplayMember = "name";
            dataGridView2.DataSource = comboBox1.DataSource;  
            dataGridView2.DataMember = dr.ToString();
        }
    }
}
