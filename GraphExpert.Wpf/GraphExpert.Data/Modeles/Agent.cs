using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Agent : IAgent
    {
        public Agent(int id, int noeudId)
        {
            Id = id;
            NoeudId = noeudId;
        }

        public int Id { get; }

        public int NoeudId { get; set; }
    }
}
