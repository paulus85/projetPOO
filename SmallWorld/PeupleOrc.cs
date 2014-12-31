using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class PeupleOrc : FabriquePeuple
    {
        public PeupleOrc()
        {
            //throw new System.NotImplementedException();
        }

        public Unite GenerationUnite(Joueur j)
        {
            return new UniteOrc(j);
        }

        public PeupleOrc(SerializationInfo info, StreamingContext context)
        {

        }

        //Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
