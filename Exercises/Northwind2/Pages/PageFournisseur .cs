using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class PageFournisseur : MenuPage
    {
        public PageFournisseur() : base("Fournisseurs", true)
        {
            Menu.AddOption("1", "Liste des pays", AfficherPays);
            Menu.AddOption("2", "Fournisseur d'un pays", SaisirPays);
            Menu.AddOption("3", "Nombre de produits d'un pays", AffichageNombreProduit);
        }
        private void AffichageNombreProduit()
        {
            string pays = Input.Read<string>("Saisir un pays:");
            int nbr = Northwind2App.DataContext.GetNbProduits(pays);
            Output.WriteLine(ConsoleColor.DarkCyan, nbr.ToString() + "produits");
        }
        private void SaisirPays()
        {
            string pays = Input.Read<string>("Saisir un pays:");
            var liste = Northwind2App.DataContext.GetFournisseurs(pays);
            ConsoleTable.From(liste, "Pays").Display("pays");
        }

        private void AfficherPays()
        {
            var pays = Northwind2App.DataContext.GetpaysFournisseur();
            ConsoleTable.From(pays, "Pays").Display("pays");
        }
    }
}
