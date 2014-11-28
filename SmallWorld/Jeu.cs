using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Jeu
    {

       
        public Monteur Monteur { get; set; }

       
        public CarteImpl CarteImpl { get; set; }

     
        public TypePartie TypePartie { get; set; }

        public JoueurImpl[] Joueurs { get; set; }
    }
}
