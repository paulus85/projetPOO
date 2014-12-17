﻿using System;
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

        private void Commencer_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrWhiteSpace(PeupleJoueur1)) && !(String.IsNullOrWhiteSpace(PeupleJoueur2)) && !(String.IsNullOrWhiteSpace(NomJoueur1)) && !(String.IsNullOrWhiteSpace(NomJoueur2)) && !(String.IsNullOrWhiteSpace(TailleCarte)))
            {
                //Passage des paramètres via l'objet parent. --> appel aux méthodes de construction du jeu
                Console.WriteLine("All is OK");
                parent.Content = new Jeu();
            }
            else
            {
                MessageBox.Show("Certains paramètres n'ont été fournis.");
            }
        }

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

        private void TailleCarte_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (sender as RadioButton);
            TailleCarte = rd.Name;
            
        }
    }
}
