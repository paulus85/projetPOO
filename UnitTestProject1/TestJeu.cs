using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTestSmallWorld
{
    [TestClass]
    public class TestJeu
    {
        private static Jeu demo = new MonteurNPartieDemo().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF);
        private static Jeu petite = new MonteurNPartiePetite().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF);
        private static Jeu normale = new MonteurNPartieNormale().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF);

        [TestMethod]
        public void TestCarte()
        {
            Assert.AreEqual(demo.Carte.Taille, 5);
            Assert.AreEqual(petite.Carte.Taille, 10);
            Assert.AreEqual(normale.Carte.Taille, 15);
        }

        //Juste tester sur la demo
        //vérifie la prise en compte des pts bonus pour les orcs
        [TestMethod]
        public void TestPointBonusDemo()
        {
            Dictionary<Unite, Point> dico = new Dictionary<Unite, Point>();
            dico = demo.Carte.GetUnites(demo.Joueur1);
            
            if(demo.JoueurCourant.Equals(demo.Joueur1) && demo.GetNbUnites(new PointImpl(0,0)) != 0)
            {
                ((UniteOrc)demo.Carte.Unites[0, 0][0]).AddPointBonus();
                demo.FinTour();
                Assert.IsTrue(demo.Joueur1.Points > 0);
                demo.FinTour();
                Assert.AreEqual(demo.Joueur2.Points, 1);
                demo.Carte.SupprimerUnite(dico.Keys.First(), dico.Values.First());
                demo.FinTour();
                Assert.IsTrue(demo.Joueur1.Points > 0);
            }
            else if (demo.JoueurCourant.Equals(demo.Joueur1) && demo.GetNbUnites(new PointImpl(0, demo.Carte.Taille - 1)) != 0)
            {
                ((UniteOrc)demo.Carte.Unites[0, demo.Carte.Taille - 1][0]).AddPointBonus();
                demo.FinTour();
                Assert.IsTrue(demo.Joueur1.Points > 0);
                demo.FinTour();
                Assert.AreEqual(demo.Joueur2.Points, 1);
                demo.Carte.SupprimerUnite(dico.Keys.First(), dico.Values.First());
                demo.FinTour();
                Assert.IsTrue(demo.Joueur1.Points > 0);
            }
            else if (demo.JoueurCourant.Equals(demo.Joueur2))
            {
                demo.FinTour();
                Assert.AreEqual(demo.Joueur2.Points, 1);
                if (demo.GetNbUnites(new PointImpl(0, 0)) != 0)
                    ((UniteOrc)demo.Carte.Unites[0, 0][0]).AddPointBonus();
                else
                    ((UniteOrc)demo.Carte.Unites[0, demo.Carte.Taille - 1][0]).AddPointBonus();
                demo.FinTour();
                Assert.IsTrue(demo.Joueur1.Points > 0);
                demo.Carte.SupprimerUnite(dico.Keys.First(), dico.Values.First());
                demo.FinTour();
                Assert.IsTrue(demo.Joueur1.Points > 0);
            }
        }

        [TestMethod]
        public void TestNomJoueur()
        {
            TestNomJoueur(demo);
            TestNomJoueur(petite);
            TestNomJoueur(normale);
        }
        private void TestNomJoueur(Jeu jeu)
        {
            Assert.AreEqual(jeu.Joueur1.NomJoueur, "test1");
            Assert.AreEqual(jeu.Joueur2.NomJoueur, "test2");
            Assert.AreNotEqual(jeu.Joueur1.Numero, jeu.Joueur2.Numero);
        }



        [TestMethod]
        public void TestGagnant()
        {
            this.TestGagnant(new MonteurNPartieDemo().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF));
            this.TestGagnant(new MonteurNPartiePetite().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF));
            this.TestGagnant(new MonteurNPartieNormale().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF));
        }
        private void TestGagnant(Jeu jeu)
        {
            jeu.Joueur1.AjoutPoints(1000);
            while (!jeu.FinDuJeu())
            {
                jeu.FinTour();
                jeu.FinTour();
            }
            Assert.IsTrue(jeu.Vainqueur().Equals(jeu.Joueur1));

            jeu.Joueur2.AjoutPoints(2000);
            Assert.IsTrue(jeu.Vainqueur().Equals(jeu.Joueur2));
        }

        [TestMethod]
        public void testTour()
        {
            TestTour(new MonteurNPartieDemo().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF));
            TestTour(new MonteurNPartiePetite().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF));
            TestTour(new MonteurNPartieNormale().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF));
        }
        private void TestTour(Jeu jeu)
        {
            for (int i = 1; i <= jeu.NbTour; i++)
            {
                if (jeu.JoueurCourant.Equals(jeu.Joueur1))
                {
                    Assert.AreEqual(i, jeu.TourActuelle);
                    jeu.FinTour();
                    Assert.IsTrue(jeu.Joueur2.Equals(jeu.JoueurCourant));
                    jeu.FinTour();
                    Assert.IsTrue(jeu.Joueur1.Equals(jeu.JoueurCourant));
                }
                else
                {
                    Assert.AreEqual(i, jeu.TourActuelle);
                    jeu.FinTour();
                    Assert.IsTrue(jeu.Joueur1.Equals(jeu.JoueurCourant));
                    jeu.FinTour();
                    Assert.IsTrue(jeu.Joueur2.Equals(jeu.JoueurCourant));
                }
            }
            Assert.IsTrue(jeu.FinDuJeu());
        }

        [TestMethod]
        public void TestSerializationJeu()
        {
            // Serialization
            Stream stream = File.Open("Jeu.sav", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, normale);
            stream.Close();

            // Deserialization
            stream = File.Open("Jeu.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            Jeu savedJeu = (Jeu)formatter.Deserialize(stream);
            stream.Close();
            Assert.IsTrue(normale.Joueur1.Equals(savedJeu.Joueur1));
            Assert.IsTrue(normale.Joueur2.Equals(savedJeu.Joueur2));
            Assert.IsTrue(normale.JoueurCourant.Equals(savedJeu.JoueurCourant));
            Assert.AreEqual(normale.NbTour, savedJeu.NbTour);
            Assert.AreEqual(normale.TourActuelle, savedJeu.TourActuelle);
            
            Carte carte = normale.Carte;
            Carte savedCarte = savedJeu.Carte;
            Assert.AreEqual(carte.Taille, savedCarte.Taille);
            for (int i = 0; i < carte.Taille; i++)
            {
                for (int j = 0; j < carte.Taille; j++)
                {
                    Assert.IsInstanceOfType(savedCarte.GetCase(new PointImpl(i, j)), carte.GetCase(new PointImpl(i, j)).GetType());
                }
            }
            for (int i = 0; i < carte.Taille; i++)
            {
                for (int j = 0; j < carte.Taille; j++)
                {
                    List<Unite> units = carte.GetUnites(new PointImpl(i, j));
                    List<Unite> savedUnites = savedCarte.GetUnites(new PointImpl(i, j));
                    Assert.AreEqual(units.Count, savedUnites.Count);
                    for (int k = 0; k < savedUnites.Count; k++)
                    {
                        Assert.IsTrue(units.Contains(savedUnites[k]));
                    }
                }
            }
        }
    }
}
