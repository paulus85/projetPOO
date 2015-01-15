using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;

namespace UnitTestWrapper
{
    [TestClass]
    public class CreationCarteTest
    {
        [TestMethod]
        public void TestValuesMap()
        {
            this.TestValeuresCases(6);
            this.TestValeuresCases(10);
            this.TestValeuresCases(14);
        }

        private void TestValeuresCases(int size)
        {
            int[][] map = Wrapper.Wrapper.genererCarte(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.IsTrue(map[i][j] <= 4);
                    Assert.IsTrue(map[i][j] >= 0);
                }
            }
        }

        [TestMethod]
        public void TestPlacementJoueurs()
        {
            this.TestPlacementJoueurs(6);
            this.TestPlacementJoueurs(10);
            this.TestPlacementJoueurs(14);
        }

        private void TestPlacementJoueurs(int size)
        {
            int[][] points =  Wrapper.Wrapper.placementJoueur(size);
            Assert.IsTrue(points[0][0] >= 0); Assert.IsTrue(points[0][0] < size);
            Assert.IsTrue(points[0][1] >= 0); Assert.IsTrue(points[0][0] < size);
            Assert.IsTrue(points[1][0] >= 0); Assert.IsTrue(points[0][0] < size);
            Assert.IsTrue(points[1][1] >= 0); Assert.IsTrue(points[0][0] < size);
        }
    }
}
