using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class UniteNain : UniteImpl
    {
        /// <summary>
        /// Constructeur de l'unité
        /// </summary>
        /// <param name="j">Le joueur auxquel appartient l'unité</param>
        public UniteNain(Joueur j) : base(j)
        {

        }

        /// <summary>
        /// Récupérer les points de l'unité en fonction de la case
        /// </summary>
        /// <param name="typeCase">La case où se trouve l'unité</param>
        /// <returns>Le nombre de point</returns>
        public override int GetPoints(Case typeCase)
        {
            if (typeCase.Numero == (int)NumCase.PLAINE)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// Récupérer les points d'attaques en fonction du terrain
        /// </summary>
        /// <param name="unite">La case visée</param>
        /// <returns>L'attaque de l'unité</returns>
        public override int GetAttaqueTerrain(Case typeCase)
        {
            if (typeCase.Numero == (int)NumCase.PLAINE)
                return POINT_ATTAQUE - 1;
            if (typeCase.Numero == (int)NumCase.MONTAGNE)
                return POINT_ATTAQUE + 1;
            return base.GetAttaqueTerrain(typeCase);
        }

        /// <summary>
        /// Récupérer les points de défenses en fonction du terrain
        /// </summary>
        /// <param name="unite">La case visée</param>
        /// <returns>La défense de l'unité</returns>
        public override int GetDefenseTerrain(Case typeCase)
        {
            if (typeCase.Numero == (int)NumCase.PLAINE)
                return POINT_DEFENSE - 1;
            if (typeCase.Numero == (int)NumCase.MONTAGNE)
                return POINT_DEFENSE + 1;
            return base.GetDefenseTerrain(typeCase);
        }

        /// <summary>
        /// Valider le déplacement d'une unité 
        /// </summary>
        /// <param name="pointCourant">Le point de départ</param>
        /// <param name="caseCour">La case de départ</param>
        /// <param name="destination">Le point de destination</param>
        /// <param name="caseDest">La case de destination</param>
        /// <returns>Vrai si l'unité peut se déplacer</returns>
        public override bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest, bool occupé)
        {
            if (caseDest.Numero == (int)NumCase.PLAINE)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT / 2
                    && destination.EstJoignable(pointCourant);
            }
            else if (caseDest.Numero == (int)NumCase.MONTAGNE && caseCour.Numero == (int)NumCase.MONTAGNE && pointCourant != destination && !occupé)
            {
                return this.pointsDeplacementRestant >= COUT_DEPLACEMENT;
            }
            return base.ValidationDeplacement(pointCourant, caseCour, destination, caseDest, occupé);
        }

        /// <summary>
        /// Effectuer le déplacement de l'unité
        /// </summary>
        /// <param name="destination">La case de destination du déplacement</param>
        /// <returns>Vrai si tous s'est bien déroulé</returns>
        public override bool Deplacement(Case destination, bool occupe)
        {
            if (destination.Numero == (int)NumCase.PLAINE)
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
