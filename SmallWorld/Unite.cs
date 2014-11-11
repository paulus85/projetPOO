using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Unite
    {
        void resetPointsDeplacement();

        void updatePointsDeplacement(Case typeCase);

        void combat(Unite uniteAdverse);

        void updatePointsVie(int newPV);
    }
}
