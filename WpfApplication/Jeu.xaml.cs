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
using System.Collections.ObjectModel;

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

        private bool selectionCaseFaite = false;
        private bool selectionUniteFaite = false;
        private SmallWorld.Point pointSelection = null;

        ObservableCollection<UniteUC> listeUniteUC;
        /// <summary>
        /// Is like the interface between code behind data and view. Is binded to ItemsSource of Unite listBox.
        /// </summary>
        /// <value>
        /// The liste unite uc.
        /// </value>
        public ObservableCollection<UniteUC> ListeUniteUC
        {
            get { return listeUniteUC; }
            set { listeUniteUC = value; }
        }

        public Jeu(SmallWorld.MonteurNPartie monteur, string NomJoueur1, FabriquePeuple fb1, string NomJoueur2, FabriquePeuple fb2)
        {
            listeUniteUC = new ObservableCollection<UniteUC>();
            Console.WriteLine("Début Jeu");
            InitializeComponent();
            DataContext = this;
            engine = monteur.CreerJeu(NomJoueur1, fb1, NomJoueur2, fb2);
            Console.WriteLine("taille : " + engine.Carte.Cases.GetLength(0));
            createUI();
            refreshUI();
        }

        private void createUI()
        {
            //Joueurs 
            //Joueur 1
            JoueurUC juc1 = new JoueurUC(engine.Joueur1, engine.GetNbUnites(engine.Joueur1));
            Grid.SetColumn(juc1, 0);
            listJoueurs.Children.Add(juc1);
            //Joueur 2
            JoueurUC juc2 = new JoueurUC(engine.Joueur2, engine.GetNbUnites(engine.Joueur2));
            Grid.SetColumn(juc2, 1);
            listJoueurs.Children.Add(juc2);
        }

        private void refreshUI()
        {
            //List<UniteUC> listUniteUC = new List<UniteUC>();
            
            //SmallWorld.Unite u = new SmallWorld.UniteOrc(null);
            //UniteUC uniteuc = new UniteUC(u);
            //ListeUniteUC.Add(uniteuc);
            //SmallWorld.Unite u1 = new SmallWorld.UniteElfe();
            //UniteUC uniteuc1 = new UniteUC(u1);
            //ListeUniteUC.Add(uniteuc1);
            //SmallWorld.Unite u2 = new SmallWorld.UniteElfe();
            //UniteUC uniteuc2 = new UniteUC(u2);
            //ListeUniteUC.Add(uniteuc2);
            
            //list.ItemsSource = ListeUniteUC;

            //SmallWorld.Joueur j1 = new SmallWorld.JoueurImpl();
            //SmallWorld.Joueur j2 = new SmallWorld.JoueurImpl();
            //JoueurUC uc1 = new JoueurUC(j1,engine.GetNbUnites(j1));
            //Grid.SetColumn(uc1, 0);
            //JoueurUC uc2 = new JoueurUC(j2, engine.GetNbUnites(j2));
            //Grid.SetColumn(uc2, 1);
            //listJoueurs.Children.Add(uc1); 
            //listJoueurs.Children.Add(uc2);
            canvas.Children.Clear();
            refreshCarte();


            //Joueurs
            foreach (JoueurUC juc in listJoueurs.Children)
            {
                juc.refresh(engine.GetNbUnites(juc.Joueur));
            }

            //Unités sur la carte
            Dictionary<Unite,SmallWorld.Point> dj1 =  engine.Carte.GetUnites(engine.Joueur1);
            Dictionary<Unite, SmallWorld.Point> dj2 = engine.Carte.GetUnites(engine.Joueur2);
            foreach (KeyValuePair<Unite, SmallWorld.Point> entry in dj1)
            {
                afficherUnite(entry.Value.x, entry.Value.y, entry.Key);
            }
            foreach (KeyValuePair<Unite, SmallWorld.Point> entry in dj2)
            {
                afficherUnite(entry.Value.x, entry.Value.y, entry.Key);
            }

        }


        private void refreshCarte()
        {
            Case[,] TabCases = engine.Carte.Cases;
            int l = engine.Carte.Taille;
            int h = engine.Carte.Taille;
            // Les valeurs sont prises avec un peu de large...
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    afficherCase(i, j, TabCases[i,j]);
                }
            }
            canvas.Height = 100 * (1 + 0.75 * (h - 1));
            canvas.Width = l * 88 + paddingLigneImpaire;
            canvas2.Height = canvas.Height;
            canvas2.Width = canvas.Width;

        }


        /// <summary>
        /// Displays the case. Contains all the values about Cases and their disposition.
        /// </summary>
        /// <param name="i">The line number.</param>
        /// <param name="j">The column number.</param>
        /// <param name="type">The type of case.</param>
        public void afficherCase(int i, int j, SmallWorld.Case c)
        {
            
            Button b = new Button();
            if (FindResource("ButtonPolygon") != null)
            {
                b.Style = FindResource("ButtonPolygon") as Style;
            }
            b.Background = chooseBackground(c);
            b.Tag = new SmallWorld.PointImpl(i, j);
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
        /// Froms the canvas to coord.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <returns>A table with line number in [0] and column number in [1]</returns>
        public int[] FromCanvasToCoord(double left, double top)
        {
            int[] res = new int[2];
            //Line number
            res[0] =  (int)(top -6) / 75;
            if (res[0] % 2 == 0)
            {
                //Even line number
                res[1] = (int)(left - paddingLignePaire) / 87;
            }
            else
            {
                res[1] = (int)(left - paddingLigneImpaire) / 87;
            }
            return res;
        }


        #endregion

        /// <summary>
        /// Chooses the background for a type of case 
        /// </summary>
        /// <param name="s">The type of case.</param>
        /// <returns>The appropriate background</returns>
        private ImageBrush chooseBackground(SmallWorld.Case c)
        {
            if (c is CaseDesert)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/desert.png"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CaseForet)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/foret.png"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CaseMontagne)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/generation_montagne.jpg"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CasePlaine)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/plaine.png"));
                ib.Stretch = Stretch.UniformToFill;
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
            //Console.Write(top);
            double left = (double)ender.GetValue(Canvas.LeftProperty);
            //Console.WriteLine(" " + left);
            int[] coord = FromCanvasToCoord(left, top);
            Console.Write("coordLineColumn : ");
            Console.Write("" + coord[0]);
            Console.WriteLine(" ; " + coord[1]);
            Canvas.SetTop(PolygonSurvole, top);
            Canvas.SetLeft(PolygonSurvole, left);
            PolygonSurvole.Visibility = Visibility.Visible;
            e.Handled = true;
            Console.WriteLine("MouseEnter");
        }

        private void polygon_MouseClick(object sender, RoutedEventArgs e)
        {
            SmallWorld.Point point = (sender as Button).Tag as SmallWorld.Point;
            if (selectionUniteFaite)
            {
                bool val = engine.Tour.SetDestination(point);
                if (val) {
                    engine.Tour.ExecuterDeplacement();
                    polygon_NoSelection(sender, null);
                    refreshUI();
                }
                else
                {
                    Console.WriteLine("Déplacement impossible");
                    polygon_NoSelection(sender, null);
                }

            }
            else
            {
                if (selectionCaseFaite && this.pointSelection.Equals(point))
                {
                    //Déselection
                    polygon_NoSelection(sender, null);
                }
                else
                {
                    PolygonSurvole.Visibility = Visibility.Hidden;
                    FrameworkElement ender = sender as FrameworkElement;
                    double top = (double)ender.GetValue(Canvas.TopProperty);
                    double left = (double)ender.GetValue(Canvas.LeftProperty);
                    Canvas.SetTop(PolygonSelection, top);
                    Canvas.SetLeft(PolygonSelection, left);
                    PolygonSelection.Visibility = Visibility.Visible;
                    this.pointSelection = point;
                    selectionCaseFaite = true;
                    e.Handled = true;
                    Console.WriteLine("Polygon_mouseClick");
                    //Mise à jour de la liste d'unités
                    List<Unite> listUnites = engine.Carte.GetUnites(point);
                    ListeUniteUC.Clear();
                    foreach (Unite u in listUnites)
                    {
                        UniteUC uuc = new UniteUC(u);
                        ListeUniteUC.Add(uuc);
                    }
                }
            }
        }

      

        private void polygon_NoSelection(object sender, MouseButtonEventArgs e)
        {
            PolygonSelection.Visibility = Visibility.Hidden;
            this.pointSelection = null;
            selectionCaseFaite = false;
            selectionUniteFaite = false;
            ListeUniteUC.Clear();
            Console.WriteLine("NoSelection");
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



        /// <summary>
        /// Handles the SelectionChanged event of the list control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            engine.Tour.UnselectUnites();
            ListBox lb = sender as ListBox;
            List<UniteUC> listUC = lb.SelectedItems.Cast<UniteUC>().ToList<UniteUC>();
            List<Unite> listUnitesSelectionnees = new List<Unite>();
            if (listUC.Count == 0) 
            {
                selectionUniteFaite = false;
            }
            else
            {
                selectionUniteFaite = true;
            }
            foreach (UniteUC uuc in listUC)
            {
                listUnitesSelectionnees.Add(uuc.Unite);
            }
            engine.Tour.SelectUnites(listUnitesSelectionnees,pointSelection);
        }

        private void FindeTour_Click(object sender, RoutedEventArgs e)
        {
            if (engine.FinDuJeu())
            {
                Joueur winner = engine.Vainqueur();
                MessageBox.Show("Bravo ! " + Environment.NewLine + "Le gagnant est : " + winner.NomJoueur + " avec " + winner.Points + " points.");
            }
            else
            {
                engine.FinTour();
                refreshUI();
            }
        }

        #endregion events

        

    }
}
