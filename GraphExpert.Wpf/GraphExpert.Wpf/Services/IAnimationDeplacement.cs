using GraphExpert.Wpf.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphExpert.Wpf.Services
{
    public interface IAnimationDeplacement
    {
        Task Executer(AgentVM agent, IEnumerable<StopVM> noeuds, byte noeudId, byte portId);
    }
}