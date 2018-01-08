using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Pages
{
    class PageProduit : MenuPage
    {
        public PageProduit() : base("Produits", true)
        {
            Menu.AddOption("1", "Liste des produits", AffichageProduits);
            Menu.AddOption("2", "Créer un nouveau produit", CreerProduit);
            Menu.AddOption("3", "Modifier un produit",ModifierProduit);
            Menu.AddOption("6", "Enregistrer", EnregistrerProduit);
        }

        private void EnregistrerProduit()
        {
            try
            {

            }
            catch
            {

            }
        }

        private void ModifierProduit()
        {
            AffichageProduits();
            int IdP= Input.Read<int>("Id de produit à modifier:");
           
         
        }

        private void CreerProduit()
        {
            IList<Category> liste = Northwind2App.DataContext.GetCategories();
            ConsoleTable.From(liste).Display("Categories");

            Guid Idcat = Input.Read<Guid>("Choix de Categogie (Id):");
            String Nom = Input.Read<string>("Nom de produit:");
            int IdF= Input.Read<int>("Id de fournisseur:");
            decimal PrixUnitaire = Input.Read<decimal>("Prix unitaire du produit:");
            Int16 UniteEnStock = Input.Read<Int16>("Unité en stock:");
            Northwind2App.DataContext.AjouterProduit(Idcat, Nom,IdF,PrixUnitaire,UniteEnStock);
            Output.WriteLine(ConsoleColor.DarkGreen,"Produit créé avec succes");
            
        }

        private void AffichageProduits()
        {
            IList<Category> liste = Northwind2App.DataContext.GetCategories();
            ConsoleTable.From(liste).Display("Categories");
            Guid Idcat = Input.Read<Guid>("Saisir un Id de categorie:");
            IList<Product> listep = Northwind2App.DataContext.GetProduits(Idcat);
            ConsoleTable.From(listep).Display("Produits");
        }
    }
}

