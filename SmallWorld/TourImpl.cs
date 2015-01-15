using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    [Serializable()]
    public class TourImpl : Tour
    {
        private Jeu jeu;
        private Joueur joueurCourant;
        private List<Unite> unitesSelectionnes;
        private Point positionSelectionne;
        private Point destination;
        private ResultatCombat resDernierCombat;
        private List<String> resumeCombat;

        public ResultatCombat ResDernierCombat
        {
            get
            {
                return this.resDernierCombat;
            }
        }
        
        public List<String> ResumeCombat
        {
            get
            {
                return this.resumeCombat;
            }
        }

        /// <summary>
        /// Constructeur d'un tour
        /// </summary>
        /// <param name="jeu">Le jeu auquel appartient le tour</param>
        /// <param name="joueurCourant">Le joueur pour ce tour</param>
        public TourImpl(Jeu jeu, Joueur joueurCourant)
        {
            this.jeu = jeu;
            this.joueurCourant = joueurCourant;
            unitesSelectionnes = new List<Unite>();
        }

        /// <summary>
        /// Définir les unités sélectionnées
        /// </summary>
        /// <param name="unites">La liste des unités sélectionnées</param>
        public void SelectUnites(List<Unite> unites)
        {
            this.unitesSelectionnes = unites;
        }

        /// <summary>
        /// Définir les unités sélectionnées à partir du point sélectionné
        /// </summary>
        /// <param name="unites">La liste des unités sélectionnées</param>
        /// <param name="position">Le point d'où on sélectionne les unités</param>
        public void SelectUnites(List<Unite> unites, Point position)
        {
            this.unitesSelectionnes = unites;
            this.positionSelectionne = position;
        }

        /// <summary>
        /// Déselectionner les unités
        /// </summary>
        public void UnselectUnites()
        {
            this.unitesSelectionnes.Clear();
        }

        /// <summary>
        /// Récupérer les unités d'un points
        /// </summary>
        /// <param name="position">Le point d'où l'on veut récupérer les unités</param>
        /// <returns>La liste des unités du point spécifié</returns>
        public List<Unite> GetUnites(Point position)
        {
            return this.jeu.Carte.GetUnites(position);
        }

        /// <summary>
        /// Savoir si le point spécifié est sous controle du joueurCourant
        /// </summary>
        /// <param name="position">Le point d'où l'on veut vérifier</param>
        /// <returns>Vrai si le point est sous le controle du joueur</returns>
        public bool JoueurSurPosition(Point position)
        {
            List<Unite> unites = this.jeu.Carte.GetUnites(position);
            return unites.Count > 0
                && unites[0].Proprio.Equals(this.joueurCourant);
        }

        /// <summary>
        /// Définir le point de destination pour les unités sélectionnées
        /// </summary>
        /// <param name="destination">Le point de destination</param>
        /// <returns>Vrai si TOUTES les unités sélectionnés peuvent se déplacer sur la destination</returns>
        public bool SetDestination(Point destination)
        {
            if (this.unitesSelectionnes.Count == 0)
            {
                return false;
            }

            bool res = true;
            foreach (Unite unite in this.unitesSelectionnes)
            {
                bool occupe = jeu.Carte.EstPositionEnnemie(destination, unite);
                if (!unite.ValidationDeplacement(this.positionSelectionne, jeu.Carte.GetCase(this.positionSelectionne), destination, jeu.Carte.GetCase(destination), occupe))
                {
                    res = false;
                    break;
                }
            }

            if (res)
            {
                this.destination = destination;
            }

            return res;
        }

        /// <summary>
        /// Exécuter le déplacement des unités selectionnés et mener des combats si il y a des unités ennemies sur la destination
        /// </summary>
        public void ExecuterDeplacement()
        {
            this.resumeCombat.Clear();
            for (int i = 0; i < this.unitesSelectionnes.Count; i++)
            {
                Unite unite = this.unitesSelectionnes[i];
                bool occupe = jeu.Carte.EstPositionEnnemie(destination, unite);
                if (this.jeu.Carte.EstPositionEnnemie(this.destination, unite))
                {
                    if (!unite.Deplacement(jeu.Carte.GetCase(destination), occupe))
                    {
                        //ne devrait pas arriver
                        //TODO exception
                    }
                    //on fait le combat
                    if (Combat(unite) == ResultatCombat.GAGNE)
                    {
                        if (!this.jeu.Carte.EstPositionEnnemie(this.destination, unite))
                        {
                            this.jeu.Carte.DeplacerUnite(unite, this.positionSelectionne, this.destination);
                        }
                    }
                }
                else
                {
                    if (!unite.Deplacement(jeu.Carte.GetCase(destination), occupe))
                    {
                        //ne devrait pas arriver
                        //TODO exception
                    }
                    this.jeu.Carte.DeplacerUnite(unite, this.positionSelectionne, this.destination);
                    
                }
            }
            UnselectUnites();
        }

        /// <summary>
        /// Algo de combat entre 2 unités adverses
        /// </summary>
        /// <param name="unite">L'unite du joueur courant</param>
        /// <returns>Le résultat du combat</returns>
        public ResultatCombat Combat(Unite unite)
        {
            int round = 0;
            Unite uniteAdverse = MeilleureUniteDefensive(this.destination);
            int NbAttaques = new Random().Next(3, Math.Max(unite.PointsDeVie, uniteAdverse.PointsDeVie)+2);

            while (NbAttaques > 0 && unite.EstEnVie() && uniteAdverse.EstEnVie())
            {
                //Calcul des points d'attaques et de défenses en fonction des PV de l'attaquant et du défenseur
                double attaque = CalculAttaque(unite);
                double defense = CalculDefense(uniteAdverse);

                //Calcul du pourcentage de chance de victoire pour l'attaquant
                double PctgAttaquant;
                if (attaque == defense)
                {
                    PctgAttaquant = 50;
                }
                else
                {
                    double rapportForce = Math.Min(attaque, defense) / Math.Max(attaque, defense);
                    if (attaque > defense)
                    {
                        PctgAttaquant = 50 - rapportForce;
                    }
                    else
                    {
                        PctgAttaquant = 50 + rapportForce;
                    }
                }

                //Tirage du nombre aléatoire
                int N = new Random().Next(0, 100);

                //Décision sur la victoire
                if (N >= PctgAttaquant)
                {
                    uniteAdverse.EnleverPV();
                    resumeCombat.Add("Round " + round + " : " + uniteAdverse.ToString() + " gagne");
                }
                else
                {
                    unite.EnleverPV();
                    resumeCombat.Add("Round " + round + " : " + uniteAdverse.ToString() + " perd");
                }

                NbAttaques--;
                round++;
            }

            if (!unite.EstEnVie())
            {
                if (unite.GetType() == typeof(UniteElfe))
                {
                    int N = new Random().Next(2);
                    if (N == 1)
                    {
                        ((UniteElfe)unite).Repli();
                        this.resDernierCombat = ResultatCombat.NUL;
                        resumeCombat.Add(unite.ToString() + " a perdu le combat mais se replie");
                    }
                    else
                    {
                        this.jeu.Carte.SupprimerUnite(unite, this.positionSelectionne);
                        this.resDernierCombat = ResultatCombat.PERDU;
                        resumeCombat.Add(unite.ToString() + " meurt");
                    }
                }
                else
                {
                    this.jeu.Carte.SupprimerUnite(unite, this.positionSelectionne);
                    this.resDernierCombat = ResultatCombat.PERDU;
                    resumeCombat.Add(unite.ToString() + " meurt");
                }
            }
            else if (!uniteAdverse.EstEnVie())
            {
                if (uniteAdverse.GetType() == typeof(UniteElfe))
                {
                    int N = new Random().Next(2);
                    if (N == 1)
                    {
                        ((UniteElfe)uniteAdverse).Repli();
                        this.resDernierCombat = ResultatCombat.NUL;
                        resumeCombat.Add(uniteAdverse.ToString() + " a perdu le combat mais se replie");
                    }
                    else
                    {
                        this.jeu.Carte.SupprimerUnite(uniteAdverse, this.destination);
                        this.resDernierCombat = ResultatCombat.GAGNE;
                        resumeCombat.Add(uniteAdverse.ToString() + " meurt");
                    }
                }
                else
                {
                    this.jeu.Carte.SupprimerUnite(uniteAdverse, this.destination);
                    this.resDernierCombat = ResultatCombat.GAGNE;
                    resumeCombat.Add(uniteAdverse.ToString() + " meurt");
                }
            }
            else
            {
                this.resDernierCombat = ResultatCombat.NUL;
                resumeCombat.Add("Match nul");
            }
            return this.resDernierCombat;
            
        }

        /// <summary>
        /// Trouver la meilleure unité défensive adverse à un point donné
        /// </summary>
        /// <param name="destination">Le point donné</param>
        /// <returns>L'unité adverse ayant la meilleure défense</returns>
        public Unite MeilleureUniteDefensive(Point destination)
        {
            List<Unite> unites = this.jeu.Carte.GetUnites(destination);
            Unite res = unites[0];
            foreach (Unite unite in unites)
            {
                if (CalculDefense(unite) > CalculDefense(res))
                    res = unite;
            }
            return res;
        }

        /// <summary>
        /// Calculer l'attaque d'une unité
        /// </summary>
        /// <param name="unite">L'unité visée</param>
        /// <returns>L'attaque de l'unité</returns>
        public Double CalculAttaque(Unite unite)
        {
            Double x = ((Double)unite.PointsDeVie / (Double)unite.PvDefault);
            return Math.Floor(((Double)unite.PointsAttaque * x) * 100);
        }

        /// <summary>
        /// Calculer la défense d'une unité
        /// </summary>
        /// <param name="unite">L'unité visée</param>
        /// <returns>La défense de l'unité</returns>
        public Double CalculDefense(Unite unite)
        {
            Double x = ((Double)unite.PointsDeVie / (Double)unite.PvDefault);
            return Math.Ceiling(((Double)unite.PointsDefense * x) * 100);
        }

        /// <summary>
        /// Avoir des suggestions de cases pour les déplacement des unité sélectionnées à un point donné
        /// </summary>
        /// <param name="unite">Une unité sélectionnée</param>
        /// <param name="pos">Le point où se trouve l'unité</param>
        /// <returns>Max 3 proposition de déplacement</returns>
        unsafe public List<Point> SuggestionsCase(Unite unite, Point pos)
        {
            Carte carte = this.jeu.Carte;
            Case[,] cases = carte.Cases;
            int[][] carteBis = new int[carte.Taille][];
            int[][] unites = new int[carte.Taille][];
            double[][] ptsdeplacement = new double[carte.Taille][];
            for (int i = 0; i < carte.Taille; i++)
            {
                carteBis[i] = new int[carte.Taille];
                unites[i] = new int[carte.Taille];
                ptsdeplacement[i] = new double[carte.Taille];
                for (int j = 0; j < carte.Taille; j++)
                {
                    carteBis[i][j] = cases[i, j].Numero;
                    List<Unite> unitsAtPos = carte.GetUnites(new PointImpl(i, j));
                    if (unitsAtPos.Count > 0)
                    {
                        unites[i][j] = unitsAtPos[0].Proprio.Numero;
                    }
                    ptsdeplacement[i][j] = unitesSelectionnes[0].PointsDeplacementRestant;
                }
            }


            int peupleJoueur1 = this.jeu.Joueur1.NumeroPeuple;
            int peupleJoueur2 = this.jeu.Joueur2.NumeroPeuple;
            int[][] result = Wrapper.Wrapper.getSuggestion(carteBis, carte.Taille, peupleJoueur1, peupleJoueur2, pos.x, pos.y, unites, ptsdeplacement, this.joueurCourant.Numero);
            List<Point> suggestions = new List<Point>();
            for (int i = 0; i < 3; i++)
            {
                if (result[i][0] != -1 && result[i][1] != -1)
                {
                    suggestions.Add(new PointImpl(result[i][0], result[i][1]));
                }
            }
            return suggestions;
        }
    }
}
