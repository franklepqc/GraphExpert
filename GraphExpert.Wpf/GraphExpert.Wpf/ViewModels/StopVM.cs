using GraphExpert.Wpf.Models;

namespace GraphExpert.Wpf.ViewModels
{
    public class StopVM : Stop
    {
        public StopVM(double x, double y, byte id, string etiquette)
            : base(id, etiquette)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
