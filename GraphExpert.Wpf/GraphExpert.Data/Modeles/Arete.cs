using GraphExpert.Data.Interfaces.Modeles;
using System.ComponentModel;

namespace GraphExpert.Data.Modeles
{
    public class Arete : IArete, INotifyPropertyChanged
    {
        private byte _noeudIdDepart,
            _portIdDepart,
            _noeudIdArrivee,
            _portIdArrivee;
        private string _couleur;

        public Arete(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee, string couleur)
        {
            _noeudIdDepart = noeudIdDepart;
            _portIdDepart = portIdDepart;
            _noeudIdArrivee = noeudIdArrivee;
            _portIdArrivee = portIdArrivee;
            _couleur = couleur;
        }

        public byte PortIdDepart => _portIdDepart;

        public byte PortIdArrivee => _portIdArrivee;

        public byte NoeudIdDepart => _noeudIdDepart;

        public byte NoeudIdArrivee => _noeudIdArrivee;

        public string Couleur
        {
            get => _couleur;
            set
            {
                _couleur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Couleur"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
