using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class PeupleNain : FabriquePeuple
    {
        public PeupleNain()
        {
            //throw new System.NotImplementedException();
        }

        public Unite GenerationUnite(Joueur j)
        {
            return new UniteNain(j);
        }

        public PeupleNain(SerializationInfo info, StreamingContext context)
        {

        }

        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
