using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Joueur
    {
        int Numero { get; }
        int NumeroPeuple { get; }
        String NomJoueur { get; }
        int Points { get;  }
        FabriquePeuple Fabrique { get; }

        List<Unite> CreerUnites(int nbUnites);

        void AjoutPoints(int n);
    }
}
