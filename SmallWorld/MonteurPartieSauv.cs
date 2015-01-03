using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SmallWorld
{
    public class MonteurPartieSauv : Monteur
    {
        /// <summary>
        /// Charger une partie sauvegardée
        /// </summary>
        /// <param name="fichier">Répertoire et nom du fichier de sauvegarde</param>
        /// <returns>Le jeu chargé</returns>
        public Jeu CreerJeu(string fichier)
        {
            using (FileStream stream = File.Open(fichier, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                JeuManager jeu = (JeuManager)formatter.Deserialize(stream);
                return jeu;
            }
        }
    }
}
