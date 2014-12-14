using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class PeupleNain : FabriquePeuple
    {
        public PeupleNain()
        {
            throw new System.NotImplementedException();
        }

        public override Unite GenerationUnite(Joueur j)
        {
            return new UniteNain(j);
        }
    }
}
