using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class PeupleElfe : PeupleImpl
    {
        public PeupleElfe()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Génère les unités pour le joueur.
        /// </summary>
        /// <param name="nbrUnites">Le nombre d'unités à générer.</param>
        /// <param name="xinit">Position x initiale.</param>
        /// <param name="yinit">Position y initiale.</param>
        public override void generationUnites(int nbrUnites, int xinit, int yinit)
        {
            for (int i = 0; i < nbrUnites; i++)
            {
                // Génération des unités propres
                Unite unit = new UniteElfe(xinit,yinit);
                base.ListUnite.Add(unit);
                // TODO: a tester
            }
        }
    }
}
