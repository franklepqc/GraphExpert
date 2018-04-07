using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Interfaces;
using GraphExpert.Wpf.Models;
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
        /// <param name="deplacements">Déplacements à effectuer.</param>
        void Animer(IEnumerable<IDeplacement> deplacements);
    }
}