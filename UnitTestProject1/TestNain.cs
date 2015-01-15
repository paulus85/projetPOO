using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestNain
    {
        private static Joueur joueurNain = new JoueurImpl("test", (int)NumUnite.NAIN);
        private static Unite nain = new UniteNain(joueurNain);

        [TestMethod]
        public void TestCanMoveNain()
        {
            Assert.IsTrue(nain.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet(), false));
        }

        [TestMethod]
        public void TestGetPointsNain()
        {
            Assert.AreEqual(1, nain.GetPoints(new CaseForet()));
            Assert.AreEqual(1, nain.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, nain.GetPoints(new CaseMontagne()));
            Assert.AreEqual(0, nain.GetPoints(new CasePlaine()));
            Assert.AreEqual(1, nain.GetPoints(new CaseMarais()));
        }
    }
}
