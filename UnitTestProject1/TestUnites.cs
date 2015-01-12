using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestProject1
{
    [TestClass]
    public class TestUnites
    {
        private static Unite elf = new UniteElfe(new JoueurImpl("test", (int)NumUnite.ELF));
        private static Unite nain = new UniteNain(new JoueurImpl("test", (int)NumUnite.NAIN));
        private static Unite orc = new UniteOrc(new JoueurImpl("test", (int)NumUnite.ORC));

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
                Assert.IsTrue(unite.Deplacement(new CaseForet()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
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
                Assert.IsTrue(unite.Deplacement(new CasePlaine()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
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
                Assert.IsTrue(unite.Deplacement(new CasePlaine()));
                Assert.AreEqual(PointMouvementDefaut - 1, unite.PointsDeplacementRestant);
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

        [TestMethod]
        public void TestSerializationUnite()
        {
            this.TestSerializationUnite(elf);
            this.TestSerializationUnite(nain);
            this.TestSerializationUnite(orc);
        }

        private void TestSerializationUnite(Unite unite)
        {
            // Serialization
            Stream stream = File.Open("Unite.sav", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, unite);
            stream.Close();

            // Deserialization
            stream = File.Open("Unite.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            Unite savedUnit = (Unite)formatter.Deserialize(stream);
            stream.Close();
            Assert.IsTrue(unite.Equals(savedUnit));
            Assert.AreEqual(unite.PointsDeVie, savedUnit.PointsDeVie);
            Assert.AreEqual(unite.PointsDeplacementRestant, savedUnit.PointsDeplacementRestant);
            Assert.IsTrue(unite.Proprio.Equals(savedUnit.Proprio));
        }
    }
}
