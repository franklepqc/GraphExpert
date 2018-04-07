using GraphExpert.Wpf.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;
using GraphExpert.Animations;

namespace GraphExpert.Wpf.Services
{
    public interface IAnimationDeplacement
    {
        /// <summary>
        /// Effectuer une animation.
        /// </summary>
        /// <param name="controleListe">Contrôle pour la liste des éléments.</param>
        /// <param name="noeuds">Noeuds.</param>
        /// <param name="deplacements">Déplacements à effectuer.</param>
        void Animer(ItemsControl controleListe, IEnumerable<IPositionCanvas> noeuds, params IDeplacement[] deplacements);
    }
}