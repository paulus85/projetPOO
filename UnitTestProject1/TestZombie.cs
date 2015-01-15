using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestZombie
    {
        private static Joueur joueurZombie = new JoueurImpl("test", (int)NumUnite.ZOMBIE);
        private static Unite zombie = new UniteZombie(joueurZombie);

        [TestMethod]
        public void TestCanMoveZombie()
        {
            Assert.IsTrue(zombie.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet(), false));
        }

        [TestMethod]
        public void TestGetPoints()
        {
            Assert.AreEqual(1, zombie.GetPoints(new CaseForet()));
            Assert.AreEqual(0, zombie.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, zombie.GetPoints(new CaseMontagne()));
            Assert.AreEqual(1, zombie.GetPoints(new CasePlaine()));
            Assert.AreEqual(1, zombie.GetPoints(new CaseMarais()));
        }
    }
}
