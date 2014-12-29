using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
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

        // override object.Equals
        //public override bool Equals(object obj)
        //{
        //    //       
        //    // See the full list of guidelines at
        //    //   http://go.microsoft.com/fwlink/?LinkID=85237  
        //    // and also the guidance for operator== at
        //    //   http://go.microsoft.com/fwlink/?LinkId=85238
        //    //

        //    if (obj == null || GetType() != obj.GetType())
        //    {
        //        return false;
        //    }

        //    // TODO: write your implementation of Equals() here
            
        //    return ((this.x == (obj as Point).x) && (this.y == (obj as Point).y );
        //}

        public bool Equals(Point p)
        {
            return ((this.x == p.x) && (this.y == p.y));
        }

        
    }
}
