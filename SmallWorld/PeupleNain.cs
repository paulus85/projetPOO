using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class PeupleNain : PeupleImpl
    {
        public PeupleNain()
        {
            throw new System.NotImplementedException();
        }

        public override void generationUnites(int nbrUnites, int xinit, int yinit)
        {
            for (int i = 0; i < nbrUnites; i++)
            {
                // Génération des unités propres
                Unite unit = new UniteElfe(xinit, yinit);
                base.ListUnite.Add(unit);
                // TODO: a tester
            }
        }
    }
}
