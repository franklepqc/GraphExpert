using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Agent : IAgent
    {
        public Agent(int id, int noeudId, string couleur)
        {
            Id = id;
            NoeudId = noeudId;
            Couleur = couleur;
        }

        public int Id { get; }

        public int NoeudId { get; set; }

        public string Couleur { get; set; }
    }
}
