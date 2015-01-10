﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class FabriquePeuple
    {
        private static FabriquePeuple instance = new FabriquePeuple();

        public static FabriquePeuple Instance
        {
            get { return instance; }
        }


        /// <summary>
        /// Constructeur vide
        /// </summary>
        public FabriquePeuple()
        {

        }

        /// <summary>
        /// Fabriquer le peuple désiré
        /// </summary>
        /// <param name="type">Numéro correspondant au peuple</param>
        /// <returns>Le peuple demandé</returns>
        public Peuple GetPeuple(int type)
        {
            switch (type)
            {
                case (int)NumUnite.ELF:
                    return new PeupleElfe();
                case (int)NumUnite.NAIN:
                    return new PeupleNain();
                case (int)NumUnite.ORC:
                    return new PeupleOrc();
                default:
                    return null;
            }
            throw new Exception();
            //TODO
            //throw new IncorrectTileNumberException(type);
        }
    }
}
