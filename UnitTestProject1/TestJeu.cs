﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;

namespace UnitTestSmallWorld
{
    [TestClass]
    public class TestJeu
    {
        private static Jeu demo = new MonteurNPartieDemo().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe());
        private static Jeu petite = new MonteurNPartiePetite().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe());
        private static Jeu normale = new MonteurNPartieNormale().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe());

        [TestMethod]
        public void TestCarte()
        {
            Assert.AreEqual(demo.Carte.Taille, 6);
            Assert.AreEqual(petite.Carte.Taille, 10);
            Assert.AreEqual(normale.Carte.Taille, 14);
        }

        //Juste tester sur la demo
        //vérifie la prise en compte des pts bonus pour les orcs
        [TestMethod]
        private void TestPointBonusDemo()
        {
            Dictionary<Unite, Point> dico = new Dictionary<Unite, Point>();
            dico = demo.Carte.GetUnites(demo.Joueur1);
            ((UniteOrc)demo.Carte.Unites[0, 0][0]).AddPointBonus();
            demo.FinTour();
            Assert.AreEqual(demo.Joueur1.Points, 8);//pt bonus donc plus de point
            demo.FinTour();
            Assert.AreEqual(demo.Joueur2.Points, 4);
            demo.Carte.SupprimerUnite(dico.Keys.First(), dico.Values.First());
            demo.FinTour();
            Assert.AreEqual(demo.Joueur1.Points, 11);//mort de l'unité avec pts bonus donc moins de pts gagnés
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
            this.TestGagnant(new MonteurNPartieDemo().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe()));
            this.TestGagnant(new MonteurNPartiePetite().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe()));
            this.TestGagnant(new MonteurNPartieNormale().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe()));
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
            TestTour(new MonteurNPartieDemo().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe()));
            TestTour(new MonteurNPartiePetite().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe()));
            TestTour(new MonteurNPartieNormale().CreerJeu("test1", new PeupleOrc(), "test2", new PeupleElfe()));
        }
        private void TestTour(Jeu jeu)
        {
            for (int i = 1; i <= jeu.NbTour; i++)
            {
                Assert.AreEqual(i, jeu.TourActuelle);
                jeu.FinTour();
                Assert.IsTrue(jeu.Joueur2.Equals(jeu.JoueurCourant));
                jeu.FinTour();
                Assert.IsTrue(jeu.Joueur1.Equals(jeu.JoueurCourant));
            }
            Assert.IsTrue(jeu.FinDuJeu());
        }
    }
}
