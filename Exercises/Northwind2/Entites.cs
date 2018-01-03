using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Categorie
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Produit
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInstock { get; set; }
    }
}

   
