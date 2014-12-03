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

        public MainWindow()
        {
            InitializeComponent();
        }

        public void creerCarte(){
            SmallWorld.CarteImpl carte = SmallWorld.CarteImpl.Instance;
            carte.instanciateCasesTab(2, 2);
            carte.Cases[1,1] = carte.fabrique.creerDesert();
            carte.Cases[0,0] = carte.fabrique.creerDesert();
            Rectangle rec = new Rectangle();
            //canvas.Children.Add(new Rectangle())
        }
       

        

        



    }
}
