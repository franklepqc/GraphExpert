using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoPorts
    {
        IEnumerable<IPort> Obtenir();
    }
}
