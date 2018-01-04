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
    public class ContexteTests
    {

        [TestMethod()]
        public void GetpaysFournisseurTest()
        {
            var liste = Contexte.GetpaysFournisseur();
            int index = liste.Count;
            Assert.AreEqual(16, index);
            Assert.AreEqual(16, liste[index - 1]);
            //Assert.AreEqual(16,Contexte.GetpaysFournisseur().Count());
            //Assert.AreEqual("USA",Contexte.GetpaysFournisseur()[15]);
        }

        [TestMethod()]
        public void GetFournisseurTest()
        {
            var liste = Contexte.GetFournisseurs("Japon");
            Assert.AreEqual(6, liste[0].SupplierId);
            Assert.AreEqual(4, liste[1].SupplierId);

        }

        [TestMethod()]
        public void GetNbProduitTest()
        {
            Assert.AreEqual(9, Contexte.GetNbProduits("UK"));

        }

        [TestMethod()]
        public void GetCategoriesTest()
        {
            var liste = Contexte.GetCategories();
            Assert.AreEqual(8, liste.Count());
            Assert.AreEqual("Seafood", liste[(liste.Count - 1)].Name);

        }

        [TestMethod()]
        public void GetProduitsTest()
        {
            Guid idcat = Guid.Parse("EB4E5F53-8711-4118-946E-F89E00615EC6");
            List<Produit> liste = Contexte.GetProduits(idcat);
            Assert.AreEqual(12, liste.Count);
            Assert.AreEqual(40, liste[6].ProductID);

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
            Contexte.AjouterModifierProduit(p, Operations.Ajout);

            Assert.AreEqual(13, Contexte.GetProduits(p.CategoryId).Count());
        }
        public void SupprimerProduitTest()
        {
            int prodmax = Contexte.GetIdProdMax();
            Contexte.SupprimerProduit(prodmax);
            Guid idcat = Guid.Parse("EB4E5F53-8711-4118-946E-F89E00615EC6");
        }
    }
}