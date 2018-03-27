using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Noeud : INoeud
    {
        private Noeud()
        {

        }

        public Noeud(byte id, string etiquette)
        {
            Id = id;
            Etiquette = etiquette;
        }

        public byte Id { get; }
        public string Etiquette { get; }
    }
}
