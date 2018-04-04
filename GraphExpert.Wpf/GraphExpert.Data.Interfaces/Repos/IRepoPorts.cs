using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoPorts
    {
        IEnumerable<IPort> Obtenir();

        IPort Ajouter(byte noeudId);

        void Supprimer(byte id, byte noeudId);

        void Vider();
    }
}
