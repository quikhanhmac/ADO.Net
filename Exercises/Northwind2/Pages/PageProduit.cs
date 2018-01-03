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
        }

        private void ModifierProduit()
        {
            AffichageProduits();
            int IdP= Input.Read<int>("Id de produit à modifier:");
            Produit P = GetProduit();
            P.CategoryId
        }

        private void CreerProduit()
        {
            List<Categorie> liste = Contexte.GetCategories();
            ConsoleTable.From(liste).Display("Categories");

            Guid Idcat = Input.Read<Guid>("Choix de Categogie (Id):");
            String Nom = Input.Read<string>("Nom de produit:");
            int IdF= Input.Read<int>("Id de fournisseur:");
            decimal PrixUnitaire = Input.Read<decimal>("Prix unitaire du produit:");
            Int16 UniteEnStock = Input.Read<Int16>("Unité en stock:");
             Contexte.AjouterProduit(Idcat, Nom,IdF,PrixUnitaire,UniteEnStock);
            Output.WriteLine(ConsoleColor.DarkGreen,"Produit créé avec succes");
            
        }

        private void AffichageProduits()
        {
            List<Categorie> liste = Contexte.GetCategories();
            ConsoleTable.From(liste).Display("Categories");
            Guid Id = Input.Read<Guid>("Choix de Categogie (Id):");
            List<Produit> listeP = Contexte.GetProduits(Id);
            ConsoleTable.From(listeP, "Produit").Display("Produit");
        }
    }
}
