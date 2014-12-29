using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Jeu
    {
        Joueur Joueur1 { get; }

        Joueur Joueur2 { get; }

        Joueur JoueurCourant { get; }

        Carte Carte { get; }

        int NbTour { get; }

        int TourActuelle { get; }

        TourImpl Tour { get; }

        bool FinDuJeu();

        int GetNbUnites(Joueur j);

        bool EstVaincu(Joueur j);

        Joueur Vainqueur();

        void FinTour();
    }
}
