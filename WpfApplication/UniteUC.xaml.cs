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
        private Unite unite;
        public Unite Unite { get; set; }

		public UniteUC(Unite u)
		{
			this.InitializeComponent();
            unite = u;

            //Initialisation des différents champs
            //Gestion de l'image associée
            image.Source = getSprite();
            //Point de vie
            pvL.Text = "0";
            //Point de déplacement
            pdL.Text = "2";
            //Point d'attaque
            attL.Text = "2";
            //Point de défense 
            defL.Text = "1";

		}

        private BitmapImage getSprite()
        {
            //return new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/mer.png")));
            BitmapImage bi = null;
            if (unite is UniteElfe)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_elfe.png"));
            }
            else if (unite is UniteNain)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_nain.png"));
            }
            else if (unite is UniteOrc)
            {
                bi = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_elfe.png"));
            }
            return bi;
        }  


	}
}