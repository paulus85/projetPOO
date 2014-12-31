using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;

namespace UnitTestWrapper
{
    [TestClass]
    public class SuggestionCaseTest
    {
        [TestMethod]
        unsafe public void TestValeurs()
        {
            for (int size = 5; size < 20; size += 5)
            {
                this.TestValeurs(size, 1, 2);
                this.TestValeurs(size, 2, 3);
                this.TestValeurs(size, 3, 1);
            }
        }
        
        private void TestValeurs(int taille, int peupleJoueur1, int peupleJoueur2)
        {
            int[][] carte = Wrapper.Wrapper.genererCarte(taille);
            int[][] unites = new int[taille][];
            for (int i = 0; i < taille; i++)
            {
                unites[i] = new int[taille];
            }
            unites[0][0] = 1;
            unites[taille - 1][taille - 1] = 2;
            unites[0][1] = 1;
            unites[1][1] = 1;

            int[][] suggestions = Wrapper.Wrapper.getSuggestion(carte, taille, peupleJoueur1, peupleJoueur2, 0, 0, unites, 1);

            Assert.IsTrue(suggestions[0][0] <= 1);
            Assert.IsTrue(suggestions[0][1] <= 1);
            for (int i = 0; i < 3; i++)
            {
                Assert.IsTrue(suggestions[i][0] <= 1);
                Assert.IsTrue(suggestions[i][1] <= 1);
            }
        }
    }
}
