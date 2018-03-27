namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface IArete
    {
        byte ArretIdDepart { get; }
        byte ArretIdArrivee { get; }
        int Poids { get; }
    }
}
