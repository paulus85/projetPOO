using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UniteImpl : Unite
    {
        private int points;
        private float pointsDeplacement;
        private int x;
        private int y;
        private int pointsAttaque;
        private int pointsDefense;

        public abstract void combat(SmallWorld.Unite uniteAdverse);

        public abstract void resetPointsDeplacement();

        public abstract void updatePointsDeplacement(SmallWorld.Case typeCase);

        public abstract void updatePointsVie(int newPV);
    }
}
