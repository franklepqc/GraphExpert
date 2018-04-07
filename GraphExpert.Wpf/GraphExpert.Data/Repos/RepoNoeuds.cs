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
        private IList<INoeud> _noeuds = new List<INoeud>();

        /// <summary>
        /// Ajoute un nouvel arrêt.
        /// </summary>
        /// <param name="etiquette">Étiquette.</param>
        /// <returns>Le nouvel arrêt.</returns>
        public INoeud Ajouter(string etiquette)
        {
            var arret = new Noeud((_noeuds.Any() ? (byte)(_noeuds.Max(k => k.Id) + 1) : (byte)1), etiquette);

            _noeuds.Add(arret);

            return arret;
        }

        /// <summary>
        /// Obtenir tous les arrêts.
        /// </summary>
        /// <returns>Arrêts</returns>
        public IEnumerable<INoeud> Obtenir() => _noeuds;

        /// <summary>
        /// Obtenir le noeud demandé.
        /// </summary>
        /// <param name="id">N° du noeud.</param>
        /// <returns>Noeud.</returns>
        public INoeud Obtenir(byte id) => _noeuds.SingleOrDefault(p => p.Id == id);

        /// <summary>
        /// Retire l'arrêt en question.
        /// </summary>
        /// <param name="id">Identifiant de l'arrêt.</param>
        public void Supprimer(byte id)
        {
            _noeuds.Remove(_noeuds.Single(p => p.Id == id));
        }

        /// <summary>
        /// Vider les objets persistés.
        /// </summary>
        public void Vider()
        {
            _noeuds.Clear();
        }
    }
}
