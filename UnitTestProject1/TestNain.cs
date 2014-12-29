using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestNain
    {
        private static Joueur joueurNain = new JoueurImpl("test", new PeupleNain());
        private static Unite nain = new UniteNain(new JoueurImpl("test", new PeupleNain()));

        [TestMethod]
        public void TestCanMove()
        {
            Assert.IsTrue(nain.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet()));
        }

        [TestMethod]
        public void TestGetPoints()
        {
            Assert.AreEqual(1, nain.GetPoints(new CaseForet()));
            Assert.AreEqual(1, nain.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, nain.GetPoints(new CaseMontagne()));
            Assert.AreEqual(0, nain.GetPoints(new CasePlaine()));
        }
    }
}
