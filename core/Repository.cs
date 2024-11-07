namespace gesdette.core
{
    public interface Repository<T>
    {
        void insert(T value);

        List<T> selectAll();

        int count();

    }
}