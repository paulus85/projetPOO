using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    [Serializable()]
    public class CarteImpl : Carte
    {
        #region Proprietes

        private int taille;
        private Case[,] cases;
        private List<Unite>[,] unites;
        private static CarteImpl instance = new CarteImpl();

       public static Carte Instance
        {
            get { return instance; }
        }

       public List<Unite>[,] Unites
       //Liste ordonnée des cases
       {
           get { return this.unites; }
       }

       public Case[,] Cases
       //Liste ordonnée des cases
       {
           get { return this.cases; }
       }

       public int Taille
       {
           get { return taille; }
       }

        #endregion

        /// <summary>
        /// Constructeur vide
        /// Patron de conception Singleton
        /// </summary>
       private CarteImpl()
       {
           
       }

        /// <summary>
        /// Constructeur de la carte
        /// </summary>
        /// <param name="cases">Matrice des cases composant la carte</param>
       public CarteImpl(Case[,] cases)
       {
           this.cases = cases;
           this.taille = this.cases.GetLength(0);
           unites = new List<Unite>[taille, taille];

           for (int x = 0; x < this.taille; x++)
           {
               for (int y = 0; y < this.taille; y++)
               {
                   unites[x, y] = new List<Unite>();
               }
           }
       }
       
        /// <summary>
        ///  Création de la carte
        /// </summary>
        /// <param name="taille">Le nombre de case en largeur/hauteur</param>
        /// <returns>La carte des cases et des unités</returns>
       /// Utilisation du Wrapper pour la génération de la carte
       public Carte ConstruireCarte(int taille)
       {
           int[][] composition = Wrapper.Wrapper.genererCarte(taille);
           Case[,] cases = new Case[taille, taille];

           for (int x = 0; x < taille; x++)
           {
               for (int y = 0; y < taille; y++)
               {
                   //utilisation poids-mouche
                   cases[x, y] = FabriqueCase.Instance.GetCase(composition[x][y]);
               }
           }
           return new CarteImpl(cases);
       }
        
        /// <summary>
        /// Placement d'une unité d'un joueurs sur la carte des unités
        /// </summary>
        /// <param name="unite">L'unité à placer</param>
        /// <param name="position">Le point où placer l'unité</param>
       public void PlacerUnite(Unite unite, Point position)
       {
           this.unites[position.x, position.y].Add(unite);
       }

        /// <summary>
        /// Récupérer toutes les unités d'un joueur spécifié
        /// </summary>
        /// <param name="j">Le joueur à partir du quel on veut récupérer les unités</param>
        /// <returns>Dictionnaire des unités avec leurs positions</returns>
       public Dictionary<Unite, Point> GetUnites(Joueur j)
       {
           Dictionary<Unite, Point> result = new Dictionary<Unite, Point>();
           for (int x = 0; x < this.taille; x++)
           {
               for (int y = 0; y < this.taille; y++)
               {
                   List<Unite> unites = this.unites[x, y];
                   if (unites.Count > 0 && unites[0].Proprio.Equals(j))
                   {
                       foreach (Unite unite in unites)
                       {
                           result.Add(unite, new PointImpl(x, y));
                       }
                   }
               }
           }
           return result;
       }

        /// <summary>
        /// Récupérer toutes les unités d'un point spécifié
        /// </summary>
        /// <param name="p">Le point où récupérer les unités</param>
        /// <returns>List des unités situées sur le point</returns>
       public List<Unite> GetUnites(Point p)
       {
           return this.unites[p.x, p.y];
       }

        /// <summary>
        /// Récupérer la case à un point spécifié
        /// </summary>
        /// <param name="p">Le point où récupérer la case</param>
        /// <returns>La case au point demandé</returns>
       public Case GetCase(Point p)
       {
           return this.cases[p.x, p.y];
       }

        /// <summary>
        /// Déplacer une unité d'un point à un autre sur la carte des unités
        /// </summary>
        /// <param name="unite">L'unité à déplacer</param>
        /// <param name="pointCourant">Le point de départ</param>
        /// <param name="destination">Le point de destination</param>
       public void DeplacerUnite(Unite unite, Point pointCourant, Point destination)
       {
           this.unites[pointCourant.x, pointCourant.y].Remove(unite);
           if (this.EstPositionEnnemie(destination, unite))
           {
               //ne devrait pas arriver
               //TODO exception
           }
           this.unites[destination.x, destination.y].Add(unite);
       }

        /// <summary>
        /// Supprimer une unité de la carte des unités
        /// </summary>
        /// <param name="unite">L'unité à supprimer</param>
        /// <param name="position">Le point où supprimer l'unité</param>
        /// <returns>Bool de la réussite ou non de l'opération</returns>
       public bool SupprimerUnite(Unite unite, Point position)
       {
           return this.unites[position.x, position.y].Remove(unite);
       }

        /// <summary>
        /// Demander si une case contient des ennemies
        /// </summary>
        /// <param name="position">Le point où vérifier</param>
        /// <param name="unite">L'unité du joueur courant</param>
        /// <returns>Vrai si la case contient des ennemies</returns>
       public bool EstPositionEnnemie(Point position, Unite unite)
       {
           if (this.unites[position.x, position.y].Count == 0)
           {
               return false;
           }
           else
           {
               bool x = !this.unites[position.x, position.y][0].Proprio.Equals(unite.Proprio);
               return x;
           }
       }

        /// <summary>
        /// Méthode pour la deserialization de la carte
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public CarteImpl(SerializationInfo info, StreamingContext context) {
            this.taille = (int)info.GetValue("Taille", typeof(int));
            this.unites = (List<Unite>[,])info.GetValue("Unites", typeof(List<Unite>[,]));
            this.cases = (Case[,])info.GetValue("Cases", typeof(Case[,]));
        }

        /// <summary>
        /// Méthode pour la serialization de la carte
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Taille", this.taille);
            info.AddValue("Unites", this.unites);
            info.AddValue("Cases", this.cases);
        }

    }

}
