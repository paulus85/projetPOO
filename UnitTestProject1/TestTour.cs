using System;
using SmallWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class TestTour
    {
        private const int NB_TESTS = 1000;

        [TestMethod]
        public void TestMultipleMouvements()
        {
            for (int i = 0; i < NB_TESTS; i++)
            {
                this.TestMouvement();
            }
        }
        
        private void TestMouvement()
        {
            Jeu jeu = new MonteurNPartieNormale().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF);
            int taille = jeu.Carte.Taille;
            Tour tour = jeu.Tour;


            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    Point pos = new PointImpl(i, j);
                    if (tour.JoueurSurPosition(pos))
                    {
                        Unite unite;
                        for (int x = 0; i < tour.GetUnites(pos).Count; i++)
                        {
                            unite = tour.GetUnites(pos)[x];
                            if (unite.PointsDeplacementRestant > 0.0)
                            {
                                List<Unite> unitesL = new List<Unite>();
                                unitesL.Add(unite);
                                tour.SelectUnites(unitesL, pos);

                                Point destination = GetDestination(jeu, pos, unite);
                                if (destination != null)
                                {
                                    Assert.IsTrue(tour.SetDestination(destination));
                                    Assert.IsFalse(jeu.Carte.EstPositionEnnemie(destination, unite));

                                    tour.ExecuterDeplacement();
                                    List<Unite> unites = jeu.Carte.GetUnites(destination);
                                    Assert.IsTrue(unites.Contains(unite));
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestMultipleAttaqueMouvements()
        {
            for (int i = 0; i < NB_TESTS; i++)
            {
                this.TestAttaqueMouvement();
            }
        }

        public void TestAttaqueMouvement()
        {
            Jeu jeu = new MonteurNPartiePetite().CreerJeu("test1", (int)NumUnite.ORC, "test2", (int)NumUnite.ELF);
            int size = jeu.Carte.Taille;
            Tour tour = jeu.Tour;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Point pos = new PointImpl(i, j);
                    if (tour.JoueurSurPosition(pos))
                    {
                        Unite unit = tour.GetUnites(pos)[0];
                        if (unit.PointsDeplacementRestant > 0.0)
                        {
                            List<Unite> unitsL = new List<Unite>();
                            unitsL.Add(unit);
                            tour.SelectUnites(unitsL, pos);

                            Point destination = GetDestination(jeu, pos, unit);
                            if (destination != null)
                            {
                                Assert.IsTrue(jeu.JoueurCourant.Equals(jeu.Joueur1));
                                Unite enemy;
                                if (!jeu.Carte.EstPositionEnnemie(destination, unit))
                                {
                                    enemy = jeu.Joueur2.CreerUnites(1)[0];
                                    jeu.Carte.PlacerUnite(enemy, destination);
                                }
                                else
                                {
                                    enemy = jeu.Carte.Unites[destination.x, destination.y][0];
                                }
                                Assert.IsTrue(tour.SetDestination(destination));
                                Assert.IsTrue(jeu.Carte.EstPositionEnnemie(destination, unit));

                                tour.ExecuterDeplacement();
                                List<Unite> unitsAtDestination = jeu.Carte.GetUnites(destination);
                                List<Unite> unitsAtOrigin = jeu.Carte.GetUnites(pos);

                                switch (tour.ResDernierCombat)
                                {
                                    case ResultatCombat.NUL:
                                        Assert.IsTrue(unitsAtOrigin.Contains(unit));
                                        Assert.IsTrue(unitsAtDestination.Contains(enemy));
                                        jeu.Carte.SupprimerUnite(enemy, destination);
                                        break;
                                    case ResultatCombat.PERDU:
                                        Assert.IsFalse(unitsAtOrigin.Contains(unit));
                                        Assert.IsTrue(unitsAtDestination.Contains(enemy));
                                        jeu.Carte.SupprimerUnite(enemy, destination);
                                        break;
                                    case ResultatCombat.GAGNE:
                                        Assert.IsFalse(unitsAtDestination.Contains(enemy));
                                        Assert.IsTrue(unitsAtOrigin.Contains(unit) || unitsAtDestination.Contains(unit));
                                        break;
                                    default:
                                        Assert.Fail();
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }


        private Point GetDestination(Jeu jeu, Point pos, Unite unite)
        {
            int[] xOffsets = new int[6] { -1, -1, 0, 0, 1, 1 };
            int[] yOffsets = new int[6] { 0,  1, -1, 1, 0, 1 };
            for (int i = 0; i < 6; i++)
            {
                Point destination = new PointImpl(pos.x + xOffsets[i], pos.y + yOffsets[i]);
                if (destination.EstValide(jeu.Carte.Taille))
                {
                    if (jeu.Tour.SetDestination(destination) && jeu.Carte.Unites[destination.x, destination.y].Count == 0)
                    {
                        return destination;
                    }
                }
            }
            return null;
        }
    }
}
