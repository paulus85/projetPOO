using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Case
    {
        public abstract int number
        {
            get;
        }

        internal List<Unite> GetUnits(Point coord)
        {
            throw new NotImplementedException();
        }
    }
}
