 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Monteur
    {
        public Jeu preparerPartie()
        {
            Jeu j = new JeuManager();
            j.CarteImpl = creationCarte();
            j.Joueurs = creationJoueurs();
            ///TODO 
            return j;
        }

        public abstract Joueur choixPremierJoueur();

        public abstract JoueurImpl[] creationJoueurs();

        public abstract TypePartie choixTypePartie();


        public abstract CarteImpl creationCarte();
    }
}
