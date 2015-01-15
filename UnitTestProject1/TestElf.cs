using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestElfe
    {
        private static Joueur joueurElfe = new JoueurImpl("test", (int)NumUnite.ELF);
        private static Unite elfe = new UniteElfe(joueurElfe);

        [TestMethod]
        public void TestCanMoveElfe()
        {
            Assert.IsTrue(elfe.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet(), false));
        }

        [TestMethod]
        public void TestGetPointsElfe()
        {
            Assert.AreEqual(1, elfe.GetPoints(new CaseForet()));
            Assert.AreEqual(1, elfe.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, elfe.GetPoints(new CaseMontagne()));
            Assert.AreEqual(1, elfe.GetPoints(new CasePlaine()));
            Assert.AreEqual(1, elfe.GetPoints(new CaseMarais()));
        }
    }
}
