using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAretes
    {
        IArete Ajouter(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee);

        void Supprimer(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee);

        void Vider();

        IEnumerable<IArete> Obtenir();
    }
}
