using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Unite
    {
        double PointsDeplacementRestant { get; }
        int PointsAttaque { get; }
        int PointsDefense { get; }
        int PvDefault { get; }
        Joueur Proprio { get; }
        int PointsDeVie { get; }
        int Numero { get; }

        void ResetPointsDeplacement();
        bool EnleverPV();
        bool EstEnVie();
        bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest, bool occupe);
        bool Deplacement(Case destination, bool occupe);
        int GetAttaqueTerrain(Case c);
        int GetDefenseTerrain(Case c);
        int GetPoints(Case c);
    }
}
