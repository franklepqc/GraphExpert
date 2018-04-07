using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public byte Executer(Agent agent, IEnumerable<StopVM> noeuds, byte noeudId, byte portId)
        {
            // Variables de travail.
            var storyBoard1 = new Storyboard();
            var storyBoard2 = new Storyboard();
            var noeudDest = ObtenirNoeud(noeudId, portId, noeuds);            

            var duree = TimeSpan.FromSeconds(1d);
            var lineaireXDebut = new LinearDoubleKeyFrame(agent.Left, KeyTime.FromTimeSpan(TimeSpan.Zero));
            var lineaireXFin = new LinearDoubleKeyFrame(noeudDest.X, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
            var lineaireYDebut = new LinearDoubleKeyFrame(agent.Top, KeyTime.FromTimeSpan(TimeSpan.Zero));
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
            Storyboard.SetTarget(storyBoard1, agent);
            Storyboard.SetTargetProperty(storyBoard1, new System.Windows.PropertyPath(Agent.LeftProperty.Name));
            Storyboard.SetTarget(storyBoard2, agent);
            Storyboard.SetTargetProperty(storyBoard2, new System.Windows.PropertyPath(Agent.TopProperty.Name));

            // Démarrer l'animation.
            storyBoard1.Begin();
            storyBoard2.Begin();

            return noeudDest.Id;
        }

        /// <summary>
        /// Obtenir le noeud (visuel) pour l'animation.
        /// </summary>
        /// <param name="noeudId">N° du noeud de départ.</param>
        /// <param name="portId">N° du pord de départ.</param>
        /// <param name="noeuds">Noeuds (visuel).</param>
        /// <returns>Noeud d'arrivée.</returns>
        private StopVM ObtenirNoeud(byte noeudId, byte portId, IEnumerable<StopVM> noeuds)
        {
            var arete = _repoAretes.Obtenir().SingleOrDefault(p => p.NoeudIdDepart == noeudId && p.PortIdDepart == portId);
            return noeuds.SingleOrDefault(p => p.Id == arete.NoeudIdArrivee);
        }
    }
}
