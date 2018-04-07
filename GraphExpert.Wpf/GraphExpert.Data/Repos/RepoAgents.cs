using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Modeles;
using System.Collections.Generic;
using System.Linq;

namespace GraphExpert.Data.Repos
{
    /// <summary>
    /// Repository des agents.
    /// </summary>
    public class RepoAgents : IRepoAgents
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IList<IAgent> _agents = new List<IAgent>();

        /// <summary>
        /// Sélections de couleur.
        /// </summary>
        private string[] _couleurs = new string[]
        {
            "#054ABA",
            "#24CE1E",
            "#E9F418",
            "#FF7200",
            "#FF0000",
            "#00A9FF"
        };

        /// <summary>
        /// Sélecteur.
        /// </summary>
        private int _indexCouleurs = 0;

        /// <summary>
        /// Ajoute un nouvel agent.
        /// </summary>
        /// <param name="noeudId">N° du noeud attaché.</param>
        /// <param name="etiquette">Étiquette.</param>
        /// <returns>Le nouvel agent.</returns>
        public IAgent Ajouter(byte noeudId, string etiquette)
        {
            if (!((_indexCouleurs >= 0) && (_indexCouleurs < _couleurs.Length)))
            {
                return null;
            }

            var id = (byte)(_agents.Any() ? (_agents.Max(k => k.Id) + 1) : 1);
            var couleur = _couleurs[_indexCouleurs++];
            var agent = new Agent(id, noeudId, etiquette, couleur);

            _agents.Add(agent);

            return agent;
        }

        /// <summary>
        /// Obtenir tous les agents.
        /// </summary>
        /// <returns>Agents</returns>
        public IEnumerable<IAgent> Obtenir() => _agents;

        /// <summary>
        /// Obtenir l'agent demandé.
        /// </summary>
        /// <param name="id">N° de l'agent.</param>
        /// <returns>Agent.</returns>
        public IAgent Obtenir(byte id) => _agents.SingleOrDefault(p => p.Id == id);

        /// <summary>
        /// Retire l'agent en question.
        /// </summary>
        /// <param name="id">Identifiant de l'agent.</param>
        public void Supprimer(byte id)
        {
            _agents.Remove(_agents.Single(p => p.Id == id));
        }

        /// <summary>
        /// Vider les objets persistés.
        /// </summary>
        public void Vider()
        {
            _agents.Clear();
            _indexCouleurs = 0;
        }
    }
}
