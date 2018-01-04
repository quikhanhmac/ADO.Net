using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class PageFournissseurs : MenuPage
    {
        public PageFournissseurs() : base("Fournisseurs", true)
        {
            Menu.AddOption("1", "Liste des pays des fournisseurs", AfficherPays);
            Menu.AddOption("2", "Fournisseurs d'un pays", AffichageFournisseur);
            Menu.AddOption("3", "Nombre de produits d'un pays", AffichageNombreProduit);
        }

        private void AffichageNombreProduit()
        {
            string pays = Input.Read<string>("Saisir un pays:");
            int nbr = Contexte.GetNbProduits(pays);
            Output.WriteLine(ConsoleColor.DarkCyan,nbr.ToString() + "produits");
        }

        private void AffichageFournisseur()
        {
            string pays = Input.Read<string>("Saisir un pays:");
            var liste = Contexte.GetFournisseurs(pays);
            ConsoleTable.From(liste, "Pays").Display("pays");
        }

        private void AfficherPays()
        {
            var pays = Contexte.GetPaysFournisseur();
            ConsoleTable.From(pays, "Pays").Display("pays");
        }
    }
}
