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

		public UniteUC(Unite u)
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
            attL.Text = ""+Unite.PointsAttaque;
            //Point de défense 
            defL.Text = "" + Unite.PointsDefense;
            //Nom du joueur 
            nomJoueur.Content = "" + Unite.Proprio.NomJoueur;

		}

        private BitmapImage getSprite()
        {
            //return new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/mer.png")));
            BitmapImage bi = null;
            if (Unite is UniteElfe)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_elfe.png"));
            }
            else if (Unite is UniteNain)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_nain.png"));
            }
            else if (Unite is UniteOrc)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_orc.png"));
            }
            return bi;
        }  


	}
}