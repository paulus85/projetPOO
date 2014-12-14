using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    public interface Point
    {
        bool EstValide(int taille);
        bool EstJoignable(PointImpl point);
    }
}
