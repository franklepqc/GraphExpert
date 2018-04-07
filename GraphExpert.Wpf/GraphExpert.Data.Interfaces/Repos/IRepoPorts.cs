using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoPorts : IRepoBase<IPort>
    {
        IPort Ajouter(byte noeudId);

        void Supprimer(byte id, byte noeudId);

        IEnumerable<IPort> ObtenirPourNoeud(byte noeudId);
    }
}
