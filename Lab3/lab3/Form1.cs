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
using System.Threading;

namespace lab3
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataSet shipSet;
        DataSet planetSet;
        Boolean cond = false;
        String connectionString = "DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS;" +
            "Initial Catalog=Airport;Integrated Security=True";
     
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("i do work");
            connection = new SqlConnection("DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS;" +
            "Initial Catalog=Airport;Integrated Security=True");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //label3.Text = "transaction 2 committed";
            Console.WriteLine("transaction 1 committed");
            new Thread(() =>
            {
                //TRANSACTION1
                //MessageBox.Show("ooo");
                int noTries = 0;
                while (noTries < 4 && !cond)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            SqlCommand command = con.CreateCommand();
                            command.CommandText = "SET DEADLOCK_PRIORITY HIGH";
                            command.ExecuteNonQuery();

                            SqlTransaction transaction = con.BeginTransaction();
                            adapter.UpdateCommand = new SqlCommand("UPDATE Technicians SET name='deadlock' WHERE id=22", con);
                            adapter.UpdateCommand.Transaction = transaction;
                            adapter.UpdateCommand.ExecuteNonQuery();
                            System.Threading.Thread.Sleep(2200);
                            adapter.UpdateCommand = new SqlCommand("UPDATE Planes SET type='deadlockPlane' WHERE id=27", con);
                            adapter.UpdateCommand.Transaction = transaction;
                            adapter.UpdateCommand.ExecuteNonQuery();
                            MessageBox.Show("transaction 1 committed");
                            transaction.Commit();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception in transaction1:" + ex.Message);
                        ++noTries;
                        //connection.Close();

                    }
                    if (noTries == 4)
                    {
                        cond = true;
                        MessageBox.Show("Tranaction 1 has given up");
                    }
                }
            }).Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {           //TRANSACTION2
            //label3.Text = "transaction 2 committed";
            //Console.WriteLine("transaction 1 committed");
            new Thread(() =>
            {
                //MessageBox.Show("ooo");
                int noTries = 0;
                while (noTries < 4 && !cond)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            //SqlCommand command = con.CreateCommand();
                            //command.CommandText = "SET DEADLOCK_PRIORITY HIGH";
                            //command.ExecuteNonQuery();
                            SqlTransaction transaction = con.BeginTransaction();
                            adapter.UpdateCommand = new SqlCommand("UPDATE Planes SET type='deadlockPlane' WHERE id=27", con);
                            adapter.UpdateCommand.Transaction = transaction;
                            //connection.Open();
                            adapter.UpdateCommand.ExecuteNonQuery();
                            System.Threading.Thread.Sleep(2200);
                            adapter.UpdateCommand = new SqlCommand("UPDATE Technicians SET name='deadlock' WHERE id=22", con);
                            adapter.UpdateCommand.Transaction = transaction;
                            adapter.UpdateCommand.ExecuteNonQuery();
                            MessageBox.Show("transaction 2 committed");
                            transaction.Commit();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("exception in transaction2:" + ex.Message);
                        ++noTries;
                        //connection.Close();

                    }
                    if (noTries == 4)
                    {
                        cond = true;
                        MessageBox.Show("Transaction 2 has given up");
                    }
                }
            }).Start();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void initializeGrids()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    cond = false;
                    adapter = new SqlDataAdapter();
                    shipSet = new DataSet();
                    planetSet = new DataSet();
                    connection.Open();
                    //shipSource = new BindingSource();
                    if (dataGridView2.RowCount > 0) dataGridView2.Rows[0].Selected = true;
                    adapter.SelectCommand = new SqlCommand("SELECT * FROM Planes", connection);
                    adapter.Fill(shipSet);
                    dataGridView1.DataSource = shipSet.Tables[0];
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;

                    if (dataGridView1.RowCount > 0) dataGridView1.Rows[0].Selected = true;
                    adapter.SelectCommand = new SqlCommand("SELECT * FROM Technicians", connection);

                    planetSet.Clear();
                    adapter.Fill(planetSet);
                    dataGridView2.DataSource = planetSet.Tables[0];

                    dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView2.MultiSelect = false;
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("something went wrong");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initializeGrids();
        }
    }
}
