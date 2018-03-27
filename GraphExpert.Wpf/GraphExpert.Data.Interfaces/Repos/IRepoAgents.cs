using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    interface IRepoAgents
    {
        IEnumerable<IAgent> Obtenir();
    }
}
