using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class UniteElfe : UniteImpl
    {
        public UniteElfe(Joueur j) : base(j)
        {
            
        }

        public override int GetPoints(Case typeCase)
        {
            return 1;
        }

        public override bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest)
        {
            if (caseDest.Number == (int)NumCase.DESERT)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT * 2
                    && destination.EstJoignable(pointCourant);
            }
            else if (caseDest.Number == (int)NumCase.FORET)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT / 2
                    && destination.EstJoignable(pointCourant);
            }
            return base.ValidationDeplacement(pointCourant, caseCour, destination, caseDest);
        }

        public override bool Deplacement(Case destination)
        {
            if (destination.Number == (int)NumCase.DESERT)
            {
                int cout_deplacement = COUT_DEPLACEMENT * 2;
                if (this.pointsDeplacementRestant < cout_deplacement)
                {
                    return false;
                }
                this.pointsDeplacementRestant -= cout_deplacement;
                return true;
            }
            else if (destination.Number == (int)NumCase.FORET)
            {
                int cout_deplacement = COUT_DEPLACEMENT / 2;
                if (this.pointsDeplacementRestant < cout_deplacement)
                {
                    return false;
                }
                this.pointsDeplacementRestant -= cout_deplacement;
                return true;
            }
            return base.Deplacement(destination);
        }
    }
}
