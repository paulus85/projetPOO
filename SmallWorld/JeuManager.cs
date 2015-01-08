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

        /// <summary>
        /// Constructeur d'une partie
        /// </summary>
        /// <param name="j1">Le joueur 1</param>
        /// <param name="j2">Le joueur 2</param>
        /// <param name="c">La carte des cases et unités</param>
        /// <param name="n">Le nombre de tour pour la partie</param>
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

        /// <summary>
        /// Vérifie si le jeu est terminé ou non
        /// </summary>
        /// <returns>Vrai si le neu est finit</returns>
        public bool FinDuJeu()
        {
            return EstVaincu(joueur1) ||
                   EstVaincu(joueur2) ||
                   this.tourActuelle > this.nbTour;
        }

        /// <summary>
        /// Avoir le nombre d'unités d'un joueur
        /// </summary>
        /// <param name="j">Le joueur concerné</param>
        /// <returns>Le nombre d'unité du joueur</returns>
        public int GetNbUnites(Joueur j)
        {
            return this.carte.GetUnites(j).Count;
        }

        /// <summary>
        /// Savoir si un joueur esr vaincu ou non
        /// </summary>
        /// <param name="j">Le joueur concerné</param>
        /// <returns>Vrai si le joueur est vaincu</returns>
        public bool EstVaincu(Joueur j)
        {
            return this.GetNbUnites(j) == 0;
        }

        /// <summary>
        /// Savoir quel joueur est vainqueur de la partie
        /// </summary>
        /// <returns>Le joueur victorieux</returns>
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

        /// <summary>
        /// Vérifie les point bonus concernant les unités de type Orc
        /// </summary>
        /// <param name="unites">Le ditionnaire des Unité/Point</param>
        /// <returns>La valeur des points bonus à prendre en compte</returns>
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

        /// <summary>
        /// Met fin au tour courant, compte les points du joueur, change les joueurs en instanciant un nouveau tour pour la partie en cours
        /// </summary>
        public void FinTour()
        {
            //Comptage des points pour le joueur courant (prise en compte des points bonus pour les orcs)
            Dictionary<Unite, Point> unites = carte.GetUnites(this.joueurCourant);
            int pointBonus = 0;
            if (unites.Keys.First().GetType() == typeof(UniteOrc))
                pointBonus = VerifPointBonus(unites);

            foreach(var point in unites.Values.Distinct())
            {
                Case c = this.carte.GetCase(point);
                this.joueurCourant.AjoutPoints(carte.GetUnites(point)[0].GetPoints(c) + pointBonus); 
            }
            
            //Réinitilisation des points de deplacement des unites
            foreach (var unite in unites)
            {
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

        /// <summary>
        /// Sauvegarder la partie en cours
        /// </summary>
        /// <param name="fichier">Repertoire et nom du fichier utliser pour la sauvegarde (ex : c:\Smallworld\jeu.sav)</param>
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

        /// <summary>
        /// Méthode pour la deserialization du jeu
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public JeuManager(SerializationInfo info, StreamingContext context) {
            this.joueur1 = (Joueur)info.GetValue("Joueur1", typeof(Joueur));
            this.joueur2 = (Joueur)info.GetValue("Joueur2", typeof(Joueur));
            this.carte = (Carte)info.GetValue("Carte", typeof(Carte));
            this.nbTour = (int)info.GetValue("NbTour", typeof(int));
            this.tourActuelle = (int)info.GetValue("TourActuelle", typeof(int));
            this.joueurCourant = (Joueur)info.GetValue("JoueurCourant", typeof(Joueur));
            this.tour = new TourImpl(this, joueurCourant);
        }

        /// <summary>
        /// Méthode pour la serialization du jeu
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
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
