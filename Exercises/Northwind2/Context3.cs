using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Context3 : DbContext,IDataContext
    {
        
        public Context3 (): base("name=Northwind2.Settings.Northwind2") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Supplier>Suppliers { get; set; }
        public DbSet<Address> Addresses { get; set; }



        public void AjouterModifierProduit(Produit produit, Operations op)
        {
            throw new NotImplementedException();
        }

        public void AjouterProduit(Guid idCat, string nom, int idS, decimal pr, short uES)
        {
            throw new NotImplementedException();
        }

        public int EnregistrerModifsProduits()
        {
            throw new NotImplementedException();
        }

        public IList<Categorie> GetCategories()
        {
            throw new NotImplementedException();
        }

        public IList<Customer> GetClientsCommandes()
        {
            throw new NotImplementedException();
        }

        public IList<Supplier> GetFournisseurs(string pays)
        {
            return Suppliers.Where(s => s.Address.Country==pays).ToList();
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
where A.Country = @nom",param).Single();
        }

        public IList<string> GetpaysFournisseur()
        {
            return Suppliers.Select(s=>s.Address.Country).Distinct().ToList();
        }

        public IList<Product> GetProduits(Guid IdCate)
        {
            throw new NotImplementedException();
        }

        public void SupprimerProduit(int id)
        {
            throw new NotImplementedException();
        }
    }
}
