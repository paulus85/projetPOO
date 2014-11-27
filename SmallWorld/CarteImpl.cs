using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class CarteImpl : Carte
    {
        public static CarteImpl instance;

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
            throw new System.NotImplementedException();
        }

        public CarteImpl(int lon, int lar)
        {
            throw new System.NotImplementedException();
        }
    
        public Case[,] Case
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
    }
}
