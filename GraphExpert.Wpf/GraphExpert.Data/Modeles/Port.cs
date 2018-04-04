using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Port : IPort
    {
        public Port(byte id, byte noeudId)
        {
            Id = id;
            NoeudId = noeudId;
        }

        public byte Id { get; }

        public byte NoeudId { get; set; }
    }
}
