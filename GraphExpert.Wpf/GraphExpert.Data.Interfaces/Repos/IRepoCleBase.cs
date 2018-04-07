namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoCleBase<TEntite, TCle> : IRepoBase<TEntite>
    {
        TEntite Obtenir(TCle id);

        void Supprimer(TCle id);        
    }
}