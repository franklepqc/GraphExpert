using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Agent : IAgent
    {
        public Agent(byte id, byte noeudId, string couleur)
        {
            Id = id;
            NoeudId = noeudId;
            Couleur = couleur;
        }

        public byte Id { get; }

        public byte NoeudId { get; set; }

        public string Couleur { get; set; }
    }
}
