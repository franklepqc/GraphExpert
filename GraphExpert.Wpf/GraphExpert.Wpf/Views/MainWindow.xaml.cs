using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Models;
using GraphExpert.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphExpert.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindowViewModel Modele => DataContext as MainWindowViewModel;

        private void SurClicBoutonGauche(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Canvas))
            {
                var position = e.GetPosition(e.Source as IInputElement);

                Modele.AjouterArret(position.X - 15, position.Y - 15, string.Empty);
            }
            else
            {
                object obj = ObtenirAgent(e.OriginalSource);

                if (null != obj)
                {
                    Modele.Deplacer(obj as AgentVM, 2);
                }
                else
                {
                    Modele.AjouterLiaison(ObtenirStop(e.OriginalSource));
                }
            }
        }

        private void SurClicBoutonDroit(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var noeud = ObtenirStop(e.OriginalSource);

            if (null != noeud)
            {
                Modele.AjouterAgent(noeud.X, noeud.Y, noeud.Id);
            }
        }
        
        private AgentVM ObtenirAgent(object element)
        {
            return (element as FrameworkElement).DataContext as AgentVM;
        }

        private StopVM ObtenirStop(object element)
        {
            return (element as FrameworkElement).DataContext as StopVM;
        }
    }
}
