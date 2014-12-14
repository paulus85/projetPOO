using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class MonteurNPartieDemo : MonteurNPartie
    {
        private const int NB_TOURS = 5;
        private const int NB_UNITES = 4;
        private const int NB_CASES = 6;

        public MonteurNPartieDemo()
        {
            this.nbTours = NB_TOURS;
            this.nbUnites = NB_UNITES;
            this.nbCases = NB_CASES;
        }
    }
}