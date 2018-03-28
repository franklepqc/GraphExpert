using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoPorts
    {
        IEnumerable<IPort> Obtenir();

        IPort Ajouter(int noeudId, int noeudIdDest);

        void Supprimer(int id);

        void Vider();
    }
}
