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

        protected const int POINT_ATTAQUE = 2;
        protected const int POINT_DEFENSE = 1;
        protected const int PV_DEFAULT = 5;
        protected const int COUT_DEPLACEMENT = 1;
        protected const int POINTS_DEPLACEMENT_DEFAULT = 1;

        protected int pointsDeVie;
        protected double pointsDeplacementRestant;
        protected Joueur proprio;

        public double PointsDeplacementRestant {
            get { return this.pointsDeplacementRestant; }
        }

        public int PointsAttaque 
        {
            get { return POINT_ATTAQUE; }
        }

        public int PointsDefense { 
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

        public UniteImpl(Joueur proprio) {
            this.proprio = proprio;
            this.pointsDeVie = PV_DEFAULT;
            this.pointsDeplacementRestant = POINTS_DEPLACEMENT_DEFAULT;
            cpt++;
            this.numero = cpt;
        }

        public bool EnleverPV()
        {
            if (this.pointsDeVie < 1)
            {
                return false;
            }
            this.pointsDeVie--;
            return true;
        }
        public bool EstEnVie()
        {
            return this.pointsDeVie > 0;
        }

        public void ResetPointsDeplacement()
        {
            this.pointsDeplacementRestant = POINTS_DEPLACEMENT_DEFAULT;
        }

        public virtual bool ValidationDeplacement(Point pointCourant, Case caseCour, Point destination, Case caseDest)
        {
            return this.pointsDeplacementRestant >= COUT_DEPLACEMENT 
                && destination.EstJoignable(pointCourant);
        }

        public virtual bool Deplacement(Case destination)
        {
            if (this.pointsDeplacementRestant < COUT_DEPLACEMENT)
            {
                return false;
            }
            this.pointsDeplacementRestant -= COUT_DEPLACEMENT;
            return true;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Unite unit = (Unite)obj;
            return this.numero == unit.Numero;
        }

        public override int GetHashCode()
        {
            return this.numero.GetHashCode();
        }

        public abstract int GetPoints(Case c);

        //Deserialization
        public UniteImpl(SerializationInfo info, StreamingContext context) {
            this.pointsDeVie = (int)info.GetValue("PointsDeVie", typeof(int));
            this.pointsDeplacementRestant = (int)info.GetValue("PointsDeplacementRestant", typeof(int));
            this.numero = (int)info.GetValue("Numero", typeof(int));
            this.proprio = (Joueur)info.GetValue("Proprio", typeof(Joueur));
        }

        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("PointsDeVie", this.pointsDeVie);
            info.AddValue("PointsDeplacementRestant", this.pointsDeplacementRestant);
            info.AddValue("Proprio", this.proprio);
            info.AddValue("Numero", this.numero);
        }
    }
}
