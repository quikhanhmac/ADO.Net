using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Pages
{
    class PageClientsCommandes:MenuPage
    {
        public PageClientsCommandes(): base("ClientsCommandes", true)
        {
            Menu.AddOption("1", "Liste des clients", Affichage);
        }

        private void Affichage()
        {
            List<Customer> liste = Contexte.GetClientsCommandes();
            string c = Input.Read<string>("Saisir Id de client:");
            ConsoleTable.From(liste).Display("Clients");
        }
    }
}
