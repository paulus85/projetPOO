using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class FabriqueCase
    {
        private CasePlaine plaine;
        private CaseDesert desert;
        private CaseMontagne montagne;
        private CaseForet foret;

        private static FabriqueCase instance = new FabriqueCase();

        public static FabriqueCase Instance
        {
            get { return instance;  }
        }

        public FabriqueCase()
        {
            plaine = new CasePlaine();
            desert = new CaseDesert();
            montagne = new CaseMontagne();
            foret = new CaseForet();
        }

        public Case GetCase(int type)
        {
            switch (type)
            {
                case 0:
                    return this.plaine;
                case 1:
                    return this.desert;
                case 2:
                    return this.montagne;
                case 3:
                    return this.foret;
            }
            throw new Exception();
            //throw new IncorrectTileNumberException(type);
        }
    }
}
