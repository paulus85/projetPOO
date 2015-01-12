using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestOrc
    {
        private static Joueur joueurOrc = new JoueurImpl("test", (int)NumUnite.ORC);
        private static Unite nain = new UniteOrc(new JoueurImpl("test", (int)NumUnite.ORC));

        [TestMethod]
        public void TestCanMove()
        {
            Assert.IsTrue(nain.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet()));
        }

        [TestMethod]
        public void TestGetPoints()
        {
            Assert.AreEqual(0, nain.GetPoints(new CaseForet()));
            Assert.AreEqual(1, nain.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, nain.GetPoints(new CaseMontagne()));
            Assert.AreEqual(1, nain.GetPoints(new CasePlaine()));
        }
    }
}
