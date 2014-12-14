using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    public class PointImpl : Point
    {
        public int x;
        public int y;

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

        public bool EstJoignable(PointImpl pt)
        {
            return Math.Abs(this.x - pt.x) + Math.Abs(this.y - pt.y) == 1;
        }
    }
}
