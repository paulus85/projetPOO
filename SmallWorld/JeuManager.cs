using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class JeuManager : Jeu
    {
        #region Properties

        private Joueur joueur1;
        private Joueur joueur2;
        private Joueur joueurCourant;
        private Carte carte;
        private Tour tour;
        private int nbTour;
        private int tourActuelle;

        public Joueur Joueur1
        {
            get { return this.joueur1; }
        }

        public Joueur Joueur2
        {
            get { return this.joueur2; }
        }

        public Joueur JoueurCourant
        {
            get { return this.joueurCourant; }
        }

        public Carte Carte
        {
            get { return this.carte; }
        }

        public Tour Tour
        {
            get { return this.tour; }
        }

        public int NbTour
        {
            get { return this.nbTour; }
        }

        public int TourActuelle
        {
            get { return this.tourActuelle; }
        }

        #endregion

        public JeuManager(Joueur j1, Joueur j2, Carte c, int n)
        {
            this.joueur1 = j1;
            this.joueur2 = j2;
            this.joueurCourant = j1;
            this.carte = c;
            this.nbTour = n;
            this.tourActuelle = 1;
            this.tour = new Tour(this, joueurCourant);
        }

        public bool FinDuJeu()
        {
            return EstVaincu(joueur1) ||
                   EstVaincu(joueur2) ||
                   this.tourActuelle > this.nbTour;
        }

        public bool EstVaincu(Joueur j)
        {
            return this.GetNbUnits(j) == 0;
        }

        public int GetNbUnits(Joueur j)
        {
            return carte.Unites.Length;
        }

        public void checkAllUnites()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Updates the variables of TypePartie
        /// </summary>
        public void updateVariables()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="JeuManager"/> class.
        /// </summary>
        public JeuManager()
        {

        }
    }
}
