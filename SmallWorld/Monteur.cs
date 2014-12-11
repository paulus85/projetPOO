 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Monteur
    {
        public string NomJoueur1 { get; set; }
        public string NomJoueur2 { get; set; }

        public Jeu preparerPartie()
        {
            Jeu j = new JeuManager();
            j.CarteImpl = creationCarte();
            j.Joueurs = creationJoueurs();
            // TODO: implémenter ici
            return j;
        }

        public abstract Joueur choixPremierJoueur();

        public abstract JoueurImpl[] creationJoueurs();

        public abstract TypePartie choixTypePartie();


        public abstract CarteImpl creationCarte();
    }
}
