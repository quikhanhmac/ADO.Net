﻿using System;
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
        public static List<Product> GetProduits(Guid IdCate)
        {
            var listeProduits = new List<Product>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select P.ProductId, P.Name,P.UnitPrice,P.UnitsInStock
from Product P
where P.CategoryId = @IdCate
Order by P.ProductId";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                ParameterName = "@IdCate",
                Value = IdCate
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
                        var product = new Product();
                        product.ProductId = (int)sdr["ProductId"];
                        product.Name = (string)sdr["Name"];
                        product.Unitprice = (decimal)sdr["Unitprice"];
                        listeProduits.Add(product);
                    }
                }
            }
            return listeProduits;
        }
        public static List<Customer> GetClientsCommandes()
        {
            var listeClientsCommandes = new List<Customer>();

            // Obtient la liste des régions et de leurs teritoires associés
            // triés par id de région et de territoire
            string req = @"select C.CustomerId, C.CompanyName,O.OrderId,O.OrderDate,O.ShippedDate,O.Freight,OD.Quantity, count(P.ProductId), sum(OD.UnitPrice*(1-OD.Discount)*OD.Quantity)
from Customer C
left join Orders O on O.CustomerId=C.CustomerId
left join OrderDetail OD on OD.OrderId=O.OrderId
group by C.CustomerId, C.CompanyName,O.OrderId,O.OrderDate,O.ShippedDate,O.Freight,OD.Quantity
order by C.CustomerId,O.OrderId";

            using (var cnx = new SqlConnection(Settings.Default.Northwind2))
            {
                var cmd = new SqlCommand(req, cnx);
                cnx.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string clientId = (string)reader["CustomerId"];
                        Customer c = null;
                        if (listeClientsCommandes.Count == 0 || listeClientsCommandes[listeClientsCommandes.Count - 1].CustomerId != clientId)
                        {
                            c = new Customer();
                            c.CustomerId = (string)reader["CustomerID"];
                            c.CompanyName = (string)reader["Nom de Société"];
                            c.Orders = new List<Orders>();
                            listeClientsCommandes.Add(c);
                        }
                        else c = listeClientsCommandes[listeClientsCommandes.Count - 1];

                        // Création du territoire et association à la région
                        Orders o = new Orders();
                        o.OrderId = (int)reader["OrderID"];
                        o.CustomerId = (string)reader["CustomerId"];
                        o.AddressId = (Guid)reader["AddressId"];
                        o.OrderDate = (DateTime)reader["OrderDate"];
                        o.ShippedDate = (DateTime)reader["ShippedDate"];
                        o.Freicht = (decimal)reader["Freicht"];

                       c.Orders.Add(o);

                    }
                }
            }

            return listeClientsCommandes;
        }

        public static void AjouterProduit(Guid idCat, string nom, int idS, decimal pr, Int16 uES)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"insert Product (CategoryId,Name,SupplierId,UnitPrice,UnitsInStock)
values(@idCat,@nom, @ idS,@pr,@ uES)";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                ParameterName = "@idCat",
                Value = idCat
            };
            var param1 = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@nom",
                Value = nom
            };
            var param2 = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@idS",
                Value = idS
            };
            var param3 = new SqlParameter
            {
                SqlDbType = SqlDbType.Money,
                ParameterName = "@pr",
                Value = pr
            };
            var param4 = new SqlParameter
            {
                SqlDbType = SqlDbType.SmallInt,
                ParameterName = "@uES",
                Value = uES
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
}


