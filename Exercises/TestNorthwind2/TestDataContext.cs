using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Tests
{
    [TestClass()]
    public class TestContext1
    {

        [TestMethod()]
        public void GetpaysFournisseurTest()
        {
            var liste = Northwind2App.DataContext.GetpaysFournisseur();
            int index = liste.Count;
            Assert.AreEqual(16, index);
            Assert.AreEqual(16, liste[index - 1]);
            //Assert.AreEqual(16,Contexte.GetpaysFournisseur().Count());
            //Assert.AreEqual("USA",Contexte.GetpaysFournisseur()[15]);
        }

        [TestMethod()]
        public void GetFournisseurTest()
        {
            var liste = Northwind2App.DataContext.GetFournisseurs("Japon");
            Assert.AreEqual(6, liste[0].SupplierId);
            Assert.AreEqual(4, liste[1].SupplierId);

        }

        [TestMethod()]
        public void GetNbProduitTest()
        {
            Assert.AreEqual(9, Northwind2App.DataContext.GetNbProduits("UK"));

        }

        [TestMethod()]
        public void GetCategoriesTest()
        {
            var liste = Northwind2App.DataContext.GetCategories();
            Assert.AreEqual(8, liste.Count());
            Assert.AreEqual("Seafood", liste[(liste.Count - 1)].Name);

        }

        [TestMethod()]
        public void GetProduitsTest()
        {
            Guid idcat = Guid.Parse("EB4E5F53-8711-4118-946E-F89E00615EC6");
            IList<Product> liste = Northwind2App.DataContext.GetProduits(idcat);
            Assert.AreEqual(12, liste.Count);
            Assert.AreEqual(40, liste[6].ProductId);

        }


        [TestMethod()]
        public void AjouterModifierProduitTest()
        {
            Guid idcat = Guid.Parse("EB4E5F53-8711-4118-946E-F89E00615EC6");
            Produit p = new Produit();

            p.SupplierId = 12;
            p.Name = "mimi";
            p.UnitPrice = decimal.Parse("3,5");
            p.UnitsInstock = Int16.Parse("1");
            Northwind2App.DataContext.AjouterModifierProduit(p, Operations.Ajout);

            Assert.AreEqual(13, Northwind2App.DataContext.GetProduits(p.CategoryId).Count());
        }
        //public void SupprimerProduitTest()
        //{
        //    int prodmax = Northwind2App.DataContext.GetIdProdMax();
        //    Northwind2App.DataContext.SupprimerProduit(prodmax);
        //    Guid idcat = Guid.Parse("EB4E5F53-8711-4118-946E-F89E00615EC6");
        //}
    }
}