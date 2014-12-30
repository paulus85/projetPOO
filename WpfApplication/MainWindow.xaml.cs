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
            /*
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage (new Uri("C:/Users/Paul/Documents/SmallWorld/WpfApplication/Ressources/desert.png", UriKind.Absolute));
            ImageBrush brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri("C:/Users/Paul/Documents/SmallWorld/WpfApplication/Ressources/foret.png", UriKind.Absolute));
            Rectangle rec = new Rectangle();
            //canvas.Children.Add(new Rectangle())
            rec.Height = 79;
            rec.Width = 69;
            rec.Fill = brush;
            rec.StrokeThickness = 4;
            rec.Stroke = Brushes.Black;
            canvas.Children.Add(rec);
            Canvas.SetZIndex(rec, 2);


            Rectangle rec2 = new Rectangle();
            rec2.Height = 79;
            rec2.Width = 69;
            rec2.Fill = brush2;
            rec2.Margin = new Thickness(69,0,0,0);
            canvas.Children.Add(rec2);
            Canvas.SetZIndex(rec2, 2);
            */


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
            //TODO : faire les méthodes de désérialisation
            MessageBox.Show("Bientôt ! :)");
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
