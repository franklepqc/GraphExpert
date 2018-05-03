using GraphExpert.Algorithmes;
using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Repos;
using GraphExpert.Wpf.Services;
using GraphExpert.Wpf.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace GraphExpert.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private static IRepoAgents _repoAgents = new RepoAgents();
        private static IRepoNoeuds _repoNoeuds = new RepoNoeuds();
        private static IRepoPorts _repoPorts = new RepoPorts();
        private static IRepoAretes _repoAretes = new RepoAretes();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Injection de dépendances.
            containerRegistry.RegisterInstance<IRepoNoeuds>(_repoNoeuds);
            containerRegistry.RegisterInstance<IRepoAretes>(_repoAretes);
            containerRegistry.RegisterInstance<IRepoAgents>(_repoAgents);
            containerRegistry.RegisterInstance<IRepoPorts>(_repoPorts);

            containerRegistry.Register<IAlgorithmeFW, AlgorithmeFW>();
            containerRegistry.Register<IAlgorithmeDFS, AlgorithmeDFS>();
            containerRegistry.Register<IAlgorithmeBFS, AlgorithmeBFS>();
            containerRegistry.Register<IStrategieAlgorithme, StrategieAlgorithme>();
            containerRegistry.Register<IResolveur, Resolveur>();
            containerRegistry.Register<IAnimationDeplacement, AnimationDeplacement>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
