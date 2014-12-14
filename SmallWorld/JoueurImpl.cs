using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class JoueurImpl : Joueur
    {
        private FabriquePeuple fabrique;

        private int numero;
        private String nomJoueur;
        private int points;

        private static int cpt = 0;

        public int Numero
        {
            get { return this.numero; }
        }

        public String NomJoueur
        {
            get { return this.nomJoueur;  }
        }

        public int Points
        {
            get { return points;  }
        }

        public JoueurImpl(String nom, FabriquePeuple fab)
        {
            this.nomJoueur = nom;
            this.fabrique = fab;
            this.points = 0;
            cpt++;
            this.numero = cpt;
        }

        public List<Unite> CreerUnites(int nbUnites)
        {
            List<Unite> unites = new List<Unite>();
            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(fabrique.GenerationUnite(this));
            }
            return unites;
        }
    }
}
