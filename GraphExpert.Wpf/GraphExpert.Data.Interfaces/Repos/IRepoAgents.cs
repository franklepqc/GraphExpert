using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAgents
    {
        IEnumerable<IAgent> Obtenir();

        IAgent Ajouter(byte noeudId);

        void Supprimer(byte id);

        void Vider();
    }
}
