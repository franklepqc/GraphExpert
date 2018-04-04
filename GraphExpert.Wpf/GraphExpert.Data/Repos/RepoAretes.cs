using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Interfaces.Repos;
using GraphExpert.Data.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExpert.Data.Repos
{
    /// <summary>
    /// Repository des liaisons.
    /// </summary>
    public class RepoAretes : IRepoAretes
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IList<IArete> _liaisons = new List<IArete>();

        /// <summary>
        /// Ajoute une nouvelle liaison.
        /// </summary>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <param name="portIdArrivee">Point d'arrivée.</param>
        /// <returns>Nouvelle liaison.</returns>
        public IArete Ajouter(byte portIdDepart, byte portIdArrivee)
        {
            var liaison = Obtenir(portIdDepart, portIdArrivee);

            if (null == liaison)
            {
                liaison = new Arete(portIdDepart, portIdArrivee);

                _liaisons.Add(liaison);
            }

            return liaison;
        }

        /// <summary>
        /// Supprime la liaison.
        /// </summary>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <param name="portIdArrivee">Point d'arrivée.</param>
        public void Supprimer(byte portIdDepart, byte portIdArrivee)
        {
            var liaison = Obtenir(portIdDepart, portIdArrivee);

            if (null == liaison) throw new Exception();

            _liaisons.Remove(liaison);
        }

        /// <summary>
        /// Obtenir une liaison.
        /// </summary>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <param name="portIdArrivee">Point d'arrivée.</param>
        /// <returns>Liaison unique.</returns>
        private IArete Obtenir(byte idDepart, byte idArrivee) => _liaisons.SingleOrDefault(k => k.PortIdDepart == idDepart && k.PortIdArrivee == idArrivee);

        /// <summary>
        /// Vider les objets persistés.
        /// </summary>
        public void Vider()
        {
            _liaisons.Clear();
        }

        /// <summary>
        /// Obtenir toutes les liaisons.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IArete> Obtenir() => _liaisons;
    }
}
