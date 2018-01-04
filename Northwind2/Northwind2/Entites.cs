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
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Country { get; set; }

    }
    public class Product
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public decimal Unitprice { get; set; }
        public Int16 UnitsOnStock { get; set; }
    }
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public virtual List<Orders> Orders { get; set; }
    }
    public class Orders
    {
        public string CustomerId { get; set; }
        public int OrderId { get; set; }
        public Guid AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate{ get; set; }
        public decimal Freicht { get; set; }
    }
}
