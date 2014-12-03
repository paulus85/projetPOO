using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Jeu
    {

       
        Monteur Monteur { get; set; }

       
        CarteImpl CarteImpl { get; set; }

     
        TypePartie TypePartie { get; set; }

        JoueurImpl[] Joueurs { get; set; }
    }
}
