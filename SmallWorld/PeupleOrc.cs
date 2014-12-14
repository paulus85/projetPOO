using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class PeupleOrc : FabriquePeuple
    {
        public PeupleOrc()
        {
            throw new System.NotImplementedException();
        }

        public override Unite GenerationUnite(Joueur j)
        {
            return new UniteOrc(j);
        }
    }
}
