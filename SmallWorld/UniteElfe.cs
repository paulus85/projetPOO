using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class UniteElfe : UniteImpl
    {
        public UniteElfe()
        {
            throw new System.NotImplementedException();
        }

        public void combat(SmallWorld.Unite uniteAdverse);

        public void resetPointsDeplacement();

        public  void updatePointsDeplacement(SmallWorld.Case typeCase);

        public void updatePointsVie(int newPV);
    }
}
