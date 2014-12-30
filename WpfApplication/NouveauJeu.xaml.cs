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
        private MainWindow parent;

        public string NomJoueur1 { get; set; }
        public string NomJoueur2 { get; set; }
        public string PeupleJoueur1 { get; set; }
        public string PeupleJoueur2 { get; set; }
        public string TailleCarte { get; set; }

        private SmallWorld.MonteurNPartie monteur;
        private SmallWorld.Jeu engine;
        
        
        public NouveauJeu()
        {
            InitializeComponent();
            // TODO: redimensionner la fenetre pour que ça soit plus joli
            DataContext = this;
            NomJoueur1 = "";
            NomJoueur2 = "";
            PeupleJoueur1 = "";
            PeupleJoueur2 = "";
            TailleCarte = "";
            parent = (Application.Current.MainWindow as MainWindow);
            //parent.Height = 700;
            
        }

        /// <summary>
        /// Handles the Click event of the Commencer button. Check if all is OK and call constructor of Jeu view with all parameters needed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Commencer_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrWhiteSpace(PeupleJoueur1)) && !(String.IsNullOrWhiteSpace(PeupleJoueur2)) && !(String.IsNullOrWhiteSpace(NomJoueur1)) && !(String.IsNullOrWhiteSpace(NomJoueur2)) && !(String.IsNullOrWhiteSpace(TailleCarte)))
            {
                //Passage des paramètres via l'objet parent. --> appel aux méthodes de construction du jeu
                Console.WriteLine("All is OK");
                //Récupération des bons éléments en fonction des choix du joueur
                switch(TailleCarte)
                {
                    case "demo":
                        monteur = new SmallWorld.MonteurNPartieDemo();
                        break;
                    case "petite":
                        monteur = new SmallWorld.MonteurNPartiePetite();
                        break;
                    case "normale":
                        monteur = new SmallWorld.MonteurNPartieNormale();
                        break;
                }
                SmallWorld.FabriquePeuple fb1 = null, fb2 = null;
                switch(PeupleJoueur1)
                {
                    case "elfe":
                        fb1 = new SmallWorld.PeupleElfe();
                        break;
                    case "nain":
                        fb1 = new SmallWorld.PeupleNain();
                        break;
                    case "orc":
                        fb1 = new SmallWorld.PeupleOrc();
                        break;
                }
                switch(PeupleJoueur2)
                {
                    case "elfe":
                        fb2 = new SmallWorld.PeupleElfe();
                        break;
                    case "nain":
                        fb2 = new SmallWorld.PeupleNain();
                        break;
                    case "orc":
                        fb2 = new SmallWorld.PeupleOrc();
                        break;
                }
                parent.Content = new Jeu(monteur,NomJoueur1,fb1,NomJoueur2,fb2);
            }
            else
            {
                MessageBox.Show("Certains paramètres n'ont été fournis.");
            }
        }

        /// <summary>
        /// Handles the Checked event of the ChoixPeupleJ1 control. Check if Joueur1's Peuple is different from Joueur2's one.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ChoixPeupleJ1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (sender as RadioButton);
            PeupleJoueur1 = rd.Tag.ToString();
            Console.WriteLine("Joueur1 : " + PeupleJoueur1);
            // Vérification si le peuple n'est pas choisi 2 fois
            if (PeupleJoueur1 == PeupleJoueur2)
            {
                MessageBox.Show("Ce peuple est déjà pris par l'autre joueur. \nVeuillez en choisir un autre.", "Choisir son peuple", MessageBoxButton.OK);
                rd.IsChecked = false;
                PeupleJoueur1 = "";
            }
        }

        /// <summary>
        /// Handles the Checked event of the ChoixPeupleJ2 control. Check if Joueur2's Peuple is different from Joueur1's one.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ChoixPeupleJ2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (sender as RadioButton);
            PeupleJoueur2 = rd.Tag.ToString();
            Console.WriteLine("Joueur2 : " + PeupleJoueur2);

            // Vérification si le peuple n'est pas choisi 2 fois
            if (PeupleJoueur1 == PeupleJoueur2)
            {
                MessageBox.Show("Ce peuple est déjà pris par l'autre joueur. \nVeuillez en choisir un autre.", "Choisir son peuple", MessageBoxButton.OK);
                rd.IsChecked = false;
                PeupleJoueur2 = "";
            }
        }

        /// <summary>
        /// Handles the Checked event of the TailleCarte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TailleCarte_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (sender as RadioButton);
            TailleCarte = rd.Name;
            
        }
    }
}
