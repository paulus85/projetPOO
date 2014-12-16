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
            //Point de vie
            //Point de déplacement

		}


	}
}