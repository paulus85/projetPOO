using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestElf
    {
        private static Joueur joueurElf = new JoueurImpl("test", (int)NumUnite.ELF);
        private static Unite elf = new UniteElfe(new JoueurImpl("test", (int)NumUnite.ELF));

        [TestMethod]
        public void TestCanMove()
        {
            Assert.IsTrue(elf.ValidationDeplacement(new PointImpl(0, 0), new CasePlaine(), new PointImpl(0, 1), new CaseForet()));
        }

        [TestMethod]
        public void TestGetPoints()
        {
            Assert.AreEqual(1, elf.GetPoints(new CaseForet()));
            Assert.AreEqual(1, elf.GetPoints(new CaseDesert()));
            Assert.AreEqual(1, elf.GetPoints(new CaseMontagne()));
            Assert.AreEqual(1, elf.GetPoints(new CasePlaine()));
        }
    }
}
