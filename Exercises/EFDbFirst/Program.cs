using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDbFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Northwind2Entities model = new Northwind2Entities();
            model.SaveChanges();
            model.Database.ExecuteSqlCommand("select....from...");
        }
    }
}
