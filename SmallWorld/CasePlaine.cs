﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class CasePlaine : Case
    {
        public int Numero
        {
            get
            {
                return 0;
            }
        }

        public CasePlaine()
        {

        }

        public CasePlaine(SerializationInfo info, StreamingContext context)
        {

        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 0);
        }        
    }
}
