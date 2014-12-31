using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class CaseForet : Case
    {
        public int Numero
        {
            get
            {
                return 2;
            }
        }

        public CaseForet()
        {

        }

        public CaseForet(SerializationInfo info, StreamingContext context) {

        }

        
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 2);
        }
    }
}
