using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallWorld;

namespace UnitTestSmallWorld
{
    [TestClass]
    public class JeuTest
    {
        private static Jeu demo = new MonteurNPartieDemo().CreerJeu("test1", new PeupleElfe(), "test2", new PeupleNain());

        [TestMethod]
        public void TestNomJoueur()
        {
            Assert.Equals(demo.Joueur1.NomJoueur, "test1");
        }

        [TestMethod]
        public void TestCarte()
        {
            Assert.Equals(demo.Carte.Taille, 6);
        }
    }
}
