using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class CaseMontagne : Case
    {
        public int Number
        {
            get
            {
                return 3;
            }
        }

        public CaseMontagne()
        {

        }

        public CaseMontagne(SerializationInfo info, StreamingContext context)
        {

        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 3);
        }
    }
}
