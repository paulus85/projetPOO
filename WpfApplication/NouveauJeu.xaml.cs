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
        
        
        public NouveauJeu()
        {
            InitializeComponent();
            // TODO: redimensionner la fenetre pour que ça soit plus joli
            DataContext = this;
            NomJoueur1 = "Paul";
            NomJoueur2 = "Stone";
            PeupleJoueur1 = "";
            PeupleJoueur2 = "";
            TailleCarte = "";
            parent = (Application.Current.MainWindow as MainWindow);
            //parent.Height = 700;
            
        }

        /// <summary>
        /// Traitant de l'événement de clic sur le bouton Commencer. Vérifie sit tout est OK et appelle le constructeur avec tous les paramètres nécessaires.
        /// </summary>
        /// <param name="sender">La source de l'événement.</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>

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
                        this.monteur = new SmallWorld.MonteurNPartieDemo();
                        break;
                    case "petite":
                        this.monteur = new SmallWorld.MonteurNPartiePetite();
                        break;
                    case "normale":
                        this.monteur = new SmallWorld.MonteurNPartieNormale();
                        break;
                }
                int fb1 = -1, fb2 = -1;
                switch(PeupleJoueur1)
                {
                    case "elfe":
                        fb1 = (int)SmallWorld.NumUnite.ELF;
                        break;
                    case "nain":
                        fb1 = (int)SmallWorld.NumUnite.NAIN;
                        break;
                    case "orc":
                        fb1 = (int)SmallWorld.NumUnite.ORC;
                        break;
                    case "zombie":
                        fb1 = (int)SmallWorld.NumUnite.ZOMBIE;
                        break;
                }
                switch(PeupleJoueur2)
                {
                    case "elfe":
                        fb2 = (int)SmallWorld.NumUnite.ELF;
                        break;
                    case "nain":
                        fb2 = (int)SmallWorld.NumUnite.NAIN;
                        break;
                    case "orc":
                        fb2 = (int)SmallWorld.NumUnite.ORC;
                        break;
                    case "zombie":
                        fb2 = (int)SmallWorld.NumUnite.ZOMBIE;
                        break;
                }
                this.parent.Content = new Jeu(monteur,NomJoueur1,fb1,NomJoueur2,fb2);
            }
            else
            {
                MessageBox.Show("Certains paramètres n'ont été fournis.");
            }
        }

        /// <summary>
        /// Traitant de l'événement de clic sur un des RadioButton de choix de peuple du joueur 1. Vérifie sur le peuple du Joueur 1 est différente de celle du Joueur 2.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>
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
        /// Traitant de l'événement de clic sur un des RadioButton de choix de peuple du joueur 2. Vérifie sur le peuple du Joueur 2 est différente de celle du Joueur 1.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>
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
        /// Traitant de l'événement de clic sur un des RadioButton de choix de taille de carte. 
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs"/> contenant les données de l'événement</param>
        private void TailleCarte_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (sender as RadioButton);
            TailleCarte = rd.Name;
            
        }

    }
}
