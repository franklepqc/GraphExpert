using GraphExpert.Wpf.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;

namespace GraphExpert.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Injection de dépendances.
            //Container.RegisterType<IUtilisateurRepo, Repos.UtilisateurRepo>();
        }

        protected override void InitializeModules()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
