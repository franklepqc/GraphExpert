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
        /// <param name="noeudIdDepart">N° du noeud de départ.</param>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <param name="noeudIdArrivee">N° du noeud d'arrivée.</param>
        /// <param name="portIdArrivee">Point d'arrivée.</param>
        /// <returns>Nouvelle liaison.</returns>
        public IArete Ajouter(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee)
        {
            var liaison = Obtenir(noeudIdDepart, portIdDepart, noeudIdArrivee, portIdArrivee);

            if (null == liaison)
            {
                liaison = new Arete(noeudIdDepart, portIdDepart, noeudIdArrivee, portIdArrivee);

                _liaisons.Add(liaison);
            }

            return liaison;
        }

        /// <summary>
        /// Supprime la liaison.
        /// </summary>
        /// <param name="noeudIdDepart">N° du noeud de départ.</param>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <param name="noeudIdArrivee">N° du noeud d'arrivée.</param>
        /// <param name="portIdArrivee">Point d'arrivée.</param>
        public void Supprimer(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee)
        {
            var liaison = Obtenir(noeudIdDepart, portIdDepart, noeudIdArrivee, portIdArrivee);

            if (null == liaison) throw new Exception();

            _liaisons.Remove(liaison);
        }

        /// <summary>
        /// Obtenir une liaison.
        /// </summary>
        /// <param name="noeudIdDepart">N° du noeud de départ.</param>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <param name="noeudIdArrivee">N° du noeud d'arrivée.</param>
        /// <param name="portIdArrivee">Point d'arrivée.</param>
        /// <returns>Liaison unique.</returns>
        private IArete Obtenir(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee) => 
            _liaisons.SingleOrDefault(k => k.NoeudIdDepart == noeudIdDepart && k.PortIdDepart == portIdDepart && k.NoeudIdArrivee == noeudIdArrivee && k.PortIdArrivee == portIdArrivee);

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

        /// <summary>
        /// Obtenir une liaison.
        /// </summary>
        /// <param name="noeudIdDepart">N° du noeud de départ.</param>
        /// <param name="portIdDepart">Point de départ.</param>
        /// <returns>Liaison unique.</returns>
        public IArete Obtenir(byte noeudIdDepart, byte portIdDepart) => _liaisons.SingleOrDefault(p => p.NoeudIdDepart == noeudIdDepart && p.PortIdDepart == portIdDepart);
    }
}
