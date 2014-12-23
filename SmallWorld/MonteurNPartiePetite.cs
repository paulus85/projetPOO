using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class MonteurNPartiePetite : MonteurNPartie
    {
        private const int NB_TOURS = 20;
        private const int NB_UNITES = 6;
        private const int NB_CASES = 10;

        public MonteurNPartiePetite()
        {
            this.nbTours = NB_TOURS;
            this.nbUnites = NB_UNITES;
            this.nbCases = NB_CASES;
        }
    }
}