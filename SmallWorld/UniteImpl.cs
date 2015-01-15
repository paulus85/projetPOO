using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public abstract class UniteImpl : Unite
    {
        #region Properties

        private int numero;
        private static int cpt = 0;

        protected const double POINT_ATTAQUE = 2;
        protected const double POINT_DEFENSE = 1;
        protected const int PV_DEFAULT = 5;
        protected const int COUT_DEPLACEMENT = 1;
        protected const int POINTS_DEPLACEMENT_DEFAULT = 1;

        protected int pointsDeVie;
        protected double pointsDeplacementRestant;
        protected Joueur proprio;

        public double PointsDeplacementRestant {
            get { return this.pointsDeplacementRestant; }
        }

        public double PointsAttaque 
        {
            get { return POINT_ATTAQUE; }
        }

        public double PointsDefense { 
            get { return POINT_DEFENSE; } 
        }

        public int PointsDeVie 
        {
            get { return this.pointsDeVie; } 
        }

        public int PvDefault 
        {
            get { return PV_DEFAULT; } 
        }

        public Joueur Proprio
        {
            get { return this.proprio; }
        }

        public int Numero
        {
            get { return this.numero; }
        }

        #endregion

        /// <summary>
        /// Constructeur d'une unité
        /// </summary>
        /// <param name="proprio">Joueur possédant l'unité</param>
        public UniteImpl(Joueur proprio) {
            this.proprio = proprio;
            this.pointsDeVie = PV_DEFAULT;
            this.pointsDeplacementRestant = POINTS_DEPLACEMENT_DEFAULT;
            cpt++;
            this.numero = cpt;
        }

        /// <summary>
        /// Enlever un PV à l'unité
        /// </summary>
        /// <returns>Vrai si tout s'est bien passé</returns>
        public bool EnleverPV()
        {
            if (this.pointsDeVie < 1)
            {
                return false;
            }
            this.pointsDeVie--;
            return true;
        }

        /// <summary>
        /// Vérifier si une unité est en vie
        /// </summary>
        /// <returns>Vrai si l'unité est en vie</returns>
        public bool EstEnVie()
        {
            return this.pointsDeVie > 0;
        }

        /// <summary>
        /// Recharger les points de déplacement
        /// </summary>
        public void ResetPointsDeplacement()
        {
            this.pointsDeplacementRestant = POINTS_DEPLACEMENT_DEFAULT;
        }

        /// <summary>
        /// Valider le déplacement d'une unité 
        /// </summary>
        /// <param name="pointCourant">Le point de départ</param>
        /// <param name="caseCour">La case de départ</param>
        /// <param name="destination">Le point de destination</param>
        /// <param name="caseDest">La case de destination</param>
        /// <returns>Vrai si l'unité peut se déplacer</returns>
        public virtual bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest, bool occupe)
        {
            return this.pointsDeplacementRestant >= COUT_DEPLACEMENT 
                && destination.EstJoignable(pointCourant);
        }

        /// <summary>
        /// Effectuer le déplacement de l'unité
        /// </summary>
        /// <param name="destination">La case de destination du déplacement</param>
        /// <returns>Vrai si tous s'est bien déroulé</returns>
        public virtual bool Deplacement(Case destination, bool occupe)
        {
            if (this.pointsDeplacementRestant < COUT_DEPLACEMENT)
            {
                return false;
            }
            this.pointsDeplacementRestant -= COUT_DEPLACEMENT;
            return true;
        }

        /// <summary>
        /// Redéfinition de la méthode Equals d'une unité
        /// </summary>
        /// <param name="obj">L'unité comparé</param>
        /// <returns>Vrai si les 2 unités sont égales</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Unite unit = (Unite)obj;
            return this.numero == unit.Numero;
        }

        /// <summary>
        /// Redéfinition de la méthode GetHashCode
        /// (Nécessaire pour redéfinir la méthode Equals)
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.numero.GetHashCode();
        }

        /// Redéfinition de la méthode ToString
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            String chaine = Convert.ToString(this.GetType());
            int indice = chaine.IndexOf("SmallWorld.Unite");
            return chaine.Substring(indice, chaine.Length);
        }

        /// <summary>
        /// Récupérer les points de l'unité en fonction de la case
        /// </summary>
        /// <param name="typeCase">La case où se trouve l'unité</param>
        /// <returns>Le nombre de point</returns>
        public abstract int GetPoints(Case c);

        /// <summary>
        /// Méthode pour la deserialization de l'unité
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public UniteImpl(SerializationInfo info, StreamingContext context) {
            this.pointsDeVie = (int)info.GetValue("PointsDeVie", typeof(int));
            this.pointsDeplacementRestant = (int)info.GetValue("PointsDeplacementRestant", typeof(int));
            this.numero = (int)info.GetValue("Numero", typeof(int));
            this.proprio = (Joueur)info.GetValue("Proprio", typeof(Joueur));
        }

        /// <summary>
        /// Méthode pour la serialization de l'unité
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("PointsDeVie", this.pointsDeVie);
            info.AddValue("PointsDeplacementRestant", this.pointsDeplacementRestant);
            info.AddValue("Proprio", this.proprio);
            info.AddValue("Numero", this.numero);
        }
    }
}
