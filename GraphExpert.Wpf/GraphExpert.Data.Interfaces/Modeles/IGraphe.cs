using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface IGraphe
    {
        List<INoeud> noeuds { get; set; }
        List<IArete> aretes { get; set; }
    }
}
