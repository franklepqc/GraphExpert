﻿using GraphExpert.Wpf.Interfaces;

namespace GraphExpert.Wpf.Models
{
    public class LineVM : IPositionCanvasDeuxPoints
    {
        public LineVM(IPositionCanvas depart, IPositionCanvas arrivee)
        {
            X = depart.X + 15;
            Y = depart.Y + 15;

            X2 = arrivee.X + 15;
            Y2 = arrivee.Y + 15;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }

        public int ZIndex => 0;
    }
}
