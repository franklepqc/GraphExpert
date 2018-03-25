namespace GraphExpert.Wpf.Models
{
    public class StopVM
    {
        public StopVM(double x, double y, byte id)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public byte Id { get; set; }
    }
}
