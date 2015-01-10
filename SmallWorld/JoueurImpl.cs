using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class JoueurImpl : Joueur
    {
        #region Properties

        private Peuple peuple;

        private int numero;
        private String nomJoueur;
        private int points;

        private static int cpt = 0;

        public int Numero
        {
            get { return this.numero; }
        }

        public int NumeroPeuple
        {
            get
            {
                return this.peuple.Numero;
            }
        }

        public String NomJoueur
        {
            get { return this.nomJoueur;  }
        }

        public int Points
        {
            get { return points;  }
        }

        public Peuple Peuple
        {
            get { return peuple; }
        }

        #endregion

        /// <summary>
        /// Constructeur du joueur
        /// </summary>
        /// <param name="nom">Nom du joueur</param>
        /// <param name="fab">Fabrique du peuple du joueur</param>
        public JoueurImpl(String nom, int peuple)
        {
            this.nomJoueur = nom;
            FabriquePeuple fabrique = new FabriquePeuple();
            this.peuple = fabrique.GetPeuple(peuple);
            this.points = 0;
            cpt++;
            this.numero = cpt;
        }

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public JoueurImpl()
        {
            
        }

        /// <summary>
        /// Créer les unités du joueur
        /// </summary>
        /// <param name="nbUnites">Nombre d'unités à créer</param>
        /// <returns>List des unités créées</returns>
        public List<Unite> CreerUnites(int nbUnites)
        {
            List<Unite> unites = new List<Unite>();
            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(peuple.GenerationUnite(this));
            }
            return unites;
        }

        /// <summary>
        /// Redéfinition de la méthode Equals
        /// </summary>
        /// <param name="obj">Objet comparé</param>
        /// <returns>Vrai si les joueurs sont égaux</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if (!(obj is Joueur))
            {
                return false;
            }
            Joueur j = (Joueur)obj;
            return this.numero == j.Numero;
        }

        /// <summary>
        /// Redéfintion de la méthode GetHashCode
        /// (Nécessaire pour laa redéfinition de la méthode Equals)
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.numero.GetHashCode();
        }

        /// <summary>
        /// Ajouter des points à un joueur
        /// </summary>
        /// <param name="n">Le nombre de points à ajouter</param>
        public void AjoutPoints(int n)
        {
            //TODO Exception
            this.points += n;
        }

        /// <summary>
        /// Méthode pour la deserialization du joueur
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public JoueurImpl(SerializationInfo info, StreamingContext context) {
            this.nomJoueur = (string)info.GetValue("NomJoueur", typeof(string));
            this.points = (int)info.GetValue("Points", typeof(int));

            Peuple fab = (Peuple)info.GetValue("Peuple", typeof(FabriquePeuple));
            FabriquePeuple fabrique = new FabriquePeuple();
            this.peuple = fabrique.GetPeuple(fab.Numero);

            /*
            if(fab.GetType() == typeof(UniteElfe)) 
            {
                this.peuple = FabriquePeuple.Instance.GetPeuple(fab.Numero);
            }
            else if (fab.GetType() == typeof(UniteNain))
            {
                this.peuple = FabriquePeuple.Instance.GetPeuple(1);
            }
            else if (fab.GetType() == typeof(UniteElfe))
            {
                this.peuple = FabriquePeuple.Instance.GetPeuple(2);
            } else 
            {
                throw new Exception();
            }*/
            this.numero = (int)info.GetValue("Numero", typeof(int));
            cpt++;
        }

        /// <summary>
        /// Méthode pour la serialization du joueur
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("NomJoueur", this.nomJoueur);
            info.AddValue("Points", this.points);
            info.AddValue("Numero", this.numero);
            info.AddValue("Peuple", this.peuple);
        }
    }
}
