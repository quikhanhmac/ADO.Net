using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public interface IDataContext
    {
        IList<string> GetpaysFournisseur();
        IList<Supplier> GetFournisseurs(string pays);
        int GetNbProduits(string nom);
        IList<Category> GetCategories();
        IList<Product> GetProduits(Guid IdCate);
        IList<Customer> GetClientsCommandes();
        void AjouterProduit(Guid idCat, string nom, int idS, decimal pr, Int16 uES);
        void AjouterModifierProduit(Product produit, Operations op);
        void SupprimerProduit(int id);
        int EnregistrerModifsProduits();
    }
}
