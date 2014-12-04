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

        SmallWorld.CarteImpl carte;
        const int dimensionCase = 5;
        private string champ;
        public string Champ
        {
            get { return champ; }
            set
            {
                if (champ == value)
                    return;
                champ = value;
                //PropertyChanged("Champ");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            champ = "Default";
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

        public void creerCarte(){
            SmallWorld.CarteImpl carte = SmallWorld.CarteImpl.Instance;
            carte.instanciateCasesTab(2, 2);
            carte.Cases[1,1] = carte.fabrique.creerDesert();
            carte.Cases[0,0] = carte.fabrique.creerDesert();
            Rectangle rec = new Rectangle();
            //canvas.Children.Add(new Rectangle())
            rec.Height = 10;
            rec.Width = 20;
            rec.Stroke = Brushes.Aqua;
            canvas.Children.Add(rec);
        }

        public void creerCase(SmallWorld.Case c, int x, int y) {

            Polygon caseDraw = new Polygon();
            caseDraw.Points = new PointCollection();
        }

        //private void Path_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    champ = sender.ToString();
        //    Console.WriteLine(champ);
        //}
       

        

        



    }
}
