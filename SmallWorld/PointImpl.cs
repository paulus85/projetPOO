using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    [Serializable()]
    public class PointImpl : Point
    {
        public int x
        {
            get;
            set;
        }

        public int y
        {
            get;
            set;
        }

        /// <summary>
        /// Constructeur d'un point
        /// </summary>
        /// <param name="x">abscisse</param>
        /// <param name="y">ordonné</param>
        public PointImpl(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Vérifier la validité d'un point
        /// </summary>
        /// <param name="taille">La taille de la la carte où on vérifie</param>
        /// <returns>Vrai si le point est valide</returns>
        public bool EstValide(int taille)
        {
            if (this.x < 0 || this.x >= taille)
            {
                return false;
            }
            if (this.y < 0 || this.y >= taille)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Vérifier si un point est bien atteignable à partir du point d'où on vérifie
        /// </summary>
        /// <param name="pt">Le point destination</param>
        /// <returns>Vrai si le point est atteignable</returns>
        public bool EstJoignable(Point pt)
        {
            if (this.x % 2 == 0){
		        int[] tabX = { -1, -1, 0, 0, 1, 1 };
		        int[] tabY = { -1, 0, -1, 1, -1, 0 };

		        for (int i = 0; i < 6; i++){
			        if((this.x + tabX[i]) == pt.x && (this.y + tabY[i]) == pt.y)
                        return true;
		        }
                return false;
	        }
	        else{
		        int[] tabX = { -1, -1, 0, 0, 1, 1 };
		        int[] tabY = { 0, 1, -1, 1, 0, 1 };
		        for (int i = 0; i < 6; i++){
			        if((this.x + tabX[i]) == pt.x && (this.y + tabY[i]) == pt.y)
                        return true;
		        }
                return false;
	        }
        }

        /// <summary>
        /// Redéfinition de la méthode Equals
        /// </summary>
        /// <param name="obj">Objet comparé</param>
        /// <returns>Vrai si les 2 objets sont égaux</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Point pt = (Point)obj;
            return this.x == pt.x && this.y == pt.y;
        }

        /// <summary>
        /// Redéfinition de la méthode GetHashCode
        /// (Nécessaire pour redéfinir la méthode Equals)
        /// </summary>
        /// <returns>L'hashcode du point</returns>
        public override int GetHashCode()
        {
            int hash = 42;
            hash = hash * 20 + this.x.GetHashCode();
            hash = hash * 20 + this.y.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Méthode pour la deserialization du point
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public PointImpl(SerializationInfo info, StreamingContext context) {
            this.x = (int)info.GetValue("x", typeof(int));
            this.y = (int)info.GetValue("y", typeof(int));
        }

        /// <summary>
        /// Méthode pour la serialization du point
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("x", this.x);
            info.AddValue("y", this.y);
        }
    }
}
