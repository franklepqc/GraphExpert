using GraphExpert.Algorithmes;
using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Data;
using GraphExpert.Data.Interfaces;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Repos;
using GraphExpert.Wpf.Services;
using GraphExpert.Wpf.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;

namespace GraphExpert.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        private static IRepoAgents _repoAgents = new RepoAgents();
        private static IRepoNoeuds _repoNoeuds = new RepoNoeuds();
        private static IRepoPorts _repoPorts = new RepoPorts();
        private static IRepoAretes _repoAretes = new RepoAretes();

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Injection de dépendances.
            Container.RegisterInstance<IRepoNoeuds>(_repoNoeuds);
            Container.RegisterInstance<IRepoAretes>(_repoAretes);
            Container.RegisterInstance<IRepoAgents>(_repoAgents);
            Container.RegisterInstance<IRepoPorts>(_repoPorts);

            Container.RegisterType<IAlgorithmeFW, AlgorithmeFW>();
            Container.RegisterType<IAlgorithmeDFS, AlgorithmeDFS>();
            Container.RegisterType<IAlgorithmeBFS, AlgorithmeBFS>();
            Container.RegisterType<IStrategieAlgorithme, StrategieAlgorithme>();
            Container.RegisterType<IResolveur, Resolveur>();
            Container.RegisterType<IAnimationDeplacement, AnimationDeplacement>();
        }

        protected override void InitializeModules()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
