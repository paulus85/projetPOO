using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
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
            this.tour = new TourImpl(this, joueurCourant);
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

        private int VerifPointBonus(Dictionary<Unite, Point> unites)
        {
            int n = 0;
            foreach (Unite unite in unites.Keys)
            {
                if (((UniteOrc)unite).PointBonus > 0)
                    n += ((UniteOrc)unite).PointBonus;
            }
            return n;
        }

        public void FinTour()
        {
            //Comptage des points pour le joueur courant (prise en compte des points bonus pour les orcs)
            //et réinitilisation des points de deplacement des unites
            Dictionary<Unite, Point> unites = carte.GetUnites(this.joueurCourant);
            int pointBonus = 0;
            if (unites.Keys.First().GetType() == typeof(UniteOrc))
                pointBonus = VerifPointBonus(unites);

            foreach (var unite in unites)
            {
                Case c = this.carte.GetCase(unite.Value);
                this.joueurCourant.AjoutPoints(unite.Key.GetPoints(c) + pointBonus);
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
                this.tourActuelle++;
            }

            //On change de tour
            this.tour = new TourImpl(this, this.joueurCourant);
        }

        //fichier doit indiquer le repertoire et nom du fichier utliser pour la sauvegarde (ex : c:\Smallworld\jeu.sav)
        public void SauvegarderJeu(string fichier)
        {
            if (!File.Exists(fichier))
            {
                using (FileStream stream = File.Create(fichier))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }
            }
            else
            {
                File.Delete(fichier);
                using (FileStream stream = File.Create(fichier))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }
            }
        }

        //Serialization
        public JeuManager(SerializationInfo info, StreamingContext context) {
            this.joueur1 = (Joueur)info.GetValue("Joueur1", typeof(Joueur));
            this.joueur2 = (Joueur)info.GetValue("Joueur2", typeof(Joueur));
            this.carte = (Carte)info.GetValue("Carte", typeof(Carte));
            this.nbTour = (int)info.GetValue("NbTour", typeof(int));
            this.tourActuelle = (int)info.GetValue("TourActuelle", typeof(int));
            this.joueurCourant = (Joueur)info.GetValue("JoueurCourant", typeof(Joueur));
            this.tour = new TourImpl(this, joueurCourant);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Joueur1", this.joueur1);
            info.AddValue("Joueur2", this.joueur2);
            info.AddValue("Carte", this.carte);
            info.AddValue("NbTour", this.nbTour);
            info.AddValue("TourActuelle", this.tourActuelle);
            info.AddValue("JoueurCourant", this.joueurCourant);
        }
    }
}
