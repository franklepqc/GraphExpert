using GraphExpert.Data.Interfaces;
using GraphExpert.Data.Interfaces.Repos;
using System;
using System.Linq;

namespace GraphExpert.Data
{
    public class FabriqueMatricePoids : IFabriqueMatricePoids
    {
        public int[][] Obtenir(IRepoNoeuds repoArrets, IRepoAretes repoLiaisons)
        {
            var arrets = repoArrets.Obtenir();
            var liaisons = repoLiaisons.Obtenir();
            var nombre = arrets.Count();
            var poids = new int[nombre][];

            foreach (var depart in arrets)
            {
                var idDepartTab = depart.Id - 1;

                // Initialisation du tableau.
                poids[idDepartTab] = new int[nombre];

                foreach (var arrivee in arrets)
                {
                    var idArriveeTab = arrivee.Id - 1;

                    if (depart.Id == arrivee.Id)
                    {
                        poids[idDepartTab][idArriveeTab] = 1;
                    }
                    else
                    {
                        var poidsLiaison = liaisons.SingleOrDefault(k => k.ArretIdDepart == depart.Id && k.ArretIdArrivee == arrivee.Id);

                        if (null != poidsLiaison)
                        {
                            poids[idDepartTab][idArriveeTab] = poidsLiaison.Poids;
                        }
                        else
                        {
                            poids[idDepartTab][idArriveeTab] = 0;
                        }
                    }
                }                
            }

            return poids;
        }
    }
}
