using GraphExpert.Data.Interfaces.Repos;

namespace GraphExpert.Data.Interfaces
{
    public interface IFabriqueMatricePoids
    {
        int[][] Obtenir(IRepoArrets repoArrets, IRepoLiaisons repoLiaisons);
    }
}
