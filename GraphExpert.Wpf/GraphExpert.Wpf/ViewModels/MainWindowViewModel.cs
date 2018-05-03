using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Animations;
using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Interfaces;
using GraphExpert.Wpf.Models;
using GraphExpert.Wpf.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
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
        #region Fields

        /// <summary>
        /// Algorithme sélectionné.
        /// </summary>
        private TypeAlogorithmeEnum _algorithme = TypeAlogorithmeEnum.FloydWarshall;

        /// <summary>
        /// Service de déplacement.
        /// </summary>
        private IAnimationDeplacement _animation;

        /// <summary>
        /// Arrêt n°1 cliqué.
        /// </summary>
        private StopVM _arret1;

        /// <summary>
        /// Déplacements.
        /// </summary>
        private ObservableCollection<IDeplacement> _deplacements = new ObservableCollection<IDeplacement>();

        /// <summary>
        /// Contrôle des items.
        /// </summary>
        private ItemsControl _itemsControl;

        /// <summary>
        /// Repos.
        /// </summary>
        private IRepoAgents _repoAgents;
        private IRepoNoeuds _repoArrets;
        private IRepoAretes _repoLiaisons;
        private IRepoPorts _repoPorts;

        /// <summary>
        /// Résolveur.
        /// </summary>
        private IResolveur _resolveur;

        /// <summary>
        /// Conteneur pour la propriété.
        /// </summary>
        private string _solution = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="repoArrets">Repository des arrêts.</param>
        /// <param name="repoLiaisons">Repository des liaisons.</param>
        /// <param name="repoAgents">Repository des agents.</param>
        /// <param name="repoPorts">Repository des ports.</param>
        /// <param name="resolveur">Résoudre par l'algorithme voulu.</param>
        /// <param name="animation">Animation.</param>
        public MainWindowViewModel(IRepoNoeuds repoArrets, IRepoAretes repoLiaisons, IRepoAgents repoAgents, IRepoPorts repoPorts, IResolveur resolveur, IAnimationDeplacement animation)
        {
            _repoArrets = repoArrets;
            _repoLiaisons = repoLiaisons;
            _repoAgents = repoAgents;
            _repoPorts = repoPorts;
            _resolveur = resolveur;
            _animation = animation;

            // Initialisation des commandes.
            CommandeResoudre = new DelegateCommand(Resoudre, PeutResoudre);
            CommandeNettoyer = new DelegateCommand(Nettoyer);
            CommandeReinitialiser = new DelegateCommand(Reinitialiser, PeutReinitialiser);

            _deplacements.CollectionChanged += ListeDeplacements_CollectionChanged;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Disposer des évènements.
        /// </summary>
        ~MainWindowViewModel()
        {
            _deplacements.CollectionChanged -= ListeDeplacements_CollectionChanged;
            _deplacements = null;
        }

        #endregion Destructors

        #region Properties

        /// <summary>
        /// Obtient la liste des agents.
        /// </summary>
        public IEnumerable<AgentVM> Agents => Formes.OfType<AgentVM>();

        /// <summary>
        /// Menu contextuel 'Nettoyer'.
        /// </summary>
        public ICommand CommandeNettoyer { get; private set; }

        /// <summary>
        /// Commande pour réinitialiser les agents et les jetons.
        /// </summary>
        public DelegateCommand CommandeReinitialiser { get; private set; }

        /// <summary>
        /// Bouton 'résoudre'.
        /// </summary>
        public DelegateCommand CommandeResoudre { get; private set; }

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
        /// Formes à afficher.
        /// </summary>
        public ObservableCollection<IPositionCanvas> Formes { get; private set; } = new ObservableCollection<IPositionCanvas>();

        /// <summary>
        /// Obtient la liste de noeuds.
        /// </summary>
        public IEnumerable<StopVM> Noeuds => Formes.OfType<StopVM>();

        /// <summary>
        /// Solution à afficher.
        /// </summary>
        public string Solution
        {
            get => _solution;
            set
            {
                _solution = value;
                RaisePropertyChanged(@"Solution");
            }
        }

        #endregion Properties

        #region Methods

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
            Formes.Add(new StopVM(x, y, arret));
            RaisePropertyChanged(@"Noeuds");

            // Aviser l'interface pour résoudre.
            CommandeResoudre.RaiseCanExecuteChanged();
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
                var ligneUI = new LineVM(_arret1, arret, Colors.Black.ToString());
                Formes.Add(ligneUI);

                // Ajouter les deux liaisons dans les deux sens.
                var portDepa = _repoPorts.Ajouter(_arret1.Id);
                var portArri = _repoPorts.Ajouter(arret.Id);
                _repoLiaisons.Ajouter(portDepa.NoeudId, portDepa.Id, portArri.NoeudId, portArri.Id);
                _repoLiaisons.Ajouter(portArri.NoeudId, portArri.Id, portDepa.NoeudId, portDepa.Id);
                _repoArrets.Obtenir(_arret1.Id).Degree = _repoArrets.Obtenir(_arret1.Id).Degree + 1;
                _repoArrets.Obtenir(arret.Id).Degree = _repoArrets.Obtenir(arret.Id).Degree + 1;
                AjouterPortVM(_arret1, ligneUI, portDepa);
                AjouterPortVM(arret, ligneUI, portArri);

                // Retirer l'arrêt.
                _arret1 = null;

                // Aviser l'interface pour résoudre.
                CommandeResoudre.RaiseCanExecuteChanged();
                CommandeReinitialiser.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Assigner la valeur du contrôle (pour les déplacements).
        /// </summary>
        /// <param name="itemsControl">Contrôle.</param>
        public void AssignerItemsControl(ItemsControl itemsControl)
        {
            _itemsControl = itemsControl;
        }

        /// <summary>
        /// Nettoyer le tout.
        /// </summary>
        public void Nettoyer()
        {
            // Vider les repos.
            _repoArrets.Vider();
            _repoLiaisons.Vider();
            _repoPorts.Vider();
            _repoAgents.Vider();

            // Vider l'instance en mémoire d'un arrêt cliqué.
            _arret1 = null;

            // Vider l'IU.
            Formes.Clear();
            RaisePropertyChanged(@"Noeuds");
            RaisePropertyChanged(@"Agents");

            // Vider la partie "Solution".
            Solution = string.Empty;

            // Aviser l'interface pour rafraichir les commandes.
            CommandeResoudre.RaiseCanExecuteChanged();
            CommandeReinitialiser.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Résoudre par l'algorithme choisi.
        /// </summary>
        public bool PeutResoudre()
        {
            // Autorisation accordée quand l'interface possède :
            // 2 agents
            // 3 noeuds
            // 3 arêtes et
            // 6 ports.
            return ((2 <= Formes.OfType<AgentVM>()?.Count()) &&
                (3 <= Formes.OfType<StopVM>()?.Count()) &&
                (3 <= Formes.OfType<LineVM>()?.Count()) &&
                (6 <= Formes.OfType<PortVM>()?.Count()));
        }

        /// <summary>
        /// Résoudre par l'algorithme choisi.
        /// </summary>
        public bool PeutReinitialiser()
        {
            return ((true == Formes.OfType<AgentVM>()?.Any()) &&
                (true == Formes.OfType<StopVM>()?.Any()));
        }

        /// <summary>
        /// Résoudre par l'algorithme choisi.
        /// </summary>
        public void Reinitialiser()
        {
            for (int i = (Formes.Count - 1); i >= 0; i--)
            {
                if (Formes[i].GetType() == typeof(AgentVM))
                {
                    Formes.RemoveAt(i);
                }
                else if (Formes[i].GetType() == typeof(StopVM))
                {
                    ((StopVM)Formes.ElementAt(i)).Jetons?.Clear();
                }
            }

            // Vider les agents.
            _repoAgents.Vider();

            // Vider l'instance en mémoire d'un arrêt cliqué.
            _arret1 = null;

            // Vider la partie "Solution".
            Solution = string.Empty;

            // Réinitialiser les listes.
            RaisePropertyChanged(@"Noeuds");
            RaisePropertyChanged(@"Agents");
        }

        /// <summary>
        /// Résoudre par l'algorithme choisi.
        /// </summary>
        public void Resoudre()
        {
            // Remettre à zéro la solution.
            Solution = string.Empty;

            _resolveur.Resoudre(_algorithme, _deplacements);
        }

        /// <summary>
        /// Ajouter un agent.
        /// </summary>
        /// <param name="x">Coordonnée X à l'écran.</param>
        /// <param name="y">Coordonnée Y à l'écran.</param>
        /// <param name="noeudId">N° du noeud.</param>
        /// <param name="etiquette">Etiquette.</param>
        internal void AjouterAgent(double x, double y, byte noeudId, string etiquette)
        {
            // Ajout dans la persistance.
            var agent = _repoAgents.Ajouter(noeudId, etiquette);

            if (null != agent)
            {
                // Afficher.
                Formes.Add(new AgentVM(x, y, agent));
                RaisePropertyChanged(@"Agents");

                // Aviser l'interface pour rafraichir les commandes.
                CommandeResoudre.RaiseCanExecuteChanged();
                CommandeReinitialiser.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Ajoute le port au noeud demandé.
        /// </summary>
        /// <param name="noeud">Noeud.</param>
        /// <param name="arete">Arête.</param>
        /// <param name="port">¨Port.</param>
        private void AjouterPortVM(StopVM noeud, LineVM arete, IPort port)
        {
            // Positionner.
            double x = 0d, y = 0d;

            PositionnerLabelPort(noeud, arete, ref x, ref y);

            // Ajout du port.
            Formes.Add(new PortVM(port, x, y));

            // Aviser l'interface pour résoudre.
            CommandeResoudre.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Positionne l'étiquette (label) du port.
        /// </summary>
        /// <param name="noeud">Noeud de départ.</param>
        /// <param name="arete">Arête.</param>
        /// <param name="x">Position X.</param>
        /// <param name="y">Position Y.</param>
        private void PositionnerLabelPort(StopVM noeud, LineVM arete, ref double x, ref double y)
        {
            double D = 30; // diametre du cercle
            double
                nx = noeud.X + D / 2,
                ny = noeud.Y + D / 2;
            double Ax = nx, Ay = ny; // Position du centre du cercle courant

            double d1 = (arete.X - Ax) * (arete.X - Ax) + (arete.Y - Ay) * (arete.Y - Ay);
            double d2 = (arete.X2 - Ax) * (arete.X2 - Ax) + (arete.Y2 - Ay) * (arete.Y2 - Ay);

            double Bx, By; // position du de l'autre bout de la ligne

            if (d1 > d2)
            {
                // Arrete P1 est le plus différent de A
                Bx = arete.X;
                By = arete.Y;
            }
            else
            {
                // Arrete P2 est le plus différent de A
                Bx = arete.X2;
                By = arete.Y2;
            }

            double
                U = 10, // distance parallèle à a ligne du # (à partir de la circonférence)
                V = 10; // distance perpendiculaire à a ligne du #

            double
                Lx = Bx - Ax,
                Ly = By - Ay; // Description vecteur de la ligne a->b

            double LL = Math.Sqrt(Lx * Lx + Ly * Ly);

            double
                L1x = Lx / LL,
                L1y = Ly / LL; //Direction de L, normalisé

            double
                M1x = -L1y, // L1 avec rotation de -90 deg.
                M1y = L1x;

            double UR = U + D / 2;

            double
                LUx = L1x * UR,
                LUy = L1y * UR;

            double
                MVx = M1x * V,
                MVy = M1y * V;

            double charW = 6, charH = 8; // taille aprox. d'un charactere

            // Position du haut gauche du # de port
            x = Ax + LUx + MVx - charW / 2;
            y = Ay + LUy + MVy - charH / 2;
        }

        /// <summary>
        /// Sur changement de la collection, animer!
        /// </summary>
        /// <param name="sender">Objet envoyant l'évènement.</param>
        /// <param name="e">Arguments de notification.</param>
        private void ListeDeplacements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var deplacements = e.NewItems.OfType<IDeplacement>();

                _animation.Animer(_itemsControl, Formes, deplacements.ToArray());

                if (Solution.Length != 0) Solution += ", ";
                Solution += string.Join(", ", deplacements.Select(p => p.AgentId));
            }
        }

        #endregion Methods

    }
}