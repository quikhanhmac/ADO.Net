using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesBases
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cnx = new SqlConnection();
            cnx.ConnectionString = Properties.Settings.Default.Connection1;
            cnx.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select productId, Name, UnitPrice from Product";

            // Mode connecté
            //SqlDataReader rd = cmd.ExecuteReader(); // execute sql
            //while (rd.Read())
            //{
            //    Console.WriteLine("{0} : {1} - {2}",
            //        rd["productId"],
            //        rd["Name"],
            //        rd["UnitPrice"]);
            //}
            //rd.Close();
            //// Modif
            //cmd.CommandText = "update product set Name='AAA' where Productid=1";
            //cmd.ExecuteNonQuery();


            // Mode déconnecté
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            SqlCommandBuilder bd = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable tableResultat = ds.Tables[0];
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (DataRow row in tableResultat.Rows)
            {
                Console.WriteLine("{0} : {1} - {2}",
                    row["ProductId"],
                    row["Name"],
                    row["UnitPrice"]);
            }
            var produit = (from p in tableResultat.AsEnumerable()
                           where p.Field<int>("ProductId") == 1
                           select p).FirstOrDefault();

            if (produit != null)
            {
                produit["Name"] = "Chai";
            }
            da.Update(ds);

            Console.Read();
        }
    }
}
