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
using Microsoft.Win32;

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// Ceci est le controller
    /// </summary>
    public partial class MainWindow : Window
    {

        const int dimensionCase = 5;
        

        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = this;

        }

       

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            // Configuration de la boite de dialogue
            String messageBoxMessage = "Est-vous sûr de vouloir vraiment quitter cette superbe application ?";
            String title = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.None;
            MessageBoxResult res = MessageBox.Show(messageBoxMessage,title,buttons,image);
            switch (res)
            {
                case MessageBoxResult.Yes :
                    MessageBox.Show("Salaud !");
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    break;
            }
            
        }

        private void Charger_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Fichier de sauvegarde (*.yolo)|*.yolo";
            if (ofd.ShowDialog() == true)
            {
                Console.WriteLine(ofd.FileName);
                //Appeler MonteurJeuSauv
            }
        }

        private void NouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new NouveauJeu();
        }

        

        

        //private void Path_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    champ = sender.ToString();
        //    Console.WriteLine(champ);
        //}
       

        

        



    }
}
