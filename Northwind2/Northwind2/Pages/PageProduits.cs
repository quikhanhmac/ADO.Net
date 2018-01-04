using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Pages
{
    class PageProduits : MenuPage
    {
        public PageProduits() : base("Produits", true)
        {
            Menu.AddOption("1", "Liste des produits", AfficherProduits);
        }

        private void AfficherProduits()
        {
            List<Category> liste = Contexte.GetCategories();
            ConsoleTable.From(liste).Display("Categories");
            Guid Idcat = Input.Read<Guid>("Saisir un Id de categorie:");
        }
    }
}
