namespace gesdette.enums
{
    public enum Etat
    {
        encours, annuler, accepter
    }

    // public static Etat? GetEtat(string value)
    // {
    //     return Enum.TryParse<Etat>(value, true, out var etat) ? etat : (Etat?)null;
    // }

    // public static Etat? GetEtatById(int id)
    // {
    //     if (Enum.IsDefined(typeof(Etat), id))
    //     {
    //         return (Etat)id;
    //     }
    //     return null;
    // }
}
