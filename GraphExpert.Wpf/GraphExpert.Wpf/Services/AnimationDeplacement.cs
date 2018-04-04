using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="repoNoeuds">Repository des noeuds.</param>
        /// <param name="repoPorts">Repository des ports.</param>
        /// <param name="repoAretes">Repository des arêtes.</param>
        public AnimationDeplacement(IRepoNoeuds repoNoeuds, IRepoPorts repoPorts, IRepoAretes repoAretes)
        {
            _repoNoeuds = repoNoeuds;
            _repoPorts = repoPorts;
            _repoAretes = repoAretes;
        }

        /// <summary>
        /// Construire l'animation pour nous.
        /// </summary>
        /// <param name="noeudId">N° du noeud.</param>
        /// <param name="portId">N° du port.</param>
        /// <returns></returns>
        public void Executer(Agent agent, IEnumerable<StopVM> noeuds, byte noeudId, byte portId)
        {
            // Variables de travail.
            var storyBoard1 = new Storyboard();
            var storyBoard2 = new Storyboard();
            var arete = _repoAretes.Obtenir().SingleOrDefault(p => p.PortIdDepart == portId);
            var portDest = _repoPorts.Obtenir().SingleOrDefault(p => p.Id == arete.PortIdArrivee && p.NoeudId != noeudId);
            var noeudDest = _repoNoeuds.Obtenir().SingleOrDefault(p => p.Id == portDest.NoeudId);
            var noeudVMDest = noeuds.SingleOrDefault(p => p.Id == noeudDest.Id);

            ((AgentVM)agent.DataContext).NoeudId = noeudDest.Id;

            var duree = TimeSpan.FromSeconds(1d);
            var lineaireXDebut = new LinearDoubleKeyFrame(agent.Left, KeyTime.FromTimeSpan(TimeSpan.Zero));
            var lineaireXFin = new LinearDoubleKeyFrame(noeudVMDest.X, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
            var lineaireYDebut = new LinearDoubleKeyFrame(agent.Top, KeyTime.FromTimeSpan(TimeSpan.Zero));
            var lineaireYFin = new LinearDoubleKeyFrame(noeudVMDest.Y, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
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
            Storyboard.SetTarget(storyBoard1, agent);
            Storyboard.SetTargetProperty(storyBoard1, new System.Windows.PropertyPath(Agent.LeftProperty.Name));
            Storyboard.SetTarget(storyBoard2, agent);
            Storyboard.SetTargetProperty(storyBoard2, new System.Windows.PropertyPath(Agent.TopProperty.Name));

            // Démarrer l'animation.
            storyBoard1.Begin();
            storyBoard2.Begin();
        }
    }
}
