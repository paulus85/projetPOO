using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    public interface Point
    {
        int x
        {
            get;
            set;
        }
        int y
        {
            get;
            set;
        }

        bool EstValide(int taille);
        bool EstJoignable(Point point);
        string ToString();
    }
}
