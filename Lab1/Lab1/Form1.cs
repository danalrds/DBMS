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

namespace Lab1
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter daTechTeams, daEngineers;
        BindingSource bsTechTeams, bsEngineers;
        int selectedTeamId, selectedEngineerId;

        public Form1()
        {
            InitializeComponent();

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            String connString = "DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS; Initial Catalog=MajorAirline; Integrated Security=true;";
            conn = new SqlConnection(connString);
            ds = new DataSet();
            daTechTeams = new SqlDataAdapter("SELECT * FROM TechTeam", conn);
            daEngineers = new SqlDataAdapter("SELECT * FROM Engineer", conn);

            daTechTeams.Fill(ds, "TechTeams");
            daEngineers.Fill(ds, "Engineers");

            DataRelation dr = new DataRelation("FK_TechTeams_Engineers", ds.Tables["TechTeams"].Columns["id"], ds.Tables["Engineers"].Columns["tid"]);
            ds.Relations.Add(dr);

            bsTechTeams = new BindingSource();
            bsEngineers = new BindingSource();

            bsTechTeams.DataSource = ds;
            bsTechTeams.DataMember = "TechTeams";
            bsEngineers.DataSource = bsTechTeams;
            bsEngineers.DataMember = "FK_TechTeams_Engineers";

            dataGridView1.DataSource = bsTechTeams;
            dataGridView2.DataSource = bsEngineers;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("INSERT Engineer VALUES (@name,@tid) ", conn);
            da.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add("@tid", SqlDbType.Int).Value = selectedTeamId;
            //throw new Exception(selectedTeamId.ToString());
            da.InsertCommand.ExecuteNonQuery();
            MessageBox.Show("Inserted Successfully to the Database");
            Form1_Load(sender, e);
            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand("DELETE FROM Engineer WHERE id=@engId", conn);
            da.DeleteCommand.Parameters.Add("@engId", SqlDbType.Int).Value = selectedEngineerId;
            da.DeleteCommand.ExecuteNonQuery();
            MessageBox.Show("Deleted successfully from the database!");
            Form1_Load(sender, e);
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand("UPDATE Engineer SET name=@newName WHERE id=@engineerId", conn);
            da.UpdateCommand.Parameters.Add("@newName", SqlDbType.VarChar).Value = textBox2.Text;
            da.UpdateCommand.Parameters.Add("@engineerId", SqlDbType.Int).Value = selectedEngineerId;
            da.UpdateCommand.ExecuteNonQuery();
            MessageBox.Show("Engineer updated successfully!");
            Form1_Load(sender, e);
            conn.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)        {
            
            if (e.RowIndex >= 0)
            { 
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                selectedEngineerId = (int)row.Cells["id"].Value;               
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                selectedTeamId = (int)row.Cells["id"].Value;
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void DataGridView2_Click(object sender, EventArgs e)
        {

        }
    }
}
