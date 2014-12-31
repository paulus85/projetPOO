using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class CaseDesert : Case
    {
        public int Numero
        {
            get{
                return 1;
            }
        }

        public CaseDesert()
        {

        }

        public CaseDesert(SerializationInfo info, StreamingContext context) {

        }

        
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 1);
        }
    }
}
