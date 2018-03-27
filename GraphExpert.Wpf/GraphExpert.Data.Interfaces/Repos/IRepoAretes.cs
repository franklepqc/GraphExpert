using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAretes
    {
        IArete Ajouter(byte idDepart, byte idArrivee);

        void AugmenterPoids(byte idDepart, byte idArrivee, int poids = 1);

        void Supprimer(byte idDepart, byte idArrivee);

        void Vider();

        IEnumerable<IArete> Obtenir();
    }
}
