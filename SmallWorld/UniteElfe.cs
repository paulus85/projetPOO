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

        public override void UpdatePointsDeplacement(SmallWorld.Case typeCase)
        {
            if (typeCase is CaseForet)
                this.pointsDeplacementRestant -= 0.5;
            else
                this.pointsDeplacementRestant--;
        }

        public override int GetPoints(Case typeCase)
        {
            return 1;
        }

        public override bool ValidationDeplacement(Case destination)
        {
            if(destination is CaseDesert)
            {
                int cout_deplacement = COUT_DEPLACEMENT * 2;
                if (this.pointsDeplacementRestant < cout_deplacement)
                {
                    return false;
                }
                this.pointsDeplacementRestant -= cout_deplacement;
                return true;
            }
            else if (destination is CaseForet)
            {
                double cout_deplacement = COUT_DEPLACEMENT / 2;
                if (this.pointsDeplacementRestant < cout_deplacement)
                {
                    return false;
                }
                this.pointsDeplacementRestant -= cout_deplacement;
                return true;
            }
            return base.ValidationDeplacement(destination);
        }
    }
}
