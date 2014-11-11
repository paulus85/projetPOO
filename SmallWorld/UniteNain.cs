using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UniteNain : UniteImpl
    {
        public UniteNain()
        {
            throw new System.NotImplementedException();
        }

        public abstract void combat(SmallWorld.Unite uniteAdverse);

        public abstract void resetPointsDeplacement();

        public abstract void updatePointsDeplacement(SmallWorld.Case typeCase);

        public abstract void updatePointsVie(int newPV);
    }
}
