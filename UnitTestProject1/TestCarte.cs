using System;
using SmallWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestProject1
{
    [TestClass]
    public class TestCarte
    {
        Carte carte = CarteImpl.Instance.ConstruireCarte(10);

        [TestMethod]
        public void TestConstructeur()
        {
            Assert.IsTrue(10 == carte.Taille);
            Case[,] tiles = carte.Cases;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // On utilise AreSame car on s'appuit sur le design pattern poids mouche
                    Assert.AreSame(tiles[i, j], carte.GetCase(new PointImpl(i, j)));
                }
            }
        }

        [TestMethod]
        public void TestUnitesPositions()
        {
            Joueur Joueur1 = new JoueurImpl("test1", (int)NumUnite.ELF);
            Joueur Joueur2 = new JoueurImpl("test2", (int)NumUnite.NAIN);
            Unite elfA = new UniteElfe(Joueur1);
            Unite elfB = new UniteElfe(Joueur1);
            Unite nainA = new UniteNain(Joueur2);
            Unite nainB = new UniteNain(Joueur2);
            carte.PlacerUnite(elfA, new PointImpl(0, 0));
            carte.PlacerUnite(elfB, new PointImpl(0, 0));
            carte.PlacerUnite(nainA, new PointImpl(9, 9));
            carte.PlacerUnite(nainB, new PointImpl(9, 9));
            List<Unite> units = carte.GetUnites(new PointImpl(0, 0));
            Assert.AreEqual(2, units.Count);
            units = carte.GetUnites(new PointImpl(9, 9));
            Assert.AreEqual(2, units.Count);

            
            Assert.IsTrue(carte.SupprimerUnite(nainB, new PointImpl(9, 9)));
            units = carte.GetUnites(new PointImpl(9, 9));
            Assert.AreEqual(1, units.Count);


            carte.DeplacerUnite(elfB, new PointImpl(0, 0), new PointImpl(0, 1));
            units = carte.GetUnites(new PointImpl(0, 0));
            Assert.AreEqual(1, units.Count);
            units = carte.GetUnites(new PointImpl(0, 1));
            Assert.AreEqual(1, units.Count);


            Assert.IsFalse(carte.EstPositionEnnemie(new PointImpl(9, 9), nainA));
            Assert.IsTrue(carte.EstPositionEnnemie(new PointImpl(0, 0), nainB));
        }

        [TestMethod]
        public void TestSerializationCarte()
        {
            // Serialization
            Stream stream = File.Open("Carte.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, carte);
            stream.Close();

            // Deserialization
            stream = File.Open("Carte.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            Carte savedMap = (Carte)formatter.Deserialize(stream);
            stream.Close();
            Assert.AreEqual(carte.Taille, savedMap.Taille);
            // Checks the map composition:
            for (int i = 0; i < carte.Taille; i++)
            {
                for (int j = 0; j < carte.Taille; j++)
                {
                    Assert.IsInstanceOfType(savedMap.GetCase(new PointImpl(i, j)), carte.GetCase(new PointImpl(i, j)).GetType());
                }
            }
            // Checks the units on the map:
            for (int i = 0; i < carte.Taille; i++)
            {
                for (int j = 0; j < carte.Taille; j++)
                {
                    List<Unite> units = carte.GetUnites(new PointImpl(i, j));
                    List<Unite> savedUnits = savedMap.GetUnites(new PointImpl(i, j));
                    Assert.AreEqual(units.Count, savedUnits.Count);
                    for (int k = 0; k < savedUnits.Count; k++)
                    {
                        Assert.IsTrue(units.Contains(savedUnits[k]));
                    }
                }
            }
        }
    }
}
