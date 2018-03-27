using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoNoeuds
    {
        INoeud Ajouter(string etiquette);

        void Supprimer(byte id);

        void Vider();

        IEnumerable<INoeud> Obtenir();
    }
}
