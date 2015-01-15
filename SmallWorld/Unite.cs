using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Unite
    {
        double PointsDeplacementRestant { get; }
        double PointsAttaque { get; }
        double PointsDefense { get; }
        int PvDefault { get; }
        Joueur Proprio { get; }
        int PointsDeVie { get; }
        int Numero { get; }

        void ResetPointsDeplacement();
        bool EnleverPV();
        bool EstEnVie();
        bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest, bool occupe);
        bool Deplacement(Case destination, bool occupe);
        int GetPoints(Case c);

        String ToString();
    }
}
