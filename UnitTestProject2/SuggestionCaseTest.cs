using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;

namespace UnitTestWrapper
{
    [TestClass]
    public class SuggestionCaseTest
    {
        [TestMethod]
        unsafe public void TestSuggestionCase()
        {
            for (int size = 5; size < 20; size += 5)
            {
                this.TestSuggestionCase(size, 0, 1);
                //this.TestSuggestionCase(size, 1, 2);
                //this.TestSuggestionCase(size, 2, 0);
            }
        }

        private void TestSuggestionCase(int taille, int peupleJoueur1, int peupleJoueur2)
        {
            int[][] carte = Wrapper.Wrapper.genererCarte(taille);
            carte[0][1] = 3;
            carte[1][0] = 1;
            int[][] unites = new int[taille][];
            for (int i = 0; i < taille; i++)
            {
                unites[i] = new int[taille];
            }
            unites[0][0] = 1 ;
            unites[taille - 1][taille - 1] = 2;
            unites[1][1] = 1;

            double[][] ptsDeplacement = new double[taille][];
            for (int i = 0; i < taille; i++)
            {
                ptsDeplacement[i] = new double[taille];
            }
            ptsDeplacement[0][0] = 0.5;
            ptsDeplacement[taille - 1][taille - 1] = 2;
            ptsDeplacement[1][1] = 1;

            int[][] suggestions = Wrapper.Wrapper.getSuggestion(carte, taille, peupleJoueur1, peupleJoueur2, 0, 0, unites, ptsDeplacement, 1);

            for (int i = 0; i < 3; i++)
            {
                Assert.IsTrue(suggestions[i][0] <= 1);
                Assert.IsTrue(suggestions[i][1] <= 1);
            }
        }
    }
}
