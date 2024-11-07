namespace gesdette.core
{
    public interface Service<T>
    {
        void create(T value);

        List<T> findAll();

        T getBy(string name);

        int count();
    }
}