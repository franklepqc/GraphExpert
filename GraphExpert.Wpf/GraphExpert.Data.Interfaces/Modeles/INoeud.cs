using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface INoeud
    {
        byte Id { get; }
        string Etiquette { get; }
        int Degree { get; set; }
        ObservableCollection<IJeton> Jetons { get; set; }
    }
}
