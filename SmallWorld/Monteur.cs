 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Monteur
    {
        public Joueur preparerPartie()
        {
            throw new System.NotImplementedException();
        }

        public abstract Joueur choixPremierJoueur();

        public abstract Joueur[] creationJoueurs();

        public abstract TypePartie choixTypePartie();


        public abstract CarteImpl creationCarte();
    }
}
