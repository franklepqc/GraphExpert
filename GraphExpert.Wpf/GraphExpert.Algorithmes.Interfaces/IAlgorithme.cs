using GraphExpert.Animations;
using System.Collections.Generic;

namespace GraphExpert.Algorithmes.Interfaces
{
    public interface IAlgorithme
    {
        IEnumerable<IDeplacement> Resoudre();
    }
}
