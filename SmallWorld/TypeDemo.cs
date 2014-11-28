using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class TypeDemo : TypePartie
    {
        public TypeDemo()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Permet de mettre à jour les variables du jeu 
        /// </summary>
        public void execute()
        {
            base.NbJoueurs = 2;
            base.NbCases = 6;
            base.NbTours = 5;
            base.NbUnites = 4;

        }
    }
}
