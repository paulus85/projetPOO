using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmallWorld;

namespace WpfApplication
{
	/// <summary>
	/// Logique d'interaction pour UniteUC.xaml
	/// </summary>
	public partial class UniteUC : UserControl
	{
        public Unite Unite { get; set; }
        public bool IsSelectable { get; set; }

		public UniteUC(Unite u, Case c)
		{
			this.InitializeComponent();
            Unite = u;
            
            //Initialisation des différents champs
            //Gestion de l'image associée
            image.Source = getSprite();
            //Point de vie
            pvL.Text = ""+Unite.PointsDeVie;
            //Point de déplacement
            pdL.Text = ""+Unite.PointsDeplacementRestant;
            //Point d'attaque
            attL.Text = "" + Unite.GetAttaqueTerrain(c);
            //Point de défense 
            defL.Text = "" + Unite.GetDefenseTerrain(c);
            //Nom du joueur 
            nomJoueur.Content = "" + Unite.Proprio.NomJoueur;

		}

        /// <summary>
        /// Permet d'obtenir le bon visuel selon le type de l'unité.
        /// </summary>
        /// <returns>L'image correspondant au type de l'unité.</returns>
        private BitmapImage getSprite()
        {
            //return new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/mer.png")));
            BitmapImage bi = null;
            if (Unite is UniteElfe)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/uniteElf.png"));
            }
            else if (Unite is UniteNain)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/uniteNain.png"));
            }
            else if (Unite is UniteOrc)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/uniteOrc.png"));
            }
            else if (Unite is UniteNain)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/uniteZombie.png"));
            }
            return bi;
        }  


	}
}