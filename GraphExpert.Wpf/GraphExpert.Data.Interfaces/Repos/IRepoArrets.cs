using GraphExpert.Data.Interfaces.Modeles;
using System.Collections.Generic;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoArrets
    {
        IArret Ajouter(string etiquette);

        void Supprimer(byte id);

        void Vider();

        IEnumerable<IArret> Obtenir();
    }
}
