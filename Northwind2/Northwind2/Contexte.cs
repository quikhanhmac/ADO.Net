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
                cmd.CommandText = @"select  distinct A.Country, S.SupplierId,S.CompanyName
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
            }
        }
        public static List<Supplier> GetFournisseurs(string pays)
        {
            var listeFournisseurs = new List<Supplier>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select S.SupplierId, S.CompanyName
                                from Address A
                                inner join Supplier S on S.AddressId = A.AddressId
                                where A.Country = @id";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@id",
                Value = pays
            };
            cmd.Parameters.Add(param);
            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                cmd.Connection = cnx;
                cnx.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var fournisseur = new Supplier();
                        fournisseur.SupplierId = (int)sdr["SupplierId"];
                        fournisseur.CompanyName = (string)sdr["CompanyName"];
                        listeFournisseurs.Add(fournisseur);
                    }
                }
            }
            return listeFournisseurs;
        }
        public static int GetNbProduits(string nom)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"select  count(*) NbProduits 
from Product P
inner join supplier S on S.SupplierId = P.SupplierId
inner join Address A on A.AddressId =S.AddressId
where A.Country = @nom";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@nom",
                Value = nom
            };
            cmd.Parameters.Add(param);
            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                cmd.Connection = cnx;
                cnx.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        public static List<Category> GetCategories()
        {
            var listeCategories = new List<Category>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select CategoryId,Name,Description
from Category";
            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                cmd.Connection = cnx;
                cnx.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var categorie = new Category();
                        categorie.CategoryId = (Guid)sdr["CategoryId"];
                        categorie.Name = (string)sdr["Name"];
                        categorie.Description = (string)sdr["Description"];
                        listeCategories.Add(categorie);
                    }
                }
            }
            return listeCategories;
        }
    }
}


