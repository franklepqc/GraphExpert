using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface INoeud
    {
        byte Id { get; }
        string Etiquette { get; }
        int Degree { get; set; }
        List<IJeton> Jetons { get; set; }
    }
}
