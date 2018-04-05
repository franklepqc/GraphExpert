namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface IAgent
    {
        byte Id { get; }

        byte NoeudId { get; set; }

        string Couleur { get; }
    }
}
