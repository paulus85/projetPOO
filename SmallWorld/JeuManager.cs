﻿using System;
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

        public int GetNbUnites(Joueur j)
        {
            return this.carte.GetUnites(j).Count;
        }

        public bool EstVaincu(Joueur j)
        {
            return this.GetNbUnites(j) == 0;
        }

        public Joueur Vainqueur()
        {
            if (this.EstVaincu(this.joueur1))
            {
                return this.joueur2;
            }
            if (this.EstVaincu(this.joueur2))
            {
                return this.joueur1;
            }
            if (this.joueur1.Points < this.joueur2.Points)
            {
                return this.joueur2;
            }
            if (this.joueur1.Points > this.joueur2.Points)
            {
                return this.joueur1;
            }
            return null;
        }

        public void FinTour()
        {
            //Comptage des points pour le joueur courant et 
            //réinitilisation des points de deplacement des unites
            Dictionary<Unite, Point> unites = carte.GetUnites(this.joueurCourant);
            foreach (var unite in unites)
            {
                Case c = this.carte.GetCase(unite.Value);
                this.joueurCourant.AjoutPoints(unite.Key.GetPoints(c));
                unite.Key.ResetPointsDeplacement();
            }

            //On met à jour le joueurCourant
            if (this.joueurCourant == this.joueur1)
            {
                this.joueurCourant = this.joueur2;
            }
            else
            {
                this.joueurCourant = joueur1;
                this.nbTour++;
            }

            //On change de tour
            this.tour = new Tour(this, this.joueurCourant);
        }
    }
}
