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
        SqlDataAdapter daClasses, daSpecies;
        DataSet ds;
        SqlCommandBuilder cmdBuilder;
        BindingSource bsCategories, bsDrinks;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Successfully to the Database");
            daSpecies.Update(ds, "Species");
            Form1_Load(null, null);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS; Initial Catalog=Zulu; Integrated Security=true;");
            ds = new DataSet();
            daClasses = new SqlDataAdapter("SELECT * FROM Classes", conn);
            daSpecies = new SqlDataAdapter("SELECT * FROM Species", conn);
            daClasses.Fill(ds, "Classes");
            daSpecies.Fill(ds, "Species");
            cmdBuilder = new SqlCommandBuilder(daSpecies);

            DataRelation dr = new DataRelation("FK_Classes_Species", ds.Tables["Classes"].Columns["cid"], ds.Tables["Species"].Columns["classid"]);
            ds.Relations.Add(dr);

            DataTable dt = new DataTable();
            daClasses.Fill(dt);

            comboBox1.DataSource = ds.Tables["Classes"];
            comboBox1.DisplayMember = "name";
            dataGridView2.DataSource = comboBox1.DataSource; 
            dataGridView2.DataMember = dr.ToString();
        }
    }
}
