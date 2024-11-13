using gesdette.core;

namespace gesdette.core.database
{
    public interface IRepositoryBD<T> : Repository<T>
    {

        T selectById(int id);

        T selectBy(string name);

        void update(T value);
    }
}