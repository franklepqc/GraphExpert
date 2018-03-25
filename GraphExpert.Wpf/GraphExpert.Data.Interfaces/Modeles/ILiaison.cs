namespace GraphExpert.Data.Interfaces.Modeles
{
    public interface ILiaison
    {
        byte ArretIdDepart { get; }
        byte ArretIdArrivee { get; }
        int Poids { get; }
    }
}
