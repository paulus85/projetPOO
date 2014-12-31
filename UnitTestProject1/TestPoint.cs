using System;
using SmallWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        [TestMethod]
        public void TestSerializationPoint()
        {
            Point pts = new PointImpl(20, 44);

            // Serializes:
            Stream stream = File.Open("Point.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, pts);
            stream.Close();

            // Deserializes and checks the values:
            stream = File.Open("Point.sav", FileMode.Open);
            formatter = new BinaryFormatter();
            Point savedPts = (Point)formatter.Deserialize(stream);
            stream.Close();
            Assert.AreEqual(pts.x, savedPts.x);
            Assert.AreEqual(pts.y, savedPts.y);
        }
    }
}
