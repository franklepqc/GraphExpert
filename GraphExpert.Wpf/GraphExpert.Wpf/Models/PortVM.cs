using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Wpf.Interfaces;

namespace GraphExpert.Wpf.Models
{
    public class PortVM : IPositionCanvas
    {
        private IPort _port;

        public PortVM(IPort port, double x, double y)
        {
            _port = port;
            X = x;
            Y = y;
        }

        public byte Id => _port.Id;

        public double X { get; set; }

        public double Y { get; set; }

        public int ZIndex => 3;
    }
}
