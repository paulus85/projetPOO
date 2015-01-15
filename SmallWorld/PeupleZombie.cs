﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmallWorld
{
    [Serializable()]
    public class PeupleZombie : PeupleImpl
    {
        public override int Numero 
        { 
            get { return (int)NumUnite.ZOMBIE; } 
        }

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public PeupleZombie()
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Génération de l'unité correspondant au peuple du joueur
        /// </summary>
        /// <param name="j">Joueur ayant l'unité</param>
        /// <returns>La nouvelle unité du joueur j</returns>
        public override Unite GenerationUnite(Joueur j)
        {
            return new UniteZombie(j);
        }

        /// <summary>
        /// Méthode pour la deserialization du peuple
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public PeupleZombie(SerializationInfo info, StreamingContext context)
        {

        }

        /// <summary>
        /// Méthode pour la serialization du peuple
        /// </summary>
        /// <param name="info">Données</param>
        /// <param name="context">Contexte</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
