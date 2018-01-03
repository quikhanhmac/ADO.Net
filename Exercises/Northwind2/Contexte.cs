using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    enum Operation { Ajouter,Modifier}
    public class Contexte
    {
        public static List<string> GetpaysFournisseur()
        {
            var listPaysFournisseurs = new List<string>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select distinct A.Country from Address A
innerjoin Supplier S on S.AddressId = A.AddressId
order by 1";
            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                cmd.Connection = cnx;
                cnx.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())// 
                {
                    while (sdr.Read())
                    {
                        listPaysFournisseurs.Add((string)sdr["Country"]);
                    }
                }
            }
            // Le fait d'avoir créé la connexion dans une instruction using
            // permet de fermer cette connexion automatiquement à la fin du bloc using
            return listPaysFournisseurs;
        }
        public static List<string> GetFournisseur(string pays)
        {
            var listeFournisseurs = new List<string>();
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
                cmd.ExecuteNonQuery();
            }
            return listeFournisseurs;
        }
        public static int GetNbProduit(string pays)
        {

            // On créé une commande et on définit le code sql à exécuter 
            var cmd = new SqlCommand();
            cmd.CommandText = @"select count(*) NbProduit 
from Product P 
inner join Supplier S on S.SupplierId = P.SupplierId 
inner join Address A on A.AddressId = S.AddressId 
group by A.Country";
            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {

                cmd.Connection = cnx;
                cnx.Open();
                return (int)cmd.ExecuteScalar();
            }

            // Le fait d'avoir créé la connexion dans une instruction using 
            // permet de fermer cette connexion automatiquement à la fin du bloc using 

        }

        public static List<Categorie> GetCategories()
        {
            var listCategories = new List<Categorie>();

            var cmd = new SqlCommand();
            cmd.CommandText = @"select CategoryId,Name,Description
from Category";

            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                cmd.Connection = cnx;
                cnx.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())// 
                {
                    while (sdr.Read())
                    {
                        var categorie = new Categorie();
                        categorie.CategoryId = (Guid)sdr["CategoryId"];
                        categorie.Name = (string)sdr["Name"];
                        categorie.Description = (string)sdr["Description"];
                        listCategories.Add(categorie);
                    }
                }
            }
            return listCategories;
        }

        public static List<Produit> GetProduits(Guid Id)
        {
            var listeProduits = new List<Produit>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select p.ProductId,P.Name, P.UnitPrice,P.UnitsInStock
from Product P
where P.CategoryId = @Id
order by P.Name";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                ParameterName = "@Id",
                Value = Id

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
                        var produit = new Produit();
                        produit.ProductID = (int)sdr["ProductId"];
                        produit.Name = (string)sdr["Name"];
                        produit.UnitPrice = (Decimal)sdr["UnitPrice"];
                        produit.UnitsInstock = (Int16)sdr["UnitsInstock"];
                        listeProduits.Add(produit);
                    }
                }
            }
            return listeProduits;
        }
        public static void AjouterProduit(Guid CategoryId, string nom, int IdF, decimal PrixUnitaire, Int16 UniteEnStock)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"insert Product(CategoryId,Name,SupplierId,UnitPrice,UnitsInStock) 
values (@Idcat,@Nom,@IdF,@PrixUnitaire,@UniteEnStock)";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                ParameterName = "@Idcat",
                Value = CategoryId
            };
            var param1 = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@Nom",
                Value = nom,
            };
            var param2 = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@IdF",
                Value = IdF
            };
            var param3 = new SqlParameter
            {
                SqlDbType = SqlDbType.Decimal,
                ParameterName = "@PrixUnitaire",
                Value = PrixUnitaire
            };
            var param4 = new SqlParameter
            {
                SqlDbType = SqlDbType.SmallInt,
                ParameterName = "@UniteEnStock",
                Value = UniteEnStock
            };
            cmd.Parameters.Add(param);
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);

            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public 
}




