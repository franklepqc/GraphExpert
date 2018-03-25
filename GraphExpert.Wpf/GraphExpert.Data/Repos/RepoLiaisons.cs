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
    public class RepoLiaisons : IRepoLiaisons
    {
        /// <summary>
        /// Conteneur.
        /// </summary>
        private IList<ILiaison> _liaisons = new List<ILiaison>();

        /// <summary>
        /// Ajoute une nouvelle liaison.
        /// </summary>
        /// <param name="idDepart">Point de départ.</param>
        /// <param name="idArrivee">Point d'arrivée.</param>
        /// <returns>Nouvelle liaison.</returns>
        public ILiaison Ajouter(byte idDepart, byte idArrivee)
        {
            var liaison = Obtenir(idDepart, idArrivee);

            if (null == liaison)
            {
                liaison = new Liaison(idDepart, idArrivee, 1);

                _liaisons.Add(liaison);
            }
            else
            {
                AugmenterPoids(idDepart, idArrivee);
            }

            return liaison;
        }

        /// <summary>
        /// Augmente le poid par le nombre voulu.
        /// </summary>
        /// <param name="idDepart">Point de départ.</param>
        /// <param name="idArrivee">Point d'arrivée.</param>
        /// <param name="poids">Poids désiré.</param>
        public void AugmenterPoids(byte idDepart, byte idArrivee, int poids = 1)
        {
            var liaison = Obtenir(idDepart, idArrivee);

            if (null == liaison) throw new Exception();

            //liaison.Poids += poids;
        }

        /// <summary>
        /// Supprime la liaison.
        /// </summary>
        /// <param name="idDepart">Point de départ.</param>
        /// <param name="idArrivee">Point d'arrivée.</param>
        public void Supprimer(byte idDepart, byte idArrivee)
        {
            var liaison = Obtenir(idDepart, idArrivee);

            if (null == liaison) throw new Exception();

            _liaisons.Remove(liaison);
        }

        /// <summary>
        /// Obtenir une liaison.
        /// </summary>
        /// <param name="idDepart">Point de départ.</param>
        /// <param name="idArrivee">Point d'arrivée.</param>
        /// <returns>Liaison unique.</returns>
        private ILiaison Obtenir(byte idDepart, byte idArrivee) => _liaisons.SingleOrDefault(k => k.ArretIdDepart == idDepart && k.ArretIdArrivee == idArrivee);

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
        public IEnumerable<ILiaison> Obtenir() => _liaisons;
    }
}
