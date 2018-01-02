using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class PageFournissseurs: MenuPage
    {
        public PageFournissseurs() : base("Fournisseurs", true)
        {
            Menu.AddOption("1", "Liste des pays", AfficherPays);

        }

        private void AfficherPays()
        {
            var pays = Contexte.GetPaysFournisseur();
            ConsoleTable.From(pays, "Pays").Display("pays");
        }
    }
}
