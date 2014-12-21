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
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class Jeu : Page
    {

        const int paddingLigneImpaire = 44;
        const int paddingLignePaire = 0;

        public Jeu()
        {
            InitializeComponent();
            refreshCarte(16,16);
            refreshUI();
        }

        private void refreshUI()
        {
            //ListBox list = new ListBox();
            List<UniteUC> listUniteUC = new List<UniteUC>();
            SmallWorld.Unite u = new SmallWorld.UniteElfe();
            UniteUC uniteuc = new UniteUC(u);
            listUniteUC.Add(uniteuc);
            SmallWorld.Unite u1 = new SmallWorld.UniteElfe();
            UniteUC uniteuc1 = new UniteUC(u1);
            listUniteUC.Add(uniteuc1);
            SmallWorld.Unite u2 = new SmallWorld.UniteElfe();
            UniteUC uniteuc2 = new UniteUC(u2);
            listUniteUC.Add(uniteuc2);
            list.ItemsSource = listUniteUC;
        }


        private void refreshCarte(int l, int h)
        {
            canvas.Height = 100 * (1 + 0.75 * (h - 1));
            canvas.Width = l * 88 + paddingLigneImpaire;
            // Les valeurs sont prises avec un peu de large...
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    afficherCase(i, j, "Foret");
                }
            }
        }


        /// <summary>
        /// Displays the case. Contains all the values about Cases and their disposition.
        /// </summary>
        /// <param name="i">The line number.</param>
        /// <param name="j">The column number.</param>
        /// <param name="type">The type of case.</param>
        public void afficherCase(int i, int j, string type)
        {
            int paddingLigne;
            if (i % 2 == 0)
            {
                //Ligne paire
                paddingLigne = paddingLignePaire;
            }
            else
            {
                //Ligne impaire
                paddingLigne = paddingLigneImpaire;
            }
            Button b = new Button();
            if (FindResource("ButtonPolygon") != null)
            {
                b.Style = FindResource("ButtonPolygon") as Style;
            }
            b.Background = chooseBackground(type);
            //b.Background = Brushes.Black ;
            b.MouseEnter += polygon_MouseEnter;
            Canvas.SetLeft(b, paddingLigne + j * 87);
            Canvas.SetTop(b,6 + i * 75);
            Canvas.SetZIndex(b, 1);
            canvas.Children.Add(b);
        }

        

        /// <summary>
        /// DEPRECATED
        /// Froms the canvas to coord.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <returns>A table with line number in [0] and column number in [1]</returns>
        public int[] FromCanvasToCoord(double left, double top)
        {
            int[] res = new int[2];
            //Line number
            res[0] =  (int)(top -70) / 130;
            if (res[0] % 2 == 0)
            {
                //Even line number
                res[1] = (int)(left -70) / 150;
            }
            else
            {
                res[1] = (int)(left -145) / 150;
            }
            return res;
        }

        /// <summary>
        /// Chooses the background for a type of case 
        /// </summary>
        /// <param name="s">The type of case.</param>
        /// <returns>The appropriate background</returns>
        private ImageBrush chooseBackground(String s)
        {
            switch (s)
            {
                case "Montagne" :
                    return new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/mer.png")));
                    
                case "Desert" :
                    return new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/desert.png")));
                case "Foret" :
                    //return new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/foret.png")));
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/ocean.png"));
                    ib.Stretch = Stretch.UniformToFill;
                    //ib.RelativeTransform = new RotateTransform(-30,0.5,0.5);
                    return ib;
            }
            return null;
        }

        //public void creerCarte()
        //{
        //    SmallWorld.CarteImpl carte = SmallWorld.CarteImpl.Instance;
        //    carte.instanciateCasesTab(2, 2);
        //    carte.Cases[1, 1] = carte.fabrique.creerDesert();
        //    carte.Cases[0, 0] = carte.fabrique.creerDesert();
        //    Rectangle rec = new Rectangle();
        //    //canvas.Children.Add(new Rectangle())
        //    rec.Height = 10;
        //    rec.Width = 20;
        //    rec.Stroke = Brushes.Aqua;
        //    canvas.Children.Add(rec);
        //}

        public void creerCase(SmallWorld.Case c, int x, int y)
        {

            Polygon caseDraw = new Polygon();
            caseDraw.Points = new PointCollection();
        }

        

        ////////////////////////////////////////////////// EVENEMENTS ////////////////////////////////////////////////
        #region events
       

        /// <summary>
        /// Handles the MouseDown event of the polygon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        void polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement ender = sender as FrameworkElement;
            double top = (double)ender.GetValue(Canvas.TopProperty);
            Console.Write(top);
            double left = (double)ender.GetValue(Canvas.LeftProperty);
            Console.WriteLine(" " + left);
            int[] coord = FromCanvasToCoord(left, top);      
            Canvas.SetTop(PolygonSelected, top);
            Canvas.SetLeft(PolygonSelected, left);
            PolygonSelected.Visibility = Visibility.Visible;
            e.Handled = true;
            Console.WriteLine("MouseEnter");
            
        }

        /// <summary>
        /// Handles the MouseLeave event of the canvas control. Allows to mask the selection Polygon.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            PolygonSelected.Visibility = Visibility.Hidden;
        }
        #endregion events
    }
}
