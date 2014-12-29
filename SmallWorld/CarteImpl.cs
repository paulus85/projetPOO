using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
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

       private CarteImpl()
       {
           
       }

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
        
       public void PlacerUnite(Unite unite, Point position)
       {
           this.unites[position.x, position.y].Add(unite);
       }

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

       public List<Unite> GetUnites(Point p)
       {
           return this.unites[p.x, p.y];
       }

       public Case GetCase(Point p)
       {
           return this.cases[p.x, p.y];
       }

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

       public bool SupprimerUnite(Unite unite, Point position)
       {
           return this.unites[position.x, position.y].Remove(unite);
       }

        //true si c'est une position avec un ennemie
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

    }

}
