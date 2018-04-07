using GraphExpert.Wpf.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace GraphExpert.Wpf.Services
{
    public interface IAnimationDeplacement
    {
        /// <summary>
        /// Effectuer une animation.
        /// </summary>
        /// <param name="controleListe">Contrôle pour la liste des éléments.</param>
        /// <param name="agentId">Agent de départ.</param>
        /// <param name="portId">Port de départ.</param>
        /// <param name="noeuds">Noeuds.</param>
        /// <returns></returns>
        void Executer(ItemsControl controleListe, byte agentId, byte portId, IEnumerable<IPositionCanvas> noeuds);

        /// <summary>
        /// Permet d'animer avec les déplacements passés en paramètre.
        /// </summary>
        /// <param name="controleListe">Liste où en faire l'exécution.</param>
        /// <param name="noeuds">Noeuds.</param>
        /// <param name="deplacements">Déplacements à effectuer.</param>
        void Animer(ItemsControl controleListe, IEnumerable<IPositionCanvas> noeuds, IEnumerable<IDeplacement> deplacements);
    }
}