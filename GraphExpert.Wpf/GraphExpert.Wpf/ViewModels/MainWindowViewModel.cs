﻿using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Data.Interfaces;
using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Models;
using GraphExpert.Wpf.Services;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        /// Agent sélectionné.
        /// </summary>
        private byte _agentId;

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
        /// Port sélectionné.
        /// </summary>
        private IPort _port;

        private IRepoAgents _repoAgents;

        /// <summary>
        /// Repos.
        /// </summary>
        private IRepoNoeuds _repoArrets;
        private IRepoAretes _repoLiaisons;
        private IRepoPorts _repoPorts;

        /// <summary>
        /// Résolveur.
        /// </summary>
        private IResolveur _resolveur;

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
            CommandeDeplacer = new DelegateCommand<ItemsControl>(DeplacerExecuter, PeutDeplacer);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// N° de l'agent sélectionné.
        /// </summary>
        public byte AgentId
        {
            get { return _agentId; }
            set { SetProperty(ref _agentId, value, () => PopulerPorts(value)); }
        }

        /// <summary>
        /// Populer la liste des ports.
        /// </summary>
        /// <param name="agentId"></param>
        private void PopulerPorts(byte agentId)
        {
            Ports.Clear();

            var agent = _repoAgents.Obtenir().Single(p => p.Id == agentId);
            Ports.AddRange(_repoPorts.Obtenir().Where(p => p.NoeudId == agent.NoeudId));
        }

        /// <summary>
        /// Agents.
        /// </summary>
        public ObservableCollection<IAgent> Agents { get; private set; } = new ObservableCollection<IAgent>();

        /// <summary>
        /// Commande déplacer.
        /// </summary>
        public DelegateCommand<ItemsControl> CommandeDeplacer { get; private set; }

        /// <summary>
        /// Menu contextuel 'Nettoyer'.
        /// </summary>
        public ICommand CommandeNettoyer { get; private set; }

        /// <summary>
        /// Bouton 'résoudre'.
        /// </summary>
        public ICommand CommandeResoudre { get; private set; }

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
        public ObservableCollection<object> Formes { get; private set; } = new ObservableCollection<object>();

        /// <summary>
        /// N° du port sélectionné.
        /// </summary>
        public IPort Port
        {
            get { return _port; }
            set { SetProperty(ref _port, value, () => CommandeDeplacer.RaiseCanExecuteChanged()); }
        }

        /// <summary>
        /// Ports.
        /// </summary>
        public ObservableCollection<IPort> Ports { get; private set; } = new ObservableCollection<IPort>();

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

                // Ajouter les deux liaisons dans les deux sens.
                var portDepa = _repoPorts.Ajouter(_arret1.Id);
                var portArri = _repoPorts.Ajouter(arret.Id);
                _repoLiaisons.Ajouter(portDepa.NoeudId, portDepa.Id, portArri.NoeudId, portArri.Id);
                _repoLiaisons.Ajouter(portArri.NoeudId, portArri.Id, portDepa.NoeudId, portDepa.Id);

                // Retirer l'arrêt.
                _arret1 = null;

                // Repopuler la liste des ports.
                PopulerPorts(AgentId);
            }
        }

        /// <summary>
        /// Effectuer le déplacement.
        /// </summary>
        public void DeplacerExecuter(ItemsControl controleListe)
        {
            // Récupérer l'agent affecté.
            var agent = _repoAgents.Obtenir(AgentId);
            var agentVM = Formes.OfType<AgentVM>().Single(p => p.Id == AgentId);

            // Effectuer l'animation.
            var conteneur = controleListe.ItemContainerGenerator.ContainerFromItem(agentVM);
            var noeudIdDest = _animation.Executer(VisualTreeHelper.GetChild(conteneur, 0) as Agent, Formes.OfType<StopVM>(), Port.NoeudId, Port.Id);

            // Déplacer.
            agent.NoeudId = noeudIdDest;
            agentVM.NoeudId = noeudIdDest;
            PopulerPorts(AgentId);
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

            // Vider les choix.
            _agentId = 0;
            _port = null;

            // Vider l'IU.
            Formes.Clear();
            Agents.Clear();
            Ports.Clear();
            CommandeDeplacer.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Détermine si on peut déplacer.
        /// </summary>
        public bool PeutDeplacer(ItemsControl controleListe)
        {
            return (_agentId != 0 && _port != null);
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

            // Afficher.
            Agents.Add(agent);
            Formes.Add(new AgentVM(x, y, agent.Id, noeudId, (Color)ColorConverter.ConvertFromString(agent.Couleur)));
        }

        #endregion Methods
    }
}
