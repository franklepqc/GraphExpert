using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Arete : IArete
    {
        private byte _portIdDepart,
                     _portIdArrivee;

        public Arete(byte portIdDepart, byte portIdArrivee)
        {
            _portIdDepart = portIdDepart;
            _portIdArrivee = portIdArrivee;
        }

        public byte PortIdDepart => _portIdDepart;

        public byte PortIdArrivee => _portIdArrivee;
    }
}
