using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Peuple
    {
        int Numero { get; }

        Unite GenerationUnite(Joueur j);
    }
}
