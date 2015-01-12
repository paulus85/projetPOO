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

        Tour Tour { get; }

        bool FinDuJeu();

        int GetNbUnites(Joueur j);

        int GetNbUnites(Point p);

        bool EstVaincu(Joueur j);

        Joueur Vainqueur();

        void FinTour();

        void SauvegarderJeu(string fichier);
    }
}
