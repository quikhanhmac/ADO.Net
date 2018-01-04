using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public Guid AddressId { get; set; }
    }
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Country { get; set; }
        
    }
    
    public class Categorie
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Produit
    {
        public Guid CategoryId { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInstock { get; set; }
        public int SupplierId { get; set; }

    }
}

   
