﻿using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoAretes : IRepoBase<IArete>
    {
        IArete Obtenir(byte noeudIdDepart, byte portIdDepart);

        IArete Ajouter(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee);

        void Supprimer(byte noeudIdDepart, byte portIdDepart, byte noeudIdArrivee, byte portIdArrivee);
    }
}
