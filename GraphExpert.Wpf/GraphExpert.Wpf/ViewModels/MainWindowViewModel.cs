﻿using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Models;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        /// Arrêt n°1 cliqué.
        /// </summary>
        private StopVM _arret1;

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
        public MainWindowViewModel(IRepoArrets repoArrets, IRepoLiaisons repoLiaisons)
        {
            _repoArrets = repoArrets;
            _repoLiaisons = repoLiaisons;

            // Initialisation des commandes.
            CommandeResoudre = new DelegateCommand(() => { }, () => false);
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
    }
}
