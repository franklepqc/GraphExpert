using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAgents
    {
        IEnumerable<IAgent> Obtenir();

        IAgent Ajouter(int noeudId);

        void Supprimer(int id);

        void Vider();
    }
}
