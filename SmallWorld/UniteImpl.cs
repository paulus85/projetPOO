using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UniteImpl : Unite
    {
        private int points;
        private float pointsDeplacement;
        private int x;
        private int y;
        private int pointsAttaque;
        private int pointsDefense;
        private int pointDeVie;

        public int PointsDeVie { get { return pointDeVie; } set { pointDeVie = value;} }

        public int X { get; set; }

        public int Y { get; set; }

        public void combat(SmallWorld.Unite uniteAdverse)
        {
            int NbAttaques = new Random().Next(3, Math.Max(this.PointsDeVie, uniteAdverse.PointsDeVie));
            Type terrain = CarteImpl.instance.Case[uniteAdverse.X, uniteAdverse.Y].GetType();

            while (NbAttaques != 0 || this.PointsDeVie != 0 || uniteAdverse.PointsDeVie != 0)
            {
                //Calcul des points d'attaques et de défenses en fonction des PV de l'attaquant et du défenseur
                double attaque = this.calculAttaque();
                double defense = uniteAdverse.calculDefense();

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

                if (N >= PctgAttaquant)
                {
                    uniteAdverse.PointsDeVie--;
                }
                else
                {
                    this.PointsDeVie--;
                }

                NbAttaques--;
            }
        }

        public Double calculAttaque()
        {
            return Math.Floor((Double)(pointsAttaque/(pointDeVie %= 5)) * 100);
        }

        public Double calculDefense()
        {
            return Math.Ceiling((Double)(pointsDefense / (pointDeVie %= 5)) * 100);
        }

        public abstract void resetPointsDeplacement();

        public abstract void updatePointsDeplacement(SmallWorld.Case typeCase);

        public abstract void updatePointsVie(int newPV);

        
    }
}
