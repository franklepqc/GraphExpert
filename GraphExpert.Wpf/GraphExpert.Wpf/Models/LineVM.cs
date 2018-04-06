using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Wpf.Models
{
    public class LineVM
    {
        private StopVM _depart, _arrivee;

        public LineVM(StopVM depart, StopVM arrivee)
        {
            _depart = depart;
            _arrivee = arrivee;
        }

        public double X1 => _depart.X + 15;
        public double Y1 => _depart.Y + 15;

        public double X2 => _arrivee.X + 15;
        public double Y2 => _arrivee.Y + 15;

        public int ZIndex => 0;
    }
}
