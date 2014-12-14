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

        public override void UpdatePointsDeplacement(SmallWorld.Case typeCase)
        {
            this.pointsDeplacementRestant--;
        }

        public override int GetPoints(Case typeCase){
            if (typeCase is CaseForet)
                return pointBonus;
            else
                return 1 + pointBonus;
        }
    }
}
