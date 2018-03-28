using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Port : IPort
    {
        public Port(int id, int noeudId, int noeudIdDest)
        {
            Id = id;
            NoeudId = noeudId;
            NoeudIdDest = noeudIdDest;
        }

        public int Id { get; }

        public int NoeudId { get; set; }

        public int NoeudIdDest { get; set; }
    }
}
