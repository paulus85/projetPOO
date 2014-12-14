using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Carte
    {
        Case[,] Cases { get; }
        List<Unite>[,] Unites { get;  }

        Carte ConstruireCarte(int size);
        void PlacerUnite(Unite unite, PointImpl position);

    }
}
