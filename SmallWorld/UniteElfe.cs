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

        public UniteElfe(int xinit, int yinit)
        {
            X = xinit;
            Y = yinit;
        }

        public override void resetPointsDeplacement()
        {

        }

        public override void updatePointsDeplacement(SmallWorld.Case typeCase)
        {

        }

        public override void validationDplacement(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
