using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoLiaisons
    {
        ILiaison Ajouter(byte idDepart, byte idArrivee);

        void AugmenterPoids(byte idDepart, byte idArrivee, int poids = 1);

        void Supprimer(byte idDepart, byte idArrivee);
    }
}
