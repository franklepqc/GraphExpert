using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Modeles;
using System.Collections.Generic;
using System.Linq;

namespace GraphExpert.Data.Repos
{
    /// <summary>
    /// Repository des arrêts.
    /// </summary>
    public class RepoNoeuds : IRepoNoeuds
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IList<INoeud> _arrets = new List<INoeud>();

        /// <summary>
        /// Ajoute un nouvel arrêt.
        /// </summary>
        /// <param name="etiquette">Étiquette.</param>
        /// <returns>Le nouvel arrêt.</returns>
        public INoeud Ajouter(string etiquette)
        {
            var arret = new Noeud((_arrets.Any() ? (byte)(_arrets.Max(k => k.Id) + 1) : (byte)1), etiquette);

            _arrets.Add(arret);

            return arret;
        }

        /// <summary>
        /// Obtenir tous les arrêts.
        /// </summary>
        /// <returns>Arrêts</returns>
        public IEnumerable<INoeud> Obtenir() => _arrets;

        /// <summary>
        /// Retire l'arrêt en question.
        /// </summary>
        /// <param name="id">Identifiant de l'arrêt.</param>
        public void Supprimer(byte id)
        {
            _arrets.Remove(_arrets.Single(p => p.Id == id));
        }

        /// <summary>
        /// Vider les objets persistés.
        /// </summary>
        public void Vider()
        {
            _arrets.Clear();
        }
    }
}
