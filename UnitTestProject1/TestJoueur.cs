using System;
using SmallWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestProject1
{
    [TestClass]
    public class TestJoueur
    {
        private static Joueur elf = new JoueurImpl("Elf", (int)NumUnite.ELF);
        private static Joueur nain = new JoueurImpl("Nain", (int)NumUnite.NAIN);
        private static Joueur orc = new JoueurImpl("Orc", (int)NumUnite.ORC);

         [TestMethod]
        public void TestPoints()
        {
            this.TestPoints(elf);
            this.TestPoints(nain);
            this.TestPoints(orc);
        }
       
        public void TestPoints(Joueur j)
        {
            Assert.AreEqual(0, j.Points);
            j.AjoutPoints(5);
            Assert.AreEqual(5, j.Points);
        }

        [TestMethod]
        public void TestCreatioUnites()
        {
            this.TestCreationUnites(elf, typeof(UniteElfe));
            this.TestCreationUnites(nain, typeof(UniteNain));
            this.TestCreationUnites(orc, typeof(UniteOrc));
        }

        private void TestCreationUnites(Joueur player, Type uniteType)
        {
            List<Unite> unites = player.CreerUnites(2);
            Assert.AreEqual(2, unites.Count);
            foreach (Unite unit in unites)
            {
                Assert.IsInstanceOfType(unit, uniteType);
            }
        }

        [TestMethod]
        public void TestEqualsJoueur()
        {
            Assert.IsFalse(elf.Equals(nain));
            Assert.IsFalse(nain.Equals(elf));
            Assert.IsTrue(elf.Equals(elf));
            Assert.IsFalse(elf.Equals(null));
            Assert.IsFalse(elf.Equals(new CasePlaine()));
        }

        [TestMethod]
        public void TestNumeroJoueur()
        {
            Assert.AreNotEqual(elf.Numero, nain.Numero);
            Assert.AreNotEqual(nain.Numero, orc.Numero);
            Assert.AreNotEqual(orc.Numero, elf.Numero);
        }

        [TestMethod]
        public void TestSerializationJoueur()
        {
            this.TestSerializationJoueur(elf);
            this.TestSerializationJoueur(nain);
            this.TestSerializationJoueur(orc);
        }

        private void TestSerializationJoueur(Joueur joueur)
        {
            // Serializes:
            Stream stream = File.Open("Joueur.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, joueur);
            stream.Close();

            // Deserializes and checks the values:
            stream = File.Open("Joueur.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            Joueur savedPlayer = (Joueur)formatter.Deserialize(stream);
            stream.Close();
            Assert.IsTrue(joueur.Equals(savedPlayer));
            Assert.AreEqual(joueur.Points, savedPlayer.Points);
            Assert.AreEqual(joueur.NomJoueur, savedPlayer.NomJoueur);
            Assert.ReferenceEquals(joueur.Peuple, savedPlayer.Peuple);
        }
    }
}
