using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Lab1
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter daParent, daChild;
        BindingSource bsParent, bsChild;
        int selectedParentId, selectedChildId;
        String connString;
        Dictionary<String, String> settings = new Dictionary<String, String>();

        public Form1()
        {
            InitializeComponent();

        }
        public void setConfiguration()
        {
            List<string> mylist = new List<string>(new string[] { "connString1", "connString2", "parent", "child",
                "selectAllParent", "selectAllChild", "foreignKey", "keyParent","keyChild", "foreignKeyChild",
            "insertQuery", "deleteQuery", "updateQuery",
            "primaryKeyChild", "column1", "column2"});
            foreach (var item in mylist)
            {
                settings[item] = ConfigurationManager.AppSettings.Get(item);
            }
            connString = settings["connString1"] + "\\" + settings["connString2"];
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            setConfiguration();
            ds = new DataSet();
            conn = new SqlConnection(connString);
            daParent = new SqlDataAdapter(settings["selectAllParent"], conn);
            daChild = new SqlDataAdapter(settings["selectAllChild"], conn);

            daParent.Fill(ds, settings["parent"]);
            daChild.Fill(ds, settings["child"]);

            DataRelation dr = new DataRelation(settings["foreignKey"], ds.Tables[settings["parent"]].Columns[settings["keyParent"]],
                ds.Tables[settings["child"]].Columns[settings["foreignKeyChild"]]);
            ds.Relations.Add(dr);

            bsParent = new BindingSource();
            bsChild = new BindingSource();

            bsParent.DataSource = ds;
            bsParent.DataMember = settings["parent"];
            bsChild.DataSource = bsParent;
            bsChild.DataMember = settings["foreignKey"];

            dataGridView1.DataSource = bsParent;
            dataGridView2.DataSource = bsChild;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand(settings["insertQuery"], conn);
            da.InsertCommand.Parameters.Add(settings["column1"], SqlDbType.VarChar).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add(settings["column2"], SqlDbType.Int).Value = selectedParentId;
            da.InsertCommand.ExecuteNonQuery();
            MessageBox.Show("Inserted Successfully to the Database");
            Form1_Load(sender, e);
            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand(settings["deleteQuery"], conn);
            da.DeleteCommand.Parameters.Add(settings["primaryKeyChild"], SqlDbType.Int).Value = selectedChildId;
            da.DeleteCommand.ExecuteNonQuery();
            MessageBox.Show("Deleted successfully from the database!");
            Form1_Load(sender, e);
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.UpdateCommand = new SqlCommand(settings["updateQuery"], conn);
            da.UpdateCommand.Parameters.Add(settings["column1"], SqlDbType.VarChar).Value = textBox2.Text;
            da.UpdateCommand.Parameters.Add(settings["primaryKeyChild"], SqlDbType.Int).Value = selectedChildId;
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                selectedChildId = (int)row.Cells[settings["keyChild"]].Value;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                selectedParentId = (int)row.Cells[settings["keyParent"]].Value;
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
