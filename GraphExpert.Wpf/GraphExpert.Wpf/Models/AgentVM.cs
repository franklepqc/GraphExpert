using System.ComponentModel;
using System.Windows.Media;

namespace GraphExpert.Wpf.Models
{
    public class AgentVM : INotifyPropertyChanged
    {
        private double _x, _y;

        public AgentVM(double x, double y, Color c)
        {
            _x = x;
            _y = y;
            Couleur = new SolidColorBrush(c);
        }

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

        public Brush Couleur { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanger(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
