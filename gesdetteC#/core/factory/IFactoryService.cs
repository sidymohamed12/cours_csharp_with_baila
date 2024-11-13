
namespace gesdette.core.factory
{
    public interface IFactoryService<T>
    {
        Service<T>? createService();
    }
}