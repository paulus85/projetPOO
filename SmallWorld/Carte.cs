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
        int Taille { get; }


        Carte ConstruireCarte(int size);
        void PlacerUnite(Unite unite, Point position);

        Dictionary<Unite, Point> GetUnites(Joueur j);

        List<Unite> GetUnites(Point p);

        Case GetCase(Point p);

        bool SupprimerUnite(Unite unit, Point position);

        bool EstPositionEnnemie(Point position, Unite unite);

        void DeplacerUnite(Unite unite, Point pointCourant, Point destination);

    }
}
