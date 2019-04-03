using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;

namespace SGBDLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sql Connection
            String conn = "DATA SOURCE=DESKTOP-795JE86\\SQLEXPRESS;" +
                "Initial Catalog=MajorAirline; Integrated Security=true;";
            //Open Connection
            SqlConnection sqlconn = new SqlConnection(conn);
            try
            {
                sqlconn.Open();
                Console.WriteLine("Conn opnened!");
                //Retrieve data from a table
                SqlCommand com1 = new SqlCommand("SELECT id,type FROM Plane",sqlconn);
                SqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows) {
                    while (reader1.Read()) {
                        Console.WriteLine(reader1.GetInt32(0));
                        Console.WriteLine(reader1.GetSqlString(1));

                    }
                }
                reader1.Close();
                //defining a dataset
                DataSet dset = new DataSet();
                SqlDataAdapter planeAdapter = new SqlDataAdapter("SELECT * FROM Plane",sqlconn);
                planeAdapter.Fill(dset,"Plane");  //saame for child table //then add the relation to the dataset
            }
            catch (Exception e) {
                Console.Write("error");
            }
            finally {
                sqlconn.Close();
            }


        }
    }
}
