using GraphExpert.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface IExploration
    {
        List<Deplacement> ListDeplacement { get; set; }
        IGraphe GrapheExplorer { get; set; }
    }
}
