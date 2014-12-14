using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public class CarteImpl : Carte
    {
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

        private CarteImpl()
        {
            
        }

        //monteur de la carte
        public Carte ConstruireCarte(int taille)
        {
            int[][] composition = Wrapper.Wrapper.genererCarte(taille);
            Case[,] cases = new Case[taille, taille];

            for (int x = 0; x < taille; x++)
            {
                for (int y = 0; y < taille; y++)
                {
                    cases[x, y] = FabriqueCase.Instance.GetCase(composition[x][y]);
                }
            }
            return new CarteImpl(cases);
        }

        public CarteImpl(Case[,] cases) {
            this.cases = cases;
            this.taille = this.cases.GetLength(0);

            unites = new List<Unite>[taille, taille];
            for(int x=0; x < this.taille; x++) {
                for(int y=0; y < this.taille; y++) {
                    unites[x, y] = new List<Unite>();
                }
            }
        }

        public void PlacerUnite(Unite unite, PointImpl position)
        {
            this.unites[position.x, position.y].Add(unite);
        }
    }
}
