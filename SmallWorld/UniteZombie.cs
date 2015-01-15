using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class UniteZombie : UniteImpl
    {
        private int pointBonus;

        public int PointBonus
        {
            get { return pointBonus; }
        }

        /// <summary>
        /// Constructeur de l'unité
        /// </summary>
        /// <param name="j">Le joueur auxquel appartient l'unité</param>
        public UniteZombie(Joueur j) : base(j)
        {
            this.pointBonus = 0;
        }

        /// <summary>
        /// Récupérer les points de l'unité en fonction de la case
        /// </summary>
        /// <param name="typeCase">La case où se trouve l'unité</param>
        /// <returns>Le nombre de point</returns>
        public override int GetPoints(Case typeCase){
            if (typeCase.Numero == (int)NumCase.DESERT)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// Valider le déplacement d'une unité 
        /// </summary>
        /// <param name="pointCourant">Le point de départ</param>
        /// <param name="caseCour">La case de départ</param>
        /// <param name="destination">Le point de destination</param>
        /// <param name="caseDest">La case de destination</param>
        /// <returns>Vrai si l'unité peut se déplacer</returns>
        public override bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest, bool occupe)
        {
            if (occupe || caseDest.Numero == (int)NumCase.MARAIS)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT / 2
                    && destination.EstJoignable(pointCourant);
            }
            return base.ValidationDeplacement(pointCourant, caseCour, destination, caseDest, occupe);
        }

        /// <summary>
        /// Effectuer le déplacement de l'unité
        /// </summary>
        /// <param name="destination">La case de destination du déplacement</param>
        /// <returns>Vrai si tous s'est bien déroulé</returns>
        public override bool Deplacement(Case destination, bool occupe)
        {
            if (occupe || destination.Numero == (int)NumCase.MARAIS)
            {
                double cout_deplacement = COUT_DEPLACEMENT / 2.0;
                if (this.pointsDeplacementRestant < cout_deplacement)
                {
                    return false;
                }
                this.pointsDeplacementRestant -= cout_deplacement;
                return true;
            }
            return base.Deplacement(destination, occupe);
        }
    }
}
