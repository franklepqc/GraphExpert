using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Wpf.Interfaces;
using System.Collections.ObjectModel;

namespace GraphExpert.Wpf.Models
{
    public class StopVM : IPositionCanvas
    {
        #region Fields

        /// <summary>
        /// Conteneurs pour les propriétés.
        /// </summary>
        private readonly INoeud _noeud;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="x">Position X.</param>
        /// <param name="y">Position Y.</param>
        /// <param name="noeud">Noeud.</param>
        public StopVM(double x, double y, INoeud noeud)
        {
            X = x;
            Y = y;
            _noeud = noeud;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// N° du noeud.
        /// </summary>
        public byte Id => _noeud.Id;

        /// <summary>
        /// Jetons à afficher.
        /// </summary>
        public ObservableCollection<IJeton> Jetons => _noeud.Jetons;

        /// <summary>
        /// Position X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Position Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Indice de superposition.
        /// </summary>
        public int ZIndex => 1;

        #endregion Properties
    }
}
