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
        }

        private void SaisirPays()
        {
            string pays = Input.Read<string>("");
            var liste = Contexte.GetFournisseur(pays);
            ConsoleTable.From(liste, "Pays").Display("pays");
        }

        private void AfficherPays()
        {
            var pays = Contexte.GetpaysFournisseur();
            ConsoleTable.From(pays, "Pays").Display("pays");
        }
    }
}
