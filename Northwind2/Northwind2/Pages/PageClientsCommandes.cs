using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Pages
{
    class PageClientsCommandes : MenuPage
    {
        public PageClientsCommandes() : base("ClientsCommandes", true)
        {
            Menu.AddOption("1","Liste des produits", Affichage); 
        }

        private void Affichage()
        {
            string idClient = Input.Read<string>("Id de client:");
            List<Customer> liste = Contexte.GetClientsCommandes();
            ConsoleTable.From(liste).Display();
        }

        
    }
}

