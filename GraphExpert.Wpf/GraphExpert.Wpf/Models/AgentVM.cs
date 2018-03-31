using System.Windows.Media;

namespace GraphExpert.Wpf.Models
{
    public class AgentVM
    {
        public AgentVM(double x, double y, Color c)
        {
            X = x;
            Y = y;
            Couleur = new SolidColorBrush(c);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public Brush Couleur { get; set; }

    }
}
