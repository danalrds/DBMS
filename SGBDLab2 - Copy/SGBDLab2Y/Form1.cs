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
        SqlDataAdapter daAirports, daTerminals;
        DataSet ds;
        SqlCommandBuilder cmdBuilder;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Successfully to the Database");
            daTerminals.Update(ds, "Terminals");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS; Initial Catalog=MajorAirline; Integrated Security=true;");
            ds = new DataSet();
            daAirports = new SqlDataAdapter("SELECT * FROM Airport", conn);
            daTerminals = new SqlDataAdapter("SELECT * FROM Terminal", conn);
            daAirports.Fill(ds, "Airports");
            daTerminals.Fill(ds, "Terminals");
            cmdBuilder = new SqlCommandBuilder(daTerminals);

            DataRelation dr = new DataRelation("FK_Airports_Terminals", ds.Tables["Airports"].Columns["id"], ds.Tables["Terminals"].Columns["airpid"]);
            ds.Relations.Add(dr);

            dataGridView1.DataSource = ds.Tables["Airports"];
            dataGridView2.DataSource = dataGridView1.DataSource;  //chaining dataGridView2 to dataGridView1
            dataGridView2.DataMember = dr.ToString();
        }
    }
}
