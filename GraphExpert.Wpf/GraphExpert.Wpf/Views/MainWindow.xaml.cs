using GraphExpert.Wpf.ViewModels;
using System.Windows;

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

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.GetPosition(e.Source as IInputElement);

            Modele.AjouterArret(position.X - 15, position.Y - 15);
        }
    }
}
