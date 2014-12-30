using System;
using SmallWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                    // On utilise AreSame car on s'appui sur le design pattern poids mouche
                    Assert.AreSame(tiles[i, j], carte.GetCase(new PointImpl(i, j)));
                }
            }
        }

        [TestMethod]
        public void TestUnitesPositions()
        {
            Joueur Joueur1 = new JoueurImpl("test1", new PeupleElfe());
            Joueur Joueur2 = new JoueurImpl("test2", new PeupleNain());
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
    }
}
