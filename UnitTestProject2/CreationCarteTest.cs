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
            this.TestValeuresCases(5);
        }

        private void TestValeuresCases(int size)
        {
            int[][] map = Wrapper.Wrapper.genererCarte(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.IsTrue(map[i][j] <= 3);
                    Assert.IsTrue(map[i][j] >= 0);
                }
            }
        }
    }
}
