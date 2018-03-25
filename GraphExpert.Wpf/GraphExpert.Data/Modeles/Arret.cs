using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Arret : IArret
    {
        private Arret()
        {

        }

        public Arret(byte id, string etiquette)
        {
            Id = id;
            Etiquette = etiquette;
        }

        public byte Id { get; }
        public string Etiquette { get; }
    }
}
