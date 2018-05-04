using GraphExpert.Data.Interfaces.Modeles;
using GraphExpert.Data.Modeles;
using GraphExpert.Wpf.Interfaces;
using System.ComponentModel;

namespace GraphExpert.Wpf.Models
{
    public class LineVM : IPositionCanvasDeuxPoints, INotifyPropertyChanged
    {
        private IArete _arete;

        public event PropertyChangedEventHandler PropertyChanged;

        public LineVM(IPositionCanvas depart, IPositionCanvas arrivee, IArete arete)
        {
            X = depart.X + 15;
            Y = depart.Y + 15;

            X2 = arrivee.X + 15;
            Y2 = arrivee.Y + 15;            

            _arete = arete;

            // Attribuer le changement de valeur.
            var instanceArete = _arete as Arete;
            
            if (null != instanceArete)
                instanceArete.PropertyChanged += LineVM_PropertyChanged;
        }

        ~LineVM()
        {
            var instanceArete = _arete as Arete;

            if (null != instanceArete)
                instanceArete.PropertyChanged -= LineVM_PropertyChanged;
        }

        private void LineVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public string Couleur => _arete.Couleur;

        public int ZIndex => 0;
    }
}
