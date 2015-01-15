using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestOrc
    {
        private static Joueur joueurOrc = new JoueurImpl("test", (int)NumUnite.ORC);
        private static Unite nain = new UniteOrc(joueurOrc);

        [TestMethod]
        public void TestCanMoveOrc()
        {
            Assert.IsTrue(nain.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet(), false));
        }

        [TestMethod]
        public void TestGetPointsOrc()
        {
            Assert.AreEqual(0, nain.GetPoints(new CaseForet()));
            Assert.AreEqual(1, nain.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, nain.GetPoints(new CaseMontagne()));
            Assert.AreEqual(1, nain.GetPoints(new CasePlaine()));
            Assert.AreEqual(1, nain.GetPoints(new CaseMarais()));
        }
    }
}
