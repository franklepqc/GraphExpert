namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface IPort
    {
        int Id { get; }

        int NoeudId { get; set; }

        int NoeudIdDest { get; set; }
    }
}
