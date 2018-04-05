using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Arete : IArete
    {
        private byte _noeudIdDepart,
            _portIdDepart,
            _noeudIdArrivee,
            _portIdArrivee;

        public Arete(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee)
        {
            _noeudIdDepart = noeudIdDepart;
            _portIdDepart = portIdDepart;
            _noeudIdArrivee = noeudIdArrivee;
            _portIdArrivee = portIdArrivee;
        }

        public byte PortIdDepart => _portIdDepart;

        public byte PortIdArrivee => _portIdArrivee;

        public byte NoeudIdDepart => _noeudIdDepart;

        public byte NoeudIdArrivee => _noeudIdArrivee;
    }
}
