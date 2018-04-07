using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoBase<TEntite>
    {
        IEnumerable<TEntite> Obtenir();

        void Vider();
    }
}
