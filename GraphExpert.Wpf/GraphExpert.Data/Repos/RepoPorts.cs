using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Modeles;
using System.Collections.Generic;
using System.Linq;

namespace GraphExpert.Data.Repos
{
    /// <summary>
    /// Repository des ports.
    /// </summary>
    public class RepoPorts : IRepoPorts
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IList<IPort> _ports = new List<IPort>();

        /// <summary>
        /// Ajoute un port au noeud.
        /// </summary>
        /// <param name="noeudId">N° du noeud attaché.</param>
        /// <returns>Port.</returns>
        public IPort Ajouter(byte noeudId)
        {
            var port = new Port((byte)(_ports.Any() ? (_ports.Max(k => k.Id) + 1) : 1), noeudId);

            _ports.Add(port);

            return port;
        }

        /// <summary>
        /// Obtenir tous les ports.
        /// </summary>
        /// <returns>Liste des ports.</returns>
        public IEnumerable<IPort> Obtenir() => _ports;

        /// <summary>
        /// Supprimer le port.
        /// </summary>
        /// <param name="id">N° du port.</param>
        /// <param name="noeudId">N° du noeud attaché.</param>
        public void Supprimer(byte id, byte noeudId)
        {
            _ports.Remove(_ports.Single(p => p.Id == id && p.NoeudId == noeudId));
        }

        /// <summary>
        /// Vider les objets persistés.
        /// </summary>
        public void Vider()
        {
            _ports.Clear();
        }
    }
}
