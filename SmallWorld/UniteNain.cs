using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class UniteNain : UniteImpl
    {

        public UniteNain(Joueur j) : base(j)
        {

        }

        public override int GetPoints(Case typeCase)
        {
            if (typeCase.Number == (int)NumCase.PLAINE)
                return 0;
            else
                return 1;
        }

        public override bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest)
        {
            if (caseDest.Number == (int)NumCase.PLAINE)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT / 2
                    && destination.EstJoignable(pointCourant);
            }
            else if (caseDest.Number == (int)NumCase.MONTAGNE && caseCour.Number == (int)NumCase.MONTAGNE)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT;
            }
            return base.ValidationDeplacement(pointCourant, caseCour, destination, caseDest);
        }

        public override bool Deplacement(Case destination)
        {
            if (destination.Number == (int)NumCase.PLAINE)
            {
                double cout_deplacement = COUT_DEPLACEMENT / 2.0;
                if (this.pointsDeplacementRestant < cout_deplacement)
                {
                    return false;
                }
                this.pointsDeplacementRestant -= cout_deplacement;
                return true;
            }
            return base.Deplacement(destination);
        }

        public UniteNain(SerializationInfo info, StreamingContext context): base(info, context) {

        }
    }
}
