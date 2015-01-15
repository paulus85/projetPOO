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
        private CaseMarais marais;

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
            marais = new CaseMarais();
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
                case (int)NumCase.PLAINE:
                    return this.plaine;
                case (int)NumCase.DESERT:
                    return this.desert;
                case (int)NumCase.MONTAGNE:
                    return this.montagne;
                case (int)NumCase.FORET:
                    return this.foret;
                case (int)NumCase.MARAIS:
                    return this.marais;
                default:
                    return null;
            }
            throw new Exception();
            //TODO
            //throw new IncorrectTileNumberException(type);
        }
    }
}
