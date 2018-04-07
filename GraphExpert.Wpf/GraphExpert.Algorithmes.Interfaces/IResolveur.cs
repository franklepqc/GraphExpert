using GraphExpert.Animations;
using System.Collections.Generic;

namespace GraphExpert.Algorithmes.Interfaces
{
    public interface IResolveur
    {
        IEnumerable<IDeplacement> Resoudre(TypeAlogorithmeEnum type);
    }
}
