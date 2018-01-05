﻿using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Northwind2App : ConsoleApplication
    {
        private static Northwind2App _instance;
        private static IDataContext _dataContext;
        /// <summary>
        /// Obtient l'instance unique de l'application
        /// </summary>
        public static Northwind2App Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Northwind2App();

                return _instance;
            }
        }
        public static IDataContext DataContext //singleton
        {
            get
            {
                if (_dataContext == null)
                    _dataContext = new Context3();
                return _dataContext;
            }
        }
        // Constructeur
        public Northwind2App()
        {
            // Définition des options de menu à ajouter dans tous les menus de pages
            MenuPage.DefaultOptions.Add(
               new Option("a", "Accueil", () => _instance.NavigateHome()));
        }

    }
}
