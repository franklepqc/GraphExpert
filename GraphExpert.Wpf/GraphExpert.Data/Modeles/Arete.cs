using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Modeles
{
    public class Arete : IArete
    {
        private byte _arretIdDepart,
                     _arretIdArrivee;
        private int _poids;

        public Arete(byte arretIdDepart, byte arretIdArrivee, int poids)
        {
            _arretIdDepart = arretIdDepart;
            _arretIdArrivee = arretIdArrivee;
            _poids = poids;
        }

        public byte ArretIdDepart => _arretIdDepart;

        public byte ArretIdArrivee => _arretIdArrivee;

        public int Poids => _poids;
    }
}
