using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class UniteOrc : UniteImpl
    {
        public int pointBonus;

        public UniteOrc(Joueur j) : base(j)
        {
            pointBonus = 0;
        }

        public override int GetPoints(Case typeCase){
            if (typeCase is CaseForet)
                return pointBonus;
            else
                return 1 + pointBonus;
        }

        public override bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest)
        {
            if (caseDest.Number == (int)NumCase.PLAINE)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT / 2
                    && destination.EstJoignable(pointCourant);
            }
            return base.ValidationDeplacement(pointCourant, caseCour, destination, caseDest);
        }

        public override bool Deplacement(Case destination)
        {
            if (destination.Number == (int)NumCase.PLAINE)
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
