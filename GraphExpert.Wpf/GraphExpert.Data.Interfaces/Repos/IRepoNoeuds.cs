using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoNoeuds : IRepoCleBase<INoeud, byte>
    {
        INoeud Ajouter(string etiquette);
    }
}
