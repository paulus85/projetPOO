using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class TypeNormale : TypePartie
    {
        public TypeNormale()
        {
            throw new System.NotImplementedException();
        }

        public override void execute()
        {
            base.NbJoueurs = 2;
            base.NbCases = 14;
            base.NbTours = 30;
            base.NbUnites = 8;

        }
    }
}
