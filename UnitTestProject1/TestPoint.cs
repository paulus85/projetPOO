using System;
using SmallWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class TestPoint
    {
        [TestMethod]
        public void TestConstructor()
        {
            Point pt = new PointImpl(1, 5);
            Assert.AreEqual(1, pt.x);
            Assert.AreEqual(5, pt.y);
            pt.x = 10;
            Assert.AreEqual(10, pt.x);
            Assert.AreEqual(5, pt.y);
        }

        [TestMethod]
        public void TestIsNext()
        {
            Assert.IsTrue(new PointImpl(0, 1).EstJoignable(new PointImpl(0, 0)));
            Assert.IsFalse(new PointImpl(1, 1).EstJoignable(new PointImpl(0, 0)));
            Assert.IsTrue(new PointImpl(14, 14).EstJoignable(new PointImpl(14, 13)));
            Assert.IsFalse(new PointImpl(14, 14).EstJoignable(new PointImpl(13, 13)));
        }

        [TestMethod]
        public void TestEquals()
        {
            Assert.IsTrue(new PointImpl(1, 5).Equals(new PointImpl(1, 5)));
            Assert.IsFalse(new PointImpl(1, 5).Equals(new PointImpl(5, 1)));
            Assert.IsFalse(new PointImpl(0, 5).Equals(new PointImpl(5, 0)));
            Assert.IsFalse(new PointImpl(1, 5).Equals(new PointImpl(1, 6)));
            Assert.IsFalse(new PointImpl(1, 5).Equals(new PointImpl(0, 5)));
        }
    }
}
