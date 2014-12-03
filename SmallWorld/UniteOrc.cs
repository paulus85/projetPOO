using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class UniteOrc : UniteImpl
    {
        public UniteOrc()
        {
            throw new System.NotImplementedException();
        }

        public UniteOrc(int xinit, int yinit)
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
