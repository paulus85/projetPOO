using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Monteur : Jeu
    {
        public Joueur Joueur1
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Joueur2
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void choixPremierJoueur()
        {
            throw new System.NotImplementedException();
        }

        public CarteImpl creationCarte()
        {
            throw new System.NotImplementedException();
        }

        public JoueurImpl creationJoueur()
        {
            throw new System.NotImplementedException();
        }

        public TypePartie creationPartie()
        {
            throw new System.NotImplementedException();
        }

        public void placerUnites()
        {
            throw new System.NotImplementedException();
        }
    }
}
