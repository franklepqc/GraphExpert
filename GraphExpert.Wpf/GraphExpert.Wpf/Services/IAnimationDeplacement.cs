using GraphExpert.Wpf.Controles;
using GraphExpert.Wpf.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphExpert.Wpf.Services
{
    public interface IAnimationDeplacement
    {
        void Executer(Agent agent, IEnumerable<StopVM> noeuds, byte noeudId, byte portId);
    }
}