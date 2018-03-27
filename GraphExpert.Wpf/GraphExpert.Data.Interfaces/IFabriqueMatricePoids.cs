using GraphExpert.Data.Interfaces.Repos;

namespace GraphExpert.Data.Interfaces
{
    public interface IFabriqueMatricePoids
    {
        int[][] Obtenir(IRepoNoeuds repoArrets, IRepoAretes repoLiaisons);
    }
}
