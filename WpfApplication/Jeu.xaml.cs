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
        public Jeu()
        {
            InitializeComponent();
        }

        public void creerCarte()
        {
            SmallWorld.CarteImpl carte = SmallWorld.CarteImpl.Instance;
            carte.instanciateCasesTab(2, 2);
            carte.Cases[1, 1] = carte.fabrique.creerDesert();
            carte.Cases[0, 0] = carte.fabrique.creerDesert();
            Rectangle rec = new Rectangle();
            //canvas.Children.Add(new Rectangle())
            rec.Height = 10;
            rec.Width = 20;
            rec.Stroke = Brushes.Aqua;
            canvas.Children.Add(rec);
        }

        public void creerCase(SmallWorld.Case c, int x, int y)
        {

            Polygon caseDraw = new Polygon();
            caseDraw.Points = new PointCollection();
        }
    }
}
