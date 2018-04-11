using GraphExpert.Animations;
using System.Collections.ObjectModel;

namespace GraphExpert.Algorithmes.Interfaces
{
    public interface IAlgorithme
    {
        void Resoudre(ObservableCollection<IDeplacement> deplacements);
    }
}
