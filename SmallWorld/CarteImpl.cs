using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class CarteImpl : Carte
    {

        private Case[,] cases;
        private static CarteImpl instance = null;
        public FabriqueCase fabrique = new FabriqueCase();

       public static CarteImpl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CarteImpl();
                }
                return instance;
            }
        }

        public CarteImpl()
        {
            
        }

        public CarteImpl(int lon, int lar)
        {
            instanciateCasesTab(lon, lar);
        }
    
        public Case[,] Cases
            //Liste ordonnée des cases
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void instanciateCasesTab(int lon, int lar)
        {
            cases = new Case[lon, lar];
        }
    }
}
