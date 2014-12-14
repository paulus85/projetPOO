using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class PeupleElfe : FabriquePeuple
    {
        public override Unite GenerationUnite(Joueur j)
        {
            return new UniteElfe(j);
        }
    }
}
