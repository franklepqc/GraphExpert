using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Agent : IAgent
    {
        public Agent(byte id, byte noeudId, string etiquette, string couleur)
        {
            Id = id;
            NoeudId = noeudId;
            Etiquette = etiquette;
            Couleur = couleur;
        }

        public byte Id { get; }

        public byte NoeudId { get; set; }

        public string Etiquette { get; set; }

        public string Couleur { get; set; }
    }
}
