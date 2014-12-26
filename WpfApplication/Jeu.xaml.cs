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
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class Jeu : Page
    {

        const int paddingLigneImpaire = 44;
        const int paddingLignePaire = 0;

        private SmallWorld.Jeu engine; 

        List<UniteUC> listeUniteUC = new List<UniteUC>();
        /// <summary>
        /// Is like the interface between code behind data and view. Is binded to ItemsSource of Unite listBox.
        /// </summary>
        /// <value>
        /// The liste unite uc.
        /// </value>
        public List<UniteUC> ListeUniteUC
        {
            get { return listeUniteUC; }
            set { listeUniteUC = value; }
        }

        public Jeu(SmallWorld.MonteurNPartie monteur, string NomJoueur1, FabriquePeuple fb1, string NomJoueur2, FabriquePeuple fb2)
        {
            Console.WriteLine("Début Jeu");
            InitializeComponent();
            DataContext = this;
            engine = monteur.CreerJeu(NomJoueur1, fb1, NomJoueur2, fb2);
            //Console.WriteLine("taille : " + engine.Carte.Cases.GetLength(0));
            refreshCarte(6,6);
            refreshUI();
        }

        private void refreshUI()
        {
            //List<UniteUC> listUniteUC = new List<UniteUC>();
            SmallWorld.Unite u = new SmallWorld.UniteOrc(null);
            UniteUC uniteuc = new UniteUC(u);
            ListeUniteUC.Add(uniteuc);
            SmallWorld.Unite u1 = new SmallWorld.UniteElfe();
            UniteUC uniteuc1 = new UniteUC(u1);
            ListeUniteUC.Add(uniteuc1);
            SmallWorld.Unite u2 = new SmallWorld.UniteElfe();
            UniteUC uniteuc2 = new UniteUC(u2);
            ListeUniteUC.Add(uniteuc2);
            //list.ItemsSource = ListeUniteUC;

            SmallWorld.Joueur j1 = new SmallWorld.JoueurImpl();
            SmallWorld.Joueur j2 = new SmallWorld.JoueurImpl();
            JoueurUC uc1 = new JoueurUC(j1);
            Grid.SetColumn(uc1, 0);
            JoueurUC uc2 = new JoueurUC(j2);
            Grid.SetColumn(uc2, 1);
            listJoueurs.Children.Add(uc1); 
            listJoueurs.Children.Add(uc2);

            afficherUnite(0, 0, u);
        }


        private void refreshCarte(int l, int h)
        {
            // Les valeurs sont prises avec un peu de large...
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    afficherCase(i, j, "Foret");
                }
            }
            canvas.Height = 100 * (1 + 0.75 * (h - 1));
            canvas.Width = l * 88 + paddingLigneImpaire;

        }


        /// <summary>
        /// Displays the case. Contains all the values about Cases and their disposition.
        /// </summary>
        /// <param name="i">The line number.</param>
        /// <param name="j">The column number.</param>
        /// <param name="type">The type of case.</param>
        public void afficherCase(int i, int j, string type)
        {
            
            Button b = new Button();
            if (FindResource("ButtonPolygon") != null)
            {
                b.Style = FindResource("ButtonPolygon") as Style;
            }
            b.Background = chooseBackground(type);
            b.Click += polygon_MouseClick;
            b.MouseEnter += polygon_MouseEnter;
            int[] coordCanvas = FromCoordToCanvas(i, j);
            Canvas.SetLeft(b, coordCanvas[0]);
            Canvas.SetTop(b, coordCanvas[1]);
            Canvas.SetZIndex(b, 1);
            //b.Content = "" + i + ";" + j;
            canvas.Children.Add(b);
        }

        public void afficherUnite(int i, int j, Unite u)
        {
            Button b = new Button();
            b.IsHitTestVisible = false;
            if (FindResource("PolygonUnite") != null)
            {
                b.Style = FindResource("PolygonUnite") as Style;
            }
            b.Background = chooseBackgroundUnite(u);
            int[] coordCanvas = FromCoordToCanvas(i, j);
            Canvas.SetLeft(b, coordCanvas[0]);
            Canvas.SetTop(b, coordCanvas[1]);
            Canvas.SetZIndex(b, 3);
            canvas.Children.Add(b);
        }


        #region Translation
        /// <summary>
        /// Translates line and column number to TOP and LEFT value for the canvas.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>The table with correct values. res[0] = left ; res[1] = top</returns>
        private int[] FromCoordToCanvas(int i, int j)
        {
            int[] res = new int[2];
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
            res[0] = paddingLigne + j * 87;
            res[1] = 6 + i * 75;
            return res;
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
        #endregion

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

        /// <summary>
        /// Chooses the sprite for a Unite
        /// </summary>
        /// <returns>The appropriate ImageBrush</returns>
        private ImageBrush chooseBackgroundUnite(Unite u)
        {
            if (u is UniteElfe)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_elfe.png"));
                ib.Stretch = Stretch.Uniform;
                return ib;
            } else
                if (u is UniteNain)
                {
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_nain.png"));
                    ib.Stretch = Stretch.Uniform;
                    return ib;
                } else
                    {
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/sprite_orc.png"));
                        ib.Stretch = Stretch.Uniform;
                        return ib;
                    }
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
            Canvas.SetTop(PolygonSurvole, top);
            Canvas.SetLeft(PolygonSurvole, left);
            PolygonSurvole.Visibility = Visibility.Visible;
            e.Handled = false;
            Console.WriteLine("MouseEnter");
            
        }

        private void polygon_MouseClick(object sender, RoutedEventArgs e)
        {
            PolygonSurvole.Visibility = Visibility.Hidden;
            FrameworkElement ender = sender as FrameworkElement;
            double top = (double)ender.GetValue(Canvas.TopProperty);
            double left = (double)ender.GetValue(Canvas.LeftProperty);
            Canvas.SetTop(PolygonSelection, top);
            Canvas.SetLeft(PolygonSelection, left);
            PolygonSelection.Visibility = Visibility.Visible;
            e.Handled = false;
            Console.WriteLine("Polygon_mouseClick");
        }

      

        private void polygon_NoSelection(object sender, MouseButtonEventArgs e)
        {
            PolygonSelection.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles the MouseLeave event of the canvas control. Allows to mask the selection Polygon.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            PolygonSurvole.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles the PreviewMouseDown event of the list control. Allows to deselect an item from the UniteUC listBox with one click only.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void list_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListBoxItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;
            ListBoxItem item = (ListBoxItem)dep;
            if (item.IsSelected)
            {
                item.IsSelected = !item.IsSelected;
                e.Handled = true;
            }
        }
        

        #endregion events

        
    }
}
