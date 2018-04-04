using GraphExpert.Data.Interfaces.Repos;
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
        public Task Executer(AgentVM agent, IEnumerable<StopVM> noeuds, byte noeudId, byte portId)
        {
            return Task.Run(() =>
            {
                // Variables de travail.
                //var storyBoard1 = new Storyboard();
                //var storyBoard2 = new Storyboard();
                var arete = _repoAretes.Obtenir().SingleOrDefault(p => p.PortIdDepart == portId);
                var portDest = _repoPorts.Obtenir().SingleOrDefault(p => p.Id == arete.PortIdArrivee);
                var noeudDest = _repoNoeuds.Obtenir().SingleOrDefault(p => p.Id == portDest.NoeudId);
                var noeudVMDest = noeuds.SingleOrDefault(p => p.Id == noeudDest.Id);

                // Test...
                agent.X = noeudVMDest.X;
                agent.Y = noeudVMDest.Y;

                //var duree = TimeSpan.FromSeconds(1d);
                //var lineaireXDebut = new LinearDoubleKeyFrame(agent.X, KeyTime.FromTimeSpan(TimeSpan.Zero));
                //var lineaireXFin = new LinearDoubleKeyFrame(noeudVMDest.X, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
                //var lineaireYDebut = new LinearDoubleKeyFrame(agent.Y, KeyTime.FromTimeSpan(TimeSpan.Zero));
                //var lineaireYFin = new LinearDoubleKeyFrame(noeudVMDest.Y, KeyTime.FromTimeSpan(TimeSpan.Zero + duree));
                //var collection1 = new DoubleAnimationUsingKeyFrames();
                //var collection2 = new DoubleAnimationUsingKeyFrames();

                //collection1.KeyFrames.Add(lineaireXDebut);
                //collection2.KeyFrames.Add(lineaireYDebut);
                //collection1.KeyFrames.Add(lineaireXFin);
                //collection2.KeyFrames.Add(lineaireYFin);

                //// Construire l'animation avec le point de départ et le point d'arrivée.
                //storyBoard1.Children
                //    .Add(collection1);

                //storyBoard2.Children
                //    .Add(collection2);

                //// Assigner la cible de l'animation.
                ////storyBoard1.SetValue(Storyboard.TargetProperty, controleAgent);
                ////storyBoard1.SetValue(Storyboard.TargetPropertyProperty, Controles.Agent.LeftProperty.Name);
                ////storyBoard2.SetValue(Storyboard.TargetProperty, controleAgent);
                ////storyBoard2.SetValue(Storyboard.TargetPropertyProperty, Controles.Agent.TopProperty.Name);

                //// Démarrer l'animation.
                //storyBoard1.Begin();
                //storyBoard2.Begin();
            });
        }
    }
}
