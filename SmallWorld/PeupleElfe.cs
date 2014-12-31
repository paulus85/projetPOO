using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class PeupleElfe : FabriquePeuple
    {
        public int Numero
        {
            get { return 0; }
        }

        public PeupleElfe()
        {

        }
        public Unite GenerationUnite(Joueur j)
        {
            return new UniteElfe(j);
        }

        public PeupleElfe(SerializationInfo info, StreamingContext context)
        {

        }

        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
