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
using System.Windows.Threading;
using SmallWorld;
using System.Collections.ObjectModel;
using Microsoft.Win32;

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
        private MainWindow parent;

        private bool selectionCaseFaite = false;
        private bool selectionUniteFaite = false;
        private SmallWorld.Point pointSelection = null;
        private SmallWorld.Point pointSurvol = null;

        ObservableCollection<UniteUC> listeUniteUC;
        /// <summary>
        /// Interface entre la donnée code behind et la vue. Est bindé à ItemsSource de la listBox Unite.
        /// </summary>
        /// <value>
        /// La liste d'UniteUC
        /// </value>
        public ObservableCollection<UniteUC> ListeUniteUC
        {
            get { return listeUniteUC; }
            set { listeUniteUC = value; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Jeu"/>.
        /// </summary>
        /// <param name="monteur">Le monteur.</param>
        /// <param name="NomJoueur1">Le nom du Joueur 1.</param>
        /// <param name="fb1">La fabrique du Joueur 1</param>
        /// <param name="NomJoueur2">Le nom du Joueur 2</param>
        /// <param name="fb2">La fabrique du Joueur 2</param>
        public Jeu(SmallWorld.MonteurNPartie monteur, string NomJoueur1, int fb1, string NomJoueur2, int fb2)
        {
            listeUniteUC = new ObservableCollection<UniteUC>();
            Console.WriteLine("Début Jeu");
            InitializeComponent();
            DataContext = this;
            parent = (Application.Current.MainWindow as MainWindow);
            engine = monteur.CreerJeu(NomJoueur1, fb1, NomJoueur2, fb2);
            Console.WriteLine("taille : " + engine.Carte.Cases.GetLength(0));
            createUI();
            refreshUI();
            NouveauTour();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="Jeu"/>.
        /// </summary>
        /// <param name="monteur">Le monteur.</param>
        /// <param name="path">Le chemin d'accès au fichier.</param>
        public Jeu(SmallWorld.MonteurPartieSauv monteur, string path)
        {
            listeUniteUC = new ObservableCollection<UniteUC>();
            InitializeComponent();
            DataContext = this;
            parent = (Application.Current.MainWindow as MainWindow);
            engine = monteur.CreerJeu(path);
            createUI();
            refreshUI();
            NouveauTour();
        }

        /// <summary>
        /// Crée l'UI la première fois.
        /// </summary>
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

        /// <summary>
        /// Rafraichit l'UI : met à jour la carte et ses différents éléments, et la console interne.
        /// </summary>
        private void refreshUI()
        {
            
            canvas.Children.Clear();
            refreshCarte();


            //Joueurs
            foreach (JoueurUC juc in listJoueurs.Children)
            {
                juc.refresh(engine.GetNbUnites(juc.Joueur));
            }

            

            //Console interne
            clearConsole();

        }


        /// <summary>
        /// Rafraichit la carte
        /// </summary>
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
                    //Affichage de l'unité
                    SmallWorld.Point pt = new SmallWorld.PointImpl(i, j);
                    List<Unite> listeUniteCase = engine.Carte.GetUnites(pt);
                    if (listeUniteCase.Count > 0)
                    {
                        afficherUnite(pt, listeUniteCase[0]);
                    }
                }
            }
            canvas.Height = 100 * (1 + 0.75 * (h - 1));
            canvas.Width = l * 88 + paddingLigneImpaire;
            canvas2.Height = canvas.Height;
            canvas2.Width = canvas.Width;
            canvas1.Height = canvas.Height;
            canvas1.Width = canvas.Width;
        }


        /// <summary>
        /// Displays the case. Contains all the values about Cases and their disposition.
        /// Affiche une case. Contient toutes les valeurs à propos des Cases et de leur disposition.
        /// </summary>
        /// <param name="i">Le numéro de ligne</param>
        /// <param name="j">Le numéro de colonne</param>
        /// <param name="c">La case à afficher</param>
        public void afficherCase(int i, int j, SmallWorld.Case c)
        {
            
            Button b = new Button();
            //if (FindResource("ButtonPolygon") != null)
            //{
            //    b.Style = FindResource("ButtonPolygon") as Style;
            //}
            if (FindResource("PolygonDemo") != null)
            {
                b.Style = FindResource("PolygonDemo") as Style;
            }
            b.Background = chooseBackground(c);
            b.Tag = new SmallWorld.PointImpl(i, j);
            b.Focusable = false;
            b.Click += polygon_MouseClick;
            b.MouseEnter += polygon_MouseEnter;
            int[] coordCanvas = FromCoordToCanvas(i, j);
            Canvas.SetLeft(b, coordCanvas[0]);
            Canvas.SetTop(b, coordCanvas[1]);
            Canvas.SetZIndex(b, 1);
            //b.Content = "" + i + ";" + j;
            canvas.Children.Add(b);
        }

        /// <summary>
        /// Affiche une unité sur une case.
        /// </summary>
        /// <param name="p">Le point représentant la case où afficher.</param>
        /// <param name="u">L'unité à afficher</param>
        public void afficherUnite(SmallWorld.Point p, Unite u)
        {
            Button b = new Button();
            b.IsHitTestVisible = false;
            if (FindResource("PolygonUnite") != null)
            {
                b.Style = FindResource("PolygonUnite") as Style;
            }
            b.Background = chooseBackgroundUnite(u);
            b.Focusable = false;
            b.Content = engine.GetNbUnites(p);
            int[] coordCanvas = FromCoordToCanvas(p.x, p.y);
            Canvas.SetLeft(b, coordCanvas[0]);
            Canvas.SetTop(b, coordCanvas[1]);
            Canvas.SetZIndex(b, 2);
            canvas.Children.Add(b);
        }

        /// <summary>
        /// Affiche les suggestions de cases
        /// </summary>
        /// <param name="u">L'unité qui doit se déplacer.</param>
        /// <param name="p">Le point où se trouve l'unité.</param>
        public void afficherSuggestions(Unite u, SmallWorld.Point p)
        {
            masquerSuggestions();
            List<SmallWorld.Point> listPoints = engine.Tour.SuggestionsCase(u, p);
            foreach (SmallWorld.Point pt in listPoints)
            {
                Button b = new Button();
                b.IsHitTestVisible = false;
                if (FindResource("PolygonSuggestion") != null)
                {
                    b.Style = FindResource("PolygonSuggestion") as Style;
                }
                int[] coordCanvas = FromCoordToCanvas(pt.x, pt.y);
                Canvas.SetLeft(b, coordCanvas[0]);
                Canvas.SetTop(b, coordCanvas[1]);
                Canvas.SetZIndex(b, 3);
                canvas1.Children.Add(b);
            }
        }
        /// <summary>
        /// Masque les suggestions.
        /// </summary>
        public void masquerSuggestions()
        {
            canvas1.Children.Clear();
        }


        #region Translation
        /// <summary>
        /// Traduit les numéros de ligne et colonne en valeurs Top et Left pour le canvas.
        /// </summary>
        /// <param name="i">Le numéro de ligne</param>
        /// <param name="j">Le numéro de colonne</param>
        /// <returns>La table avec les valeurs correctes. res[0] = left ; res[1] = top</returns>
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
            res[1] = 0 + i * 75;
            return res;
        }
      

        /// <summary>
        /// Traduit les valeurs Top et Left du canvas en numéros de ligne/colonne
        /// </summary>
        /// <param name="left">La valeur Left du canvas</param>
        /// <param name="top">La valeur Top du canvas</param>
        /// <returns>Un tableau avec le numéro de ligne en [0] et le numéro de colonne en [1]</returns>
        public int[] FromCanvasToCoord(double left, double top)
        {
            int[] res = new int[2];
            //Line number
            res[0] =  (int)(top - 0) / 75;
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
        /// Choisit le fond selon le type de la case
        /// </summary>
        /// <param name="c">La case dont il faut le fond</param>
        /// <returns>L'<see cref="ImageBrush"/> adapté selon la case</returns>
        private ImageBrush chooseBackground(SmallWorld.Case c)
        {
            if (c is CaseDesert)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/desert_ok.png"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CaseForet)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/foret_ok.png"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CaseMontagne)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/montagne_ok.png"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CasePlaine)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/plaine_ok.png"));
                ib.Stretch = Stretch.UniformToFill;
                return ib;
            }
            else if (c is CaseMarais)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Ressources/marais_ok.png"));
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


        /// <summary>
        /// Rafraichit l'interface à chaque nouveau tour
        /// </summary>
        private void NouveauTour()
        {
            refreshUI();
            texteJoueurActif.Text = "C'est au tour de " + engine.JoueurCourant.NomJoueur + " de jouer !";
            numeroTour.Content = "" + engine.TourActuelle + "/" + engine.NbTour;
        }

        /// <summary>
        /// Permet d'écrire un texte dans la console intégrée.
        /// </summary>
        /// <param name="p">La chaine de caractère à écrire.</param>
        /// <remarks>Le texte reste à l'écran seulement 3 secondes.</remarks>
        private void ecritureConsole(string p)
        {
            //consoleInterne.Text = p;
            consoleInterne.Text = "YOLO \n Yolo";
            Console.WriteLine(p);
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 5);
            messageTimer.Start();
        }

        /// <summary>
        /// Vide la console intégrée.
        /// </summary>
        private void clearConsole()
        {
            consoleInterne.Text = "";
        }



        ////////////////////////////////////////////////// EVENEMENTS ////////////////////////////////////////////////
        #region events
       

        /// <summary>
        /// Gère l'événement MouseEnter sur le polygone de la carte
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="MouseEventArgs" /> qui contient les données de l'événement</param>
        void polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement ender = sender as FrameworkElement;
            SmallWorld.Point point = (sender as Button).Tag as SmallWorld.Point;
            pointSurvol = point;
            displayPolygonSurvole(point);
            //double top = (double)ender.GetValue(Canvas.TopProperty);
            //double left = (double)ender.GetValue(Canvas.LeftProperty);

            //int[] coord = FromCanvasToCoord(left, top);
            //Console.Write("coordLineColumn : ");
            //Console.Write("" + coord[0]);
            //Console.WriteLine(" ; " + coord[1]);
            //Canvas.SetTop(PolygonSurvole, top);
            //Canvas.SetLeft(PolygonSurvole, left);
            //PolygonSurvole.Visibility = Visibility.Visible;
            e.Handled = true;
        }

        private void displayPolygonSurvole(SmallWorld.Point point)
        {
            int[] canvasCoord = FromCoordToCanvas(point.x, point.y);
            double top = canvasCoord[1];
            double left = canvasCoord[0];
            Canvas.SetTop(PolygonSurvole, top);
            Canvas.SetLeft(PolygonSurvole, left);
            PolygonSurvole.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Gère l'événement MouseClick sur le polygone de la carte
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs" /> qui contient les données de l'événement</param>
        private void polygon_MouseClick(object sender, RoutedEventArgs e)
        {
            SmallWorld.Point point = (sender as Button).Tag as SmallWorld.Point;
            selectionChange(point);
            e.Handled = true;
        }

        /// <summary>
        /// Méthode de changement de sélection de cases.
        /// </summary>
        /// <param name="pt">Le point sous-jacent à la case sélectionné.</param>
        private void selectionChange(SmallWorld.Point pt)
        {
            if (selectionUniteFaite)
            {
                bool val = engine.Tour.SetDestination(pt);
                if (val)
                {
                    engine.Tour.ExecuterDeplacement();
                    Console.WriteLine(engine.Tour.ResumeCombat);
                    ecritureConsole(engine.Tour.ResumeCombat);
                    undisplaySelection();
                    refreshUI();
                }
                else
                {
                    ecritureConsole("Déplacement impossible");
                    undisplaySelection();
                }

            }
            else
            {
                if (selectionCaseFaite && this.pointSelection.Equals(pt))
                {
                    //Déselection
                    undisplaySelection();
                }
                else
                {
                    this.pointSelection = pt;
                    displayPolygonSelection(pt);
                    selectionCaseFaite = true;
                    Console.WriteLine("Polygon_mouseClick");
                    //Mise à jour de la liste d'unités
                    List<Unite> listUnites = engine.Carte.GetUnites(pt);
                    ListeUniteUC.Clear();
                    foreach (Unite u in listUnites)
                    {
                        UniteUC uuc = new UniteUC(u);
                        uuc.IsSelectable = (u.PointsDeplacementRestant > 0) && (u.Proprio.Equals(engine.JoueurCourant));
                        ListeUniteUC.Add(uuc);
                    }
                }
            }
        }

        /// <summary>
        /// Affiche le polygone de sélection.
        /// </summary>
        /// <param name="point">Le point sous-jacent à la case sélectionné.</param>
        private void displayPolygonSelection(SmallWorld.Point point)
        {
            PolygonSurvole.Visibility = Visibility.Hidden;
            int[] canvasCoord = FromCoordToCanvas(point.x, point.y);
            double top = canvasCoord[1];
            double left = canvasCoord[0];
            Canvas.SetTop(PolygonSelection, top);
            Canvas.SetLeft(PolygonSelection, left);
            PolygonSelection.Visibility = Visibility.Visible;
            Console.WriteLine("Point Sélection : " + point.ToString());
        }





        /// <summary>
        /// Gère l'événement de déselection d'un polygone.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="MouseButtonEventArgs" /> qui contient les données de l'événement</param>
        private void polygon_NoSelection(object sender, MouseButtonEventArgs e)
        {
            undisplaySelection();
            e.Handled = true;
        }

        /// <summary>
        /// Efface le polygone de sélection de case.
        /// </summary>
        private void undisplaySelection()
        {
            PolygonSelection.Visibility = Visibility.Hidden;
            this.pointSelection = null;
            selectionCaseFaite = false;
            selectionUniteFaite = false;
            ListeUniteUC.Clear();
            Console.WriteLine("NoSelection");
            masquerSuggestions();
        }

        /// <summary>
        /// Gère l'événement MouseLeave du canvas. Permet de masquer le polygone de sélection.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="MouseEventArgs" /> qui contient les données de l'événement</param>
        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            PolygonSurvole.Visibility = Visibility.Hidden;
            pointSurvol = null;
        }

        /// <summary>
        /// Gère l'événement PreviewMouseDown sur la liste des unités. Permet de déselectionner un item de la listBox d'UniteUC avec juste un seul clic.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="MouseButtonEventArgs" /> qui contient les données de l'événement</param>
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
        /// Gère l'événement SelectionChanged de la liste d'unités. Permet de contrôler les unités et de préparer les méthodes de changements de position.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="SelectionChangedEventArgs" /> qui contient les données de l'événement</param>
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            engine.Tour.UnselectUnites();
            ListBox lb = sender as ListBox;
            List<UniteUC> listUC = lb.SelectedItems.Cast<UniteUC>().ToList<UniteUC>();
            List<UniteUC> listUCvalide = new List<UniteUC>();          
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
            if (listUnitesSelectionnees.Count != 0)
            {
                afficherSuggestions(listUnitesSelectionnees[0], pointSelection);
            }
            else
            {
                masquerSuggestions();
            }
        }

        /// <summary>
        /// Gère l'événement Click sur le bouton Fin de Tour.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs" /> qui contient les données de l'événement</param>
        private void FindeTour_Click(object sender, RoutedEventArgs e)
        {
            engine.FinTour();
            if (engine.FinDuJeu())
            {
                Joueur winner = engine.Vainqueur();
                if (winner == null) MessageBox.Show("Match nul! ");
                else MessageBox.Show("Bravo ! " + Environment.NewLine + "Le gagnant est : " + winner.NomJoueur + " avec " + winner.Points + " points.");
            }
            else
            {  
                NouveauTour();
            }
        }

        /// <summary>
        /// Gère l'événement Tick sur le contrôle messageTimer. Est appelée à chaque tic du timer de la console intégrée, et efface le contenu de la console.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="EventArgs" /> qui contient les données de l'événement</param>
        private void messageTimer_Tick(object sender, EventArgs e)
        {
            clearConsole();
        }




        /// <summary>
        /// Gère l'événement PreviewKeyDown de la page. Exécute les commandes associées à chaque appui de touche.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="KeyEventArgs" /> qui contient les données de l'événement</param>
        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindeTour_Click(this, null);
            }
            else if (e.Key == Key.Right)
            {
                //Déplacement du survol vers la droite
                if (pointSurvol == null) pointSurvol = new SmallWorld.PointImpl(0, 0);
                else pointSurvol.y = (pointSurvol.y + 1) % engine.Carte.Taille;
                displayPolygonSurvole(pointSurvol);
            }
            else if (e.Key == Key.Left)
            {
                //Déplacement du survol vers la gauche
                if (pointSurvol == null) pointSurvol = new SmallWorld.PointImpl(0, 0);
                else
                {
                    if (pointSurvol.y == 0) pointSurvol.y = engine.Carte.Taille - 1;
                    else pointSurvol.y--;
                }
                displayPolygonSurvole(pointSurvol);
            }
            else if (e.Key == Key.Down)
            {
                //Déplacement du survol vers le haut
                if (pointSurvol == null) pointSurvol = new SmallWorld.PointImpl(0, 0);
                else pointSurvol.x = (pointSurvol.x + 1) % engine.Carte.Taille;
                displayPolygonSurvole(pointSurvol);
            }
            else if (e.Key == Key.Up)
            {
                //Déplacement du survol vers le haut
                if (pointSurvol == null) pointSurvol = new SmallWorld.PointImpl(0, 0);
                else
                {
                    if (pointSurvol.x == 0) pointSurvol.x = engine.Carte.Taille - 1;
                    else pointSurvol.x--;
                }
                displayPolygonSurvole(pointSurvol);
            }
            else if (e.Key == Key.LeftCtrl | e.Key == Key.RightCtrl)
            {
                //Gestion de la sélection 
                if(pointSurvol != null) selectionChange(pointSurvol);
            }
        }


        /// <summary>
        /// Gère l'événement PageLoaded de la page. Se lance dès que la page avec le jeu est chargée.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs" /> qui contient les données de l'événement</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
        }

        /// <summary>
        /// Gère l'événement Click sur le bouton Sauvegarde. Enclenche les mécanismes de sauvegarde du jeu.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs" /> qui contient les données de l'événement</param>
        private void Sauvegarde_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Fichier de sauvegarde (*.yolo)|*.yolo";
            if (sfd.ShowDialog() == true)
            {
                Console.WriteLine(sfd.FileName);
                engine.SauvegarderJeu(sfd.FileName);
            }
        }

        /// <summary>
        /// Gère l'événement Click sur le bouton Menu. Quitte le jeu et affiche le menu principal.
        /// </summary>
        /// <param name="sender">La source de l'événement</param>
        /// <param name="e">L'instance de <see cref="RoutedEventArgs" /> qui contient les données de l'événement</param>
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            String messageBoxMessage = "Êtes-vous sûr de vouloir revenir au menu principal ? \nLes changements non sauvegardés ne seront pas conservés.";
            String title = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.None;
            MessageBoxResult res = MessageBox.Show(messageBoxMessage, title, buttons, image);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    parent.Content = new MenuDebut();
                    break;
                default:
                    break;
            }
        }

        #endregion events

    }
}
