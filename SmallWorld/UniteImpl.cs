using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UniteImpl : Unite
    {
        private int _points;
        private float _pointsDeplacement;
        private int _x;
        private int _y;
        private int _pointsAttaque;
        private int _pointsDefense;
        private int _pointDeVie;

        public float PointsDeplacement { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int PointsAttaque { get; set; }

        public int PointsDefense { get; set; }

        public int PointsDeVie { get; set; }

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

                //Décision sur la victoire
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
            return Math.Floor((Double)(PointsAttaque / (PointsDeVie %= 5)) * 100);
        }

        public Double calculDefense()
        {
            return Math.Ceiling((Double)(PointsDefense / (PointsDeVie %= 5)) * 100);
        }

        public abstract void resetPointsDeplacement();

        public abstract void updatePointsDeplacement(SmallWorld.Case typeCase);

        public abstract void validationDplacement(int x, int y);
                
    }
}
