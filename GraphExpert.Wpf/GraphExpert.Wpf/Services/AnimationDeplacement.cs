using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Interfaces;
using GraphExpert.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GraphExpert.Wpf.Services
{
    public class AnimationDeplacement : IAnimationDeplacement
    {
        /// <summary>
        /// Conteneurs.
        /// </summary>
        private IRepoNoeuds _repoNoeuds;
        private IRepoPorts _repoPorts;
        private IRepoAretes _repoAretes;
        private IRepoAgents _repoAgents;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="repoNoeuds">Repository des noeuds.</param>
        /// <param name="repoPorts">Repository des ports.</param>
        /// <param name="repoAretes">Repository des arêtes.</param>
        /// <param name="repoAgents">Repository des agents.</param>
        public AnimationDeplacement(IRepoNoeuds repoNoeuds, IRepoPorts repoPorts, IRepoAretes repoAretes, IRepoAgents repoAgents)
        {
            _repoNoeuds = repoNoeuds;
            _repoPorts = repoPorts;
            _repoAretes = repoAretes;
            _repoAgents = repoAgents;
        }

        /// <summary>
        /// Permet d'animer avec les déplacements passés en paramètre.
        /// </summary>
        /// <param name="deplacements">Déplacements à effectuer.</param>
        public void Animer(IEnumerable<IDeplacement> deplacements)
        {
            foreach (var deplacement in deplacements)
            {
                Executer(null, deplacement.AgentId, deplacement.PortId, null);
            }
        }

        /// <summary>
        /// Construire l'animation pour nous.
        /// </summary>
        /// <param name="noeudId">N° du noeud.</param>
        /// <param name="portId">N° du port.</param>
        /// <returns></returns>
        public void Executer(ItemsControl controleListe, byte agentId, byte portId, IEnumerable<IPositionCanvas> noeuds)
        {
            // Variables de travail.
            var storyBoard1 = new Storyboard();
            var storyBoard2 = new Storyboard();
            var agentVM = noeuds.OfType<AgentVM>().SingleOrDefault(p => p.Id == agentId);
            var controleAgent = VisualTreeHelper.GetChild(controleListe.ItemContainerGenerator.ContainerFromItem(agentVM), 0) as Agent;
            var noeudDest = ObtenirNoeud(agentId, portId, noeuds.OfType<StopVM>());            

            var duree = TimeSpan.FromSeconds(1d);
            var lineaireXDebut = new LinearDoubleKeyFrame(controleAgent.Left, KeyTime.FromTimeSpan(TimeSpan.Zero));
            var lineaireXFin = new LinearDoubleKeyFrame(noeudDest.X, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
            var lineaireYDebut = new LinearDoubleKeyFrame(controleAgent.Top, KeyTime.FromTimeSpan(TimeSpan.Zero));
            var lineaireYFin = new LinearDoubleKeyFrame(noeudDest.Y, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
            var collection1 = new DoubleAnimationUsingKeyFrames();
            var collection2 = new DoubleAnimationUsingKeyFrames();

            collection1.KeyFrames.Add(lineaireXDebut);
            collection2.KeyFrames.Add(lineaireYDebut);
            collection1.KeyFrames.Add(lineaireXFin);
            collection2.KeyFrames.Add(lineaireYFin);

            // Construire l'animation avec le point de départ et le point d'arrivée.
            storyBoard1.Children
                .Add(collection1);

            storyBoard2.Children
                .Add(collection2);

            // Assigner la cible de l'animation.
            Storyboard.SetTarget(storyBoard1, controleAgent);
            Storyboard.SetTargetProperty(storyBoard1, new System.Windows.PropertyPath(Agent.LeftProperty.Name));
            Storyboard.SetTarget(storyBoard2, controleAgent);
            Storyboard.SetTargetProperty(storyBoard2, new System.Windows.PropertyPath(Agent.TopProperty.Name));

            // Démarrer l'animation.
            storyBoard1.Begin();
            storyBoard2.Begin();

            // Changer le noeud de l'agent.
            agentVM.NoeudId = noeudDest.Id;
            _repoAgents.Obtenir(agentId).NoeudId = noeudDest.Id;
        }

        /// <summary>
        /// Obtenir le noeud (visuel) pour l'animation.
        /// </summary>
        /// <param name="agentId">N° de l'agent de départ.</param>
        /// <param name="portId">N° du pord de départ.</param>
        /// <param name="noeuds">Noeuds (visuel).</param>
        /// <returns>Noeud d'arrivée.</returns>
        private StopVM ObtenirNoeud(byte agentId, byte portId, IEnumerable<StopVM> noeuds)
        {
            var noeudIdDepart = _repoAgents.Obtenir(agentId).NoeudId;
            var arete = _repoAretes.Obtenir(noeudIdDepart, portId);
            return noeuds.SingleOrDefault(p => p.Id == arete.NoeudIdArrivee);
        }
    }
}
