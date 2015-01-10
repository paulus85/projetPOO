using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public abstract class PeupleImpl : Peuple
    {
        public virtual int Numero
        {
            get { return -1; }
        }

        public abstract Unite GenerationUnite(Joueur j);
    }
}
