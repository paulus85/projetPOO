using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Monteur
    {
        public void preparerPartie()
        {
            throw new System.NotImplementedException();
        }

        public abstract CarteImpl creationCarte()
        {
            throw new System.NotImplementedException();
        }

        public abstract Joueur[] creationJoueurs();

        public abstract TypePartie choixTypePartie();

        public abstract Joueur choixPremierJoueur();
    }
}
