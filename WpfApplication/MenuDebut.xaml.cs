using Microsoft.Win32;
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
    /// Logique d'interaction pour MenuDebut.xaml
    /// </summary>
    public partial class MenuDebut : Page
    {
        /// <summary>
        /// La fenêtre principale de l'application
        /// </summary>
        private MainWindow parent;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MenuDebut"/>.
        /// </summary>
        public MenuDebut()
        {
            InitializeComponent();
            this.DataContext = this;
            parent = (Application.Current.MainWindow as MainWindow);
        }
        /// <summary>
        /// Traitant de l'événement de clic sur le bouton "Quitter"
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>
        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            // Configuration de la boite de dialogue
            String messageBoxMessage = "Est-vous sûr de vouloir vraiment quitter cette superbe application ?";
            String title = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.None;
            MessageBoxResult res = MessageBox.Show(messageBoxMessage, title, buttons, image);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Traitant de l'événement de clic sur le bouton "Charger"
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>
        private void Charger_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Fichier de sauvegarde (*.yolo)|*.yolo";
            if (ofd.ShowDialog() == true)
            {
                Console.WriteLine(ofd.FileName);
                SmallWorld.MonteurPartieSauv monteur = new SmallWorld.MonteurPartieSauv();
                parent.Content = new Jeu(monteur, ofd.FileName);
            }
        }

        /// <summary>
        /// Traitant de l'événement de clic sur le bouton "Nouvelle Partie"
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>
        private void NouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            parent.Content = new NouveauJeu();
        }

    }
}
