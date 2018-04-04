using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAretes
    {
        IArete Ajouter(byte portIdDepart, byte portIdArrivee);

        void Supprimer(byte portIdDepart, byte portIdArrivee);

        void Vider();

        IEnumerable<IArete> Obtenir();
    }
}
