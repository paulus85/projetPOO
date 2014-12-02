using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class PeupleImpl : Peuple
    {
        public List<Unite> ListUnite { get; set; }
    
        public abstract void generationUnites(int nbrUnites, int xinit, int yinit);
    }
}
