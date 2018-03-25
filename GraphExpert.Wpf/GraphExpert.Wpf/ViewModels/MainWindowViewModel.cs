using GraphExpert.Data.Interfaces.Repos;
using System.Collections.ObjectModel;

namespace GraphExpert.Wpf.ViewModels
{
    /// <summary>
    /// Selon la convention MVVM.
    /// Vue-modèle de l'écran d'accueil.
    /// </summary>
    public class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        /// <summary>
        /// Repos.
        /// </summary>
        private IRepoArrets _repoArrets;
        private IRepoLiaisons _repoLiaisons;

        /// <summary>
        /// Formes à afficher.
        /// </summary>
        public ObservableCollection<StopVM> Formes { get; private set; } = new ObservableCollection<StopVM>();

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="repoArrets">Repository des arrêts.</param>
        /// <param name="repoLiaisons">Repository des liaisons.</param>
        public MainWindowViewModel(IRepoArrets repoArrets, IRepoLiaisons repoLiaisons)
        {
            _repoArrets = repoArrets;
            _repoLiaisons = repoLiaisons;
        }

        /// <summary>
        /// Ajouter l'arrêt à la persistance et ensuite l'afficher.
        /// </summary>
        /// <param name="x">Coordonnée X à l'écran.</param>
        /// <param name="y">Coordonnée Y à l'écran.</param>
        /// <param name="etiquette">Étiquette à afficher.</param>
        public void AjouterArret(double x, double y, string etiquette)
        {
            // Ajout dans la persistance.
            var arret = _repoArrets.Ajouter(etiquette);

            // Afficher.
            Formes.Add(new StopVM(x, y, arret.Id));
        }

        /// <summary>
        /// Ajouter une liaison entre ces deux arrêts à l'écran.
        /// </summary>
        /// <param name="arret1">Arrêt de départ.</param>
        /// <param name="arret2">Arrêt d'arrivée.</param>
        /// <returns>Vrai si la liaison est effective.</returns>
        public bool AjouterLiaison(StopVM arret1, StopVM arret2)
        {
            _repoLiaisons.Ajouter(arret1.Id, arret2.Id);
            return true;
        }
    }
}
