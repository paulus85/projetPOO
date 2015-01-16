using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class MonteurNPartieNormale : MonteurNPartie
    {
        private const int NB_TOURS = 30;
        private const int NB_UNITES = 8;
        private const int NB_CASES = 15;

        public MonteurNPartieNormale()
        {
            this.nbTours = NB_TOURS;
            this.nbUnites = NB_UNITES;
            this.nbCases = NB_CASES;
        }
    }
}