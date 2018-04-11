using GraphExpert.Animations;
using System.Collections.ObjectModel;

namespace GraphExpert.Algorithmes.Interfaces
{
    public interface IResolveur
    {
        void Resoudre(TypeAlogorithmeEnum type, ObservableCollection<IDeplacement> deplacements);
    }
}
