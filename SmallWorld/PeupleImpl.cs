using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class PeupleImpl : Peuple
    {
        public abstract Unite GenerationUnite(Joueur j);
    }
}
