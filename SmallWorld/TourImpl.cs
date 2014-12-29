using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    public class TourImpl : Tour
    {
        private Jeu jeu;
        private Joueur joueurCourant;
        private List<Unite> unitesSelectionnes;
        private Point positionSelectionne;
        private Point destination;
        private ResultatCombat resDernierCombat;
        public ResultatCombat ResDernierCombat
        {
            get
            {
                return this.resDernierCombat;
            }
        }

        public TourImpl(Jeu jeu, Joueur joueurCourant)
        {
            this.jeu = jeu;
            this.joueurCourant = joueurCourant;
            unitesSelectionnes = new List<Unite>();
        }

        public void SelectUnites(List<Unite> unites)
        {
            this.unitesSelectionnes = unites;
        }

        public void SelectUnites(List<Unite> unites, Point position)
        {
            this.unitesSelectionnes = unites;
            this.positionSelectionne = position;
        }

        public void UnselectUnites()
        {
            this.unitesSelectionnes.Clear();
        }

        public List<Unite> GetUnites(Point position)
        {
            return this.jeu.Carte.GetUnites(position);
        }

        //Savoir si la position est sous controle du joueurCourant
        public bool JoueurSurPosition(Point position)
        {
            List<Unite> unites = this.jeu.Carte.GetUnites(position);
            return unites.Count > 0
                && unites[0].Proprio.Equals(this.joueurCourant);
        }

        public bool SetDestination(Point destination)
        {
            if (this.unitesSelectionnes.Count == 0)
            {
                return false;
            }

            bool res = true;
            foreach (Unite unite in this.unitesSelectionnes)
            {
                bool occupé = jeu.Carte.EstPositionEnnemie(destination, unite);
                if (!unite.ValidationDeplacement(this.positionSelectionne, jeu.Carte.GetCase(this.positionSelectionne), destination, jeu.Carte.GetCase(destination)))
                {
                    res = false;
                    //TODO exception
                    break;
                }
            }

            if (res)
            {
                this.destination = destination;
            }

            return res;
        }

        public void ExecuterDeplacement()
        {
            for (int i = 0; i < this.unitesSelectionnes.Count; i++)
            {
                Unite unite = this.unitesSelectionnes[i];
                if (this.jeu.Carte.EstPositionEnnemie(this.destination, unite))
                {
                    if (!unite.Deplacement(jeu.Carte.GetCase(destination)))
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
                    if (!unite.Deplacement(jeu.Carte.GetCase(destination)))
                    {
                        //ne devrait pas arriver
                        //TODO exception
                    }
                    this.jeu.Carte.DeplacerUnite(unite, this.positionSelectionne, this.destination);
                    
                }
            }
            UnselectUnites();
        }

        public ResultatCombat Combat(Unite unite)
        {
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
                }
                else
                {
                    unite.EnleverPV();
                }

                NbAttaques--;
            }


            if (!unite.EstEnVie())
            {
                if (unite.GetType() == typeof(UniteElfe))
                {
                    int N = new Random().Next(0, 100);
                    if (N >= 50)
                    {
                        ((UniteElfe)unite).Repli();
                        this.resDernierCombat = ResultatCombat.NUL;
                    }
                    else
                    {
                        this.jeu.Carte.SupprimerUnite(unite, this.positionSelectionne);
                        this.resDernierCombat = ResultatCombat.PERDU;
                    }
                }
                else
                {
                    this.jeu.Carte.SupprimerUnite(unite, this.positionSelectionne);
                    this.resDernierCombat = ResultatCombat.PERDU;
                }
            }
            else if (!uniteAdverse.EstEnVie())
            {
                if (uniteAdverse.GetType() == typeof(UniteElfe))
                {
                    int N = new Random().Next(0, 100);
                    if (N >= 50)
                    {
                        ((UniteElfe)uniteAdverse).Repli();
                        this.resDernierCombat = ResultatCombat.NUL;
                    }
                    else
                    {
                        this.jeu.Carte.SupprimerUnite(uniteAdverse, this.destination);
                        this.resDernierCombat = ResultatCombat.GAGNE;
                    }
                        
                }
                else
                {
                    this.jeu.Carte.SupprimerUnite(uniteAdverse, this.destination);
                    this.resDernierCombat = ResultatCombat.GAGNE;
                }
            }
            else
            {
                this.resDernierCombat = ResultatCombat.NUL;
            }
            return this.resDernierCombat;
            
        }

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

        public Double CalculAttaque(Unite unite)
        {
            Double x = ((Double)unite.PointsDeVie / (Double)unite.PvDefault);
            return Math.Floor(((Double)unite.PointsAttaque * x) * 100);
        }

        public Double CalculDefense(Unite unite)
        {
            Double x = ((Double)unite.PointsDeVie / (Double)unite.PvDefault);
            return Math.Ceiling(((Double)unite.PointsDefense * x) * 100);
        }
    }
}
