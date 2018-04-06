using System.ComponentModel;
using System.Windows.Media;

namespace GraphExpert.Wpf.Models
{
    public class AgentVM : INotifyPropertyChanged
    {
        private double _x, _y;

        public AgentVM(double x, double y, byte noeudId, Color c)
        {
            _x = x;
            _y = y;
            NoeudId = noeudId;
            Couleur = new SolidColorBrush(c);
        }

        public int ZIndex => 2;

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanger(@"X");
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanger(@"Y");
            }
        }

        public byte NoeudId { get; set; }

        public Brush Couleur { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanger(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
