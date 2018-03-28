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
        /// <param name="noeudIdDest">N° du noeud de destination.</param>
        /// <returns>Port.</returns>
        public IPort Ajouter(int noeudId, int noeudIdDest)
        {
            var port = new Port((_ports.Any() ? (_ports.Max(k => k.Id) + 1) : 1), noeudId, noeudIdDest);

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
        public void Supprimer(int id)
        {
            _ports.Remove(_ports.Single(p => p.Id == id));
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
