﻿using GraphExpert.Data.Interfaces.Modeles;
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
        /// Ajoute un nouvel agent.
        /// </summary>
        /// <param name="noeudId">N° du noeud attaché.</param>
        /// <returns>Le nouvel agent.</returns>
        public IAgent Ajouter(int noeudId)
        {
            var agent = new Agent((_agents.Any() ? (_agents.Max(k => k.Id) + 1) : 1), noeudId);

            _agents.Add(agent);

            return agent;
        }

        /// <summary>
        /// Obtenir tous les agents.
        /// </summary>
        /// <returns>Agents</returns>
        public IEnumerable<IAgent> Obtenir() => _agents;

        /// <summary>
        /// Retire l'agent en question.
        /// </summary>
        /// <param name="id">Identifiant de l'agent.</param>
        public void Supprimer(int id)
        {
            _agents.Remove(_agents.Single(p => p.Id == id));
        }

        /// <summary>
        /// Vider les objets persistés.
        /// </summary>
        public void Vider()
        {
            _agents.Clear();
        }
    }
}