using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class TypePartie
    {
        /*private int nbJoueurs;
        private int nbCases;
        private int nbTours;
        private int nbUnites;*/

        public int NbJoueurs { get; set; }
        public int NbCases { get; set; }
        public int NbTours { get; set; }
        public int NbUnites { get; set; }
        
        void execute();
    }
}
