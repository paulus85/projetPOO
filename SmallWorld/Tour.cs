using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    public class Tour
    {
        public Tour(JeuManager jeuManager, Joueur joueurCourant)
        {
            throw new NotImplementedException();
        }

        /*
        public void Combat(Point coord)
        {
            Unite uniteAdverse = MeilleureUniteDefensive(coord);
            int NbAttaques = new Random().Next(3, Math.Max(this.pointsDeVie, uniteAdverse.PointsDeVie));
            //Type terrain = CarteImpl.Instance.Cases[uniteAdverse.X, uniteAdverse.Y].GetType();

            while (NbAttaques != 0 || this.EstEnVie() || uniteAdverse.EstEnVie())
            {
                //Calcul des points d'attaques et de défenses en fonction des PV de l'attaquant et du défenseur
                double attaque = this.CalculAttaque();
                double defense = uniteAdverse.CalculDefense();

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
                    this.pointsDeVie--;
                }

                NbAttaques--;
            }
        }

        public Unite MeilleureUniteDefensive(Point coord)
        {
            List<Unite> unites = JoueurImpl.Unites[coord.X, coord.Y].GetUnits();
            Unite res = unites[0];
            foreach (Unite unite in unites)
            {
                if (unite.CalculDefense() > res.CalculDefense())
                    res = unite;
            }
            return res;
        }

        public Double CalculAttaque()
        {
            return Math.Floor((Double)(PointsAttaque / (this.pointsDeVie %= 5)) * 100);
        }

        public Double CalculDefense()
        {
            return Math.Ceiling((Double)(PointsDefense / (this.pointsDeVie %= 5)) * 100);
        }*/
    }
}
