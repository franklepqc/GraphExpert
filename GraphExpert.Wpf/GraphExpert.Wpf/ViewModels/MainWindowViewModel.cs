using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Data.Interfaces;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Models;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

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
        private IRepoNoeuds _repoArrets;
        private IRepoAretes _repoLiaisons;
        private IRepoAgents _repoAgents;

        /// <summary>
        /// Résolveur.
        /// </summary>
        private IResolveur _resolveur;

        /// <summary>
        /// Arrêt n°1 cliqué.
        /// </summary>
        private StopVM _arret1;

        /// <summary>
        /// Algorithme sélectionné.
        /// </summary>
        private TypeAlogorithmeEnum _algorithme = TypeAlogorithmeEnum.FloydWarshall;

        /// <summary>
        /// Sélection du choix.
        /// </summary>
        public bool EstAlgoFW
        {
            get { return _algorithme == TypeAlogorithmeEnum.FloydWarshall; }
            set
            {
                SetProperty(ref _algorithme, TypeAlogorithmeEnum.FloydWarshall, @"EstAlgoFW");
                RaisePropertyChanged(@"EstAlgoDFS");
                RaisePropertyChanged(@"EstAlgoBFS");
            }
        }

        /// <summary>
        /// Ajouter un agent.
        /// </summary>
        /// <param name="x">Coordonnée X à l'écran.</param>
        /// <param name="y">Coordonnée Y à l'écran.</param>
        /// <param name="noeudId">N° du noeud.</param>
        internal void AjouterAgent(double x, double y, int noeudId)
        {
            // Ajout dans la persistance.
            var agent = _repoAgents.Ajouter(noeudId);

            // Afficher.
            Formes.Add(new AgentVM(x, y, (Color)ColorConverter.ConvertFromString(agent.Couleur)));
        }

        /// <summary>
        /// Sélection du choix.
        /// </summary>
        public bool EstAlgoDFS
        {
            get { return _algorithme == TypeAlogorithmeEnum.DFS; }
            set
            {
                SetProperty(ref _algorithme, TypeAlogorithmeEnum.DFS, @"EstAlgoDFS");
                RaisePropertyChanged(@"EstAlgoFW");
                RaisePropertyChanged(@"EstAlgoBFS");
            }
        }

        /// <summary>
        /// Sélection du choix.
        /// </summary>
        public bool EstAlgoBFS
        {
            get { return _algorithme == TypeAlogorithmeEnum.BFS; }
            set
            {
                SetProperty(ref _algorithme, TypeAlogorithmeEnum.BFS, @"EstAlgoBFS");
                RaisePropertyChanged(@"EstAlgoDFS");
                RaisePropertyChanged(@"EstAlgoFW");
            }
        }

        /// <summary>
        /// Formes à afficher.
        /// </summary>
        public ObservableCollection<object> Formes { get; private set; } = new ObservableCollection<object>();

        /// <summary>
        /// Bouton 'résoudre'.
        /// </summary>
        public ICommand CommandeResoudre { get; private set; }

        /// <summary>
        /// Menu contextuel 'Nettoyer'.
        /// </summary>
        public ICommand CommandeNettoyer { get; private set; }

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="repoArrets">Repository des arrêts.</param>
        /// <param name="repoLiaisons">Repository des liaisons.</param>
        /// <param name="repoAgents">Repository des agents.</param>
        /// <param name="resolveur">Résoudre par l'algorithme voulu.</param>
        public MainWindowViewModel(IRepoNoeuds repoArrets, IRepoAretes repoLiaisons, IRepoAgents repoAgents, IResolveur resolveur)
        {
            _repoArrets = repoArrets;
            _repoLiaisons = repoLiaisons;
            _repoAgents = repoAgents;
            _resolveur = resolveur;

            // Initialisation des commandes.
            CommandeResoudre = new DelegateCommand(Resoudre, PeutResoudre);
            CommandeNettoyer = new DelegateCommand(Nettoyer);
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
        public void AjouterLiaison(StopVM arret)
        {
            // Il s'agit du premier clic.
            if (null == _arret1)
            {
                _arret1 = arret;
            }
            // Il s'agit du deuxième ; on crée la liaison.
            else
            {
                // Ajout aux éléments à afficher.
                Formes.Add(new LineVM(_arret1, arret));

                // Afficher.
                _repoLiaisons.Ajouter(_arret1.Id, arret.Id);

                // Retirer l'arrêt.
                _arret1 = null;
            }
        }

        /// <summary>
        /// Nettoyer le tout.
        /// </summary>
        public void Nettoyer()
        {
            // Vider les repos.
            _repoArrets.Vider();
            _repoLiaisons.Vider();

            // Vider l'instance en mémoire d'un arrêt cliqué.
            _arret1 = null;

            // Vider l'IU.
            Formes.Clear();
        }
        
        /// <summary>
        /// Résoudre par l'algorithme choisi.
        /// </summary>
        public bool PeutResoudre()
        {
            return true;
        }

        /// <summary>
        /// Résoudre par l'algorithme choisi.
        /// </summary>
        public void Resoudre()
        {
            _resolveur.Resoudre(_algorithme);
        }
    }
}
