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
        public Joueur Joueur { get; set; }

        public JoueurUC(Joueur j,int nbUnites)
        {
            InitializeComponent();
            Joueur = j;
            NomJoueur.Text = "" + Joueur.NomJoueur;
            Peuple.Text = GetPeupleStringFromFabrique(j.Fabrique);
            refresh(nbUnites);
        }

        public void refresh(int nbUnites)
        {
            points.Text = "" + Joueur.Points;
            nbrUnite.Text = "" + nbUnites; 
        }

        private string GetPeupleStringFromFabrique(SmallWorld.FabriquePeuple fp)
        {
            string res = "";
            if (fp is PeupleElfe)
            {
                res = "Elfe";
            }
            else if (fp is PeupleNain)
            {
                res = "Nain";
            }
            else if (fp is PeupleOrc)
            {
                res = "Orc";
            }
            return res;
        }

    }
}
