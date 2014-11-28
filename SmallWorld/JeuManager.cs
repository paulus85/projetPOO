using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class JeuManager : Jeu
    {

        #region Properties
        
        
        public Monteur Monteur
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public CarteImpl CarteImpl
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public TypePartie TypePartie
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public JoueurImpl[] Joueurs
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        #endregion

        public void checkAllUnites()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Defines the correct TypePartie for the game.
        /// </summary>
        /// <param name="tp">The correct TypePartie (choosen between Demo, Petite and Normale)</param>
        public void definirTypePartie(TypePartie tp)
        {

        }

        /// <summary>
        /// Updates the variables of TypePartie
        /// </summary>
        public void updateVariables()
        {

        }
    }
}
