﻿using GraphExpert.Algorithmes.Interfaces;
using GraphExpert.Animations;
using System.Collections.ObjectModel;

namespace GraphExpert.Algorithmes
{
    /// <summary>
    /// Effectue la résolution selon le type d'algorithme utilisé.
    /// </summary>
    public class Resolveur : IResolveur
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IStrategieAlgorithme _strategie;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        /// <param name="strategie">Initialise la stratégie à adopter.</param>
        public Resolveur(IStrategieAlgorithme strategie)
        {
            _strategie = strategie;
        }

        /// <summary>
        /// Résoudre.
        /// </summary>
        /// <param name="type">Type algorithme.</param>
        /// <param name="matrice">Matrice des poids selon les arrêts et leurs liaisons.</param>
        public void Resoudre(TypeAlogorithmeEnum type, ObservableCollection<IDeplacement> deplacements)
        {
            _strategie.Obtenir(type).Resoudre(deplacements);
        }
    }
}
