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

        private FabriquePeuple fabrique;

        private int numero;
        private String nomJoueur;
        private int points;

        private static int cpt = 0;

        public int Numero
        {
            get { return this.numero; }
        }

        public String NomJoueur
        {
            get { return this.nomJoueur;  }
        }

        public int Points
        {
            get { return points;  }
        }

        public FabriquePeuple Fabrique
        {
            get { return fabrique; }
        }

        #endregion

        public JoueurImpl(String nom, FabriquePeuple fab)
        {
            this.nomJoueur = nom;
            this.fabrique = fab;
            this.points = 0;
            cpt++;
            this.numero = cpt;
        }

        public JoueurImpl()
        {
            
        }

        public List<Unite> CreerUnites(int nbUnites)
        {
            List<Unite> unites = new List<Unite>();
            for (int i = 0; i < nbUnites; i++)
            {
                unites.Add(fabrique.GenerationUnite(this));
            }
            return unites;
        }

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

        public override int GetHashCode()
        {
            return this.numero.GetHashCode();
        }

        public void AjoutPoints(int n)
        {
            //TODO Exception
            this.points += n;
        }

        //Deserialization
        public JoueurImpl(SerializationInfo info, StreamingContext context) {
            this.nomJoueur = (string)info.GetValue("NomJoueur", typeof(string));
            this.points = (int)info.GetValue("Points", typeof(int));

            FabriquePeuple peupleElf = new PeupleElfe();
            FabriquePeuple peupleNain = new PeupleNain();
            FabriquePeuple peupleOrc = new PeupleOrc();
            FabriquePeuple fab = (FabriquePeuple)info.GetValue("Fabrique", typeof(FabriquePeuple));
            if(fab.GetType() == typeof(UniteElfe)) 
            {
                this.fabrique = peupleElf;
            }
            else if (fab.GetType() == typeof(UniteNain))
            {
                this.fabrique = peupleNain;
            }
            else if (fab.GetType() == typeof(UniteElfe))
            {
                this.fabrique = peupleOrc;
            } else 
            {
                throw new Exception();
            }
            this.numero = (int)info.GetValue("Numero", typeof(int));
            cpt++;
        }

        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("NomJoueur", this.nomJoueur);
            info.AddValue("Points", this.points);
            info.AddValue("Numero", this.numero);
            info.AddValue("Fabrique", this.fabrique);
        }
    }
}
