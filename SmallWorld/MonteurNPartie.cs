using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class MonteurNPartie : Monteur
    {
        /// <summary>
        /// Créer une partie avec un monteur
        /// </summary>
        /// <param name="nom1">Nom deu joueur 1</param>
        /// <param name="peuple1">Peuple du joueur 1</param>
        /// <param name="nom2">Nom du joueur 2</param>
        /// <param name="peuple2">Peuple du joueur 2</param>
        /// <returns>Le nouveau jeu créé</returns>
        public Jeu CreerJeu(string nom1, int peuple1, string nom2, int peuple2)
        {
            // Créer la map
            Carte carte = CarteImpl.Instance.ConstruireCarte(this.nbCases);

            int[][] starts = Wrapper.Wrapper.placementJoueur(this.nbCases);

            //Créer les joueurs et leurs points de départ
            Joueur joueur1 = new JoueurImpl(nom1, peuple1);
            Point point1 = new PointImpl(starts[0][0], starts[0][1]);
            
            Joueur joueur2 = new JoueurImpl(nom2, peuple2);
            Point point2 = new PointImpl(starts[1][0], starts[1][1]);

            //Créer les unités des joueurs puis les placer sur la carte
            List<Unite> unites1 = joueur1.CreerUnites(this.nbUnites);
            for (int i = 0; i < this.nbUnites; i++)
            {
                carte.PlacerUnite(unites1[i], point1);
            }
            List<Unite> unites2 = joueur2.CreerUnites(this.nbUnites);
            for (int i = 0; i < this.nbUnites; i++)
            {
                carte.PlacerUnite(unites2[i], point2);
            }

            return new JeuManager(joueur1, joueur2, carte, this.nbTours);
        }
    }
}
