using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.ObjectModel;

namespace GraphExpert.Data.Modeles
{
    public class Noeud : INoeud
    {
        private Noeud()
        {

        }

        public Noeud(byte id, string etiquette)
        {
            Id = id;
            Etiquette = etiquette;
        }

        public byte Id { get; }
        public string Etiquette { get; }
        public int Degree { get; set; }
        public ObservableCollection<IJeton> Jetons { get; set; } = new ObservableCollection<IJeton>();
    }
}
