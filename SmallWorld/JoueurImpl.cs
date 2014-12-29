using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class JoueurImpl : Joueur
    {
        #region Properties

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

        public FabriquePeuple Fabrique
        {
            get { return fabrique; }
        }

        #endregion

        public JoueurImpl(String nom, FabriquePeuple fab)
        {
            this.nomJoueur = nom;
            this.fabrique = fab;
            this.points = 0;
            cpt++;
            this.numero = cpt;
        }

        public JoueurImpl()
        {
            //Temporaire, histoire de faire des tests
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

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Joueur))
            {
                return false;
            }
            Joueur j = (Joueur)obj;
            return this.numero == j.Numero;
        }

        public override int GetHashCode()
        {
            return this.numero.GetHashCode();
        }

        public void AjoutPoints(int n)
        {
            //TODO Exception
            this.points += n;
        }
    }
}
