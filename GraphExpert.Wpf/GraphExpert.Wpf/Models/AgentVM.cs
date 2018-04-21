using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Wpf.Interfaces;
using System.ComponentModel;
using System.Windows.Media;

namespace GraphExpert.Wpf.Models
{
    public class AgentVM : INotifyPropertyChanged, IPositionCanvas
    {
        private double _x, _y;
        private IAgent _agent;

        public AgentVM(double x, double y, IAgent agent)
        {
            _x = x;
            _y = y;
            _agent = agent;
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

        public byte NoeudId
        {
            get => _agent.NoeudId;
            set => _agent.NoeudId = value;
        }

        public string Couleur => _agent.Couleur;

        public byte Id => _agent.Id;

        public string Etiquette
        {
            get => _agent.Etiquette;
            set => _agent.Etiquette = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanger(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
