using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Contexte
    {
        public static List<string> GetPaysFournisseur()
        {
            {
                var listePaysFournisseur = new List<string>();

                // On créé une commande et on définit le code sql à exécuter
                var cmd = new SqlCommand();
                cmd.CommandText = @"select distinct A.Country
from Address A
inner
join Supplier S on S.AddressId = A.AddressId
Order by 1";

                // On crée une connexion à partir de la chaîne de connexion stockée
                // dans les paramètres de l'appli
                using (var cnx = new SqlConnection(Settings.Default.Northwind2))
                {
                    // On affecte la connexion à la commande
                    cmd.Connection = cnx;
                    // On ouvre la connexion
                    cnx.Open();

                    // On exécute la commande en récupérant son résultat dans un objet SqlDataRedader
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            listePaysFournisseur.Add((string)sdr["Country"]);
                        }

                    }
                }
                // Le fait d'avoir créé la connexion dans une instruction using
                // permet de fermer cette connexion automatiquement à la fin du bloc using

                return listePaysFournisseur;
                ;

            }
        }
//        public static List<string> GetFournisseurs()
//        {
//            var listeFournisseurs = new List<string>();

//            // On créé une commande et on définit le code sql à exécuter
//            var cmd = new SqlCommand();
//            cmd.CommandText = @"select distinct S.SupplierId, S.CompanyName
//from Supplier S
//inner join Address A on A.AddressId = S.AddressId
//group by S.SupplierId, S.CompanyName
//order by S.SupplierId";
//            var param = new SqlParameter()
//            {
//                SqlDbType = SqlDbType.NVarChar,
//                ParameterName = "@Country",
//                Value = 
//            };
//            // Ajout à la collection des paramètres de la commande
//            cmd.Parameters.Add(param);

//            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
//            {
//                cmd.Connection = cnx;
//                cnx.Open();
//                cmd.ExecuteNonQuery();
//            }

//        }
        public static List<int> GetNbProduit()
        {
            var listePaysFournisseur = new List<string>();

            // On créé une commande et on définit le code sql à exécuter
            var cmd = new SqlCommand();
            cmd.CommandText = @"select distinct A.Country, count(*) NbProduit
from Product P
inner join Supplier S on S.SupplierId = P.SupplierId
inner join Address A on A.AddressId = S.AddressId
group by A.Country";
            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                // On affecte la connexion à la commande
                cmd.Connection = cnx;
                // On ouvre la connexion
                cnx.Open();

                // On exécute la commande en récupérant son résultat dans un objet SqlDataRedader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        listePaysFournisseur.Add((string)sdr["Country"]);
                    }

                }
            }
            // Le fait d'avoir créé la connexion dans une instruction using
            // permet de fermer cette connexion automatiquement à la fin du bloc using

            return listePaysFournisseur;
        }
    }

}
