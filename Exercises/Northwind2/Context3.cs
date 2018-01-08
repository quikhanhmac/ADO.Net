using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Context3 : DbContext, IDataContext
    {

        public Context3() : base("name=Northwind2.Settings.Northwind2") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public void AjouterModifierProduit(Product produit, Operations op)
        {
            Products.Add(produit);
            var prod = Products.Where(pr => pr.ProductID == produit.ProductID).FirstOrDefault();
            prod.Name= produit.Name;
            prod.SupplierId = produit.SupplierId;
            prod.UnitPrice = produit.UnitPrice;
            prod.UnitsInstock = produit.UnitsInstock;

        }

        public void AjouterProduit(Guid idCat, string nom, int idS, decimal pr, short uES)
        {
            throw new NotImplementedException();
        }

        public int EnregistrerModifsProduits()
        {
            return SaveChanges();
        }

        public IList<Category> GetCategories()
        {
            return Categories.Include("Products").OrderBy(cat => cat.CategoryId).ToList();
        }

        public IList<Customer> GetClientsCommandes()
        {
            return Customers.AsNoTracking().Include("Orders").OrderBy(c => c.CompanyName).ToList();

        }

        public IList<Supplier> GetFournisseurs(string pays)
        {
            return Suppliers.Where(s => s.Address.Country == pays).ToList();
        }

        public int GetNbProduits(string nom)
        {
            var param = new System.Data.SqlClient.SqlParameter
            {
                SqlDbType = System.Data.SqlDbType.NVarChar,
                ParameterName = "@nom",
                Value = nom
            };
            return Database.SqlQuery<int>(@"select  count(*) NbProduits
from Product P
inner join supplier S on S.SupplierId = P.SupplierId
inner join Address A on A.AddressId = S.AddressId
where A.Country = @nom", param).Single();
        }

        public IList<string> GetpaysFournisseur()
        {
            return Suppliers.Select(s => s.Address.Country).Distinct().ToList();
        }

        public IList<Product> GetProduits(Guid IdCate)
        {
            Products.Where(p => p.CategoryId == IdCate).Load();
            return Products.Local.Where(p => p.CategoryId == IdCate).ToList();
        }

        public void SupprimerProduit(int id)
        {
            Product pr = Products.Find(id);
            if (pr != null)
            {
                Products.Remove(pr);
            }



        }
    }
}
