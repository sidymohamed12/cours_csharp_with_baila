namespace gesdette.core
{
    public interface View<T>
    {
        T saisie();

        void afficher(List<T> listes);

        T getBy();

        bool askToContinue();
    }
}