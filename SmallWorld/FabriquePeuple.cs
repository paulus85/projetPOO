using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class FabriquePeuple
    {
        /// <summary>
        /// Thoses booleans says if we have already created a Peuple form the specie.
        /// </summary>
        private bool ElfeCreated;
        private bool OrcCreated;
        private bool NainCreated;
        public FabriquePeuple()
        {
            ElfeCreated = false;
            OrcCreated = false;
            NainCreated = false;
        }
    
        public Peuple creerOrc()
        {
            return new PeupleOrc();
        }

        public Peuple creerElfe()
        {
            return new PeupleElfe();
        }

        public Peuple creerNain()
        {
            return new PeupleNain();
        }
    }
}
