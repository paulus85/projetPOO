using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Unite
    {
        int PointsDeVie { get; }
        void ResetPointsDeplacement();
        void UpdatePointsDeplacement(Case typeCase);
        bool EnleverPV();
        bool EstEnVie();
        bool ValidationDeplacement(Case destination);
        int GetPoints(Case c);
    }
}
