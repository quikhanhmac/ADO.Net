using Northwind2.Pages;
using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    class Program
    {
        static void Main(string[] args)
        {
            Northwind2App app = Northwind2App.Instance;
            app.Title = "Northwind2";

            // Ajout des pages
            Page accueil = new PageAccueil();
            app.AddPage(accueil);
            Page fournisseur = new PageFournisseur();
            app.AddPage(fournisseur);
            app.AddPage(new PageProduit());

            // Affichage de la page d'accueil
            app.NavigateTo(accueil);
            app.Run();

        }
    }
}

