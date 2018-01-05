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
            Menu.AddOption("2", "Créer un nouveau produit", CreerProduit);
        }

        private void CreerProduit()
        {
            List<Category> liste = Contexte.GetCategories();
            ConsoleTable.From(liste).Display("Categories");
            Guid idCat = Input.Read<Guid>("Id de la categorie:");
           string nom = Input.Read<string>("Nom du produit:");
            int idS = Input.Read<int>("Id de de fourniseur:");
            decimal prixunitaire = Input.Read<decimal>("Prix unitaire:");
            Int16 uniteEnStock = Input.Read<Int16>("Unités en stock:");
            Contexte.AjouterProduit(idCat, nom, idS, prixunitaire, uniteEnStock);
            Output.WriteLine(ConsoleColor.DarkGreen,"Produit créé avec succes");
        }

        private void AfficherProduits()
        {
            List<Category> liste = Contexte.GetCategories();
            ConsoleTable.From(liste).Display("Categories");
            Guid Idcat = Input.Read<Guid>("Saisir un Id de categorie:");
            List<Product> listep = Contexte.GetProduits(Idcat);
            ConsoleTable.From(listep).Display("Produits");
        }
    }
}
