using GraphExpert.Algorithmes;
using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Repos;
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
            Container.RegisterType<IRepoArrets, RepoArrets>();
            Container.RegisterType<IRepoLiaisons, RepoLiaisons>();

            Container.RegisterType<IAlgorithmeFW, AlgorithmeFW>();
            Container.RegisterType<IAlgorithmeDFS, AlgorithmeDFS>();
            Container.RegisterType<IAlgorithmeBFS, AlgorithmeBFS>();
            Container.RegisterType<IStrategieAlgorithme, StrategieAlgorithme>();
            Container.RegisterType<IResolveur, Resolveur>();
        }

        protected override void InitializeModules()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
