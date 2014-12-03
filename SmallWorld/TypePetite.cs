using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class TypePetite : TypePartie
    {
        public TypePetite()
        {
            throw new System.NotImplementedException();
        }

        public override void execute()
        {
            base.NbJoueurs = 2;
            base.NbCases = 10;
            base.NbTours = 20;
            base.NbUnites = 6;

        }
    }
}
