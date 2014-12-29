using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestProject1
{
    [TestClass]
    public class TestUnites
    {
        private static Unite elf = new UniteElfe(new JoueurImpl("test", new PeupleElfe()));
        private static Unite nain = new UniteNain(new JoueurImpl("test", new PeupleNain()));
        private static Unite orc = new UniteOrc(new JoueurImpl("test", new PeupleOrc()));

        [TestMethod]
        public void TestEquals()
        {
            Assert.IsTrue(elf.Equals(elf));
            Assert.IsFalse(nain.Equals(elf));
            Assert.IsFalse(orc.Equals(nain));
        }

        [TestMethod]
        public void TestMouvementEtPoint()
        {
            this.TestMouvementEtPoint(elf);
            this.TestMouvementEtPoint(nain);
            this.TestMouvementEtPoint(orc);
        }

        private void TestMouvementEtPoint(Unite unite)
        {
            double PointMouvementDefaut = unite.PointsDeplacementRestant;

            if(unite.GetType() == typeof(UniteElfe))
            {
                Assert.IsTrue(unite.Deplacement(new CaseForet()));
                Assert.AreEqual(PointMouvementDefaut - 0.5, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsFalse(unite.Deplacement(new CaseDesert()));

                Assert.IsTrue(unite.Deplacement(new CaseMontagne()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CasePlaine()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
            }

            if(unite.GetType() == typeof(UniteNain))
            {
                Assert.IsTrue(unite.Deplacement(new CasePlaine()));
                Assert.AreEqual(PointMouvementDefaut - 0.5, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CaseForet()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CaseMontagne()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CaseDesert()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
            }

            if (unite.GetType() == typeof(UniteOrc))
            {
                Assert.IsTrue(unite.Deplacement(new CasePlaine()));
                Assert.AreEqual(PointMouvementDefaut - 0.5, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CaseForet()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CaseMontagne()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
                unite.ResetPointsDeplacement();
                Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);

                Assert.IsTrue(unite.Deplacement(new CaseDesert()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
            }

            unite.ResetPointsDeplacement();
            Assert.AreEqual(PointMouvementDefaut, unite.PointsDeplacementRestant);
        }

        [TestMethod]
        public void TestLifePoints()
        {
            this.TestLifePoints(elf);
            this.TestLifePoints(nain);
            this.TestLifePoints(orc);
        }

        private void TestLifePoints(Unite unit)
        {
            Assert.AreEqual(unit.PvDefault, unit.PointsDeVie);
            Assert.IsTrue(unit.EstEnVie());
            Assert.IsTrue(unit.EnleverPV());
            Assert.AreEqual(unit.PvDefault - 1, unit.PointsDeVie);

            while (unit.PointsDeVie > 0)
            {
                Assert.IsTrue(unit.EstEnVie());
                Assert.IsTrue(unit.EnleverPV());
            }
            Assert.IsFalse(unit.EstEnVie());
        }
    }
}
