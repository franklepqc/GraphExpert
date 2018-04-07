using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAgents : IRepoCleBase<IAgent, byte>
    {
        IAgent Ajouter(byte noeudId, string etiquette);
    }
}
