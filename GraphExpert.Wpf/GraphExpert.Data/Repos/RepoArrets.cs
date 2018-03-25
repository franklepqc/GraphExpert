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
    public class RepoArrets : IRepoArrets
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IList<IArret> _arrets = new List<IArret>();

        /// <summary>
        /// Ajoute un nouvel arrêt.
        /// </summary>
        /// <param name="etiquette">Étiquette.</param>
        /// <returns>Le nouvel arrêt.</returns>
        public IArret Ajouter(string etiquette)
        {
            var arret = new Arret((_arrets.Any() ? (byte)(_arrets.Max(k => k.Id) + 1) : (byte)1), etiquette);

            _arrets.Add(arret);

            return arret;
        }

        /// <summary>
        /// Retire l'arrêt en question.
        /// </summary>
        /// <param name="id">Identifiant de l'arrêt.</param>
        public void Supprimer(byte id)
        {
            _arrets.Remove(_arrets.Single(p => p.Id == id));
        }
    }
}
