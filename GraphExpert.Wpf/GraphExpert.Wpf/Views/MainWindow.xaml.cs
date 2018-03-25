using GraphExpert.Wpf.Models;
using GraphExpert.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace GraphExpert.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private StopVM _arret1;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindowViewModel Modele => DataContext as MainWindowViewModel;

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Canvas))
            {
                var position = e.GetPosition(e.Source as IInputElement);

                Modele.AjouterArret(position.X - 15, position.Y - 15, string.Empty);
            }
            else if (_arret1 == null)
            {
                _arret1 = ObtenirStop(e.OriginalSource);
            }
            else
            {
                var arret2 = ObtenirStop(e.OriginalSource);

                // Faire la liaison.
                if (Modele.AjouterLiaison(_arret1, arret2))
                {
                    _arret1 = null;
                }
            }
        }

        private StopVM ObtenirStop(object element)
        {
            return (element as FrameworkElement).DataContext as StopVM;
        }
    }
}
