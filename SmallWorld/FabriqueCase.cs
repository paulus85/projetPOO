using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class FabriqueCase
    {

        public FabriqueCase()
        {
            
        }

        public CasePlaine casePlaine = new CasePlaine();

        public CaseDesert caseDesert = new CaseDesert();

        public CaseMontagne caseMontagne = new CaseMontagne();

        public CaseForet caseForet = new CaseForet();

        public CasePlaine creerPlaine()
        {
            return casePlaine;
        }

        public CaseDesert creerDesert()
        {
            return caseDesert;
        }

        public CaseMontagne creerMontagne()
        {
            return caseMontagne;
        }

        public CaseForet creerForet()
        {
            return caseForet;
        }
    }
}
