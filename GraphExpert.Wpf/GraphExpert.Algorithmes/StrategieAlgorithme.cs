using GraphExpert.Algorithmes.Interfaces;
using System;

namespace GraphExpert.Algorithmes
{
    /// <summary>
    /// Strategy Pattern : sélection de l'algorithme à utiliser.
    /// </summary>
    public class StrategieAlgorithme : IStrategieAlgorithme
    {
        /// <summary>
        /// Algorithmes.
        /// </summary>
        private IAlgorithmeFW _algoFW;
        private IAlgorithmeDFS _algoDfs;
        private IAlgorithmeBFS _algoBfs;

        /// <summary>
        /// Constructeur par défaut.
        /// Instanciation des algorithmes.
        /// </summary>
        /// <param name="algoFW">Algorithme Floyd-Warshall.</param>
        /// <param name="algoDfs">Algorithme DFS.</param>
        /// <param name="algoBfs">Algorithme BFS.</param>
        public StrategieAlgorithme(IAlgorithmeFW algoFW, IAlgorithmeDFS algoDfs, IAlgorithmeBFS algoBfs)
        {
            _algoFW = algoFW;
            _algoDfs = algoDfs;
            _algoBfs = algoBfs;
        }

        /// <summary>
        /// Obtenir l'instance de son choix.
        /// </summary>
        /// <param name="type">Type désiré.</param>
        /// <returns>Algo.</returns>
        public IAlgorithme Obtenir(TypeAlogorithmeEnum type)
        {
            switch (type)
            {
                case TypeAlogorithmeEnum.FloydWarshall:
                    return _algoFW;

                case TypeAlogorithmeEnum.DFS:
                    return _algoDfs;

                case TypeAlogorithmeEnum.BFS:
                    return _algoBfs;
            }

            throw new NotImplementedException();
        }
    }
}
