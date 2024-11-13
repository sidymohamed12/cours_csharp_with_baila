
namespace gesdette.core.factory
{
    public interface IFactoryRepo<T>
    {
        Repository<T>? createRepository();
    }
}