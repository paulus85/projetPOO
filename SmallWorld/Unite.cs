using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Unite
    {

        int PointsDeVie { get; set; }

        int X { get; set; }

        int Y { get; set; }

        void resetPointsDeplacement();

        void updatePointsDeplacement(Case typeCase);

        void combat(Unite uniteAdverse);

        void updatePointsVie(int newPV);

        double calculAttaque();

        double calculDefense();
    }
}
