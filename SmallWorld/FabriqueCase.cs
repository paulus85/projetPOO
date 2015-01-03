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

        /// <summary>
        /// Constructeur de la fabrique des cases
        /// </summary>
        public FabriqueCase()
        {
            plaine = new CasePlaine();
            desert = new CaseDesert();
            montagne = new CaseMontagne();
            foret = new CaseForet();
        }

        /// <summary>
        /// Récupérer le type de case en fonction de leurs numéros
        /// </summary>
        /// <param name="type">Numéro du type de la case</param>
        /// <returns>La case demandée</returns>
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
