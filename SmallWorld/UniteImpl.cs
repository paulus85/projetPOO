using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UniteImpl : Unité, Unite
    {
        private int points;
        private int pointsDeplacement;
        private int x;
        private int y;
        private int pointsAttaque;
        private int pointsDefense;
    }
}
