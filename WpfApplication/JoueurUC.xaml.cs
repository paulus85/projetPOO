using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour JoueurUC.xaml
    /// </summary>
    public partial class JoueurUC : UserControl
    {
        private Joueur joueur;
        public Joueur Joueur { get; set; }

        public JoueurUC(Joueur j)
        {
            InitializeComponent();
            Joueur = j;
            //Mise en place de valeurs bidon
            NomJoueur.Text = "Paul";
            Peuple.Text = "Elfe";
            points.Text = "" + 4;
            nbrUnite.Text = "" + 14; 

        }
    }
}
