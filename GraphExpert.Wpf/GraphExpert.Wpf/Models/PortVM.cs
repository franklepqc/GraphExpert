using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Wpf.Models
{
    public class PortVM
    {
        private IPort _port;

        public PortVM(IPort port, double x, double y)
        {
            _port = port;
            X = x;
            Y = y;
        }

        public byte Id => _port.Id;

        public double X { get; }

        public double Y { get; }

        public int ZIndex => 3;
    }
}
