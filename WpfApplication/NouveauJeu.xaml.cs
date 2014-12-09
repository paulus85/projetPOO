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

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour NouveauJeu.xaml
    /// </summary>
    public partial class NouveauJeu : Page
    {
        public NouveauJeu()
        {
            InitializeComponent();
        }

        private void Commencer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (Application.Current.MainWindow as MainWindow);
            //Passage des paramètres via l'objet parent. --> appel aux méthodes de construction du jeu.

        }
    }
}
