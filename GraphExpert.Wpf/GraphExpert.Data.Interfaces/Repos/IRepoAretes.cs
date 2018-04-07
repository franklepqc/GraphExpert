using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAretes : IRepoBase<IArete>
    {
        IArete Ajouter(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee);

        void Supprimer(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee);
    }
}
