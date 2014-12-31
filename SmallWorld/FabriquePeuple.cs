using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface FabriquePeuple
    {
        int Numero { get; }  

        Unite GenerationUnite(Joueur j);

    }
}
