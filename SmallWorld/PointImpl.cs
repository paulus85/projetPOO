using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    [Serializable()]
    public class PointImpl : Point
    {
        public int x
        {
            get;
            set;
        }

        public int y
        {
            get;
            set;
        }

        public PointImpl(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool EstValide(int taille)
        {
            if (this.x < 0 || this.x >= taille)
            {
                return false;
            }
            if (this.y < 0 || this.y >= taille)
            {
                return false;
            }
            return true;
        }

        public bool EstJoignable(Point pt)
        {
            return Math.Abs(this.x - pt.x) + Math.Abs(this.y - pt.y) == 1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Point pt = (Point)obj;
            return this.x == pt.x && this.y == pt.y;
        }

        public override int GetHashCode()
        {
            int hash = 42;
            hash = hash * 20 + this.x.GetHashCode();
            hash = hash * 20 + this.y.GetHashCode();
            return hash;
        }

        //Deserialization
        public PointImpl(SerializationInfo info, StreamingContext context) {
            this.x = (int)info.GetValue("x", typeof(int));
            this.y = (int)info.GetValue("y", typeof(int));
        }

        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("x", this.x);
            info.AddValue("y", this.y);
        }
    }
}
