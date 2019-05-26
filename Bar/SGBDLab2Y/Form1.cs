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

namespace SGBDLab2Y
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter daCategories, daDrinks;
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
            daDrinks.Update(ds, "Drinks");
            Form1_Load(null, null);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS; Initial Catalog=Bar; Integrated Security=true;");
            ds = new DataSet();
            daCategories = new SqlDataAdapter("SELECT * FROM Categories", conn);
            daDrinks = new SqlDataAdapter("SELECT * FROM Drinks", conn);
            daCategories.Fill(ds, "Categories");
            daDrinks.Fill(ds, "Drinks");
            cmdBuilder = new SqlCommandBuilder(daDrinks);

            DataRelation dr = new DataRelation("FK_Categories_Drinks", ds.Tables["Categories"].Columns["cid"], ds.Tables["Drinks"].Columns["categoryId"]);
            ds.Relations.Add(dr);

            DataTable dt = new DataTable();
            daCategories.Fill(dt);

            comboBox1.DataSource = ds.Tables["Categories"];
            comboBox1.DisplayMember = "name";
            dataGridView2.DataSource = comboBox1.DataSource;  //chaining dataGridView2 to dataGridView1
            dataGridView2.DataMember = dr.ToString();
        }
    }
}
