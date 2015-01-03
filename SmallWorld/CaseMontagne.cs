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
        public int Numero
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public CaseMontagne()
        {

        }

        /// <summary>
        /// Méthode pour la deserialization de la case
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public CaseMontagne(SerializationInfo info, StreamingContext context)
        {

        }

        /// <summary>
        /// Méthode pour la serialization de la case
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Number", 3);
        }
    }
}
