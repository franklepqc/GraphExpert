﻿using GraphExpert.Data.Interfaces.Modeles;

namespace GraphExpert.Data.Interfaces.Repos
{
    public interface IRepoArrets
    {
        IArret Ajouter(string etiquette);

        void Supprimer(byte id);
    }
}
